namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

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

        context.AddSource($"{data.Value.Scalar.QualifiedName}.Maths.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private InterfaceCollector InterfaceCollector { get; }

        private Composer(DataModel data)
        {
            SeparationHandler = new(Builder);

            Data = data;

            InterfaceCollector = InterfaceCollector.Delayed(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            Builder.Append(Data.Scalar.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
        }

        private string Retrieve() => Builder.ToString();

        private void ComposeTypeBlock(Indentation indentation)
        {
            SeparationHandler.MarkUnncecessary();

            AppendCommonProperties(indentation);

            if (Data.ImplementSum)
            {
                AppendSumMethod(indentation);
            }

            if (Data.ImplementDifference && Data.Difference is not null)
            {
                AppendDifferenceMethods(indentation);
            }

            AppendUnaryMethods(indentation);
            AppendMultiplyAndDivideScalarMethods(indentation);

            AppendDivideSameTypeMethod(indentation);

            AppendUnaryOperators(indentation);

            if (Data.ImplementSum)
            {
                AppendSumOperator(indentation);
            }

            if (Data.ImplementDifference && Data.Difference is not null)
            {
                AppendDifferenceOperator(indentation);
            }

            AppendDivideSameTypeOperator(indentation);
            AppendMultiplyAndDivideScalarOperators(indentation);
        }

        private void AppendCommonProperties(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.IsNaN());
            Builder.AppendLine($"{indentation}public bool IsNaN => double.IsNaN(Magnitude.Value);");
            AppendDocumentation(indentation, Data.Documentation.IsZero());
            Builder.AppendLine($"{indentation}public bool IsZero => Magnitude.Value is 0;");
            AppendDocumentation(indentation, Data.Documentation.IsPositive());
            Builder.AppendLine($"{indentation}public bool IsPositive => Magnitude.Value > 0;");
            AppendDocumentation(indentation, Data.Documentation.IsNegative());
            Builder.AppendLine($"{indentation}public bool IsNegative => Magnitude.Value < 0;");
            AppendDocumentation(indentation, Data.Documentation.IsFinite());
            Builder.AppendLine($"{indentation}public bool IsFinite => double.IsFinite(Magnitude.Value);");
            AppendDocumentation(indentation, Data.Documentation.IsInfinite());
            Builder.AppendLine($"{indentation}public bool IsInfinite => double.IsInfinity(Magnitude.Value);");
            AppendDocumentation(indentation, Data.Documentation.IsPositiveInfinity());
            Builder.AppendLine($"{indentation}public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude.Value);");
            AppendDocumentation(indentation, Data.Documentation.IsNegativeInfinity());
            Builder.AppendLine($"{indentation}public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude.Value);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.Absolute());
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Absolute() => new(global::System.Math.Abs(Magnitude.Value));");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.Sign());
            Builder.AppendLine($"{indentation}public int Sign() => global::System.Math.Sign(Magnitude.Value);");
        }

        private void AppendSumMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Scalar.FullyQualifiedName} Add";
            var expression = "new(Magnitude.Value + addend.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "addend") };

            AppendDocumentation(indentation, Data.Documentation.AddSameTypeMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDifferenceMethods(Indentation indentation)
        {
            AppendSubtractSameTypeMetod(indentation);
            
            if (Data.Difference != Data.Scalar.AsNamedType())
            {
                AppendAddDifferenceMethod(indentation);
                AppendSubtractDifferenceMethod(indentation);
            }
        }

        private void AppendSubtractSameTypeMetod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifier = $"public {Data.Difference!.Value.FullyQualifiedName} Subtract";
            var expression = "new(Magnitude.Value - subtrahend.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "subtrahend") };

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifier, expression, parameters);
        }

        private void AppendAddDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Scalar.FullyQualifiedName} Add";
            var expression = "new(Magnitude.Value + addend.Magnitude.Value";
            var parameters = new[] { (Data.Difference!.Value, "addend") };

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSubtractDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Scalar.FullyQualifiedName} Subtract";
            var expression = "new(Magnitude.Value - subtrahend.Magnitude.Value";
            var parameters = new[] { (Data.Difference!.Value, "subtrahend") };

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendUnaryMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Plus() => this;");
            AppendDocumentation(indentation, Data.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Negate() => new(-Magnitude.Value);");
        }

        private void AppendMultiplyAndDivideScalarMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Multiply(global::SharpMeasures.Scalar factor) => new(Magnitude.Value * factor.Value);");
            AppendDocumentation(indentation, Data.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Divide(global::SharpMeasures.Scalar divisor) => new(Magnitude.Value / divisor.Value);");
        }

        private void AppendDivideSameTypeMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = "public global::SharpMeasures.Scalar Divide";
            var expression = "new(Magnitude.Value / divisor.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "divisor") };

            AppendDocumentation(indentation, Data.Documentation.DivideSameTypeMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendUnaryOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator +({Data.Scalar.FullyQualifiedName} x) => x;");

            if (Data.Scalar.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator -";
            var expression = "new(-x.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x") };

            AppendDocumentation(indentation, Data.Documentation.NegateOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSumOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator +";
            var expression = "new(x.Magnitude.Value + y.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.Documentation.AddSameTypeOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDifferenceOperator(Indentation indentation)
        {
            AppendSubtractSameTypeOperator(indentation);

            if (Data.Difference != Data.Scalar.AsNamedType())
            {
                AppendAddDifferenceOperators(indentation);
                AppendSubtractDifferenceOperator(indentation);
            }
        }

        private void AppendSubtractSameTypeOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Difference!.Value.FullyQualifiedName} operator -";
            var expression = "new(x.Magnitude.Value - y.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendAddDifferenceOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator +";
            var expression = "new(x.Magnitude.Value + y.Magnitude.Value)";

            var lhsParameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Difference!.Value, "y") };
            var rhsParameters = new[] { (Data.Difference.Value, "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, lhsParameters);

            if (Data.Scalar.IsReferenceType || Data.Difference.Value.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, rhsParameters);
        }

        private void AppendSubtractDifferenceOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator -";
            var expression = "new(x.Magnitude.Value - y.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Difference!.Value, "y") };

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDivideSameTypeOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = "public static global::SharpMeasures.Scalar operator /";
            var expression = "new(x.Magnitude.Value / y.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.Documentation.DivideSameTypeOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMultiplyAndDivideScalarOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var multiplyMethodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator *";
            var divideMethodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator /";

            var multiplyLHSExpression = "new(x.Magnitude.Value * y.Value)";
            var multiplyRHSExpression = "new(x.Value * y.Magnitude.Value)";
            var divideExpression = "new(x.Magnitude.Value / y.Value)";

            var LHSParameters = new[] { (Data.Scalar.AsNamedType(), "x"), (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "y") };
            var RHSParameters = new[] { (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, multiplyMethodNameAndModifiers, multiplyLHSExpression, LHSParameters);

            if (Data.Scalar.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, multiplyMethodNameAndModifiers, multiplyRHSExpression, RHSParameters);
            
            if (Data.Scalar.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.Documentation.DivideScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, divideMethodNameAndModifiers, divideExpression, LHSParameters);
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
