namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Maths;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Vector.Name}_Maths.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private Composer(DataModel data)
        {
            Data = data;
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.AppendLine(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            if (Data.ImplementSum)
            {
                ComposeSumMethod(indentation);

                Builder.AppendLine();
            }

            if (Data.ImplementDifference)
            {
                ComposeDifferenceMethod(indentation);

                Builder.AppendLine();
            }

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Plus() => this;");
            AppendDocumentation(indentation, Data.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Negate() => new({ConstantVectorTexts.Upper.Negate(Data.Dimension)});");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Multiply(global::SharpMeasures.Scalar factor) => new({ConstantVectorTexts.Upper.MultiplyFactorScalar(Data.Dimension)});");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Divide(global::SharpMeasures.Scalar divisor) => new({ConstantVectorTexts.Upper.DivideDivisorScalar(Data.Dimension)});");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator +({Data.Vector.FullyQualifiedName} a) => a;");

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine();
            }

            ComposeNegate(indentation);

            if (Data.ImplementSum || Data.ImplementDifference)
            {
                Builder.AppendLine();
            }

            if (Data.ImplementSum)
            {
                ComposeSumOperator(indentation);
            }

            if (Data.ImplementDifference)
            {
                ComposeDifferenceOperator(indentation);
            }

            Builder.AppendLine();

            ComposeMultiplyScalarLHS(indentation);

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine();
            }

            ComposeMultiplyScalarRHS(indentation);

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine();
            }

            ComposeDivideScalarLHS(indentation);

            Builder.AppendLine();

            ComposeMathUtility(indentation);
        }

        private void ComposeNegate(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.NegateOperator());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.FullyQualifiedName}} operator -({{Data.Vector.FullyQualifiedName}} a)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.NegateA(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator -({Data.Vector.FullyQualifiedName} a) => ({ConstantVectorTexts.Upper.NegateA(Data.Dimension)});");
            }
        }

        private void ComposeMultiplyScalarLHS(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorLHS());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.FullyQualifiedName}} operator *({{Data.Vector.FullyQualifiedName}} a, global::SharpMeasures.Scalar b)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.MultiplyAScalar(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator *({Data.Vector.FullyQualifiedName} a, global::SharpMeasures.Scalar b) => ({ConstantVectorTexts.Upper.MultiplyAScalar(Data.Dimension)});");
            }
        }

        private void ComposeMultiplyScalarRHS(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.FullyQualifiedName}} operator *(global::SharpMeasures.Scalar a, {{Data.Vector.FullyQualifiedName}} b)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(b);

                    {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.MultiplyBScalar(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator *(global::SharpMeasures.Scalar a, {Data.Vector.FullyQualifiedName} b) => ({ConstantVectorTexts.Upper.MultiplyBScalar(Data.Dimension)});");
            }
        }

        private void ComposeDivideScalarLHS(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.FullyQualifiedName}} operator /({{Data.Vector.FullyQualifiedName}} a, global::SharpMeasures.Scalar b)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.DivideAScalar(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator /({Data.Vector.FullyQualifiedName} a, global::SharpMeasures.Scalar b) => ({ConstantVectorTexts.Upper.DivideAScalar(Data.Dimension)});");
            }
        }

        private void ComposeSumMethod(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddSameTypeMethod());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Vector.FullyQualifiedName}} Add({{Data.Vector.FullyQualifiedName}} addend)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(addend);
                        
                    {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Add({Data.Vector.FullyQualifiedName} addend) => new({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)});");
            }
        }

        private void ComposeDifferenceMethod(Indentation indentation)
        {
            if (Data.Difference == Data.Vector.AsNamedType())
            {
                ComposeDifferenceMethodAsSameType(indentation);
            }
            else
            {
                ComposeDifferenceMethodAsDifferentType(indentation);
            }
        }

        private void ComposeDifferenceMethodAsSameType(Indentation indentation)
        {
            if (Data.Vector.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.FullyQualifiedName}} Subtract({{Data.Vector.FullyQualifiedName}} subtrahend)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return new({{ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.Append($"{indentation}public {Data.Difference.FullyQualifiedName} Subtract({Data.Vector.FullyQualifiedName} subtrahend) " +
                    $"=> ({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)});");
            }
        }

        private void ComposeDifferenceMethodAsDifferentType(Indentation indentation)
        {
            if (Data.Difference.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Vector.FullyQualifiedName}} Add({{Data.Difference.FullyQualifiedName}} addend)
                    {{indentation}}}
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(addend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Vector.FullyQualifiedName}} Subtract({{Data.Difference.FullyQualifiedName}} subtrahend)
                    {{indentation}}}
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Add({Data.Difference.FullyQualifiedName} addend) => ({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)});");

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Subtract({Data.Difference.FullyQualifiedName} subtrahend) => ({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)});");
            }

            if (Data.Vector.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.FullyQualifiedName}} Subtract({{Data.Vector.FullyQualifiedName}} subtrahend)
                    {{indentation}}}
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($"{indentation}public {Data.Difference.FullyQualifiedName} Subtract({Data.Vector.FullyQualifiedName} subtrahend) => ({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)});");
            }
        }

        private void ComposeSumOperator(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddSameTypeOperator());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator +({Data.Vector.FullyQualifiedName} a, {Data.Vector.FullyQualifiedName} b) => new({ConstantVectorTexts.Upper.AddBVector(Data.Dimension)});");
        }

        private void ComposeDifferenceOperator(Indentation indentation)
        {
            if (Data.Difference == Data.Vector.AsNamedType())
            {
                ComposeDifferenceOperatorAsSameType(indentation);
            }
            else
            {
                ComposeDifferenceOperatorAsDifferentType(indentation);
            }
        }

        private void ComposeDifferenceOperatorAsSameType(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeOperator());
            Builder.AppendLine($"{indentation}public static {Data.Difference.FullyQualifiedName} operator -({Data.Vector.FullyQualifiedName} a, {Data.Vector.FullyQualifiedName} b) => new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)});");
        }

        private void ComposeDifferenceOperatorAsDifferentType(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator +({Data.Vector.FullyQualifiedName} a, {Data.Difference.FullyQualifiedName} b) => new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)});");

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorRHS());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator +({Data.Difference.FullyQualifiedName} a, {Data.Vector.FullyQualifiedName} b) => new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)});");

            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeOperator());
            Builder.AppendLine($"{indentation}public static {Data.Difference.FullyQualifiedName} operator -({Data.Vector.FullyQualifiedName} a, {Data.Vector.FullyQualifiedName} b) => new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)});");

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} operator -({Data.Vector.FullyQualifiedName} a, {Data.Difference.FullyQualifiedName} b) => new({ConstantVectorTexts.Upper.SubtractBVector(Data.Dimension)});");
        }

        private void ComposeMathUtility(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"global::SharpMeasures.Scalar\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> ScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult();");
            }
            else
            {
                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"global::SharpMeasures.Scalar\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> PureScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult();");

                Builder.AppendLine();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Scalar.Value.FullyQualifiedName}\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<{Data.Scalar.Value.FullyQualifiedName}> ScalarMaths {{ get; }} " +
                    $"= global::SharpMeasures.Maths.MathFactory.ScalarResult<{Data.Scalar.Value.FullyQualifiedName}();");
            }

            if (Data.SquaredScalar is not null)
            {
                Builder.AppendLine();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.SquaredScalar.Value.FullyQualifiedName}\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<{Data.SquaredScalar.Value.Name}> SquaredScalarMaths {{ get; }} " +
                    $"= global::SharpMeasures.Maths.MathFactory.ScalarResult<{Data.SquaredScalar.Value.Name}();");
            }

            Builder.AppendLine();

            Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Vector.FullyQualifiedName}\"/>.</summary>");
            Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IVector{Data.Dimension}ResultingMaths<{Data.Vector.FullyQualifiedName}> VectorMaths {{ get; }} " +
                $"= global::SharpMeasures.Maths.MathFactory.Vector{Data.Dimension}Result<{Data.Vector.FullyQualifiedName}>();");
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
