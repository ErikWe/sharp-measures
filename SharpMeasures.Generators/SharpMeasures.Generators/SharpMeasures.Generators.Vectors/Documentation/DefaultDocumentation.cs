namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

internal class DefaultDocumentation<TDataModel> : IDocumentationStrategy, IEquatable<DefaultDocumentation<TDataModel>>
    where TDataModel : IDataModel<TDataModel>
{
    private DefinedType VectorType { get; }
    private int Dimension { get; }
    private UnitInterface Unit { get; }
    private ScalarInterface? Scalar { get; }

    private string? DefaultUnitName { get; }
    private string? DefaultUnitSymbol { get; }

    private string UnitParameterName { get; }

    private UnitInstance? ExampleScalarBase { get; }
    private UnitInstance? ExampleUnit { get; }

    private bool HasComponent => Scalar is not null;

    private SpecificTexts Texts { get; }

    public DefaultDocumentation(TDataModel model)
    {
        VectorType = model.VectorType;
        Dimension = model.Dimension;
        Unit = model.Unit;

        DefaultUnitName = model.DefaultUnitName;
        DefaultUnitSymbol = model.DefaultUnitSymbol;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.UnitType.Name);

        if (model.Scalar is not null)
        {
            ExampleScalarBase = GetExampleBase(model.Unit, model.Scalar.IncludedBases, model.Scalar.ExcludedBases);
        }

        ExampleUnit = GetExampleUnit(model.Unit, model.IncludeUnits, model.ExcludeUnits);

        Texts = new(Dimension, VectorReference, UnitParameterName);
    }

    public string Header() => HasComponent switch
    {
        true => ComponentedHeader(),
        false => UncomponentedHeader()
    };

    private string ComponentedHeader() => $"""
        /// <summary>A measure of the {Dimension}-dimensional vector quantity {VectorType.Name}, composed of {ScalarReference},
        /// and expressed in {UnitReference}.</summary>
        """;

    private string UncomponentedHeader() => $"""
        /// <summary>A measure of the {Dimension}-dimensional vector quantity {VectorType.Name}, expressed in {UnitReference}.</summary>
        """;

    public string Zero() => $$"""/// <summary>The {{VectorReference}} representing { {{ConstantVectorTexts.Zeros(Dimension)}} }.</summary>""";
    public string Constant(RefinedVectorConstantDefinition definition)
    {
        StringBuilder componentText = new();
        IterativeBuilding.AppendEnumerable(componentText, components(), ", ");

        return $$"""/// <summary>The {{ScalarReference}} representing the constant {{definition.Name}}, with value { ({{componentText}}) [{{definition.Unit.Plural}}] }.</summary>""";

        IEnumerable<string> components()
        {
            foreach (double value in definition.Value)
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
        /// <remarks>Consider preferring construction through <see cref="{{VectorType.Name}}({{ConstantVectorTexts.UnnamedScalars(Dimension)}}, {{Unit.UnitType.Name}})"/>,
        /// where the components are expressed in a certain {{UnitReference}}.</remarks>
        """;

    public string VectorConstructor() => $"""
        /// <summary>Constructs a new {VectorReference} components of magnitudes <paramref name="components"/>, expressed in an arbitrary unit.</summary>
        /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled3"/>, expressed in an arbitrary unit.</param>
        /// <remarks>Consider preferring construction through <see cref="{VectorType.Name}(Vector{Dimension}, {Unit.UnitType.Name})"/>,
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
                /// <code>{VectorReference} x = ({ConstantVectorTexts.SampleValues(Dimension)}) * <see cref="{Scalar.ScalarType.Name}.One{ExampleScalarBase.Value.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }
    public string VectorAndUnitConstructor()
    {
        var commonText = $"""
            /// <summary>Constructs a new {VectorReference} components of magnitudes <paramref name="components"/>, expressed in <paramref name="{UnitParameterName}"/>.</summary>
            /// <param name="components">The magnitudes of the components of the constructed <see cref="Unhandled3"/>, expressed in <paramref name="{UnitParameterName}"/>.</param>
            """;

        if (Scalar is not null && ExampleScalarBase is not null)
        {
            commonText = $"""
                {commonText}
                /// <remarks>A {VectorReference} may also be constructed as demonstrated below.
                /// <code>{VectorReference} x = ({ConstantVectorTexts.SampleValues(Dimension)}) * <see cref="{Scalar.ScalarType.Name}.One{ExampleScalarBase.Value.Name}"/>;</code>
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
            /// <remarks>In most cases, expressing the magnitudes in a certain {UnitReference} should be preferred. This can be achieved through
            /// <see cref="InUnit({Unit.UnitType.Name})"/>
            """;

        if (ExampleUnit is not null)
        {
            return $"""{commonText} or an associated property - such as <see cref="In{ExampleUnit.Value.Plural}"/>""";
        }

        return $"{commonText}.</remarks>";
    }
    
    public string InUnit() => $"""
        /// <summary>The components of <see langword="this"/>, expressed in <paramref name="{UnitParameterName}"/>.</summary>
        /// <param name="{UnitParameterName}">The {UnitReference} in which the components of <see langword="this"/> are expressed.</param>
        """;
    public string InConstantMultiples(RefinedVectorConstantDefinition definition) => $"""
        /// <summary>The components of <see langword="this", expressed in multiples of <see cref="{VectorReference}.{definition.Name}"/>.</summary>
        """;
    public string InSpecifiedUnit(UnitInstance unitInstance) => $"""
        /// <summary>The components of <see langword="this"/>, expressed in <see cref="{UnitReference}.{unitInstance.Name}"/>.</summary>
        """;

    public string AsDimensionallyEquivalent(IVector vector) => $"""
        /// <summary>Converts <see langword="this"/> to the equivalent <see cref="{vector.VectorType.Name}"/>.</summary>
        """;
    public string CastToDimensionallyEquivalent(IVector vector) => $"""
        /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="{vector.VectorType.Name}"/>.</summary>
        /// <param name="x">This {VectorReference} is converted to the equivalent <see cref="{vector.VectorType.Name}"/>.</param>
        """;

    public string IsNaN() => """/// <inheritdoc cref="Vector3.IsNaN"/>""";
    public string IsZero() => """/// <inheritdoc cref="Vector3.IsZero"/>""";
    public string IsFinite() => """/// <inheritdoc cref="Vector3.IsFinite"/>""";
    public string IsInfinite() => """/// <inheritdoc cref="Vector3.IsInfinite"/>""";

    public string Magnitude() => InheritDoc;
    public string SquaredMagnitude() => InheritDoc;

    public string ScalarMagnitude() => InheritDoc;
    public string ScalarSquaredMagnitude() => InheritDoc;

    public string Normalize() => InheritDoc;
    public string Transform() => InheritDoc;

    public string ToStringDocumentation()
    {
        var commonText = $"""/// <summary>Produces a description of <see langword="this"/> containing the type""";

        if (DefaultUnitName is not null && DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the components expressed in <see cref="{Unit.UnitType.Name}.{DefaultUnitName}"/>, and the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        if (DefaultUnitName is not null)
        {
            return $"""{commonText} and the components expressed in <see cref="{Unit.UnitType.Name}.{DefaultUnitName}"/>.</summary>""";
        }

        if (DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the components expressed in an arbitrary unit, and the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        return $"""{commonText} and the components expressed in an arbitrary unit.</summary>""";
    }

    public string EqualsSameTypeMethod() => InheritDoc;
    public string EqualsObjectMethod() => InheritDoc;

    public string EqualitySameTypeOperator()
    {
        string text = $"""/// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent components.""";

        return $"""
            {text}.</summary>
            /// <param name="lhs">The left-hand side of the equality check.</param>
            /// <param name="rhs">The right-hand side of the equality check.</param>
            """;
    }

    public string InequalitySameTypeOperator()
    {
        string text = $"""/// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent components.""";

        return $"""
            {text}.</summary>
            /// <param name="lhs">The left-hand side of the inequality check.</param>
            /// <param name="rhs">The right-hand side of the inequality check.</param>
            """;
    }

    public string GetHashCodeDocumentation() => InheritDoc;

    public string Deconstruct() => $"""
        /// <summary>Deconstructs <see langword="this"/> into the individual components.</summary>
        {Texts.Deconstruct()}
        """;

    public string UnaryPlusMethod() => InheritDoc;
    public string NegateMethod() => InheritDoc;

    public string AddSameTypeMethod() => InheritDoc;
    public string SubtractSameTypeMethod() => InheritDoc;
    public string SubtractFromSameTypeMethod() => InheritDoc;

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

    private string VectorReference => $"""<see cref="{VectorType.Name}"/>""";
    private string UnitReference => $"""<see cref="{Unit.UnitType.Name}"/>""";
    private string ScalarReference => $"""<see cref={Scalar?.ScalarType.Name}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    private static UnitInstance? GetExampleBase(UnitInterface unit, IEnumerable<IncludeBasesInterface> includeBases, IEnumerable<ExcludeBasesInterface> excludeBases)
    {
        foreach (var inclusionList in includeBases)
        {
            foreach (var includedBase in inclusionList.IncludedBases)
            {
                if (unit.UnitsByName.TryGetValue(includedBase, out var unitInstance))
                {
                    return unitInstance;
                }
            }
        }

        HashSet<string> excludedBases = new(excludeBases.SelectMany((x) => x.ExcludedBases));

        foreach (var candidateBase in unit.UnitsByName.Values)
        {
            if (excludedBases.Contains(candidateBase.Name) is false)
            {
                return candidateBase;
            }
        }

        return null;
    }

    private static UnitInstance? GetExampleUnit(UnitInterface unit, IEnumerable<IncludeUnitsDefinition> includeUnits, IEnumerable<ExcludeUnitsDefinition> excludeUnits)
    {
        foreach (var inclusionList in includeUnits)
        {
            foreach (var includedUnit in inclusionList.IncludedUnits)
            {
                if (unit.UnitsByName.TryGetValue(includedUnit, out var unitInstance))
                {
                    return unitInstance;
                }
            }
        }

        HashSet<string> excludedUnits = new(excludeUnits.SelectMany((x) => x.ExcludedUnits));

        foreach (var candidateUnit in unit.UnitsByName.Values)
        {
            if (excludedUnits.Contains(candidateUnit.Name) is false)
            {
                return candidateUnit;
            }
        }

        return null;
    }

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

    public virtual bool Equals(DefaultDocumentation<TDataModel>? other) => other is not null && VectorType == other.VectorType && Dimension == other.Dimension
        && Unit == other.Unit && Scalar == other.Scalar && DefaultUnitName == other.DefaultUnitName && DefaultUnitSymbol == other.DefaultUnitSymbol;

    public override bool Equals(object? obj) => obj is DefaultDocumentation<TDataModel> other && Equals(other);
    
    public static bool operator ==(DefaultDocumentation<TDataModel>? lhs, DefaultDocumentation<TDataModel>? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultDocumentation<TDataModel>? lhs, DefaultDocumentation<TDataModel>? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (VectorType, Dimension, Unit, Scalar, DefaultUnitName, DefaultUnitSymbol).GetHashCode();
}
