namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Maths;

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

        private bool HasComponent => Data.Scalar is not null;
        private bool DifferenceHasComponent => Data.DifferenceScalar is not null;

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

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

            AppendMultiplyScalarLHS(indentation);
            AppendMultiplyScalarRHS(indentation);
            AppendDivideScalarLHS(indentation);

            AppendGenericOperators(indentation);
            AppendUnhandledOperators(indentation);
        }

        private void AppendUnaryMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Plus() => this;");

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Negate() => new({ConstantVectorTexts.Upper.Negate(Data.Dimension)});");
        }

        private void AppendSumMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Vector.FullyQualifiedName} Add";
            var expression = $"new({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension, HasComponent, HasComponent)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "addend") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddSameTypeMethod());
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
            var expression = $"new({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension, HasComponent, HasComponent)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "subtrahend") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractSameTypeMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendAddDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Vector.FullyQualifiedName} Add";
            var expression = $"new({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension, HasComponent, DifferenceHasComponent)})";
            var parameters = new[] { (Data.Difference!.Value, "addend") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSubtractDifferenceMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public {Data.Vector.FullyQualifiedName} Subtract";
            var expression = $"new({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension, HasComponent, DifferenceHasComponent)})";
            var parameters = new[] { (Data.Difference!.Value, "subtrahend") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractDifferenceMethod());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMultiplyAndDivideScalarMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Multiply(global::SharpMeasures.Scalar factor) => new({ConstantVectorTexts.Upper.MultiplyFactorScalar(Data.Dimension, HasComponent, false)});");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Divide(global::SharpMeasures.Scalar divisor) => new({ConstantVectorTexts.Upper.DivideDivisorScalar(Data.Dimension, HasComponent, false)});");
        }

        private void AppendGenericMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddTVectorMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled{{Data.Dimension}} Add<TVector>(TVector addend) where TVector : global::SharpMeasures.IVector{{Data.Dimension}}Quantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(addend);

                {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension, HasComponent, false)}});
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractTVectorMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled{{Data.Dimension}} Subtract<TVector>(TVector subtrahend) where TVector : global::SharpMeasures.IVector{{Data.Dimension}}Quantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension, HasComponent, false)}});
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractFromTVectorMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled{{Data.Dimension}} SubtractFrom<TVector>(TVector minuend) where TVector : global::SharpMeasures.IVector{{Data.Dimension}}Quantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(minuend);

                {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.SubtractFromMinuendVector(Data.Dimension, false, HasComponent)}});
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled{{Data.Dimension}} Multiply<TScalar>(TScalar factor) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(factor);

                {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.MultiplyFactorScalar(Data.Dimension, HasComponent, true)}});
                {{indentation}}}
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideTScalarMethod());
            DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            Builder.AppendLine($$"""
                {{indentation}}public global::SharpMeasures.Unhandled{{Data.Dimension}} Divide<TScalar>(TScalar divisor) where TScalar : global::SharpMeasures.IScalarQuantity
                {{indentation}}{
                {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(divisor);
                
                {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.DivideDivisorScalar(Data.Dimension, HasComponent, true)}});
                {{indentation}}}
                """);
        }

        private void AppendUnaryOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator +({Data.Vector.FullyQualifiedName} a) => a;");

            if (Data.Vector.IsReferenceType)
            {
                SeparationHandler.Add();
            }

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator -";
            var expression = $"new({ConstantVectorTexts.Upper.NegateA(Data.Dimension)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.NegateOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendSumOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator +";
            var expression = $"new({ConstantVectorTexts.Upper.AddBVector(Data.Dimension, HasComponent, HasComponent)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddSameTypeOperator());
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
            var expression = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension, HasComponent, HasComponent)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractSameTypeOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendAddDifferenceOperators(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator +";
            var lhsExpression = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension, HasComponent, DifferenceHasComponent)})";
            var rhsExpression = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension, DifferenceHasComponent, HasComponent)})";

            var lhsParameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Difference!.Value, "b") };
            var rhsParameters = new[] { (Data.Difference!.Value, "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, lhsExpression, lhsParameters);
            
            if (Data.Vector.IsReferenceType || (Data.Difference!.Value.IsReferenceType))
            {
                SeparationHandler.Add();
            }

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddDifferenceOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, rhsExpression, rhsParameters);
        }

        private void AppendSubtractDifferenceOperator(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator -";
            var expression = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension, HasComponent, DifferenceHasComponent)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (Data.Difference!.Value, "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractDifferenceOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMultiplyScalarLHS(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator *";
            var expression = $"new({ConstantVectorTexts.Upper.MultiplyAByScalarB(Data.Dimension, HasComponent, false)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMultiplyScalarRHS(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator *";
            var expression = $"new({ConstantVectorTexts.Upper.MultiplyScalarAByB(Data.Dimension, HasComponent, false)})";
            var parameters = new[] { (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "a"), (Data.Vector.AsNamedType(), "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyScalarOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDivideScalarLHS(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public static {Data.Vector.FullyQualifiedName} operator /";
            var expression = $"new({ConstantVectorTexts.Upper.DivideAByScalarB(Data.Dimension, HasComponent, false)})";
            var parameters = new[] { (Data.Vector.AsNamedType(), "a"), (new NamedType("Scalar", "SharpMeasures", "SharpMeasures.Base", true), "b") };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyScalarOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendGenericOperators(Indentation indentation)
        {
            NamedType iVectorQuantity = new($"IVector{Data.Dimension}Quantity", "SharpMeasures", "SharpMeasures.Base", false);
            NamedType iScalarQuantity = new("IScalarQuantity", "SharpMeasures", "SharpMeasures.Base", false);

            var methodNameAndModifiers = (string symbol) => $"public static global::SharpMeasures.Unhandled{Data.Dimension} operator {symbol}";

            var vectorExpression = (string symbol) => $"new({ConstantVectorTexts.Upper.AddBVector(Data.Dimension, HasComponent, false)})";
            var vectorParameters = new[] { (Data.Vector.AsNamedType(), "a"), (iVectorQuantity, "b") };

            var scalarExpressionLHS = (string symbol) => $"new({ConstantVectorTexts.Upper.MultiplyAByScalarB(Data.Dimension, HasComponent, true)})";
            var scalarExpressionRHS = (string symbol) => $"new({ConstantVectorTexts.Upper.MultiplyScalarAByB(Data.Dimension, HasComponent, true)})";
            var scalarParametersLHS = new[] { (Data.Vector.AsNamedType(), "a"), (iScalarQuantity, "b") };
            var scalarParametersRHS = new[] { (iScalarQuantity, "a"), (Data.Vector.AsNamedType(), "b") };

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddIVectorOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("+"), vectorExpression("+"), vectorParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractIVectorOperator());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("-"), vectorExpression("-"), vectorParameters);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyIScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("*"), scalarExpressionLHS("*"), scalarParametersLHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyIScalarOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("*"), scalarExpressionRHS("*"), scalarParametersRHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideIScalarOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("/"), scalarExpressionLHS("/"), scalarParametersLHS);
        }

        private void AppendUnhandledOperators(Indentation indentation)
        {
            NamedType unhandledVector = new($"Unhandled{Data.Dimension}", "SharpMeasures", "SharpMeasures.Base", true);
            NamedType unhandledScalar = new("Unhandled", "SharpMeasures", "SharpMeasures.Base", true);

            var methodNameAndModifiers = (string symbol) => $"public static {unhandledVector.FullyQualifiedName} operator {symbol}";

            var vectorAdditionExpressionLHS = $"new({ConstantVectorTexts.Upper.AddBVector(Data.Dimension, HasComponent, false)})";
            var vectorAdditionExpressionRHS = $"new({ConstantVectorTexts.Upper.AddBVector(Data.Dimension, false, HasComponent)})";
            var vectorSubtractionExpressionLHS = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension, HasComponent, false)})";
            var vectorSubtractionExpressionRHS = $"new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension, false, HasComponent)})";

            var vectorParametersLHS = new[] { (Data.Vector.AsNamedType(), "a"), (unhandledVector, "b") };
            var vectorParametersRHS = new[] { (unhandledVector, "a"), (Data.Vector.AsNamedType(), "b") };

            var scalarMultiplicationExpressionLHS = $"new({ConstantVectorTexts.Upper.MultiplyAByScalarB(Data.Dimension, HasComponent, true)})";
            var scalarMultiplicationExpressionRHS = $"new({ConstantVectorTexts.Upper.MultiplyScalarAByB(Data.Dimension, HasComponent, true)})";
            var scalarDivisionExpressionLHS = $"new({ConstantVectorTexts.Upper.DivideAByScalarB(Data.Dimension, HasComponent, true)})";

            var scalarParametersLHS = new[] { (Data.Vector.AsNamedType(), "a"), (unhandledScalar, "b") };
            var scalarParametersRHS = new[] { (unhandledScalar, "a"), (Data.Vector.AsNamedType(), "b") };

            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("+"), vectorAdditionExpressionLHS, vectorParametersLHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.AddUnhandledOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("+"), vectorAdditionExpressionRHS, vectorParametersRHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("-"), vectorSubtractionExpressionLHS, vectorParametersLHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.SubtractUnhandledOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("-"), vectorSubtractionExpressionRHS, vectorParametersRHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("*"), scalarMultiplicationExpressionLHS, scalarParametersLHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.MultiplyUnhandledOperatorRHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("*"), scalarMultiplicationExpressionRHS, scalarParametersRHS);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.DivideUnhandledOperatorLHS());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers("/"), scalarDivisionExpressionLHS, scalarParametersLHS);
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }
}
