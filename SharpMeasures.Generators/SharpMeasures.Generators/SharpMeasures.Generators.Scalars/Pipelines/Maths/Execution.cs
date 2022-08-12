namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

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

        private InterfaceCollector InterfaceCollector { get; }

        private Composer(DataModel data)
        {
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
            Builder.AppendLine($"{indentation}public {Data.Scalar.Name} Absolute() => new(global::System.Math.Abs(Magnitude.Value));");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.Sign());
            Builder.AppendLine($"{indentation}public int Sign() => global::System.Math.Sign(Magnitude.Value);");

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
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Plus() => this;");
            AppendDocumentation(indentation, Data.Documentation.NegateMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Negate() => this;");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Multiply(global::SharpMeasures.Scalar factor) => new(Magnitude.Value * factor.Value);");
            AppendDocumentation(indentation, Data.Documentation.DivideScalarMethod());
            Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Divide(global::SharpMeasures.Scalar divisor) => new(Magnitude.Value / divisor.Value);");

            Builder.AppendLine();

            if (Data.Square is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.MultiplySameTypeMethod());
                Builder.AppendLine($"{indentation}public {Data.Square.Value.FullyQualifiedName} Multiply({Data.Scalar.FullyQualifiedName} factor) => new(Magnitude.Value * factor.Magnitude.Value);");
            }

            AppendDocumentation(indentation, Data.Documentation.DivideSameTypeMethod());
            Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar Divide({Data.Scalar.FullyQualifiedName} divisor) => new(Magnitude.Value / divisor.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.UnaryPlusOperator());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator +({Data.Scalar.FullyQualifiedName} x) => x;");
            AppendDocumentation(indentation, Data.Documentation.NegateOperator());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator -({Data.Scalar.FullyQualifiedName} x) => new(-x.Magnitude);");

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
                Builder.AppendLine($"{indentation}public static {Data.Square.Value.FullyQualifiedName} operator *({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                    "=> new(x.Magnitude.Value * y.Magnitude.Value);");
            }

            AppendDocumentation(indentation, Data.Documentation.DivideSameTypeOperator());
            Builder.AppendLine($"{indentation}public static global::SharpMeasures.Scalar operator /({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) " +
                "=> new(x.Magnitude.Value / y.Magnitude.Value);");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator *({Data.Scalar.FullyQualifiedName} x, global::SharpMeasures.Scalar y) => new(x.Magnitude.Value * y.Value);");
            AppendDocumentation(indentation, Data.Documentation.MultiplyScalarOperatorRHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator *(global::SharpMeasures.Scalar x, {Data.Scalar.FullyQualifiedName} y) => new(x.Value * y.Magnitude.Value);");
            AppendDocumentation(indentation, Data.Documentation.DivideScalarOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator /({Data.Scalar.FullyQualifiedName} x, global::SharpMeasures.Scalar y) => new(x.Magnitude.Value / y.Value);");

            if (Data.Reciprocal is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.DivideScalarOperatorRHS());
                Builder.AppendLine($"{indentation}public static {Data.Reciprocal.Value.FullyQualifiedName} operator /(global::SharpMeausures.Scalar x, {Data.Scalar.FullyQualifiedName} y) => new(x.Value / y.Magnitude.Value);");
            }
        }

        private void ComposePowerFunctions(Indentation indentation)
        {
            int startLength = Builder.Length;

            if (Data.Reciprocal is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.Reciprocal());
                Builder.AppendLine($"{indentation}public {Data.Reciprocal.Value.FullyQualifiedName} Reciprocal() => new(1 / Magnitude.Value);");
            }

            if (Data.Square is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.Square());
                Builder.AppendLine($"{indentation}public {Data.Square.Value.FullyQualifiedName} Square() => new(global::System.Math.Pow(Magnitude.Value, 2));");
            }

            if (Data.Cube is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.Cube());
                Builder.AppendLine($"{indentation}public {Data.Cube.Value.FullyQualifiedName} Cube() => new(global::System.Math.Pow(Magnitude.Value, 3));");
            }

            if (Data.SquareRoot is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.SquareRoot());
                Builder.AppendLine($"{indentation}public {Data.SquareRoot.Value.FullyQualifiedName} SquareRoot() => new(global::System.Math.Sqrt(Magnitude.Value));");
            }

            if (Data.CubeRoot is not null)
            {
                AppendDocumentation(indentation, Data.Documentation.CubeRoot());
                Builder.AppendLine($"{indentation}public {Data.CubeRoot.Value.FullyQualifiedName} CubeRoot() => new(global::System.Math.Cbrt(Magnitude.Value));");
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
                Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} From({Data.Reciprocal.Value.FullyQualifiedName} {parameterName}) => " +
                    $"new(1 / {parameterName}.Magnitude.Value);");
            }

            if (Data.Square is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.Square.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromSquare());
                Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} From({Data.Square.Value.FullyQualifiedName} {parameterName}) => " +
                    $"new(global::System.Math.Sqrt({parameterName}.Magnitude.Value));");
            }

            if (Data.Cube is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.Cube.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromCube());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Name} From({Data.Cube.Value.Name} {parameterName}) => " +
                    $"new(global::System.Math.Cbrt({parameterName}.Magnitude.Value));");
            }

            if (Data.SquareRoot is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.SquareRoot.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromSquareRoot());
                Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} From({Data.SquareRoot.Value.FullyQualifiedName} {parameterName}) => " +
                    $"new(global::System.Math.Pow({parameterName}.Magnitude.Value, 2));");
            }

            if (Data.CubeRoot is not null)
            {
                string parameterName = SourceBuildingUtility.ToParameterName(Data.CubeRoot.Value.Name);

                AppendDocumentation(indentation, Data.Documentation.FromCubeRoot());
                Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} From({Data.CubeRoot.Value.FullyQualifiedName} {parameterName}) => " +
                    $"new(global::System.Math.Pow({parameterName}.Magnitude.Value, 3));");
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
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Scalar.FullyQualifiedName}} Add({{Data.Scalar.FullyQualifiedName}} addend)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(addend);
                        
                    {{indentation.Increased}}return new(Magnitude.Value + addend.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Add({Data.Scalar.FullyQualifiedName} addend) => new(Magnitude.Value + addend.Magnitude.Value);");
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
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.FullyQualifiedName}} Subtract({{Data.Scalar.FullyQualifiedName}} subtrahend)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return new(Magnitude.Value - subtrahend.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.Append($"{indentation}public {Data.Difference.FullyQualifiedName} Subtract({Data.Scalar.FullyQualifiedName} subtrahend) " +
                    $"=> new(Magnitude.Value - subtrahend.Magnitude.Value);");
            }
        }

        private void ComposeDifferenceMethodAsDifferentType(Indentation indentation)
        {
            if (Data.Difference.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Scalar.FullyQualifiedName}} Add({{Data.Difference.FullyQualifiedName}} addend)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(addend);

                    {{indentation.Increased}}return new(Magnitude.Value + addend.Magnitude.Value);
                    {{indentation}}}
                    """);

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Scalar.FullyQualifiedName}} Subtract({{Data.Difference.FullyQualifiedName}} subtrahend)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return new(Magnitude.Value - subtrahend.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.AddDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Add({Data.Difference.FullyQualifiedName} addend) => new(Magnitude.Value + addend.Magnitude.Value);");

                AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceMethod());
                Builder.AppendLine($"{indentation}public {Data.Scalar.FullyQualifiedName} Subtract({Data.Difference.FullyQualifiedName} subtrahend) => new(Magnitude.Value - subtrahend.Magnitude.Value);");
            }

            if (Data.Scalar.IsReferenceType)
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public {{Data.Difference.FullyQualifiedName}} Subtract({{Data.Scalar.FullyQualifiedName}} subtrahend)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(subtrahend);

                    {{indentation.Increased}}return new(Magnitude.Value - subtrahend.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeMethod());
                Builder.AppendLine($"{indentation}public {Data.Difference.FullyQualifiedName} Subtract({Data.Scalar.FullyQualifiedName} subtrahend) => new(Magnitude.Value - subtrahend.Magnitude.Value);");
            }
        }

        private void ComposeSumOperator(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddSameTypeOperator());

            if (Data.Scalar.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public static {{Data.Scalar.FullyQualifiedName}} operator +({{Data.Scalar.FullyQualifiedName}} x, {{Data.Scalar.FullyQualifiedName}} y)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(x);
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(y);

                    {{indentation.Increased}}return new(x.Magnitude.Value * y.Magnitude.Value);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator +({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) => new(x.Magnitude.Value + y.Magnitude.Value);");
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
            Builder.AppendLine($"{indentation}public static {Data.Difference.FullyQualifiedName} operator -({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) => new(x.Magnitude.Value - y.Magnitude.Value);");
        }

        private void ComposeDifferenceOperatorAsDifferentType(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator +({Data.Scalar.FullyQualifiedName} x, {Data.Difference.FullyQualifiedName} y) => new(x.Magnitude.Value + y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.AddDifferenceOperatorRHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator +({Data.Difference.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) => new(x.Magnitude.Value + y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.SubtractSameTypeOperator());
            Builder.AppendLine($"{indentation}public static {Data.Difference.FullyQualifiedName} operator -({Data.Scalar.FullyQualifiedName} x, {Data.Scalar.FullyQualifiedName} y) => new(x.Magnitude.Value - y.Magnitude.Value);");

            AppendDocumentation(indentation, Data.Documentation.SubtractDifferenceOperatorLHS());
            Builder.AppendLine($"{indentation}public static {Data.Scalar.FullyQualifiedName} operator -({Data.Scalar.FullyQualifiedName} x, {Data.Difference.FullyQualifiedName} y) => new(x.Magnitude.Value - y.Magnitude.Value);");
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
