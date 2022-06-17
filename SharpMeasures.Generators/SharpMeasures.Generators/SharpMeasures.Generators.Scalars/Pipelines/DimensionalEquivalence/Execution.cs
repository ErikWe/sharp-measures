namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;
using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Text;
using System.Linq;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        if (data.DimensionalEquivalences.Any() is false)
        {
            return;
        }

        string source = Composer.Compose(data);

        context.AddSource($"{data.Scalar.Name}_DimensionalEquivalence.g.cs", SourceText.From(source, Encoding.UTF8));
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
        private UsingsCollector UsingsCollector { get; }
        private InterfaceCollector InterfaceCollector { get; }

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, data.Scalar.Namespace);
            InterfaceCollector = InterfaceCollector.Delayed(Builder);

            UsingsCollector.AddUsing("SharpMeasures.ScalarAbstractions");

            if (Data.Scalar.IsReferenceType)
            {
                UsingsCollector.AddUsing("System");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            UsingsCollector.MarkInsertionPoint();

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (RefinedDimensionalEquivalenceDefinition definition in Data.DimensionalEquivalences)
            {
                foreach (ScalarInterface scalar in definition.Quantities)
                {
                    ComposeInstanceConversion(indentation, scalar);
                    Builder.AppendLine();
                }
            }

            foreach (RefinedDimensionalEquivalenceDefinition definition in Data.DimensionalEquivalences)
            {
                if (definition.CastOperatorBehaviour is ConversionOperationBehaviour.None)
                {
                    continue;
                }

                Action<Indentation, ScalarInterface> composer = definition.CastOperatorBehaviour switch
                {
                    ConversionOperationBehaviour.Explicit => ComposeExplicitOperatorConversion,
                    ConversionOperationBehaviour.Implicit => ComposeImplicitOperatorConversion,
                    _ => throw new NotSupportedException("Invalid cast operation")
                };

                foreach (ScalarInterface scalar in definition.Quantities)
                {
                    composer(indentation, scalar);
                    Builder.AppendLine();
                }
            }

            SourceBuildingUtility.RemoveOneNewLine(Builder);
        }

        private void ComposeInstanceConversion(Indentation indentation, ScalarInterface scalar)
        {
            AppendDocumentation(indentation, Data.Documentation.AsDimensionallyEquivalent(scalar));
            Builder.AppendLine($"{indentation}public {scalar.ScalarType.Name} As{scalar.ScalarType.Name} => new(Magnitude);");
        }

        private void ComposeExplicitOperatorConversion(Indentation indentation, ScalarInterface scalar)
            => ComposeOperatorConversion(indentation, scalar, "explicit");

        private void ComposeImplicitOperatorConversion(Indentation indentation, ScalarInterface scalar)
            => ComposeOperatorConversion(indentation, scalar, "implicit");

        private void ComposeOperatorConversion(Indentation indentation, ScalarInterface scalar, string behaviour)
        {
            AppendDocumentation(indentation, Data.Documentation.CastToDimensionallyEquivalent(scalar));

            if (Data.Scalar.IsReferenceType)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="ArgumentNullException"/>""");
            }

            Builder.Append($"{indentation}public static {behaviour} operator {scalar.ScalarType.Name}({Data.Scalar.Name} x)");

            if (Data.Scalar.IsReferenceType)
            {
                Builder.AppendLine();

                Builder.AppendLine($$"""
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(x);

                    {{indentation.Increased}}return new(x.Magnitude);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine(" => new(x.Magnitude);");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
