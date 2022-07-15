namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Scalar.Name}_Vectors.g.cs", SourceText.From(source, Encoding.UTF8));
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

            UsingsCollector = UsingsCollector.Delayed(Builder, data.Scalar.Namespace);
            UsingsCollector.AddUsing("SharpMeasures");
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            UsingsCollector.MarkInsertionPoint();

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (var vector in Data.Vectors)
            {
                UsingsCollector.AddUsing(vector.Type.Namespace);

                ComposeForVector(indentation, vector);
                Builder.AppendLine();
            }
        }

        private void ComposeForVector(Indentation indentation, IUnresolvedVectorType vector)
        {
            AppendDocumentation(indentation, Data.Documentation.MultiplyVectorMethod(vector.Definition.Dimension));
            Builder.AppendLine($"{indentation}public {vector.Type.Name} Multiply(Vector{vector.Definition.Dimension} factor) => new(Magnitude.Value * factor);");

            Builder.AppendLine();

            if (Data.Scalar.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.MultiplyVectorOperatorLHS(vector.Definition.Dimension));
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(x);

                    {{indentation.Increased}}return new(x.Magnitude.Value * y);
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.MultiplyVectorOperatorRHS(vector.Definition.Dimension));
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(y);

                    {{indentation.Increased}}return new(x * y.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.MultiplyVectorOperatorLHS(vector.Definition.Dimension));
                Builder.AppendLine($"{indentation}public static {vector.Type.Name} operator *({Data.Scalar.Name} x, Vector{vector.Definition.Dimension} y) => new(x.Magnitude.Value * y);");

                AppendDocumentation(indentation, Data.Documentation.MultiplyVectorOperatorRHS(vector.Definition.Dimension));
                Builder.AppendLine($"{indentation}public static {vector.Type.Name} operator *(Vector{vector.Definition.Dimension} x, {Data.Scalar.Name} y) => new(x * y.Magnitude.Value);");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
