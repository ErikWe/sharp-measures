namespace SharpMeasures.Generators.Vectors.Pipelines.DimensionalEquivalence;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Vector.Name}_{data.Dimension}_DimensionalEquivalence.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private DataModel Data { get; }
        private UsingsCollector UsingsCollector { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, Data.Vector.Namespace);
            UsingsCollector.AddUsing("SharpMeasures");

            if (Data.Vector.IsReferenceType)
            {
                UsingsCollector.AddUsing("System");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            UsingsCollector.MarkInsertionPoint();

            Builder.AppendLine(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (var dimensionallyEquivalentVectorGroup in Data.DimensionalEquivalences)
            {
                if (dimensionallyEquivalentVectorGroup.Key.VectorsByDimension.TryGetValue(Data.Dimension, out var dimensionallyEquivalentVector))
                {
                    ComposeInstanceConversion(dimensionallyEquivalentVector, indentation);
                }
            }

            foreach (var dimensionallyEquivalentVectorGroup in Data.DimensionalEquivalences)
            {
                if (dimensionallyEquivalentVectorGroup.Value is ConversionOperationBehaviour.None)
                {
                    continue;
                }

                Action<ResizedVector, Indentation> composer = dimensionallyEquivalentVectorGroup.Value switch
                {
                    ConversionOperationBehaviour.Explicit => ComposeExplicitOperatorConversion,
                    ConversionOperationBehaviour.Implicit => ComposeImplicitOperatorConversion,
                    _ => throw new NotSupportedException("Invalid cast operation")
                };

                if (dimensionallyEquivalentVectorGroup.Key.VectorsByDimension.TryGetValue(Data.Dimension, out var dimensionallyEquivalentVector))
                {
                    composer(dimensionallyEquivalentVector, indentation);
                }
            }
        }

        private void ComposeInstanceConversion(ResizedVector vector, Indentation indentation)
        {
            UsingsCollector.AddUsing(vector.VectorType.Namespace);

            AppendDocumentation(indentation, Data.Documentation.AsDimensionallyEquivalent(vector));
            Builder.AppendLine($"{indentation}public {vector.VectorType.Name} As{vector.VectorType.Name} => new(Components);");
        }

        private void ComposeExplicitOperatorConversion(ResizedVector vector, Indentation indentation)
            => ComposeOperatorConversion(vector, indentation, "explicit");

        private void ComposeImplicitOperatorConversion(ResizedVector vector, Indentation indentation)
            => ComposeOperatorConversion(vector, indentation, "implicit");

        private void ComposeOperatorConversion(ResizedVector vector, Indentation indentation, string behaviour)
        {
            AppendDocumentation(indentation, Data.Documentation.CastToDimensionallyEquivalent(vector));
            
            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public static {{behaviour}} operator {{vector.VectorType.Name}}({{Data.Vector.Name}} x)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(x);

                    {{indentation.Increased}}return new(x.Components);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {behaviour} operator {vector.VectorType.Name}({Data.Vector.Name} x) => new(x.Components);");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
