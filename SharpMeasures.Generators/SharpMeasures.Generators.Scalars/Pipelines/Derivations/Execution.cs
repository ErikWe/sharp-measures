namespace SharpMeasures.Generators.Scalars.Pipelines.Derivations;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        if (context.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        string source = Composer.Compose(data);

        context.AddSource($"{data.Scalar.Name}_Derivations.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();
        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private InterfaceCollector InterfaceCollector { get; }

        private HashSet<DerivedQuantitySignature> ImplementedSignatures { get; } = new();

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);

            InterfaceCollector = InterfaceCollector.Delayed(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var derivation in Data.Derivations)
            {
                AppendMethodDerivation(indentation, derivation);
            }

            foreach (var operatorDerivation in Data.OperatorDerivations)
            {
                if (operatorDerivation.LeftHandSide == Data.Scalar.AsNamedType())
                {
                    AppendOperatorDerivation(indentation, operatorDerivation);
                }
            }
        }

        private void AppendMethodDerivation(Indentation indentation, IDerivedQuantity derivation)
        {
            var parameterNames = GetSignatureParameterNames(derivation.Signature);

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} From";
            var expression = $"new({ProcessExpression(derivation, parameterNames)})";

            foreach ((var permutedSignature, var permutedParameterNames) in GetPermutedSignatures(derivation, parameterNames))
            {
                AppendMethodDefinition(indentation, methodNameAndModifiers, expression, permutedSignature, permutedParameterNames);

                AppendMethodDefinitionWithoutScalars(indentation, methodNameAndModifiers, expression, permutedSignature, permutedParameterNames);
            }
        }

        private void AppendOperatorDerivation(Indentation indentation, IOperatorDerivation operatorDerivation)
        {
            SeparationHandler.AddIfNecessary();

            var operatorSymbol = GetOperatorSymbol(operatorDerivation.OperatorType);

            var methodNameAndModifiers = $"public static {operatorDerivation.Result.FullyQualifiedName} operator {operatorSymbol}";
            (NamedType Type, string Name)[] parameters = new[] { (operatorDerivation.LeftHandSide, "a"), (operatorDerivation.RightHandSide, "b") };

            AppendDocumentation(indentation, Data.Documentation.OperatorDerivationLHS(operatorDerivation));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, getExpression(), parameters);

            string getExpression()
            {
                if (operatorDerivation.RightHandSide.FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    return $"new({parameters[0].Name}.Magnitude {operatorSymbol} {parameters[1].Name})";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(operatorDerivation.RightHandSide))
                {
                    return $"new({parameters[0].Name}.Magnitude {operatorSymbol} {parameters[1].Name}.Magnitude)";
                }

                return $"new({parameters[0].Name}.Magnitude {operatorSymbol} {parameters[1].Name}.Components)";
            }
        }

        private static string GetOperatorSymbol(OperatorType operatorType)
        {
            return operatorType switch
            {
                OperatorType.Addition => "+",
                OperatorType.Subtraction => "-",
                OperatorType.Multiplication => "*",
                OperatorType.Division => "/",
                _ => throw new NotSupportedException($"Invalid {typeof(OperatorType).Name}: {operatorType}")
            };
        }

        private void AppendMethodDefinition(Indentation indentation, string methodNameAndModifiers, string expression, DerivedQuantitySignature signature, IReadOnlyList<string> parameterNames)
        {
            if (ImplementedSignatures.Add(signature) is false)
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            var parameters = GetSignatureComponents(signature, parameterNames);

            AppendDocumentation(indentation, Data.Documentation.Derivation(signature));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMethodDefinitionWithoutScalars(Indentation indentation, string methodNameAndModifiers, string expression, DerivedQuantitySignature signature, IReadOnlyList<string> parameterNames)
        {
            for (int i = 0; i < signature.Count; i++)
            {
                if (signature[i].FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    expression = expression.Replace(parameterNames[i], "1");

                    signature = new DerivedQuantitySignature(signature.Take(i).Concat(signature.Skip(1)).ToList());
                    parameterNames = new List<string>(parameterNames.Take(i).Concat(parameterNames.Skip(1)).ToList());

                    i -= 1;
                }
            }

            AppendMethodDefinition(indentation, methodNameAndModifiers, expression, signature, parameterNames);
        }

        private static IEnumerable<(DerivedQuantitySignature Signature, IReadOnlyList<string> ParameterNames)> GetPermutedSignatures(IDerivedQuantity derivation, IReadOnlyList<string> parameterNames)
        {
            if (derivation.Permutations is false)
            {
                return new[] { (new DerivedQuantitySignature(derivation.Signature), parameterNames) };
            }

            return recurse(derivation.Signature.ToArray(), parameterNames.ToArray(), 0);

            static IEnumerable<(DerivedQuantitySignature Signature, IReadOnlyList<string> ParameterNames)> recurse(NamedType[] signatureElements, string[] parameterNames, int fromIndex)
            {
                if (fromIndex == signatureElements.Length - 1)
                {
                    yield return (new DerivedQuantitySignature(new List<NamedType>(signatureElements)), new List<string>(parameterNames));

                    yield break;
                }

                for (int i = fromIndex; i < signatureElements.Length; i++)
                {
                    (signatureElements[fromIndex], signatureElements[i]) = (signatureElements[i], signatureElements[fromIndex]);
                    (parameterNames[fromIndex], parameterNames[i]) = (parameterNames[i], parameterNames[fromIndex]);

                    foreach (var signatureAndParameterNames in recurse(signatureElements, parameterNames, fromIndex + 1))
                    {
                        yield return signatureAndParameterNames;
                    }

                    (signatureElements[fromIndex], signatureElements[i]) = (signatureElements[i], signatureElements[fromIndex]);
                    (parameterNames[fromIndex], parameterNames[i]) = (parameterNames[i], parameterNames[fromIndex]);
                }
            }
        }

        private static IEnumerable<(NamedType Type, string Name)> GetSignatureComponents(IReadOnlyList<NamedType> signature, IReadOnlyList<string> parameterNames)
        {
            var parameterEnumerator = parameterNames.GetEnumerator();
            var signatureUnitTypeEnumerator = signature.GetEnumerator();

            while (parameterEnumerator.MoveNext() && signatureUnitTypeEnumerator.MoveNext())
            {
                yield return (signatureUnitTypeEnumerator.Current, parameterEnumerator.Current);
            }
        }

        private string ProcessExpression(IDerivedQuantity derivation, IEnumerable<string> parameterNames)
        {
            var parameterNameAndQuantity = new string[derivation.Signature.Count];

            var parameterNameEnumerator = parameterNames.GetEnumerator();

            int index = 0;
            while (parameterNameEnumerator.MoveNext())
            {
                if (derivation.Signature[index].FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}";
                }
                else if (Data.ScalarPopulation.Scalars.ContainsKey(derivation.Signature[index]))
                {
                    parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.Magnitude";
                }
                else
                {
                    parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.Components";
                }

                index += 1;
            }

            return string.Format(CultureInfo.InvariantCulture, derivation.Expression, parameterNameAndQuantity);
        }

        private static IReadOnlyList<string> GetSignatureParameterNames(IReadOnlyList<NamedType> signature)
        {
            Dictionary<string, int> counts = new();

            foreach (var signatureComponent in signature)
            {
                countParameter(signatureComponent);
            }

            var parameterNames = new string[signature.Count];

            int index = 0;
            foreach (var signatureComponent in signature)
            {
                var name = SourceBuildingUtility.ToParameterName(signatureComponent.Name);
                name = appendParameterNumber(name, signatureComponent);

                parameterNames[index] = name;
                index += 1;
            }

            return parameterNames;

            void countParameter(NamedType signatureComponent)
            {
                if (counts.TryGetValue(signatureComponent.Name, out int count))
                {
                    counts[signatureComponent.Name] = count - 1;
                }
                else
                {
                    counts[signatureComponent.Name] = -1;
                }
            }

            string appendParameterNumber(string text, NamedType signatureComponent)
            {
                var count = counts[signatureComponent.Name];

                if (count == -1)
                {
                    return text;
                }
                else if (count < 0)
                {
                    counts[signatureComponent.Name] = 1;
                    return $"{text}1";
                }
                else
                {
                    counts[signatureComponent.Name] += 1;
                    return $"{text}{counts[signatureComponent.Name]}";
                }
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
