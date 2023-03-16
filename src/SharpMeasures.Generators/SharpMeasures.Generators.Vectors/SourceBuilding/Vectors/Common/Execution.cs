namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Common;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
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

        context.AddSource($"{data.Value.Vector.QualifiedName}.Common.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private SpecificTexts Texts { get; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);

            Texts = new(data.Dimension, data.Scalar, data.UnitQuantity, Data.UnitParameterName);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder, Data.SourceBuildingContext.HeaderContent);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            AppendDocumentation(new Indentation(0), Data.SourceBuildingContext.Documentation.Header());
            Builder.Append(Data.Vector.ComposeDeclaration());

            AppendInterfaces();

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve() => Builder.ToString();

        private void AppendInterfaces()
        {
            List<string> interfaceNames = new()
            {
                $"global::SharpMeasures.IVector{Data.Dimension}Quantity<{Data.Vector.FullyQualifiedName}>"
            };

            if (Data.Vector.IsRecord is false)
            {
                interfaceNames.Add($"global::System.IEquatable<{Data.Vector.FullyQualifiedName}>");
            }

            InterfaceBuilding.AppendInterfaceImplementationOnNewLines(Builder, new Indentation(1), interfaceNames);
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            AppendZero(indentation);
            AppendComponents(indentation);
            AppendConstructors(indentation);
            AppendMagnitude(indentation);
            AppendInUnit(indentation);
            AppendNormalize(indentation);

            if (Data.Dimension is 3)
            {
                AppendTransform(indentation);
            }

            AppendToString(indentation);
            AppendDeconstruct(indentation);

            if (Data.Vector.IsRecord is false)
            {
                AppendEquality(indentation);
                AppendGetHashCode(indentation);
            }

            if (Data.Scalar is not null)
            {
                AppendCastFromComponents(indentation);
            }

            AppendWithComponentsMethods(indentation);

            if (Data.Unit.IsReferenceType)
            {
                AppendComputationUtilityMethod(indentation);
            }

            AppendMathUtilityMethod(indentation);
        }

        private void AppendZero(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Zero());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} Zero {{ get; }} = new({ConstantVectorTexts.Zeros(Data.Dimension)});");
        }

        private void AppendComponents(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                AppendComponentsAsScalars(indentation);

                return;
            }

            AppendComponentsAsTypes(indentation, Data.Scalar.Value);
        }

        private void AppendComponentsAsScalars(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            for (var i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ComponentMagnitude(i));
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar {Texts.Upper.ComponentName(i)} {{ get; }}");
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Components());
            Builder.AppendLine($"{indentation}public global::SharpMeasures.Vector{Data.Dimension} Components => ({ConstantVectorTexts.Upper.Name(Data.Dimension)});");
        }

        private void AppendComponentsAsTypes(Indentation indentation, NamedType scalar)
        {
            SeparationHandler.AddIfNecessary();

            for (var i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Component(i));
                Builder.AppendLine($"{indentation}public {scalar.FullyQualifiedName} {Texts.Upper.ComponentName(i)} {{ get; }}");
            }

            SeparationHandler.Add();

            for (var i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ComponentMagnitude(i));
                Builder.AppendLine($"{indentation}global::SharpMeasures.Scalar global::SharpMeasures.IVector{Data.Dimension}Quantity.{Texts.Upper.ComponentName(i)} => {Texts.Upper.ComponentName(i)}.Magnitude;");
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Components());
            Builder.AppendLine($"{indentation}public global::SharpMeasures.Vector{Data.Dimension} Components => ({ConstantVectorTexts.Upper.Magnitude(Data.Dimension)});");
        }

        private void AppendConstructors(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                AppendConstructorsToScalars(indentation);
            }
            else
            {
                AppendConstructorsToComponents(indentation);
            }

            AppendCommonConstructors(indentation);
        }

        private void AppendConstructorsToScalars(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarsConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)})");
            BlockBuilding.AppendBlock(Builder, AppendConstructorBlock, indentation);
        }

        private void AppendConstructorsToComponents(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ComponentsConstructor());

            if (Data.Scalar!.Value.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
                Builder.AppendLine($$"""
                    {{indentation}}{{DocumentationBuilding.ArgumentNullExceptionTag}}
                    {{indentation}}public {{Data.Vector.Name}}({{Texts.Lower.Component}})
                    {{indentation}}{
                    """);

                for (var i = 0; i < Data.Dimension; i++)
                {
                    Builder.AppendLine($"{indentation.Increased}{StaticBuilding.NullArgumentGuard(Texts.Lower.ComponentName(i))}");
                }

                SeparationHandler.Add();

                AppendConstructorBlock(indentation.Increased);

                BlockBuilding.AppendClosingBracket(Builder, indentation);
            }
            else
            {
                Builder.AppendLine($"{indentation}public {Data.Vector.Name}({Texts.Lower.Component})");
                BlockBuilding.AppendBlock(Builder, AppendConstructorBlock, indentation);
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarsConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)}) : this({Texts.Lower.NewComponent}) {{ }}");
        }

        private void AppendConstructorBlock(Indentation indentation)
        {
            for (var i = 0; i < Data.Dimension; i++)
            {
                Builder.AppendLine($"{indentation}{Texts.Upper.ComponentName(i)} = {Texts.Lower.ComponentName(i)};");
            }
        }

        private void AppendCommonConstructors(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.VectorConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}(global::SharpMeasures.Vector{Data.Dimension} components) : this({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)}) {{ }}");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarsAndUnitConstructor());

            if (Data.Unit.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            }

            Builder.Append($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)}, {Data.Unit.FullyQualifiedName} {Data.UnitParameterName}) ");

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($": this(ComputeRepresentedComponents({ConstantVectorTexts.Lower.Name(Data.Dimension)}, {Data.UnitParameterName})) {{ }}");
            }
            else
            {
                Builder.AppendLine($": this({Texts.Lower.ScalarMultiplyUnit}) {{ }}");
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.VectorAndUnitConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}(global::SharpMeasures.Vector{Data.Dimension} components, {Data.Unit.FullyQualifiedName} {Data.UnitParameterName}) : this({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)}, {Data.UnitParameterName}) {{ }}");
        }

        private void AppendMagnitude(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            if (Data.Scalar is null)
            {
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarMagnitude());
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar Magnitude() => PureScalarMaths.Magnitude{Data.Dimension}(this);");
            }
            else
            {
                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Magnitude());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Value.FullyQualifiedName} Magnitude() => ScalarMaths.Magnitude{Data.Dimension}(this);");

                SeparationHandler.Add();

                AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarMagnitude());
                Builder.AppendLine($"{indentation}global::SharpMeasures.Scalar global::SharpMeasures.IVector{Data.Dimension}Quantity.Magnitude() => PureScalarMaths.Magnitude{Data.Dimension}(this);");
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ScalarSquaredMagnitude());
            Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar SquaredMagnitude() => PureScalarMaths.SquaredMagnitude{Data.Dimension}(this);");
        }

        private void AppendInUnit(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public global::SharpMeasures.Vector{Data.Dimension} InUnit";
            var expression = $"new({ComposeInUnitComputation()})";
            var parameters = new[] { (Data.Unit, Data.UnitParameterName) };

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InUnit());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private string ComposeInUnitComputation()
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, Data.Scalar is null ? scalarComponent() : typeComponent(), ", ");

            return source.ToString();

            IEnumerable<string> scalarComponent()
            {
                for (var i = 0; i < Data.Dimension; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Value / {Data.UnitParameterName}.{Data.UnitQuantity.Name}.Magnitude.Value";
                }
            }

            IEnumerable<string> typeComponent()
            {
                for (var i = 0; i < Data.Dimension; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Magnitude.Value / {Data.UnitParameterName}.{Data.UnitQuantity.Name}.Magnitude.Value";
                }
            }
        }

        private void AppendNormalize(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Normalize());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Normalize() => VectorMaths.Normalize(this);");
        }

        private void AppendTransform(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Transform());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Transform(global::System.Numerics.Matrix4x4 transform) => VectorMaths.Transform(this, transform);");
        }

        private void AppendToString(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringMethod());
            Builder.AppendLine($"{indentation}public override string ToString() => ToString(global::System.Globalization.CultureInfo.CurrentCulture);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringFormat());
            Builder.AppendLine($"{indentation}public string ToString(string? format) => ToString(format, global::System.Globalization.CultureInfo.CurrentCulture);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringProvider());
            Builder.AppendLine($"""{indentation}public string ToString(global::System.IFormatProvider? formatProvider) => ToString("G", formatProvider);""");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.ToStringFormatAndProvider());
            Builder.Append($"{indentation}public string ToString(string? format, global::System.IFormatProvider? formatProvider) => ");

            if (Data.DefaultUnitInstanceSymbol is not null)
            {
                Builder.AppendLine($$"""$"{InUnit({{Data.Unit.FullyQualifiedName}}.{{Data.DefaultUnitInstanceName}}).ToString(format, formatProvider)} [{{Data.DefaultUnitInstanceSymbol}}]";""");

                return;
            }

            if (Data.DefaultUnitInstanceName is not null)
            {
                Builder.AppendLine($"InUnit({Data.Unit.FullyQualifiedName}.{Data.DefaultUnitInstanceName}).ToString(format, formatProvider);");

                return;
            }

            Builder.AppendLine($"Components.ToString(format, formatProvider);");
        }

        private void AppendDeconstruct(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.Deconstruct());
            Builder.AppendLine($"{indentation}public void Deconstruct({Texts.DeconstructHeader})");
            BlockBuilding.AppendBlock(Builder, composeBlock, indentation);

            void composeBlock(Indentation indentation)
            {
                for (var i = 0; i < Data.Dimension; i++)
                {
                    Builder.AppendLine($"{indentation}{Texts.Lower.ComponentName(i)} = {Texts.Upper.ComponentName(i)};");
                }
            }
        }

        private void AppendEquality(Indentation indentation)
        {
            if (Data.Vector.IsReferenceType)
            {
                AppendReferenceTypeEquality(indentation);

                return;
            }

            AppendValueTypeEquality(indentation);
        }

        private void AppendReferenceTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var virtualText = Data.Vector.IsSealed ? " virtual" : string.Empty;

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"""
                {indentation}public{virtualText} bool Equals({Data.Vector.FullyQualifiedName}? other)
                {indentation.Increased}=> other is not null && {ComposeEqualityChecks()};
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Vector.Name);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Vector.FullyQualifiedName}? lhs, {Data.Vector.FullyQualifiedName}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Vector.FullyQualifiedName}? lhs, {Data.Vector.FullyQualifiedName}? rhs) => (lhs == rhs) is false;");
        }

        private void AppendValueTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"{indentation}public bool Equals({Data.Vector.FullyQualifiedName} other)");
            Builder.AppendLine($"{indentation.Increased}=> {ComposeEqualityChecks()};");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Vector.Name);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Vector.FullyQualifiedName} lhs, {Data.Vector.FullyQualifiedName} rhs) => lhs.Equals(rhs);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Vector.FullyQualifiedName} lhs, {Data.Vector.FullyQualifiedName} rhs) => (lhs == rhs) is false;");
        }

        private string ComposeEqualityChecks()
        {
            StringBuilder equalityChecksText = new();
            IterativeBuilding.AppendEnumerable(equalityChecksText, equalityChecks(), " && ");
            return equalityChecksText.ToString();

            IEnumerable<string> equalityChecks()
            {
                if (Data.Scalar is null)
                {
                    for (var i = 0; i < Data.Dimension; i++)
                    {
                        yield return $"{Texts.Upper.ComponentName(i)}.Value == other.{Texts.Upper.ComponentName(i)}.Value";
                    }

                    yield break;
                }

                for (var i = 0; i < Data.Dimension; i++)
                {
                    yield return $"{Texts.Upper.ComponentName(i)}.Magnitude.Value == other.{Texts.Upper.ComponentName(i)}.Magnitude.Value";
                }
            }
        }

        private void AppendGetHashCode(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.GetHashCodeDocumentation());
            Builder.AppendLine($"{indentation}public override int GetHashCode() => ({ConstantVectorTexts.Upper.Name(Data.Dimension)}).GetHashCode();");
        }

        private void AppendCastFromComponents(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.CastFromComponents());

            if (Data.Scalar!.Value.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            }

            Builder.Append($$"""
                {{indentation}}[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achived through a constructor")]
                {{indentation}}public static implicit operator {{Data.Vector.FullyQualifiedName}}(({{Texts.Upper.Component}}) components)
                """);

            if (Data.Scalar!.Value.IsReferenceType)
            {
                Builder.AppendLine();

                Builder.AppendLine($$"""{{indentation}}{""");

                for (var i = 0; i < Data.Dimension; i++)
                {
                    Builder.AppendLine($"{indentation.Increased}{StaticBuilding.NullArgumentGuard($"components.{Texts.Upper.ComponentName(i)}")}");
                }

                SeparationHandler.Add();

                Builder.AppendLine($"{indentation.Increased}return new({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)});");
                Builder.AppendLine($$"""{{indentation}}}""");

                return;
            }

            Builder.AppendLine($" => new({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)});");
        }

        private void AppendWithComponentsMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.WithScalarComponents());
            Builder.AppendLine($"{indentation}static {Data.Vector.FullyQualifiedName} global::SharpMeasures.IVector{Data.Dimension}Quantity<{Data.Vector.FullyQualifiedName}>.WithComponents({ConstantVectorTexts.Lower.Scalar(Data.Dimension)}) => new({ConstantVectorTexts.Lower.Name(Data.Dimension)});");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.SourceBuildingContext.Documentation.WithVectorComponents());
            Builder.AppendLine($"{indentation}static {Data.Vector.FullyQualifiedName} global::SharpMeasures.IVector{Data.Dimension}Quantity<{Data.Vector.FullyQualifiedName}>.WithComponents(global::SharpMeasures.Vector{Data.Dimension} components) => new(components);");
        }

        private void AppendComputationUtilityMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            Builder.AppendLine($$"""
                {{indentation}}/// <summary>Computes the magnitudes of the represented components based on magnitudes expressed in a certain <paramref name="{{Data.UnitParameterName}}"/>.</summary>
                """);

            for (var i = 0; i < Data.Dimension; i++)
            {
                Builder.AppendLine($"""{indentation}/// <param name="{Texts.Lower.ComponentName(i)}">The magnitude of the {Texts.Upper.ComponentName(i)}-component, expressed in a certain unit <paramref name="{Data.UnitParameterName}"/>.</param>""");
            }

            Builder.AppendLine($$"""
                {{indentation}}/// <param name="{{Data.UnitParameterName}}">The {{Data.Unit.FullyQualifiedName}} in which the magnitudes of the components are expressed.</param>
                """);

            var methodNameAndModifiers = $"private static global::SharpMeasures.Vector{Data.Dimension} ComputeRepresentedComponents";
            var expression = $"({Texts.Lower.ScalarMultiplyUnit})";
            List<(NamedType, string)> parameters = new();

            NamedType scalarType = new("Scalar", "SharpMeasures", "SharpMeasures.Base", true);

            for (var i = 0; i < Data.Dimension; i++)
            {
                parameters.Add((scalarType, VectorTextBuilder.GetLowerCasedComponentName(i, Data.Dimension)));
            }

            parameters.Add((Data.Unit, Data.UnitParameterName));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendMathUtilityMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in a pure <see cref=\"global::SharpMeasures.Scalar\"/>.</summary>");
            Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> PureScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult();");

            if (Data.Scalar is not null)
            {
                SeparationHandler.Add();

                Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Scalar.Value.FullyQualifiedName}\"/>.</summary>");
                Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IScalarResultingMaths<{Data.Scalar.Value.FullyQualifiedName}> ScalarMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.ScalarResult<{Data.Scalar.Value.FullyQualifiedName}>();");
            }

            SeparationHandler.Add();

            Builder.AppendLine($"{indentation}/// <summary>Describes mathematical operations that result in <see cref=\"{Data.Vector.FullyQualifiedName}\"/>.</summary>");
            Builder.AppendLine($"{indentation}private static global::SharpMeasures.Maths.IVector{Data.Dimension}ResultingMaths<{Data.Vector.FullyQualifiedName}> VectorMaths {{ get; }} = global::SharpMeasures.Maths.MathFactory.Vector{Data.Dimension}Result<{Data.Vector.FullyQualifiedName}>();");
        }

        private void AppendDocumentation(Indentation indentation, string text) => DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
    }

    private sealed class SpecificTexts
    {
        public UpperTexts Upper { get; }
        public LowerTexts Lower { get; }

        public string DeconstructHeader => DeconstructHeaderBuilder.GetText(Dimension);

        private int Dimension { get; }

        private VectorTextBuilder DeconstructHeaderBuilder { get; }

        public SpecificTexts(int dimension, NamedType? scalar, NamedType unitQuantity, string unitParameterName)
        {
            Dimension = dimension;

            Upper = new(dimension, scalar);
            Lower = new(dimension, scalar, unitQuantity, unitParameterName);

            DeconstructHeaderBuilder = scalar is null ? ConstantVectorTexts.Builders.DeconstructScalarHeader : CommonTextBuilders.DeconstructComponents(scalar.Value.Name);
        }

        public sealed class UpperTexts
        {
            public string ComponentName(int index) => VectorTextBuilder.GetUpperCasedComponentName(index, Dimension);

            public string Component => ComponentBuilder.GetText(Dimension);

            public int Dimension { get; }

            private VectorTextBuilder ComponentBuilder { get; }

            public UpperTexts(int dimension, NamedType? scalar)
            {
                Dimension = dimension;

                ComponentBuilder = scalar is null ? ConstantVectorTexts.Builders.Upper.Scalar : CommonTextBuilders.Upper.Component(scalar.Value.Name);
            }
        }

        public sealed class LowerTexts
        {
            public string ComponentName(int index) => VectorTextBuilder.GetLowerCasedComponentName(index, Dimension);

            public string Component => ComponentBuilder.GetText(Dimension);
            public string NewComponent => NewComponentBuilder.GetText(Dimension);
            public string ScalarMultiplyUnit => ScalarMultiplyUnitBuilder.GetText(Dimension);

            private int Dimension { get; }

            private VectorTextBuilder ComponentBuilder { get; }
            private VectorTextBuilder NewComponentBuilder { get; }
            private VectorTextBuilder ScalarMultiplyUnitBuilder { get; }

            public LowerTexts(int dimension, NamedType? scalar, NamedType unitQuantity, string unitParameterName)
            {
                Dimension = dimension;

                ComponentBuilder = scalar is null ? ConstantVectorTexts.Builders.Lower.Scalar : CommonTextBuilders.Lower.Component(scalar.Value.Name);
                NewComponentBuilder = scalar is null ? ConstantVectorTexts.Builders.Lower.NewScalar : CommonTextBuilders.Lower.NewComponent(scalar.Value.Name);

                ScalarMultiplyUnitBuilder = CommonTextBuilders.Lower.ScalarMultiplyUnit(unitParameterName, unitQuantity.Name);
            }
        }
    }
}
