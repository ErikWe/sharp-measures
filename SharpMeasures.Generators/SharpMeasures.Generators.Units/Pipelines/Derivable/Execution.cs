namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

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

        string source = Composer.ComposeAndReportDiagnostics(data.Value);

        if (source.Length is 0)
        {
            return;
        }

        context.AddSource($"{data.Value.Unit.QualifiedName}.Derivable.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string ComposeAndReportDiagnostics(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();

        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private HashSet<DerivableUnitSignature> ImplementedSignatures { get; } = new();

        private bool AnyImplementations { get; set; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit);

            Builder.Append(Data.Unit.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
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

            foreach (DerivableUnitDefinition definition in Data.Derivations)
            {
                AppendDefinition(definition, indentation);
            }
        }

        private void AppendDefinition(DerivableUnitDefinition definition, Indentation indentation)
        {
            var parameterNames = GetSignatureParameterNames(definition.Signature);

            var processedExpression = ProcessExpression(definition, parameterNames, Data.UnitPopulation);

            if (processedExpression is null)
            {
                return;
            }

            var methodNameAndModifiers = $"public static {Data.Unit.FullyQualifiedName} From";
            var expression = $"new(new {Data.Quantity.FullyQualifiedName}({processedExpression}))";

            foreach ((var permutedSignature, var permutedParameterNames) in GetPermutedSignatures(definition, parameterNames))
            {
                AppendPermutedDefinition(indentation, methodNameAndModifiers, expression, permutedSignature, permutedParameterNames);
            }
        }

        private void AppendPermutedDefinition(Indentation indentation, string methodNameAndModifiers, string expression, DerivableUnitSignature signature, IReadOnlyList<string> parameterNames)
        {
            if (ImplementedSignatures.Add(signature) is false)
            {
                return;
            }

            AnyImplementations = true;

            SeparationHandler.AddIfNecessary();

            var parameters = GetSignatureComponents(signature, parameterNames);

            AppendDocumentation(indentation, Data.Documentation.Derivation(signature));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private static IEnumerable<(DerivableUnitSignature Signature, IReadOnlyList<string> ParameterNames)> GetPermutedSignatures(DerivableUnitDefinition definition, IReadOnlyList<string> parameterNames)
        {
            if (definition.Permutations is false)
            {
                return new[] { (new DerivableUnitSignature(definition.Signature), parameterNames) };
            }

            return recurse(definition.Signature.ToArray(), parameterNames.ToArray(), 0);

            static IEnumerable<(DerivableUnitSignature Signature, IReadOnlyList<string> ParameterNames)> recurse(NamedType[] signatureElements, string[] parameterNames, int fromIndex)
            {
                if (fromIndex == signatureElements.Length - 1)
                {
                    yield return (new DerivableUnitSignature(new List<NamedType>(signatureElements)), new List<string>(parameterNames));

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

        private static string? ProcessExpression(DerivableUnitDefinition definition, IEnumerable<string> parameterNames, IUnitPopulation unitPopulation)
        {
            var parameterNameAndQuantity = new string[definition.Signature.Count];

            var parameterNameEnumerator = parameterNames.GetEnumerator();
            var quantityEnumerator = GetQuantitiesOfSignatureUnits(definition.Signature, unitPopulation).GetEnumerator();

            int index = 0;
            while (parameterNameEnumerator.MoveNext() && quantityEnumerator.MoveNext())
            {
                if (quantityEnumerator.Current is null)
                {
                    return null;
                }

                parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.{quantityEnumerator.Current.Value.Name}.Magnitude";

                index += 1;
            }

            try
            {
                return string.Format(CultureInfo.InvariantCulture, definition.Expression, parameterNameAndQuantity);
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

        private static IEnumerable<NamedType?> GetQuantitiesOfSignatureUnits(IReadOnlyList<NamedType> signature, IUnitPopulation unitPopulation)
        {
            foreach (var signatureElement in signature)
            {
                if (unitPopulation.Units.TryGetValue(signatureElement, out var unit) is false)
                {
                    yield return null;

                    continue;
                }

                yield return unit.Definition.Quantity;
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
