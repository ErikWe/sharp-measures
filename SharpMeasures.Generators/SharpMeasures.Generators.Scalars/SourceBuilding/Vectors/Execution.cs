namespace SharpMeasures.Generators.Scalars.SourceBuilding.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, Optional<DataModel> data)
    {
        if (context.CancellationToken.IsCancellationRequested || data.HasValue is false)
        {
            return;
        }

        var source = Composer.Compose(data.Value);

        context.AddSource($"{data.Value.Scalar.QualifiedName}.Vectors.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            foreach (var vectorAndDimension in Data.VectorByDimension)
            {
                AppendMultiplyVectorMethod(indentation, vectorAndDimension.Value, vectorAndDimension.Key);
                AppendMultiplyVectorOperators(indentation, vectorAndDimension.Value, vectorAndDimension.Key);
            }
        }

        private void AppendMultiplyVectorMethod(Indentation indentation, NamedType vector, int dimension)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyVectorMethod(dimension));
            Builder.AppendLine($"{indentation}public {vector.FullyQualifiedName} Multiply(global::SharpMeasures.Vector{dimension} factor) => new(Magnitude.Value * factor);");
        }

        private void AppendMultiplyVectorOperators(Indentation indentation, NamedType vector, int dimension)
        {
            SeparationHandler.AddIfNecessary();

            NamedType vectorType = new($"Vector{dimension}", "SharpMeasures", "SharpMeasures.Base", true);

            var methodNameAndModifiers = $"public static {vector.FullyQualifiedName} operator *";

            var lhsExpression = "new(a.Magnitude.Value * b)";
            var rhsExpression = "new(a * b.Magnitude.Value)";

            var lhsParameters = new[] { (Data.Scalar.AsNamedType(), "a"), (vectorType, "b") };
            var rhsParameters = new[] { (vectorType, "a"), (Data.Scalar.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyVectorOperatorLHS(dimension));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, lhsExpression, lhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyVectorOperatorRHS(dimension));
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, rhsExpression, rhsParameters);
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
