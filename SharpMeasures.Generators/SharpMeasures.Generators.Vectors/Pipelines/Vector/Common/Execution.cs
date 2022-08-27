namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Common;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Vector.Name}_Common.g.cs", SourceText.From(source, Encoding.UTF8));
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
        private NewlineSeparationHandler SeparationHandler { get; }

        private DataModel Data { get; }

        private string UnitParameterName { get; }
        private SpecificTexts Texts { get; }

        private Composer(DataModel data)
        {
            Data = data;

            SeparationHandler = new(Builder);

            UnitParameterName = SourceBuildingUtility.ToParameterName(Data.Unit.Name);
            Texts = new(data.Dimension, data.Scalar, data.UnitQuantity, UnitParameterName);
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            AppendDocumentation(new Indentation(0), Data.Documentation.Header());
            Builder.Append(Data.Vector.ComposeDeclaration());

            InterfaceBuilding.AppendInterfaceImplementationOnNewLines(Builder, new Indentation(1), new string[]
            {
                $"global::SharpMeasures.IVector{Data.Dimension}Quantity<{Data.Vector.FullyQualifiedName}>"
            });

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0, initialNewLine: true);
        }

        private string Retrieve()
        {
            return Builder.ToString();
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
        }

        private void AppendZero(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Zero());
            Builder.AppendLine($"{indentation}public static {Data.Vector.FullyQualifiedName} Zero {{ get; }} = new({ConstantVectorTexts.Zeros(Data.Dimension)});");
        }

        private void AppendComponents(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                AppendComponentsAsScalars(indentation);
            }
            else
            {
                AppendComponentsAsTypes(indentation, Data.Scalar.Value);
            }
        }

        private void AppendComponentsAsScalars(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            for (int i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.Documentation.ComponentMagnitude(i));
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar {Texts.Upper.ComponentName(i)} {{ get; }}");
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.Components());
            Builder.AppendLine($"{indentation}public global::SharpMeasures.Vector{Data.Dimension} Components => ({ConstantVectorTexts.Upper.Name(Data.Dimension)});");
        }

        private void AppendComponentsAsTypes(Indentation indentation, NamedType scalar)
        {
            SeparationHandler.AddIfNecessary();

            for (int i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.Documentation.Component(i));
                Builder.AppendLine($"{indentation}public {scalar.FullyQualifiedName} {Texts.Upper.ComponentName(i)} {{ get; }}");
            }

            SeparationHandler.Add();

            for (int i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.Documentation.ComponentMagnitude(i));
                Builder.AppendLine($"{indentation}global::SharpMeasures.Scalar global::SharpMeasures.IVector{Data.Dimension}Quantity.{Texts.Upper.ComponentName(i)} => {Texts.Upper.ComponentName(i)}.Magnitude;");
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.Components());
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

            AppendDocumentation(indentation, Data.Documentation.ScalarsConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)})");
            BlockBuilding.AppendBlock(Builder, AppendConstructorBlock, indentation);
        }

        private void AppendConstructorsToComponents(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.ComponentsConstructor());

            if (Data.Scalar!.Value.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}{{DocumentationBuilding.ArgumentNullExceptionTag}}
                    {{indentation}}public {{Data.Vector.Name}}({{Texts.Lower.Component}})
                    {{indentation}}{
                    """);

                for (int i = 0; i < Data.Dimension; i++)
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

            AppendDocumentation(indentation, Data.Documentation.ScalarsConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)})");
            Builder.AppendLine($"{indentation.Increased}: this({Texts.Lower.NewComponent}) {{ }}");
        }

        private void AppendConstructorBlock(Indentation indentation)
        {
            for (int i = 0; i < Data.Dimension; i++)
            {
                Builder.AppendLine($"{indentation}{Texts.Upper.ComponentName(i)} = {Texts.Lower.ComponentName(i)};");
            }
        }

        private void AppendCommonConstructors(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.VectorConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}(global::SharpMeasures.Vector{Data.Dimension} components)");
            Builder.AppendLine($"{indentation.Increased}: this({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)}) {{ }}");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.ScalarsAndUnitConstructor());

            if (Data.Unit.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            }

            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)}, {Data.Unit.FullyQualifiedName} {UnitParameterName})");

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($"{indentation.Increased}: this(ComputeRepresentedComponents({ConstantVectorTexts.Lower.Name(Data.Dimension)}, {UnitParameterName})) {{ }}");
            }
            else
            {
                Builder.AppendLine($"{indentation.Increased}: this({Texts.Lower.ScalarMultiplyUnit}) {{ }}");
            }

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.VectorAndUnitConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}(global::SharpMeasures.Vector{Data.Dimension} components, {Data.Unit.FullyQualifiedName} {UnitParameterName})");
            Builder.AppendLine($"{indentation.Increased}: this({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)}, {UnitParameterName}) {{ }}");
        }

        private void AppendMagnitude(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            if (Data.Scalar is null)
            {
                AppendDocumentation(indentation, Data.Documentation.ScalarMagnitude());
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar Magnitude() => ScalarMaths.Magnitude{Data.Dimension}(this);");
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.Magnitude());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Value.FullyQualifiedName} Magnitude() => ScalarMaths.Magnitude{Data.Dimension}(this);");

                SeparationHandler.Add();

                AppendDocumentation(indentation, Data.Documentation.ScalarMagnitude());
                Builder.AppendLine($"{indentation}global::SharpMeasures.Scalar global::SharpMeasures.IVector{Data.Dimension}.Magnitude() => PureScalarMaths.Magnitude{Data.Dimension}(this);");
            }

            SeparationHandler.Add();

            if (Data.SquaredScalar is null)
            {
                AppendDocumentation(indentation, Data.Documentation.ScalarSquaredMagnitude());
                Builder.AppendLine($"{indentation}public global::SharpMeasures.Scalar SquaredMagnitude() => ScalarMaths.SquaredMagnitude{Data.Dimension}(this);");
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SquaredMagnitude());
                Builder.AppendLine($"{indentation}public {Data.SquaredScalar.Value.FullyQualifiedName} SquaredMagnitude() => SquaredScalarMaths.SquaredMagnitude{Data.Dimension}(this);");

                SeparationHandler.Add();

                AppendDocumentation(indentation, Data.Documentation.ScalarSquaredMagnitude());
                Builder.AppendLine($"{indentation}Scalar global::SharpMeasures.IVector{Data.Dimension}.SquaredMagnitude() => PureScalarMaths.SquaredMagnitude{Data.Dimension}(this);");
            }
        }

        private void AppendInUnit(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            var methodNameAndModifiers = $"public global::SharpMeasures.Vector{Data.Dimension} InUnit";
            var expression = $"new({ComposeInUnitComputation()})";
            var parameters = new[] { (Data.Unit, Data.UnitParameterName) };

            AppendDocumentation(indentation, Data.Documentation.InUnit());
            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private string ComposeInUnitComputation()
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, Data.Scalar is null ? scalarComponent() : typeComponent(), ", ");

            return source.ToString();

            IEnumerable<string> scalarComponent()
            {
                for (int i = 0; i < Data.Dimension; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Value / " +
                        $"{UnitParameterName}.{Data.UnitQuantity.Name}.Magnitude.Value";
                }
            }

            IEnumerable<string> typeComponent()
            {
                for (int i = 0; i < Data.Dimension; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Magnitude.Value / " +
                        $"{UnitParameterName}.{Data.UnitQuantity.Name}.Magnitude.Value";
                }
            }
        }

        private void AppendNormalize(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Normalize());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Normalize() => VectorMaths.Normalize(this);");
        }

        private void AppendTransform(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Transform());
            Builder.AppendLine($"{indentation}public {Data.Vector.FullyQualifiedName} Transform(global::System.Numerics.Matrix4x4 transform) => VectorMaths.Transform(this, transform);");
        }

        private void AppendToString(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.ToStringDocumentation());
            
            if (Data.DefaultUnitName is null)
            {
                Builder.Append($$"""{{indentation}}public override string ToString() => $"({{ConstantVectorTexts.Upper.ComponentsPropertyAccess(Data.Dimension)}})""");
            }
            else
            {
                Builder.Append($$"""
                    {{indentation}}public override string ToString()
                    {{indentation}}{
                    {{indentation.Increased}}var components = InUnit({{Data.Unit.FullyQualifiedName}}.{{Data.DefaultUnitName}});

                    {{indentation.Increased}}return $"({{ConstantVectorTexts.Upper.ComponentsPropertyAccess(Data.Dimension)}})
                    """);

                SeparationHandler.Add();
            }

            if (Data.DefaultUnitSymbol is not null)
            {
                Builder.Append($""" [{Data.DefaultUnitSymbol}]""");
            }

            Builder.AppendLine("""
                ";
                """);

            if (Data.DefaultUnitName is not null)
            {
                BlockBuilding.AppendClosingBracket(Builder, indentation, finalNewLine: true);
            }
        }

        private void AppendDeconstruct(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.Deconstruct());
            Builder.AppendLine($"{indentation}public void Deconstruct({Texts.DeconstructHeader})");
            BlockBuilding.AppendBlock(Builder, composeBlock, indentation);

            void composeBlock(Indentation indentation)
            {
                for (int i = 0; i < Data.Dimension; i++)
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
            }
            else
            {
                AppendValueTypeEquality(indentation);
            }
        }

        private void AppendReferenceTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            string virtualText = Data.Vector.IsSealed ? " virtual" : string.Empty;

            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"""
                {indentation}public{virtualText} bool Equals({Data.Vector.FullyQualifiedName}? other)
                {indentation.Increased}=> other is not null && {ComposeEqualityChecks()};
                """);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Vector.Name);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Vector.FullyQualifiedName}? lhs, {Data.Vector.FullyQualifiedName}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Vector.FullyQualifiedName}? lhs, {Data.Vector.FullyQualifiedName}? rhs) => (lhs == rhs) is false;");
        }

        private void AppendValueTypeEquality(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"{indentation}public bool Equals({Data.Vector.FullyQualifiedName} other)");
            Builder.AppendLine($"{indentation.Increased}=> {ComposeEqualityChecks()};");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Vector.Name);

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Vector.FullyQualifiedName} lhs, {Data.Vector.FullyQualifiedName} rhs) => lhs.Equals(rhs);");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
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
                    for (int i = 0; i < Data.Dimension; i++)
                    {
                        yield return $"{Texts.Upper.ComponentName(i)}.Value == other.{Texts.Upper.ComponentName(i)}.Value";
                    }
                }
                else
                {
                    for (int i = 0; i < Data.Dimension; i++)
                    {
                        yield return $"{Texts.Upper.ComponentName(i)}.Magnitude.Value == other.{Texts.Upper.ComponentName(i)}.Magnitude.Value";
                    }
                }
            }
        }

        private void AppendGetHashCode(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.GetHashCodeDocumentation());
            Builder.AppendLine($"{indentation}public override int GetHashCode() => ({ConstantVectorTexts.Upper.Name(Data.Dimension)}).GetHashCode();");
        }

        private void AppendCastFromComponents(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.CastFromComponents());

            if (Data.Scalar!.Value.IsReferenceType)
            {
                DocumentationBuilding.AppendArgumentNullExceptionTag(Builder, indentation);
            }

            Builder.Append($$"""
                [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achived through a constructor")]
                public static implicit operator {{Data.Vector.FullyQualifiedName}}(({{Texts.Upper.Component}}) components)
                """);

            if (Data.Scalar!.Value.IsReferenceType)
            {
                Builder.AppendLine($$"""{{indentation}}{""");

                for (int i = 0; i < Data.Dimension; i++)
                {
                    Builder.AppendLine($"{indentation.Increased}{StaticBuilding.NullArgumentGuard($"components.{Texts.Upper.ComponentName(i)}")}");
                }

                SeparationHandler.Add();

                Builder.AppendLine($"{indentation.Increased}return new({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)});");
                Builder.AppendLine($$"""{{indentation}}}""");
            }
            else
            {
                Builder.AppendLine($" => new({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)});");
            }
        }

        private void AppendWithComponentsMethods(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            AppendDocumentation(indentation, Data.Documentation.WithScalarComponents());
            Builder.AppendLine($"{indentation}static {Data.Vector.FullyQualifiedName} global::SharpMeasures.IVector{Data.Dimension}Quantity<{Data.Vector.FullyQualifiedName}>.WithComponents({ConstantVectorTexts.Lower.Scalar(Data.Dimension)}) => new({ConstantVectorTexts.Lower.Name(Data.Dimension)});");

            SeparationHandler.Add();

            AppendDocumentation(indentation, Data.Documentation.WithVectorComponents());
            Builder.AppendLine($"{indentation}static {Data.Vector.FullyQualifiedName} global::SharpMeasures.IVector{Data.Dimension}Quantity<{Data.Vector.FullyQualifiedName}>.WithComponents(global::SharpMeasures.Vector{Data.Dimension} components) => new(components);");
        }

        private void AppendComputationUtilityMethod(Indentation indentation)
        {
            SeparationHandler.AddIfNecessary();

            Builder.AppendLine($$"""
                {{indentation}}/// <summary>Computes the magnitudes of the represented components based on magnitudes expressed in a certain <paramref name="{{Data.UnitParameterName}}"/>.</summary>
                """);

            for (int i = 0; i < Data.Dimension; i++)
            {
                Builder.AppendLine($"""{indentation}/// <param name="{Texts.Lower.ComponentName(i)}">The magnitude of the {Texts.Upper.ComponentName(i)}-component, expressed in a certain unit <paramref name="{Data.UnitParameterName}"/>.</param>""");
            }

            Builder.AppendLine($$"""
                {{indentation}}/// <param name="{{Data.UnitParameterName}}">The {{Data.Unit.FullyQualifiedName}} in which the magnitudes of the components are expressed.</param>
                """);

            var methodNameAndModifiers = $"private static global::SharpMeasures.Vector{Data.Dimension} ComputeRepresentedComponents";
            var expression = $"({Texts.Lower.ScalarMultiplyUnit})";
            List<(NamedType, string)> parameters = new();

            NamedType scalarType = new("Scalar", "SharpMeasures", true);

            for (int i = 0; i < Data.Dimension; i++)
            {
                parameters.Add((scalarType, VectorTextBuilder.GetLowerCasedComponentName(i, Data.Dimension)));
            }

            parameters.Add((Data.Unit, UnitParameterName));

            StaticBuilding.AppendSingleLineMethodWithPotentialNullArgumentGuards(Builder, indentation, methodNameAndModifiers, expression, parameters);
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }

    private class SpecificTexts
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

            DeconstructHeaderBuilder = scalar is null
                ? ConstantVectorTexts.Builders.DeconstructScalarHeader
                : CommonTextBuilders.DeconstructComponents(scalar.Value.Name);
        }

        public class UpperTexts
        {
            public string ComponentName(int index) => VectorTextBuilder.GetUpperCasedComponentName(index, Dimension);

            public string Component => ComponentBuilder.GetText(Dimension);

            public int Dimension { get; }

            private VectorTextBuilder ComponentBuilder { get; }

            public UpperTexts(int dimension, NamedType? scalar)
            {
                Dimension = dimension;

                ComponentBuilder = scalar is null
                    ? ConstantVectorTexts.Builders.Upper.Scalar
                    : CommonTextBuilders.Upper.Component(scalar.Value.Name);
            }
        }

        public class LowerTexts
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

                ComponentBuilder = scalar is null
                    ? ConstantVectorTexts.Builders.Lower.Scalar
                    : CommonTextBuilders.Lower.Component(scalar.Value.Name);

                NewComponentBuilder = scalar is null
                    ? ConstantVectorTexts.Builders.Lower.NewScalar
                    : CommonTextBuilders.Lower.NewComponent(scalar.Value.Name);

                ScalarMultiplyUnitBuilder = CommonTextBuilders.Lower.ScalarMultiplyUnit(unitParameterName, unitQuantity.Name);
            }
        }
    }
}
