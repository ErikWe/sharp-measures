namespace SharpMeasures.Generators.Scalars.SourceBuilding.Maths;

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
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContentLevel);

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

            AppendGenericMethods(indentation);

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

            AppendGenericOperators(indentation);
            AppendUnhandledOperators(indentation);
        }

        private void AppendCommonProperties(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsNaN());
            Builder.AppendLine($"{indentation}public bool IsNaN => double.IsNaN(Magnitude.Value);");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsZero());
            Builder.AppendLine($"{indentation}public bool IsZero => Magnitude.Value is 0;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsPositive());
            Builder.AppendLine($"{indentation}public bool IsPositive => Magnitude.Value > 0;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsNegative());
            Builder.AppendLine($"{indentation}public bool IsNegative => Magnitude.Value < 0;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsFinite());
            Builder.AppendLine($"{indentation}public bool IsFinite => double.IsFinite(Magnitude.Value);");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsInfinite());
            Builder.AppendLine($"{indentation}public bool IsInfinite => double.IsInfinity(Magnitude.Value);");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsPositiveInfinity());
            Builder.AppendLine($"{indentation}public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude.Value);");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.IsNegativeInfinity());
            Builder.AppendLine($"{indentation}public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude.Value);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Absolute());
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Absolute() => new(global::System.Math.Abs(Magnitude.Value));");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Sign());
            Builder.AppendLine($"{indentation}public int Sign() => global::System.Math.Sign(Magnitude.Value);");
        }

        private void AppendSumMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Scalar.FullyQualifiedName} Add";
            var expression = "new(Magnitude.Value + addend.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "addend") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddSameTypeMethod());
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

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifier, expression, parameters);
        }

        private void AppendAddDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Scalar.FullyQualifiedName} Add";
            var expression = "new(Magnitude.Value + addend.Magnitude.Value)";
            var parameters = new[] { (Data.Difference!.Value, "addend") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSubtractDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Scalar.FullyQualifiedName} Subtract";
            var expression = "new(Magnitude.Value - subtrahend.Magnitude.Value)";
            var parameters = new[] { (Data.Difference!.Value, "subtrahend") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendUnaryMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Plus() => this;");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Negate() => new(-Magnitude.Value);");
        }

        private void AppendMultiplyAndDivideScalarMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Multiply(global::SharpMeasures.Scalar factor) => new(Magnitude.Value * factor.Value);");
            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Divide(global::SharpMeasures.Scalar divisor) => new(Magnitude.Value / divisor.Value);");
        }

        private void AppendDivideSameTypeMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = "public global::SharpMeasures.Scalar Divide";
            var expression = "new(Magnitude.Value / divisor.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "divisor") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideSameTypeMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendGenericMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled Add<TScalar>(TScalar addend) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(addend);

                {{indentation.Increased}}return new(Magnitude + addend.Magnitude);
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled Subtract<TScalar>(TScalar subtrahend) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                {{indentation.Increased}}return new(Magnitude - subtrahend.Magnitude);
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractFromTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled SubtractFrom<TScalar>(TScalar minuend) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(minuend);

                {{indentation.Increased}}return new(minuend.Magnitude - Magnitude);
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled Multiply<TScalar>(TScalar factor) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(factor);

                {{indentation.Increased}}return new(Magnitude * factor.Magnitude);
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled Divide<TScalar>(TScalar divisor) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(divisor);

                {{indentation.Increased}}return new(Magnitude / divisor.Magnitude);
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideIntoTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled DivideInto<TScalar>(TScalar dividend) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(dividend);

                {{indentation.Increased}}return new(dividend.Magnitude / Magnitude);
                {{indentation}}}
                """);
        }

        private void AppendUnaryOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator +({Data.Scalar.FullyQualifiedName} x) => x;");

            if (Data.Scalar.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator -";
            var expression = "new(-x.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.NegateOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSumOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator +";
            var expression = "new(x.Magnitude.Value + y.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddSameTypeOperator());
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

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractSameTypeOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendAddDifferenceOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator +";
            var expression = "new(x.Magnitude.Value + y.Magnitude.Value)";

            var lhsParameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Difference!.Value, "y") };
            var rhsParameters = new[] { (Data.Difference.Value, "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, lhsParameters);

            if (Data.Scalar.IsReferenceType || Data.Difference.Value.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddDifferenceOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, rhsParameters);
        }

        private void AppendSubtractDifferenceOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Scalar.FullyQualifiedName} operator -";
            var expression = "new(x.Magnitude.Value - y.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Difference!.Value, "y") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDivideSameTypeOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = "public static global::SharpMeasures.Scalar operator /";
            var expression = "new(x.Magnitude.Value / y.Magnitude.Value)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideSameTypeOperator());
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

            var lhsParameters = new[] { (Data.Scalar.AsNamedType(), "x"), (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "y") };
            var rhsParameters = new[] { (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "x"), (Data.Scalar.AsNamedType(), "y") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, multiplyMethodNameAndModifiers, multiplyLHSExpression, lhsParameters);

            if (Data.Scalar.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyScalarOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, multiplyMethodNameAndModifiers, multiplyRHSExpression, rhsParameters);
            
            if (Data.Scalar.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, divideMethodNameAndModifiers, divideExpression, lhsParameters);
        }

        private void AppendGenericOperators(Indentation indentation)
        {
            NamedType iScalarQuantity = new("IScalarQuantity", "SharpMeasures", "SharpMeasures.Base", false);

            var methodNameAndModifiers = (string symbol) => $"public static global::SharpMeasures.Unhandled operator {symbol}";
            var expression = (string symbol) => $"new(x.Magnitude {symbol} y.Magnitude)";
            var parameters = new[] { (Data.Scalar.AsNamedType(), "x"), (iScalarQuantity, "y") };

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddIScalarOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("+"), expression("+"), parameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractIScalarOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("-"), expression("-"), parameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyIScalarOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("*"), expression("*"), parameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideIScalarOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("/"), expression("/"), parameters);
        }

        private void AppendUnhandledOperators(Indentation indentation)
        {
            NamedType unhandled = new("Unhandled", "SharpMeasures", "SharpMeasures.Base", true);

            var methodNameAndModifiers = (string symbol) => $"public static {unhandled.FullyQualifiedName} operator {symbol}";
            var expression = (string symbol) => $"new(x.Magnitude {symbol} y.Magnitude)";

            var lhsParameters = new[] { (Data.Scalar.AsNamedType(), "x"), (unhandled, "y") };
            var rhsParameters = new[] { (unhandled, "x"), (Data.Scalar.AsNamedType(), "y") };

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("+"), expression("+"), lhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddUnhandledOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("+"), expression("+"), rhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("-"), expression("-"), lhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractUnhandledOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("-"), expression("-"), rhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("*"), expression("*"), lhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyUnhandledOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("*"), expression("*"), rhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("/"), expression("/"), lhsParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideUnhandledOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("/"), expression("/"), rhsParameters);
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
