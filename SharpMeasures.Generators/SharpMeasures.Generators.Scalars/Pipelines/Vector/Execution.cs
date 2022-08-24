namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

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
        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            if (Data.Vector is not null)
            {
                foreach (var dimension in Data.Dimensions)
                {
                    AppendMultiplyVectorMethod(indentation, Data.Vector.Value, dimension);
                    AppendMultiplyVectorOperators(indentation, Data.Vector.Value, dimension);
                }
            }
        }

        private void AppendMultiplyVectorMethod(Indentation indentation, NamedType vector, int dimension)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.MultiplyVectorMethod(dimension));
            Builder.AppendLine($"{indentation}public {vector.Name} Multiply(Vector{dimension} factor) => new(Magnitude.Value * factor);");
        }

        private void AppendMultiplyVectorOperators(Indentation indentation, NamedType vector, int dimension)
        {
            SeparationHandler.AddIfNecessary();

            NamedType vectorType = new($"Vector{dimension}", "SharpMeasures", true);

            var methodNameAndModifiers = $"public static {vector.Name} operator *";
            var expression = "new(x.Magnitude.Value * y)";

            var lhsParameters = new[] { (Data.Scalar.AsNamedType(), "x"), (vectorType, "y") };
            var rhsParameters = new[] { (vectorType, "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.Documentation.MultiplyVectorOperatorLHS(dimension));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, lhsParameters);

            AppendDocumentation(indentation, Data.Documentation.MultiplyVectorOperatorRHS(dimension));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, rhsParameters);
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
