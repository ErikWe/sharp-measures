namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.UnitInstances;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

internal class DefaultVectorDocumentation : IVectorDocumentationStrategy, IEquatable<DefaultVectorDocumentation>
{
    private DefinedType Type { get; }
    private int Dimension { get; }
    private IUnitType Unit { get; }

    private IScalarType? Scalar { get; }

    private IUnitInstance? DefaultUnit { get; }
    private string? DefaultUnitSymbol { get; }

    private string UnitParameterName { get; }

    private IUnitInstance? ExampleScalarBase { get; }
    private IUnitInstance? ExampleUnit { get; }

    private bool HasComponent => Scalar is not null;

    private SpecificTexts Texts { get; }

    public DefaultVectorDocumentation(VectorDataModel model)
    {
        Type = model.Vector.Type;
        Dimension = model.Vector.Dimension;
        Unit = model.UnitPopulation.Units[model.Vector.Unit];

        DefaultUnit = model.Vector.DefaultUnitName is not null ? Unit.UnitsByName[model.Vector.DefaultUnitName] : null;
        DefaultUnitSymbol = model.Vector.DefaultUnitSymbol;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.Type.Name);

        if (model.Vector.Scalar is not null)
        {
            var scalar = model.ScalarPopulation.Scalars[model.Vector.Scalar.Value];
            var scalarUnit = model.UnitPopulation.Units[model.ScalarPopulation.Scalars[model.Vector.Scalar.Value].Unit];

            ExampleScalarBase = scalar.IncludedBases.Count > 0 ? scalarUnit.UnitsByName[scalar.IncludedBases[0]] : null;
        }

        ExampleUnit = model.Vector.IncludedUnits.Count > 0 ? Unit.UnitsByName[model.Vector.IncludedUnits[0]] : null;

        Texts = new(Dimension, VectorReference, UnitParameterName);
    }

    public string Header() => HasComponent switch
    {
        true => ComponentedHeader(),
        false => UncomponentedHeader()
    };

    private string ComponentedHeader() => $"""
        /// <summary>A measure of the {Dimension}-dimensional vector quantity {Type.Name}, composed of {ScalarReference},
        /// and expressed in {UnitReference}.</summary>
        """;

    private string UncomponentedHeader() => $"""
        /// <summary>A measure of the {Dimension}-dimensional vector quantity {Type.Name}, expressed in {UnitReference}.</summary>
        """;

    public string Zero() => $$"""/// <summary>The {{VectorReference}} representing { {{ConstantVectorTexts.Zeros(Dimension)}} }.</summary>""";
    public string Constant(IVectorConstant constant)
    {
        StringBuilder componentText = new();
        IterativeBuilding.AppendEnumerable(componentText, components(), ", ");

        return $$"""/// <summary>The {{ScalarReference}} representing the constant {{constant.Name}}, with value { ({{componentText}}) [{{Unit.UnitsByName[constant.Unit].Plural}}] }.</summary>""";

        IEnumerable<string> components()
        {
            foreach (double value in constant.Value)
            {
                yield return value switch
                {
                    > 10000 or < 0.0001 and > -0.0001 or < -10000 => value.ToString("0.000E0", CultureInfo.InvariantCulture),
                    _ => value.ToString("0.####", CultureInfo.InvariantCulture)
                };
            }
        }
    }

    public string WithScalarComponents() => InheritDoc;
    public string WithVectorComponents() => InheritDoc;

    public string ComponentsConstructor() => $$"""
        /// <summary>Constructs a new {{VectorReference}} representing { {{Texts.ParameterTuple()}} }.</summary>
        {{Texts.ComponentsConstructor()}}
        """;

    public string ScalarsConstructor() => $$"""
        /// <summary>Constructs a new {{VectorReference}} representing { {{Texts.ParameterTuple()}} }, expressed in an arbitrary unit.</summary>
        {{Texts.ScalarsConstructor()}}
        /// <remarks>Consider preferring construction through <see cref="{{Type.FullyQualifiedName}}({{ConstantVectorTexts.UnnamedScalars(Dimension)}}, {{Unit.Type.FullyQualifiedName}})"/>,
        /// where the components are expressed in a certain {{UnitReference}}.</remarks>
        """;

    public string VectorConstructor() => $"""
        /// <summary>Constructs a new {VectorReference} components of magnitudes <paramref name="components"/>, expressed in an arbitrary unit.</summary>
        /// <param name="components">The magnitudes of the components of the constructed {VectorReference}, expressed in an arbitrary unit.</param>
        /// <remarks>Consider preferring construction through <see cref="{Type.FullyQualifiedName}(global::SharpMeasures.Vector{Dimension}, {Unit.Type.FullyQualifiedName})"/>,
        /// where the components are expressed in a certain {UnitReference}.</remarks>
        """;

    public string ScalarsAndUnitConstructor()
    {
        var commonText = $$"""
            /// <summary>Constructs a new {{VectorReference}} representing { {{Texts.ParameterTuple()}} }, expressed in <paramref name="{{UnitParameterName}}"/>.</summary>
            {{Texts.ScalarsAndUnitConstructor()}}
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which the magnitudes of the components are expressed.</param>
            """;

        if (Scalar is not null && ExampleScalarBase is not null)
        {
            commonText = $"""
                {commonText}
                /// <remarks>A {VectorReference} may also be constructed as demonstrated below.
                /// <code>{VectorReference} x = ({ConstantVectorTexts.SampleValues(Dimension)}) * <see cref="{Scalar.Type.FullyQualifiedName}.One{ExampleScalarBase.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }
    public string VectorAndUnitConstructor()
    {
        var commonText = $"""
            /// <summary>Constructs a new {VectorReference} components of magnitudes <paramref name="components"/>, expressed in <paramref name="{UnitParameterName}"/>.</summary>
            /// <param name="components">The magnitudes of the components of the constructed {VectorReference}, expressed in <paramref name="{UnitParameterName}"/>.</param>
            """;

        if (Scalar is not null && ExampleScalarBase is not null)
        {
            commonText = $"""
                {commonText}
                /// <remarks>A {VectorReference} may also be constructed as demonstrated below.
                /// <code>{VectorReference} x = ({ConstantVectorTexts.SampleValues(Dimension)}) * <see cref="{Scalar.Type.FullyQualifiedName}.One{ExampleScalarBase.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }

    public string CastFromComponents() => $"""
        /// <summary>Constructs the {VectorReference} with the elements of <paramref name="components"/> as components.</summary>
        """;

    public string Component(int componentIndex) => $"""
        /// <summary>The {VectorTextBuilder.GetUpperCasedComponentName(componentIndex, Dimension)}-component of <see langword="this"/>.</summary>
        """;

    public string ComponentMagnitude(int componentIndex) => $"""
        /// <summary>The magnitude of the {VectorTextBuilder.GetUpperCasedComponentName(componentIndex, Dimension)}-component of <see langword="this"/>.</summary>
        """;

    public string Components()
    {
        var commonText = $"""
            /// <summary>The magnitudes of the components of <see langword="this"/>, expressed in an arbitrary unit.</summary>
            /// <remarks>In most cases, expressing the magnitudes in a certain {UnitReference} should be preferred. This is achieved through
            /// <see cref="InUnit({Unit.Type.FullyQualifiedName})"/>
            """;

        if (ExampleUnit is not null)
        {
            return $"""{commonText} or an associated property - such as <see cref="In{ExampleUnit.Plural}"/>""";
        }

        return $"{commonText}.</remarks>";
    }
    
    public string InUnit() => $"""
        /// <summary>The components of <see langword="this"/>, expressed in <paramref name="{UnitParameterName}"/>.</summary>
        /// <param name="{UnitParameterName}">The {UnitReference} in which the components of <see langword="this"/> are expressed.</param>
        """;
    public string InConstantMultiples(IVectorConstant constant) => $"""
        /// <summary>The components of <see langword="this", expressed in multiples of <see cref="{VectorReference}.{constant.Name}"/>.</summary>
        """;
    public string InSpecifiedUnit(IUnitInstance unitInstance) => $"""
        /// <summary>The components of <see langword="this"/>, expressed in <see cref="{UnitReference}.{unitInstance.Name}"/>.</summary>
        """;

    public string Conversion(NamedType vectorGroupMember) => $"""
        /// <summary>Converts <see langword="this"/> to the equivalent <see cref="{vectorGroupMember.Name}"/>.</summary>
        """;
    public string CastConversion(NamedType vectorGroupMember) => $"""
        /// <summary>Converts <paramref name="a"/> to the equivalent <see cref="{vectorGroupMember.Name}"/>.</summary>
        /// <param name="a">This {VectorReference} is converted to the equivalent <see cref="{vectorGroupMember.Name}"/>.</param>
        """;

    public string IsNaN() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsNaN"/>""";
    public string IsZero() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsZero"/>""";
    public string IsFinite() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsFinite"/>""";
    public string IsInfinite() => $"""/// <inheritdoc cref="global::SharpMeasures.Vector{Dimension}.IsInfinite"/>""";

    public string Magnitude() => InheritDoc;
    public string SquaredMagnitude() => InheritDoc;

    public string ScalarMagnitude() => InheritDoc;
    public string ScalarSquaredMagnitude() => InheritDoc;

    public string Normalize() => InheritDoc;
    public string Transform() => InheritDoc;

    public string ToStringDocumentation()
    {
        var commonText = $"""/// <summary>Produces a description of <see langword="this"/> containing the type""";

        if (DefaultUnit is not null && DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the components expressed in <see cref="{Unit.Type.FullyQualifiedName}.{DefaultUnit}"/>, and the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        if (DefaultUnit is not null)
        {
            return $"""{commonText} and the components expressed in <see cref="{Unit.Type.FullyQualifiedName}.{DefaultUnit}"/>.</summary>""";
        }

        if (DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the components expressed in an arbitrary unit, follow by the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        return $"""{commonText} and the components expressed in an arbitrary unit.</summary>""";
    }

    public string EqualsSameTypeMethod() => InheritDoc;
    public string EqualsObjectMethod() => InheritDoc;

    public string EqualitySameTypeOperator() => $"""
            /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent components.</summary>
            /// <param name="lhs">The left-hand side of the equality check.</param>
            /// <param name="rhs">The right-hand side of the equality check.</param>
            """;

    public string InequalitySameTypeOperator() => $"""
            /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent components.</summary>
            /// <param name="lhs">The left-hand side of the inequality check.</param>
            /// <param name="rhs">The right-hand side of the inequality check.</param>
            """;

    public string GetHashCodeDocumentation() => InheritDoc;

    public string Deconstruct() => $"""
        /// <summary>Deconstructs <see langword="this"/> into the individual components.</summary>
        {Texts.Deconstruct()}
        """;

    public string UnaryPlusMethod() => InheritDoc;
    public string NegateMethod() => InheritDoc;

    public string AddSameTypeMethod() => InheritDoc;
    public string SubtractSameTypeMethod() => InheritDoc;

    public string AddDifferenceMethod() => InheritDoc;
    public string SubtractDifferenceMethod() => InheritDoc;

    public string MultiplyScalarMethod() => InheritDoc;
    public string DivideScalarMethod() => InheritDoc;

    public string UnaryPlusOperator() => InheritDoc;
    public string NegateOperator() => InheritDoc;

    public string AddSameTypeOperator() => InheritDoc;
    public string SubtractSameTypeOperator() => InheritDoc;

    public string AddDifferenceOperatorLHS() => InheritDoc;
    public string AddDifferenceOperatorRHS() => InheritDoc;
    public string SubtractDifferenceOperatorLHS() => InheritDoc;

    public string MultiplyScalarOperatorLHS() => InheritDoc;
    public string MultiplyScalarOperatorRHS() => InheritDoc;
    public string DivideScalarOperatorLHS() => InheritDoc;

    private string VectorReference => $"""<see cref="{Type.FullyQualifiedName}"/>""";
    private string UnitReference => $"""<see cref="{Unit.Type.FullyQualifiedName}"/>""";
    private string ScalarReference => $"""<see cref={Scalar?.Type.FullyQualifiedName}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    private class SpecificTexts
    {
        public string ParameterTuple() => ParameterTupleBuilder.GetText(Dimension);
        public string ComponentsConstructor() => ComponentsConstructorBuilder.GetText(Dimension);
        public string ScalarsConstructor() => ScalarsConstructorBuilder.GetText(Dimension);
        public string ScalarsAndUnitConstructor() => ScalarsAndUnitConstructorBuilder.GetText(Dimension);
        public string Deconstruct() => DeconstructBuilder.GetText(Dimension);

        private int Dimension { get; }

        private VectorTextBuilder ParameterTupleBuilder { get; }
        private VectorTextBuilder ComponentsConstructorBuilder { get; }
        private VectorTextBuilder ScalarsConstructorBuilder { get; }
        private VectorTextBuilder ScalarsAndUnitConstructorBuilder { get; }
        private static VectorTextBuilder DeconstructBuilder { get; } = GetDeconstructBuilder();

        public SpecificTexts(int dimension, string vectorReference, string unitParameterName)
        {
            Dimension = dimension;

            ParameterTupleBuilder = GetParameterTupleBuilder();
            ComponentsConstructorBuilder = GetComponentsConstructorBuilder(vectorReference);
            ScalarsConstructorBuilder = GetScalarsConstructorBuilder(vectorReference);
            ScalarsAndUnitConstructorBuilder = GetScalarsAndUnitConstructorBuilder(vectorReference, unitParameterName);
        }

        private static VectorTextBuilder GetParameterTupleBuilder()
        {
            return new VectorTextBuilder(parameterTupleComponent, ", ");

            static string parameterTupleComponent(int componentIndex, int dimension)
            {
                string componentName = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);

                return $"""<paramref name="{componentName}"/>""";
            }
        }

        private static VectorTextBuilder GetComponentsConstructorBuilder(string vectorReference)
        {
            return new VectorTextBuilder(componentsConstructorComponent, Environment.NewLine);

            string componentsConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The {componentUpperCase}-component of the constructed {vectorReference}.</param>""";
            }
        }

        private static VectorTextBuilder GetScalarsConstructorBuilder(string vectorReference)
        {
            return new VectorTextBuilder(scalarsConstructorComponent, Environment.NewLine);

            string scalarsConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The magnitude of the {componentUpperCase}-component of the constructed {vectorReference}, expressed in an arbitrary unit.</param>""";
            }
        }

        private static VectorTextBuilder GetScalarsAndUnitConstructorBuilder(string vectorReference, string unitParameterName)
        {
            return new VectorTextBuilder(scalarsAndUnitConstructorComponent, Environment.NewLine);

            string scalarsAndUnitConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The magnitude of the {componentUpperCase}-component of the constructed {vectorReference}, expressed in <paramref name="{unitParameterName}"/>.</param>""";
            }
        }

        private static VectorTextBuilder GetDeconstructBuilder()
        {
            return new VectorTextBuilder(deconstructComponent, Environment.NewLine);

            static string deconstructComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The {componentUpperCase}-component of <see langword="this"/>.</param>""";
            }
        }
    }

    public virtual bool Equals(DefaultVectorDocumentation? other) => other is not null && Type == other.Type && Dimension == other.Dimension && Unit == other.Unit && Scalar == other.Scalar
        && DefaultUnit == other.DefaultUnit && DefaultUnitSymbol == other.DefaultUnitSymbol;

    public override bool Equals(object? obj) => obj is DefaultVectorDocumentation other && Equals(other);
    
    public static bool operator ==(DefaultVectorDocumentation? lhs, DefaultVectorDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultVectorDocumentation? lhs, DefaultVectorDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Type, Dimension, Unit, Scalar, DefaultUnit, DefaultUnitSymbol).GetHashCode();
}
