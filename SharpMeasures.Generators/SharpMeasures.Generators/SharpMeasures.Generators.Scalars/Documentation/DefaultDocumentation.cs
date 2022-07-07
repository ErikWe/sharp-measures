namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

internal class DefaultDocumentation : IDocumentationStrategy, IEquatable<DefaultDocumentation>
{
    private DefinedType ScalarType { get; }
    private IUnitType Unit { get; }

    private string? DefaultUnitName { get; }
    private string? DefaultUnitSymbol { get; }

    private string UnitParameterName { get; }

    private UnitInstance? ExampleBase { get; }
    private UnitInstance? ExampleUnit { get; }

    public DefaultDocumentation(DataModel model)
    {
        ScalarType = model.ScalarData.ScalarType;
        Unit = model.ScalarDefinition.Unit;

        DefaultUnitName = model.ScalarDefinition.DefaultUnitName;
        DefaultUnitSymbol = model.ScalarDefinition.DefaultUnitSymbol;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.Type.Name);

        ExampleBase = GetExampleBase(model.ScalarDefinition.Unit, model.ScalarData.includeBases, model.ScalarData.excludeBases);
        ExampleUnit = GetExampleUnit(model.ScalarDefinition.Unit, model.ScalarData.includeUnits, model.ScalarData.excludeUnits);
    }

    public string Header() => $"""/// <summary>A measure of the scalar quantity {ScalarType.Name}, expressed in {UnitReference}.</summary>""";
    public string Zero() => $$"""/// <summary>The {{ScalarReference}} representing { 0 }.</summary>""";

    public string Constant(RefinedScalarConstantDefinition definition)
    {
        string value = definition.Value switch
        {
            > 10000 or < 0.0001 and > -0.0001 or < -10000 => definition.Value.ToString("0.000E0", CultureInfo.InvariantCulture),
            _ => definition.Value.ToString("0.####", CultureInfo.InvariantCulture)
        };

        return $$"""/// <summary>The {{ScalarReference}} representing the constant {{definition.Name}}, with value { {{value}} [{{definition.Unit.Plural}}] }.</summary>""";
    }

    public string UnitBase(UnitInstance unitInstance) => $$"""/// <summary>The {{ScalarReference}} representing { 1 [<see cref="{{Unit.Type.Name}}.{{unitInstance.Name}}"/>] }.</summary>""";

    public string WithMagnitude() => "/// <inheritdoc/>";

    public string FromReciprocal() => InheritDoc;
    public string FromSquare() => InheritDoc;
    public string FromCube() => InheritDoc;
    public string FromSquareRoot() => InheritDoc;
    public string FromCubeRoot() => InheritDoc;

    public string Magnitude()
    {
        var commonText = $"""
            /// <summary>The magnitude of <see langword="this"/>, expressed in an arbitrary unit.</summary>
            /// <remarks>In most cases, expressing the magnitude in a certain {UnitReference} should be preferred. This can be achieved through
            /// <see cref="InUnit({Unit.Type.Name})"/>
            """;

        if (ExampleUnit is not null)
        {
            commonText = $"""{commonText} or an associated property - such as <see cref="{ExampleUnit.Value.Plural}"/>""";
        }

        return $"{commonText}.</remarks>";
    }

    public string ScalarConstructor() => $$"""
        /// <summary>Constructs a new {{ScalarReference}} representing { <paramref name="magnitude"/> }, expressed in an arbitrary unit.</summary>
        /// <param name="magnitude">The magnitude represented by the constructed {{ScalarReference}}, expressed in an arbitrary unit.</param>
        /// <remarks>Consider preferring construction through <see cref="{{ScalarType.Name}}(Scalar, {{Unit.Type.Name}})"/>, where the magnitude is expressed in
        /// a certain {{UnitReference}}.</remarks>
        """;

    public string ScalarAndUnitConstructor()
    {
        var commonText = $$"""
            /// <summary>Constructs a new {{ScalarReference}} representing { <paramref name="magnitude"/> }, expressed in <paramref name="{{UnitParameterName}}"/>.</summary>
            /// <param name="magnitude">The magnitude represented by the constructed {{ScalarReference}}, expressed in <paramref name="{{UnitParameterName}}"/>.</param>
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which the magnitude, <paramref name="magnitude"/>, is expressed.</param>
            """;

        if (ExampleBase is not null)
        {
            return $"""
                {commonText}
                /// <remarks>A {ScalarReference} may also be constructed as demonstrated below.
                /// <code>{ScalarReference} x = 2.3 * <see cref="{ScalarType.Name}.One{ExampleBase.Value.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }

    public string InUnit() => $"""
        /// <summary>The magnitude of <see langword="this"/>, expressed in <paramref name="{UnitParameterName}"/>.</summary>
        /// <param name="{UnitParameterName}">The {UnitReference} in which the magnitude of <see langword="this"/> is expressed.</param>
        """;

    public string InConstantMultiples(RefinedScalarConstantDefinition definition) => $"""
        /// <summary>The magnitude of <see langword="this", expressed in multiples of <see cref="{ScalarType.Name}.{definition.Name}"/>.</summary>
        """;

    public string InSpecifiedUnit(UnitInstance unitInstance) => $"""
        /// <summary>The magnitude of <see langword="this"/>, expressed in <see cref="{Unit.Type.Name}.{unitInstance.Name}"/>.</summary>
        """;

    public string AsDimensionallyEquivalent(IScalarType scalar) => $"""
        /// <summary>Converts <see langword="this"/> to the equivalent <see cref="{scalar.Type.Name}"/>.</summary>
        """;

    public string CastToDimensionallyEquivalent(IScalarType scalar) => $"""
        /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="{scalar.Type.Name}"/>.</summary>
        /// <param name="x">This {ScalarReference} is converted to the equivalent <see cref="{scalar.Type.Name}"/>.</param>
        """;
    
    public string IsNaN() => """/// <inheritdoc cref="Scalar.IsNaN"/>""";
    public string IsZero() => """/// <inheritdoc cref="Scalar.IsZero"/>""";
    public string IsPositive() => """/// <inheritdoc cref="Scalar.IsPositive"/>""";
    public string IsNegative() => """/// <inheritdoc cref="Scalar.IsNegative"/>""";
    public string IsFinite() => """/// <inheritdoc cref="Scalar.IsFinite"/>""";
    public string IsInfinite() => """/// <inheritdoc cref="Scalar.IsInfinite"/>""";
    public string IsPositiveInfinity() => """/// <inheritdoc cref="Scalar.IsPositiveInfinity"/>""";
    public string IsNegativeInfinity() => """/// <inheritdoc cref="Scalar.IsNegativeInfinity"/>""";

    public string Absolute() => """/// <inheritdoc cref="Scalar.Absolute"/>""";
    public string Sign() => """/// <inheritdoc cref="Scalar.Sign"/>""";

    public string Reciprocal() => InheritDoc;
    public string Square() => InheritDoc;
    public string Cube() => InheritDoc;
    public string SquareRoot() => InheritDoc;
    public string CubeRoot() => InheritDoc;

    public string ToStringDocumentation()
    {
        var commonText = $"""/// <summary>Produces a description of <see langword="this"/> containing the type""";

        if (DefaultUnitName is not null && DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the magnitude expressed in <see cref="{Unit.Type.Name}.{DefaultUnitName}"/>, and the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        if (DefaultUnitName is not null)
        {
            return $"""{commonText} and the magnitude expressed in <see cref="{Unit.Type.Name}.{DefaultUnitName}"/>.</summary>""";
        }

        if (DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the magnitude expressed in an arbitrary unit, and the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        return $"""{commonText} and the magnitude expressed in an arbitrary unit.</summary>""";
    }

    public string EqualsSameTypeMethod() => InheritDoc;
    public string EqualsObjectMethod() => InheritDoc;

    public string EqualitySameTypeOperator()
    {
        string text = $"""/// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent magnitudes.""";

        return $"""
            {text}.</summary>
            /// <param name="lhs">The left-hand side of the equality check.</param>
            /// <param name="rhs">The right-hand side of the equality check.</param>
            """;
    }

    public string InequalitySameTypeOperator()
    {
        string text = $"""/// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent magnitudes.""";

        return $"""
            {text}.</summary>
            /// <param name="lhs">The left-hand side of the inequality check.</param>
            /// <param name="rhs">The right-hand side of the inequality check.</param>
            """;
    }

    public string GetHashCodeDocumentation() => InheritDoc;

    public string UnaryPlusMethod() => InheritDoc;
    public string NegateMethod() => InheritDoc;

    public string AddSameTypeMethod() => InheritDoc;
    public string SubtractSameTypeMethod() => InheritDoc;
    public string SubtractFromSameTypeMethod() => InheritDoc;

    public string AddDifferenceMethod() => InheritDoc;
    public string SubtractDifferenceMethod() => InheritDoc;

    public string MultiplyScalarMethod() => InheritDoc;
    public string DivideScalarMethod() => InheritDoc;

    public string MultiplySameTypeMethod() => InheritDoc;
    public string DivideSameTypeMethod() => InheritDoc;

    public string MultiplyVectorMethod(int dimension) => InheritDoc;

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
    public string DivideScalarOperatorRHS() => InheritDoc;

    public string MultiplySameTypeOperator() => InheritDoc;
    public string DivideSameTypeOperator() => InheritDoc;

    public string MultiplyVectorOperatorLHS(int dimension) => InheritDoc;
    public string MultiplyVectorOperatorRHS(int dimension) => InheritDoc;

    public string CompareToSameType() => """/// <inheritdoc cref="Scalar.CompareTo(Scalar)"/>""";

    public string LessThanSameType()=> $$"""/// <inheritdoc cref="Scalar.operator &lt;(Scalar, Scalar)"/>""";
    public string GreaterThanSameType() => $$"""/// <inheritdoc cref="Scalar.operator &gt;(Scalar, Scalar)"/>""";
    public string LessThanOrEqualSameType() => $$"""/// <inheritdoc cref="Scalar.operator &lt;=(Scalar, Scalar)"/>""";
    public string GreaterThanOrEqualSameType() => $$"""/// <inheritdoc cref="Scalar.operator &gt;=(Scalar, Scalar)"/>""";

    private string ScalarReference => $"""<see cref="{ScalarType.Name}"/>""";
    private string UnitReference => $"""<see cref="{Unit.Type.Name}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    private static UnitInstance? GetExampleBase(IUnitType unit, IEnumerable<IncludeBasesDefinition> includeBases, IEnumerable<ExcludeBasesDefinition> excludeBases)
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

    private static UnitInstance? GetExampleUnit(IUnitType unit, IEnumerable<IncludeUnitsDefinition> includeUnits, IEnumerable<ExcludeUnitsDefinition> excludeUnits)
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

    public virtual bool Equals(DefaultDocumentation? other) => other is not null && ScalarType == other.ScalarType && Unit == other.Unit
        && DefaultUnitName == other.DefaultUnitName && DefaultUnitSymbol == other.DefaultUnitSymbol && UnitParameterName == other.UnitParameterName
        && ExampleBase == other.ExampleBase && ExampleUnit == other.ExampleUnit;

    public override bool Equals(object? obj) => obj is DefaultDocumentation other && Equals(other);

    public static bool operator ==(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (ScalarType, Unit, DefaultUnitName, DefaultUnitSymbol, UnitParameterName, ExampleBase, ExampleUnit).GetHashCode();
}
