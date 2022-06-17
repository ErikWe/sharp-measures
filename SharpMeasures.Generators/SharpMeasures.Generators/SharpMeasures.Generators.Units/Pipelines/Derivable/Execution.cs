namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Refinement.DerivableUnit;

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
            foreach (RefinedDerivableUnitDefinition definition in Data.Derivations)
            {
                ComposeDefinition(definition, indentation);
            }
        }

        private void ComposeDefinition(RefinedDerivableUnitDefinition definition, Indentation indentation)
        {
            IEnumerable<string> signatureComponents = GetSignatureComponents(definition);
            bool anyNullableTypes = AnyNullableTypesInSignature(definition);

            AppendDocumentation(indentation, Data.Documentation.Derivation(definition.Signature));

            if (anyNullableTypes)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="ArgumentNullException"/>""");
            }

            Builder.Append($"{indentation}public static {Data.Unit.Name} From(");
            IterativeBuilding.AppendEnumerable(Builder, signatureComponents, ", ", ")");

            if (anyNullableTypes is false)
            {
                Builder.AppendLine($" => new({definition.Expression});");
                return;
            }

            UsingsCollector.AddUsings("System");

            IEnumerator<string> parameterEnumerator = definition.ParameterNames.GetEnumerator();
            IEnumerator<NamedType> namedTypeEnumerator = definition.Signature.GetEnumerator();

            Builder.AppendLine($$"""{{indentation}}{""");

            while (parameterEnumerator.MoveNext() && namedTypeEnumerator.MoveNext())
            {
                if (namedTypeEnumerator.Current.IsReferenceType)
                {
                    Builder.AppendLine($"{indentation.Increased}ArgumentNullException.ThrowIfNull({parameterEnumerator.Current});");
                }
            }

            Builder.AppendLine();
            Builder.AppendLine($"{indentation.Increased}return new({definition.Expression});");
            Builder.AppendLine($$"""{{indentation}}}""");
        }

        private IEnumerable<string> GetSignatureComponents(RefinedDerivableUnitDefinition definition)
        {
            IEnumerator<NamedType> signatureIterator = definition.Signature.GetEnumerator();
            IEnumerator<string> parameterIterator = definition.ParameterNames.GetEnumerator();

            while (parameterIterator.MoveNext() && signatureIterator.MoveNext())
            {
                UsingsCollector.AddUsing(signatureIterator.Current.Namespace);
                yield return $"{signatureIterator.Current.Name} {parameterIterator.Current}";
            }
        }

        private static bool AnyNullableTypesInSignature(RefinedDerivableUnitDefinition definition)
        {
            foreach (var namedType in definition.Signature)
            {
                if (namedType.IsReferenceType)
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
