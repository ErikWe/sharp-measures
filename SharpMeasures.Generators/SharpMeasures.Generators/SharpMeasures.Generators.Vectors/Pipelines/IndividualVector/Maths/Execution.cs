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

        context.AddSource($"{data.Vector.Name}_{data.Dimension}_Maths.g.cs", SourceText.From(source, Encoding.UTF8));
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
        private InterfaceCollector InterfaceCollector { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, Data.Vector.Namespace);
            InterfaceCollector = InterfaceCollector.Delayed(Builder);

            UsingsCollector.AddUsings("SharpMeasures", "SharpMeasures.Maths", Data.Unit.Namespace);

            if (Data.Scalar is not null)
            {
                UsingsCollector.AddUsing(Data.Scalar.Value.Namespace);
            }

            if (Data.Vector.IsReferenceType)
            {
                UsingsCollector.AddUsing("System");
            }

            if (Data.Dimension is 3)
            {
                UsingsCollector.AddUsing("System.Numerics");
            }

            if (Data.ImplementSum)
            {
                InterfaceCollector.AddInterface($"IAddendVector{Data.Dimension}Quantity<{Data.Vector.Name}>");
            }

            if (Data.ImplementDifference)
            {
                if (Data.Difference == Data.Vector.AsNamedType())
                {
                    InterfaceCollector.AddInterfaces
                    (
                        $"IMinuendVector{Data.Dimension}Quantity<{Data.Vector.Name}>",
                        $"ISubtrahendVector{Data.Dimension}Quantity<{Data.Vector.Name}>"
                    );
                }
                else
                {
                    InterfaceCollector.AddInterfaces
                    (
                        $"IMinuendVector{Data.Dimension}Quantity<{Data.Vector.Name}, {Data.Difference.Name}, {Data.Vector.Name}>",
                        $"ISubtrahendVector{Data.Dimension}Quantity<{Data.Vector.Name}, {Data.Difference.Name}, {Data.Vector.Name}>",
                        $"IAddendVector{Data.Dimension}Quantity<{Data.Vector.Name}, {Data.Vector.Name}, {Data.Difference.Name}>",
                        $"IMinuendVector{Data.Dimension}Quantity<{Data.Vector.Name}, {Data.Vector.Name}, {Data.Difference.Name}>"
                    );
                }
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            UsingsCollector.MarkInsertionPoint();

            Builder.AppendLine(Data.Vector.ComposeDeclaration());

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
            if (Data.ImplementSum)
            {
                ComposeSumMethod(indentation);
            }

            if (Data.ImplementDifference)
            {
                ComposeDifferenceMethod(indentation);
            }

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Plus() => this;");
            AppendDocumentation(indentation, Data.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Negate() => this;");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Multiply(Scalar factor) => ({ConstantVectorTexts.Upper.MultiplyFactorScalar(Data.Dimension)});");
            AppendDocumentation(indentation, Data.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Divide(Scalar divisor) => ({ConstantVectorTexts.Upper.MultiplyFactorScalar(Data.Dimension)});");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator +({Data.Vector.Name} a) => a;");

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
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.Name}} operator -({{Data.Vector.Name}} a)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.NegateA(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator -({Data.Vector.Name} a) => ({ConstantVectorTexts.Upper.NegateA(Data.Dimension)});");
            }
        }

        private void ComposeMultiplyScalarLHS(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorLHS());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.Name}} operator *({{Data.Vector.Name}} a, Scalar b)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.MultiplyAScalar(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator *({Data.Vector.Name} a, Scalar b) => ({ConstantVectorTexts.Upper.MultiplyAScalar(Data.Dimension)});");
            }
        }

        private void ComposeMultiplyScalarRHS(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.Name}} operator *(Scalar a, {{Data.Vector.Name}} b)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(b);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.MultiplyBScalar(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator *(Scalar a, {Data.Vector.Name} b) => ({ConstantVectorTexts.Upper.MultiplyBScalar(Data.Dimension)});");
            }
        }

        private void ComposeDivideScalarLHS(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public static {{Data.Vector.Name}} operator /({{Data.Vector.Name}} a, Scalar b)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.DivideAScalar(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator /({Data.Vector.Name} a, Scalar b) => ({ConstantVectorTexts.Upper.DivideAScalar(Data.Dimension)});");
            }
        }

        private void ComposeSumMethod(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddSameTypeMethod());

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Vector.Name}} Add({{Data.Vector.Name}} addend)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(addend);
                        
                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public {Data.Vector.Name} Add({Data.Vector.Name} addend) => ({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)});");
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
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.Name}} Subtract({{Data.Vector.Name}} subtrahend)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.Name}} SubtractFrom({{Data.Vector.Name}} minuend)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(minuend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.SubtractFromMinuendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.Append($"{indentation}public {Data.Difference.Name} Subtract({Data.Vector.Name} subtrahend) " +
                    $"=> ({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)});");

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.Append($"{indentation}public {Data.Difference.Name} SubtractFrom({Data.Vector.Name} minuend) " +
                    $"=> ({ConstantVectorTexts.Upper.SubtractFromMinuendVector(Data.Dimension)});");
            }
        }

        private void ComposeDifferenceMethodAsDifferentType(Indentation indentation)
        {
            if (Data.Difference.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Vector.Name}} Add({{Data.Difference.Name}} addend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(addend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Vector.Name}} Subtract({{Data.Difference.Name}} subtrahend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Vector.Name} Add({Data.Difference.Name} addend) => ({ConstantVectorTexts.Upper.AddAddendVector(Data.Dimension)});");

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Vector.Name} Subtract({Data.Difference.Name} subtrahend) => ({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)});");
            }

            if (Data.Vector.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.Name}} Subtract({{Data.Vector.Name}} subtrahend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}{{Data.Difference.Name}} ISubtrahendScalarQuantity<{{Data.Vector.Name}}, {{Data.Difference.Name}}, {{Data.Vector.Name}}>SubtractFrom({{Data.Vector.Name}} minuend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(minuend);

                    {{indentation.Increased}}return ({{ConstantVectorTexts.Upper.SubtractFromMinuendVector(Data.Dimension)}});
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($"{indentation}public {Data.Difference.Name} Subtract({Data.Vector.Name} subtrahend) => ({ConstantVectorTexts.Upper.SubtractSubtrahendVector(Data.Dimension)});");

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.AppendLine($"{indentation}{Data.Difference.Name} ISubtrahendScalarQuantity<{Data.Vector.Name}, {Data.Difference.Name}, {Data.Vector.Name}>" +
                    $".SubtractFrom({Data.Vector.Name} minuend) => new({ConstantVectorTexts.Upper.SubtractFromMinuendVector(Data.Dimension)});");
            }
        }

        private void ComposeSumOperator(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddSameTypeOperator());
            Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator +({Data.Vector.Name} x, {Data.Vector.Name} y) => new(x.Magnitude.Value + y.Magnitude.Value);");
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
            Builder.AppendLine($"{indentation}public static {Data.Difference.Name} operator -({Data.Vector.Name} x, {Data.Vector.Name} y) => new(x.Magnitude.Value - y.Magnitude.Value);");
        }

        private void ComposeDifferenceOperatorAsDifferentType(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator +({Data.Vector.Name} x, {Data.Difference.Name} y) => new(x.Magnitude.Value + y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorRHS());
            Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator +({Data.Difference.Name} x, {Data.Vector.Name} y) => new(x.Magnitude.Value + y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeOperator());
            Builder.AppendLine($"{indentation}public static {Data.Difference.Name} operator -({Data.Vector.Name} x, {Data.Vector.Name} y) => new(x.Magnitude.Value - y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator -({Data.Vector.Name} x, {Data.Difference.Name} y) => new(x.Magnitude.Value - y.Magnitude.Value);");
        }

        private void ComposeMathUtility(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"Scalar\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static IScalarResultingMaths<Scalar> ScalarMaths {{ get; }} = MathFactory.ScalarResult();");
            }
            else
            {
                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"Scalar\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static IScalarResultingMaths<Scalar> PureScalarMaths {{ get; }} = MathFactory.ScalarResult();");

                Builder.AppendLine();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Scalar.Value.Name}\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static IScalarResultingMaths<{Data.Scalar.Value.Name}> ScalarMaths {{ get; }} " +
                    $"= MathFactory.ScalarResult<{Data.Scalar.Value.Name}();");
            }

            if (Data.SquaredScalar is not null)
            {
                Builder.AppendLine();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.SquaredScalar.Value.Name}\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static IScalarResultingMaths<{Data.SquaredScalar.Value.Name}> SquaredScalarMaths {{ get; }} " +
                    $"= MathFactory.ScalarResult<{Data.SquaredScalar.Value.Name}();");
            }

            Builder.AppendLine();

            Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Vector.Name}\"/>.</summary>");
            Builder.AppendLine($"{indentation}private static IVector{Data.Dimension}ResultingMaths<{Data.Vector.Name}> VectorMaths {{ get; }} " +
                $"= MathFactory.Vector{Data.Dimension}Result<{Data.Vector.Name}>();");
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
