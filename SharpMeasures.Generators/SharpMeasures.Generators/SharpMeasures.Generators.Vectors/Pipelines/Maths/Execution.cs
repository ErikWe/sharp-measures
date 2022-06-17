﻿namespace SharpMeasures.Generators.Vectors.Pipelines.Maths;

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

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, Data.Vector.Namespace);
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
            AppendDocumentation(indentation, Data.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Plus() => this;");
            AppendDocumentation(indentation, Data.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Negate() => this;");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Multiply(Scalar factor) => this * factor;");
            AppendDocumentation(indentation, Data.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Divide(Scalar divisor) => this / divisor;");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Vector.Name} operator +({Data.Vector.Name} a) => a;");

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine();
            }

            ComposeNegate(indentation);

            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine();
            }

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
