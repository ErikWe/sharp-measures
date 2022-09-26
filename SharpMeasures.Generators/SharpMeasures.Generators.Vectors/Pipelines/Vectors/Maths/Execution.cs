namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Maths;

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

        string source = Composer.Compose(data.Value);

        context.AddSource($"{data.Value.Vector.QualifiedName}.Maths.g.cs", SourceText.From(source, Encoding.UTF8));
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
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.AppendLine(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendUnaryMethods(indentation);
            
            if (Data.ImplementSum)
            {
                AppendSumMethod(indentation);
            }

            if (Data.ImplementDifference && Data.Difference is not null)
            {
                AppendDifferenceMethod(indentation);
            }

            AppendMultiplyAndDivideScalarMethods(indentation);

            AppendUnaryOperators(indentation);

            if (Data.ImplementSum)
            {
                AppendSumOperator(indentation);
            }

            if (Data.ImplementDifference && Data.Difference is not null)
            {
                AppendDifferenceOperator(indentation);
            }

            AppendMultiplyScalarLHS(indentation);
            AppendMultiplyScalarRHS(indentation);
            AppendDivideScalarLHS(indentation);

            AppendMathUtility(indentation);
        }

        private void AppendUnaryMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Plus() => this;");

            AppendDocumentation(indentation, Data.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Negate() => new({ConstantVectorTexts.Upper.Negate(Data.Dimension)});");
        }

        private void AppendSumMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Vector.FullyQualifiedName} Add";
            var expression = $"new({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "addend") };

            AppendDocumentation(indentation, Data.Documentation.AddSameTypeMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDifferenceMethod(Indentation indentation)
        {
            AppendSubtractSameTypeMethod(indentation);
            
            if (Data.Difference != Data.Vector.AsNamedType())
            {
                AppendAddDifferenceMethod(indentation);
                AppendSubtractDifferenceMethod(indentation);
            }
        }

        private void AppendSubtractSameTypeMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Difference!.Value.FullyQualifiedName} Subtract";
            var expression = $"new({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "subtrahend") };

            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendAddDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Vector.FullyQualifiedName} Add";
            var expression = $"new({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)})";
            var parameters = new[] { (Data.Difference!.Value, "addend") };

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSubtractDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Vector.FullyQualifiedName} Subtract";
            var expression = $"new({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)})";
            var parameters = new[] { (Data.Difference!.Value, "subtrahend") };

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMultiplyAndDivideScalarMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Multiply(global::SharpMeasures.Scalar factor) => new({ConstantVectorTexts.Upper.MultiplyFactorScalar(Data.Dimension)});");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Divide(global::SharpMeasures.Scalar divisor) => new({ConstantVectorTexts.Upper.DivideDivisorScalar(Data.Dimension)});");
        }

        private void AppendUnaryOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator +({Data.Vector.FullyQualifiedName} a) => a;");

            if (Data.Vector.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator -";
            var expression = $"new({ConstantVectorTexts.Upper.NegateA(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a") };

            AppendDocumentation(indentation, Data.Documentation.NegateOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSumOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator +";
            var expression = $"new({ConstantVectorTexts.Upper.AddBVector(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.Documentation.AddSameTypeOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDifferenceOperator(Indentation indentation)
        {
            AppendSubtractSameTypeOperator(indentation);
            
            if (Data.Difference != Data.Vector.AsNamedType())
            {
                AppendAddDifferenceOperators(indentation);
                AppendSubtractDifferenceOperator(indentation);
            }
        }

        private void AppendSubtractSameTypeOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Difference!.Value.FullyQualifiedName} operator -";
            var expression = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendAddDifferenceOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator +";
            var expression = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)})";

            var lhsParameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Difference!.Value, "b") };
            var rhsParameters = new[] { (Data.Difference!.Value, "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, lhsParameters);
            
            if (Data.Vector.IsReferenceType || (Data.Difference!.Value.IsReferenceType))
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, rhsParameters);
        }

        private void AppendSubtractDifferenceOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator -";
            var expression = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Difference!.Value, "b") };

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMultiplyScalarLHS(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator *";
            var expression = $"new({ConstantVectorTexts.Upper.MultiplyAScalar(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "b") };

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMultiplyScalarRHS(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator *";
            var expression = $"new({ConstantVectorTexts.Upper.MultiplyBScalar(Data.Dimension)})";
            var parameters = new[] { (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDivideScalarLHS(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator /";
            var expression = $"new({ConstantVectorTexts.Upper.DivideAScalar(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "b") };

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMathUtility(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            if (Data.Scalar is null)
            {
                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"global::SharpMeasures.Scalar\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> ScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult();");
            }
            
            if (Data.Scalar is not null)
            {
                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"global::SharpMeasures.Scalar\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> PureScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult();");

                SeparationHandler.Add();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Scalar.Value.FullyQualifiedName}\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<{Data.Scalar.Value.FullyQualifiedName}> ScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult<{Data.Scalar.Value.FullyQualifiedName}();");
            }

            SeparationHandler.Add();

            Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Vector.FullyQualifiedName}\"/>.</summary>");
            Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IVector{Data.Dimension}ResultingMaths<{Data.Vector.FullyQualifiedName}> VectorMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.Vector{Data.Dimension}Result<{Data.Vector.FullyQualifiedName}>();");
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
