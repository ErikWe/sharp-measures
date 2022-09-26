namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Derivations;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        string source = Composer.Compose(data.Value);

        if (source.Length is 0)
        {
            return;
        }

        context.AddSource($"{data.Value.Vector.QualifiedName}.Derivations.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private sealed class Composer
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
        private HashSet<OperatorDerivation> ImplementedOperators { get; } = new();

        private bool AnyImplementations { get; set; }

        private bool AppendPureVector3Maths { get; set; }
        private bool AppendPureScalarMaths { get; set; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);

            InterfaceCollector = InterfaceCollector.Delayed(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.Append(Data.Vector.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
        }

        private string Retrieve()
        {
            if (AnyImplementations is false)
            {
                return string.Empty;
            }

            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var derivation in Data.Derivations)
            {
                if (derivation.ExpandedVectorResults.TryGetValue(Data.Dimension, out var expandedVectorResults) is false)
                {
                    continue;
                }

                foreach (var expandedDerivation in expandedVectorResults)
                {
                    AppendMethodDerivation(indentation, expandedDerivation.Expression, expandedDerivation.Signature, derivation.Permutations);
                }
            }

            foreach (var derivation in Data.Derivations)
            {
                if (derivation.ExpandedVectorResults.TryGetValue(Data.Dimension, out var expandedVectorResults) is false)
                {
                    continue;
                }

                foreach (var expandedDerivation in expandedVectorResults)
                {
                    var operatorDerivations = VectorOperatorDerivationSearcher.GetDerivations(Data.Vector.AsNamedType(), expandedDerivation.Expression, expandedDerivation.Signature, derivation.OperatorImplementation, Data.ScalarPopulation);

                    foreach (var operatorDerivation in operatorDerivations)
                    {
                        AppendOperatorDerivation(indentation, operatorDerivation);
                    }
                }
            }

            AppendMathUtility(indentation);
        }

        private void AppendMethodDerivation(Indentation indentation, string expression, IReadOnlyList<NamedType> signature, bool permutations)
        {
            var parameterNames = GetSignatureParameterNames(signature);

            var processedExpression = ProcessExpression(expression, signature, parameterNames);

            if (processedExpression is null)
            {
                return;
            }

            if (expression.Contains("PureScalarMaths."))
            {
                AppendPureScalarMaths = true;
            }

            if (expression.Contains("PureVector3Maths."))
            {
                AppendPureVector3Maths = true;
            }

            AnyImplementations = true;

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} From";
            expression = $"new({processedExpression})";

            foreach ((var permutedSignature, var permutedParameterNames) in GetPermutedSignatures(signature, permutations, parameterNames))
            {
                AppendMethodDefinition(indentation, methodNameAndModifiers, expression, permutedSignature, permutedParameterNames);

                AppendMethodDefinitionWithoutScalars(indentation, methodNameAndModifiers, expression, permutedSignature, permutedParameterNames);
            }
        }

        private void AppendOperatorDerivation(Indentation indentation, OperatorDerivation operatorDerivation)
        {
            if (ImplementedOperators.Add(operatorDerivation) is false)
            {
                return;
            }

            AnyImplementations = true;

            SeparationHandler.AddIfNecessary();

            var operatorSymbol = GetOperatorSymbol(operatorDerivation.OperatorType);

            var methodNameAndModifiers = $"public static {operatorDerivation.Result.FullyQualifiedName} operator {operatorSymbol}";
            (NamedType Type, string Name)[] parameters = new[] { (operatorDerivation.LeftHandSide, "a"), (operatorDerivation.RightHandSide, "b") };

            AppendDocumentation(indentation, Data.Documentation.OperatorDerivation(operatorDerivation));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, getExpression(), parameters);

            string getExpression()
            {
                if (operatorDerivation.LeftHandSide.FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    return $"new({parameters[0].Name} {operatorSymbol} {parameters[1].Name}.Components)";
                }

                if (operatorDerivation.RightHandSide.FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    return $"new({parameters[0].Name}.Components {operatorSymbol} {parameters[1].Name})";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(operatorDerivation.RightHandSide))
                {
                    return $"new({parameters[0].Name}.Components {operatorSymbol} {parameters[1].Name}.Magnitude)";
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(operatorDerivation.LeftHandSide))
                {
                    return $"new({parameters[0].Name}.Magnitude {operatorSymbol} {parameters[1].Name}.Components)";
                }

                return $"new({parameters[0].Name}.Components {operatorSymbol} {parameters[1].Name}.Components)";
            }
        }

        private static string GetOperatorSymbol(OperatorType operatorType) => operatorType switch
        {
            OperatorType.Addition => "+",
            OperatorType.Subtraction => "-",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            _ => throw new NotSupportedException($"Invalid {typeof(OperatorType).Name}: {operatorType}")
        };

        private void AppendMethodDefinition(Indentation indentation, string methodNameAndModifiers, string expression, DerivedQuantitySignature signature, IReadOnlyList<string> parameterNames)
        {
            if (ImplementedSignatures.Add(signature) is false)
            {
                return;
            }

            SeparationHandler.AddIfNecessary();

            var parameters = GetSignatureComponents(signature, parameterNames);

            AppendDocumentation(indentation, Data.Documentation.Derivation(signature, parameterNames));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMethodDefinitionWithoutScalars(Indentation indentation, string methodNameAndModifiers, string expression, DerivedQuantitySignature signature, IReadOnlyList<string> parameterNames)
        {
            for (int i = 0; i < signature.Count; i++)
            {
                if (signature[i].FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    expression = expression.Replace(parameterNames[i], "1");

                    signature = new DerivedQuantitySignature(signature.Take(i).Concat(signature.Skip(i + 1)).ToList());
                    parameterNames = new List<string>(parameterNames.Take(i).Concat(parameterNames.Skip(1)).ToList());

                    i -= 1;
                }
            }

            AppendMethodDefinition(indentation, methodNameAndModifiers, expression, signature, parameterNames);
        }

        private void AppendMathUtility(Indentation indentation)
        {
            if (Data.HasDefinedScalar is false && AppendPureScalarMaths)
            {
                SeparationHandler.AddIfNecessary();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"global::SharpMeasures.Scalar\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> PureScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult();");
            }

            if (AppendPureVector3Maths)
            {
                SeparationHandler.AddIfNecessary();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"global::SharpMeasures.Vector3\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IVector3ResultingMaths<global::SharpMeasures.Vector3> PureVector3Maths {{ get; }} = global::SharpMeasures.Maths.MathFactory.Vector3Result();");
            }
        }

        private static IEnumerable<(DerivedQuantitySignature Signature, IReadOnlyList<string> ParameterNames)> GetPermutedSignatures(IReadOnlyList<NamedType> signature, bool permutations, IReadOnlyList<string> parameterNames)
        {
            if (permutations is false)
            {
                return new[] { (new DerivedQuantitySignature(signature), parameterNames) };
            }

            return recurse(signature.ToArray(), parameterNames.ToArray(), 0);

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

        private string? ProcessExpression(string expression, IReadOnlyList<NamedType> signature, IEnumerable<string> parameterNames)
        {
            var parameterNameAndQuantity = new string[signature.Count];

            var parameterNameEnumerator = parameterNames.GetEnumerator();

            int index = -1;
            while (parameterNameEnumerator.MoveNext())
            {
                index += 1;

                if (signature[index].FullyQualifiedName is "global::SharpMeasures.Scalar")
                {
                    parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}";

                    continue;
                }

                if (Data.ScalarPopulation.Scalars.ContainsKey(signature[index]))
                {
                    parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.Magnitude";

                    continue;
                }

                parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.Components";
            }

            try
            {
                return string.Format(CultureInfo.InvariantCulture, expression, parameterNameAndQuantity);
            }
            catch (FormatException)
            {
                return null;
            }
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

                    return;
                }
                
                counts[signatureComponent.Name] = -1;
            }

            string appendParameterNumber(string text, NamedType signatureComponent)
            {
                var count = counts[signatureComponent.Name];

                if (count == -1)
                {
                    return text;
                }
                
                if (count < 0)
                {
                    counts[signatureComponent.Name] = 1;
                    return $"{text}1";
                }

                counts[signatureComponent.Name] += 1;
                return $"{text}{counts[signatureComponent.Name]}";
            }
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
