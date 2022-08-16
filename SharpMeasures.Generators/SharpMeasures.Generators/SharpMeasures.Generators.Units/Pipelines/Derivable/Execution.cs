namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;
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

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);
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
            var methodNameAndModifiers = $"public static {Data.Unit.FullyQualifiedName} From";
            var expression = $"new({definition.Expression})";
            var parameters = GetSignatureComponents(definition);

            AppendDocumentation(indentation, Data.Documentation.Derivation(definition.Signature));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation,methodNameAndModifiers, expression, parameters);
        }

        private static IEnumerable<(NamedType Type, string Name)> GetSignatureComponents(DerivableUnitDefinition definition)
        {
            IEnumerator<string> parameterEnumerator = definition.ParameterNames.GetEnumerator();
            IEnumerator<IRawUnitType> signatureUnitTypeEnumerator = definition.Signature.GetEnumerator();

            while (parameterEnumerator.MoveNext() && signatureUnitTypeEnumerator.MoveNext())
            {
                yield return (signatureUnitTypeEnumerator.Current.Type.AsNamedType(), parameterEnumerator.Current);
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
