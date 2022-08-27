namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;
using System.Globalization;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.ComposeAndReportDiagnostics(data);

        context.AddSource($"{data.Unit.Name}_Derivable.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;
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
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (DerivableUnitDefinition definition in Data.Derivations)
            {
                AppendDefinition(definition, indentation);
            }
        }

        private void AppendDefinition(DerivableUnitDefinition definition, Indentation indentation)
        {
            var parameterNames = GetSignatureParameterNames(definition.Signature);

            var methodNameAndModifiers = $"public static {Data.Unit.FullyQualifiedName} From";
            var expression = $"new(new {Data.Quantity.FullyQualifiedName}({ProcessExpression(definition, parameterNames, Data.UnitPopulation)}))";
            var parameters = GetSignatureComponents(definition, parameterNames);

            AppendDocumentation(indentation, Data.Documentation.Derivation(definition.Signature));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private static IEnumerable<(NamedType Type, string Name)> GetSignatureComponents(DerivableUnitDefinition definition, IReadOnlyList<string> parameterNames)
        {
            IEnumerator<string> parameterEnumerator = parameterNames.GetEnumerator();
            IEnumerator<NamedType> signatureUnitTypeEnumerator = definition.Signature.GetEnumerator();

            while (parameterEnumerator.MoveNext() && signatureUnitTypeEnumerator.MoveNext())
            {
                yield return (signatureUnitTypeEnumerator.Current, parameterEnumerator.Current);
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }

        private static string ProcessExpression(DerivableUnitDefinition definition, IEnumerable<string> parameterNames, IUnitPopulation unitPopulation)
        {
            string[] parameterNameAndQuantity = new string[definition.Signature.Count];

            IEnumerator<string> parameterNameEnumerator = parameterNames.GetEnumerator();
            IEnumerator<NamedType> quantityEnumerator = GetQuantitiesOfSignatureUnits(definition.Signature, unitPopulation).GetEnumerator();

            int index = 0;
            while (parameterNameEnumerator.MoveNext() && quantityEnumerator.MoveNext())
            {
                parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.{quantityEnumerator.Current.Name}.Magnitude";

                index += 1;
            }

            return string.Format(CultureInfo.InvariantCulture, definition.Expression, parameterNameAndQuantity);
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
                string name = SourceBuildingUtility.ToParameterName(signatureComponent.Name);
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
                int count = counts[signatureComponent.Name];

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

        private static IEnumerable<NamedType> GetQuantitiesOfSignatureUnits(IReadOnlyList<NamedType> signature, IUnitPopulation unitPopulation)
        {
            foreach (var signatureElement in signature)
            {
                yield return unitPopulation.Units[signatureElement].Definition.Quantity;
            }
        }
    }
}
