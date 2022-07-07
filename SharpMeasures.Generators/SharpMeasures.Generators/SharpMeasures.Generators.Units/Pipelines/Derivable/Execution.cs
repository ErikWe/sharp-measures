namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        if (data.Derivations.Any() is false)
        {
            return;
        }

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
        private UsingsCollector UsingsCollector { get; }

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, data.Unit.Namespace);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            UsingsCollector.MarkInsertionPoint();

            NamespaceBuilding.AppendNamespace(Builder, Data.Unit);

            Builder.Append(Data.Unit.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (DerivableUnitDefinition definition in Data.Derivations)
            {
                ComposeDefinition(definition, indentation);
            }
        }

        private void ComposeDefinition(DerivableUnitDefinition definition, Indentation indentation)
        {
            IEnumerable<string> signatureComponents = GetSignatureComponents(definition);
            bool anyNullableTypes = AnyReferenceTypesInSignature(definition);

            AppendDocumentation(indentation, Data.Documentation.Derivation(definition.Signature));

            if (anyNullableTypes)
            {
                UsingsCollector.AddUsings("System");
                Builder.AppendLine($"""{indentation}/// <exception cref="ArgumentNullException"/>""");
            }

            Builder.Append($"{indentation}public static {Data.Unit.Name} From(");
            IterativeBuilding.AppendEnumerable(Builder, signatureComponents, ", ", ")");

            if (anyNullableTypes)
            {
                ComposeDefinitionWithReferenceTypeArgument(definition, indentation);
                return;
            }
            
            Builder.AppendLine($" => new({definition.Expression});");
        }

        private void ComposeDefinitionWithReferenceTypeArgument(DerivableUnitDefinition definition, Indentation indentation)
        {
            IEnumerator<string> parameterEnumerator = definition.ParameterNames.GetEnumerator();
            IEnumerator<IUnresolvedUnitType> signatureUnitTypeEnumerator = definition.Signature.GetEnumerator();

            Builder.AppendLine($$"""{{indentation}}{""");

            while (parameterEnumerator.MoveNext() && signatureUnitTypeEnumerator.MoveNext())
            {
                if (signatureUnitTypeEnumerator.Current.Type.IsReferenceType)
                {
                    Builder.AppendLine($"{indentation.Increased}ArgumentNullException.ThrowIfNull({parameterEnumerator.Current});");
                }
            }

            Builder.AppendLine();
            Builder.AppendLine($"{indentation.Increased}return new({definition.Expression});");
            Builder.AppendLine($$"""{{indentation}}}""");
        }

        private IEnumerable<string> GetSignatureComponents(DerivableUnitDefinition definition)
        {
            IEnumerator<string> parameterEnumerator = definition.ParameterNames.GetEnumerator();
            IEnumerator<IUnresolvedUnitType> signatureUnitTypeEnumerator = definition.Signature.GetEnumerator();

            while (parameterEnumerator.MoveNext() && signatureUnitTypeEnumerator.MoveNext())
            {
                UsingsCollector.AddUsing(signatureUnitTypeEnumerator.Current.Type.Namespace);
                yield return $"{signatureUnitTypeEnumerator.Current.Type.Name} {parameterEnumerator.Current}";
            }
        }

        private static bool AnyReferenceTypesInSignature(DerivableUnitDefinition definition)
        {
            foreach (var unitType in definition.Signature)
            {
                if (unitType.Type.IsReferenceType)
                {
                    return true;
                }
            }

            return false;
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
