﻿namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Scalar.Name}_Maths.g.cs", SourceText.From(source, Encoding.UTF8));
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

            UsingsCollector = UsingsCollector.Delayed(Builder, data.Scalar.Namespace);
            InterfaceCollector = InterfaceCollector.Delayed(Builder);

            UsingsCollector.AddUsings("SharpMeasures", "SharpMeasures.ScalarAbstractions", "System");

            InterfaceCollector.AddInterfaces(new []
            {
                $"IFactorScalarQuantity<{Data.Scalar.Name}, {Data.Scalar.Name}, Scalar>",
                $"IDividendScalarQuantity<{Data.Scalar.Name}, {Data.Scalar.Name}, Scalar>"
            });

            if (Data.ImplementSum)
            {
                InterfaceCollector.AddInterface($"IAddendScalarQuantity<{Data.Scalar.Name}>");
            }

            if (Data.ImplementDifference)
            {
                if (Data.Difference == Data.Scalar.AsNamedType())
                {
                    InterfaceCollector.AddInterfaces
                    (
                        $"IMinuendScalarQuantity<{Data.Scalar.Name}>",
                        $"ISubtrahendScalarQuantity<{Data.Scalar.Name}>"
                    );
                }
                else
                {
                    InterfaceCollector.AddInterfaces
                    (
                        $"IMinuendScalarQuantity<{Data.Scalar.Name}, {Data.Difference.Name}, {Data.Scalar.Name}>",
                        $"ISubtrahendScalarQuantity<{Data.Scalar.Name}, {Data.Difference.Name}, {Data.Scalar.Name}>",
                        $"IAddendScalarQuantity<{Data.Scalar.Name}, {Data.Scalar.Name}, {Data.Difference.Name}>",
                        $"IMinuendScalarQuantity<{Data.Scalar.Name}, {Data.Scalar.Name}, {Data.Difference.Name}>"
                    );
                }
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Scalar.Namespace);

            UsingsCollector.MarkInsertionPoint();

            Builder.Append(Data.Scalar.ComposeDeclaration());

            InterfaceCollector.MarkInsertionPoint();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);

            InterfaceCollector.InsertInterfacesOnNewLines(new Indentation(1));
            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
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

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.Absolute());
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Absolute() => new(Math.Abs(Magnitude.Value));");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.Sign());
            Builder.AppendLine($"{indentation}public int Sign() => Math.Sign(Magnitude.Value);");

            Builder.AppendLine();

            ComposePowerFunctions(indentation);

            ComposeFromPowerFunctions(indentation);

            if (Data.ImplementSum || Data.ImplementDifference)
            {
                Builder.AppendLine();
            }

            if (Data.ImplementSum)
            {
                ComposeSumMethod(indentation);
            }

            if (Data.ImplementDifference)
            {
                ComposeDifferenceMethod(indentation);
            }

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Plus() => this;");
            AppendDocumentation(indentation, Data.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Negate() => this;");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Multiply(Scalar factor) => new(Magnitude.Value * factor.Value);");
            AppendDocumentation(indentation, Data.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Divide(Scalar divisor) => new(Magnitude.Value / divisor.Value);");

            Builder.AppendLine();

            if (Data.Square is not null)
            {
                InterfaceCollector.AddInterfaces($"IMultiplicableScalar<{Data.Square.Value.Name}, {Data.Scalar.Name}>");

                AppendDocumentation(indentation, Data.Documentation.MultiplySameTypeMethod());
                Builder.AppendLine($"{indentation}public {Data.Square.Value.Name} Multiply({Data.Scalar.Name} factor) => new(Magnitude.Value * factor.Magnitude.Value);");
            }

            AppendDocumentation(indentation, Data.Documentation.DivideSameTypeMethod());
            Builder.AppendLine($"{indentation}public Scalar Divide({Data.Scalar.Name} divisor) => new(Magnitude.Value / divisor.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator +({Data.Scalar.Name} x) => x;");
            AppendDocumentation(indentation, Data.Documentation.NegateOperator());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator -({Data.Scalar.Name} x) => -x;");

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

            if (Data.Square is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.MultiplySameTypeOperator());
                Builder.AppendLine($"{indentation}public static {Data.Square.Value.Name} operator *({Data.Scalar.Name} x, {Data.Scalar.Name} y) => new(x.Magnitude.Value * y.Magnitude.Value);");
            }

            AppendDocumentation(indentation, Data.Documentation.DivideSameTypeOperator());
            Builder.AppendLine($"{indentation}public static Scalar operator /({Data.Scalar.Name} x, {Data.Scalar.Name} y) => new(x.Magnitude.Value / y.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator *({Data.Scalar.Name} x, Scalar y) => new(x.Magnitude.Value * y.Value);");
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator *(Scalar x, {Data.Scalar.Name} y) => new(x.Value * y.Magnitude.Value);");
            AppendDocumentation(indentation, Data.Documentation.DivideScalarOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator /({Data.Scalar.Name} x, Scalar y) => new(x.Magnitude.Value / y.Value);");

            if (Data.Reciprocal is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.DivideScalarOperatorRHS());
                Builder.AppendLine($"{indentation}public static {Data.Reciprocal.Value.Name} operator /(Scalar x, {Data.Scalar.Name} y) => new(x.Value / y.Magnitude.Value);");
            }
        }

        private void ComposePowerFunctions(Indentation indentation)
        {
            int startLength = Builder.Length;

            if (Data.Reciprocal is not null)
            {
                UsingsCollector.AddUsing(Data.Reciprocal.Value.Namespace);

                AppendDocumentation(indentation, Data.Documentation.Reciprocal());
                Builder.AppendLine($"{indentation}public {Data.Reciprocal.Value.Name} Reciprocal() => new(1 / Magnitude.Value);");
            }

            if (Data.Square is not null)
            {
                UsingsCollector.AddUsing(Data.Square.Value.Namespace);
                UsingsCollector.AddUsing("System");

                AppendDocumentation(indentation, Data.Documentation.Square());
                Builder.AppendLine($"{indentation}public {Data.Square.Value.Name} Square() => new(Math.Pow(Magnitude.Value, 2));");
            }

            if (Data.Cube is not null)
            {
                UsingsCollector.AddUsing(Data.Cube.Value.Namespace);
                UsingsCollector.AddUsing("System");

                AppendDocumentation(indentation, Data.Documentation.Cube());
                Builder.AppendLine($"{indentation}public {Data.Cube.Value.Name} Cube() => new(Math.Pow(Magnitude.Value, 3));");
            }

            if (Data.SquareRoot is not null)
            {
                UsingsCollector.AddUsing(Data.SquareRoot.Value.Namespace);
                UsingsCollector.AddUsing("System");

                AppendDocumentation(indentation, Data.Documentation.SquareRoot());
                Builder.AppendLine($"{indentation}public {Data.SquareRoot.Value.Name} SquareRoot() => new(Math.Sqrt(Magnitude.Value));");
            }

            if (Data.CubeRoot is not null)
            {
                UsingsCollector.AddUsing(Data.CubeRoot.Value.Namespace);
                UsingsCollector.AddUsing("System");

                AppendDocumentation(indentation, Data.Documentation.CubeRoot());
                Builder.AppendLine($"{indentation}public {Data.CubeRoot.Value.Name} CubeRoot() => new(Math.Cbrt(Magnitude.Value));");
            }

            if (Builder.Length > startLength)
            {
                Builder.AppendLine();
            }
        }

        private void ComposeFromPowerFunctions(Indentation indentation)
        {
            int startLength = Builder.Length;

            if (Data.Reciprocal is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.Reciprocal.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromReciprocal());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} From({Data.Reciprocal.Value.Name} {parameterName}) => " +
                    $"new(1 / {parameterName}.Magnitude.Value);");
            }

            if (Data.Square is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.Square.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromSquare());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} From({Data.Square.Value.Name} {parameterName}) => " +
                    $"new(Math.Sqrt({parameterName}.Magnitude.Value));");
            }

            if (Data.Cube is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.Cube.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromCube());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} From({Data.Cube.Value.Name} {parameterName}) => " +
                    $"new(Math.Cbrt({parameterName}.Magnitude.Value));");
            }

            if (Data.SquareRoot is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.SquareRoot.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromSquareRoot());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} From({Data.SquareRoot.Value.Name} {parameterName}) => " +
                    $"new(Math.Pow({parameterName}.Magnitude.Value, 2));");
            }

            if (Data.CubeRoot is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.CubeRoot.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromCubeRoot());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} From({Data.CubeRoot.Value.Name} {parameterName}) => " +
                    $"new(Math.Pow({parameterName}.Magnitude.Value, 3));");
            }

            if (Builder.Length > startLength)
            {
                Builder.AppendLine();
            }
        }

        private void ComposeSumMethod(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddSameTypeMethod());

            if (Data.Scalar.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Scalar.Name}} Add({{Data.Scalar.Name}} addend)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(addend);
                        
                    {{indentation.Increased}}return new(Magnitude.Value + addend.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Add({Data.Scalar.Name} addend) => new(Magnitude.Value + addend.Magnitude.Value);");
            }
        }

        private void ComposeDifferenceMethod(Indentation indentation)
        {
            if (Data.Difference == Data.Scalar.AsNamedType())
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
            if (Data.Scalar.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.Name}} Subtract({{Data.Scalar.Name}} subtrahend)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return new(Magnitude.Value - subtrahend.Magnitude.Value);
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.Name}} SubtractFrom({{Data.Scalar.Name}} minuend)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(minuend);

                    {{indentation.Increased}}return new(minuend.Magnitude.Value - Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.Append($"{indentation}public {Data.Difference.Name} Subtract({Data.Scalar.Name} subtrahend) " +
                    $"=> new(Magnitude.Value - subtrahend.Magnitude.Value);");

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.Append($"{indentation}public {Data.Difference.Name} SubtractFrom({Data.Scalar.Name} minuend) " +
                    $"=> new(minuend.Magnitude.Value - Magnitude.Value);");
            }
        }

        private void ComposeDifferenceMethodAsDifferentType(Indentation indentation)
        {
            if (Data.Difference.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Scalar.Name}} Add({{Data.Difference.Name}} addend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(addend);

                    {{indentation.Increased}}return new(Magnitude.Value + addend.Magnitude.Value);
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Scalar.Name}} Subtract({{Data.Difference.Name}} subtrahend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return new(Magnitude.Value - subtrahend.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Add({Data.Difference.Name} addend) => new(Magnitude.Value + addend.Magnitude.Value);");

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Subtract({Data.Difference.Name} subtrahend) => new(Magnitude.Value - subtrahend.Magnitude.Value);");
            }

            if (Data.Scalar.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.Name}} Subtract({{Data.Scalar.Name}} subtrahend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return new(Magnitude.Value - subtrahend.Magnitude.Value);
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}{{Data.Difference.Name}} ISubtrahendScalarQuantity<{{Data.Scalar.Name}}, {{Data.Difference.Name}}, {{Data.Scalar.Name}}>SubtractFrom({{Data.Scalar.Name}} minuend)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(minuend);

                    {{indentation.Increased}}return new(minuend.Magnitude.Value - Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($"{indentation}public {Data.Difference.Name} Subtract({Data.Scalar.Name} subtrahend) => new(Magnitude.Value - subtrahend.Magnitude.Value);");

                AppendDocumentation(indentation, Data.Documentation.SubtractFromSameTypeMethod());
                Builder.AppendLine($"{indentation}{Data.Difference.Name} ISubtrahendScalarQuantity<{Data.Scalar.Name}, {Data.Difference.Name}, {Data.Scalar.Name}>" +
                    $".SubtractFrom({Data.Scalar.Name} minuend) => new(minuend.Magnitude.Value - Magnitude.Value);");
            }
        }

        private void ComposeSumOperator(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddSameTypeOperator());

            if (Data.Scalar.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public static {{Data.Scalar.Name}} operator +({{Data.Scalar.Name}} x, {{Data.Scalar.Name}} y)
                    {{indentation}}}
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(x);
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(y);

                    {{indentation.Increased}}return new(x.Magnitude.Value * y.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator +({Data.Scalar.Name} x, {Data.Scalar.Name} y) => new(x.Magnitude.Value + y.Magnitude.Value);");
            }
        }

        private void ComposeDifferenceOperator(Indentation indentation)
        {
            if (Data.Difference == Data.Scalar.AsNamedType())
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
            Builder.AppendLine($"{indentation}public static {Data.Difference.Name} operator -({Data.Scalar.Name} x, {Data.Scalar.Name} y) => new(x.Magnitude.Value - y.Magnitude.Value);");
        }

        private void ComposeDifferenceOperatorAsDifferentType(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator +({Data.Scalar.Name} x, {Data.Difference.Name} y) => new(x.Magnitude.Value + y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorRHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator +({Data.Difference.Name} x, {Data.Scalar.Name} y) => new(x.Magnitude.Value + y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeOperator());
            Builder.AppendLine($"{indentation}public static {Data.Difference.Name} operator -({Data.Scalar.Name} x, {Data.Scalar.Name} y) => new(x.Magnitude.Value - y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.Name} operator -({Data.Scalar.Name} x, {Data.Difference.Name} y) => new(x.Magnitude.Value - y.Magnitude.Value);");
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
