namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System;
using System.Globalization;

internal class DefaultDocumentation : IDocumentationStrategy, IEquatable<DefaultDocumentation>
{
    private DefinedType Type { get; }
    private IUnresolvedUnitType Unit { get; }

    private IUnresolvedUnitInstance? DefaultUnit { get; }
    private string? DefaultUnitSymbol { get; }

    private string UnitParameterName { get; }

    private IUnresolvedUnitInstance? ExampleBase { get; }
    private IUnresolvedUnitInstance? ExampleUnit { get; }

    public DefaultDocumentation(DataModel model)
    {
        Type = model.Scalar.Type;
        Unit = model.Scalar.Definition.Unit;

        DefaultUnit = model.Scalar.Definition.DefaultUnit;
        DefaultUnitSymbol = model.Scalar.Definition.DefaultUnitSymbol;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.Type.Name);

        ExampleBase = model.Scalar.IncludedBases.Count > 0 ? model.Scalar.IncludedBases[0] : null;
        ExampleUnit = model.Scalar.IncludedUnits.Count > 0 ? model.Scalar.IncludedUnits[0] : null;
    }

    public string Header() => $"""/// <summary>A measure of the scalar quantity {Type.Name}, expressed in {UnitReference}.</summary>""";
    public string Zero() => $$"""/// <summary>The {{ScalarReference}} representing { 0 }.</summary>""";

    public string Constant(IScalarConstant constant)
    {
        string value = constant.Value switch
        {
            > 10000 or < 0.0001 and > -0.0001 or < -10000 => constant.Value.ToString("0.000E0", CultureInfo.InvariantCulture),
            _ => constant.Value.ToString("0.####", CultureInfo.InvariantCulture)
        };

        return $$"""/// <summary>The {{ScalarReference}} representing the constant {{constant.Name}}, with value { {{value}} [{{constant.Unit.Plural}}] }.</summary>""";
    }

    public string UnitBase(IUnresolvedUnitInstance unitInstance)
        => $$"""/// <summary>The {{ScalarReference}} representing { 1 [<see cref="{{Unit.Type.FullyQualifiedName}}.{{unitInstance.Name}}"/>] }.</summary>""";

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
            /// <remarks>In most cases, expressing the magnitude in a certain {UnitReference} should be preferre. This is achieved using <see cref="InUnit({Unit.Type.FullyQualifiedName})"/>
            """;

        if (ExampleUnit is not null)
        {
            commonText = $"""{commonText} or an associated property - for example, <see cref="{ExampleUnit.Plural}"/>""";
        }

        return $"{commonText}.</remarks>";
    }

    public string ScalarConstructor() => $$"""
        /// <summary>Constructs a new {{ScalarReference}} representing { <paramref name="magnitude"/> }, expressed in an arbitrary unit.</summary>
        /// <param name="magnitude">The magnitude represented by the constructed {{ScalarReference}}, expressed in an arbitrary unit.</param>
        /// <remarks>Consider preferring construction through <see cref="{{Type.FullyQualifiedName}}(global::SharpMeasures.Scalar, {{Unit.Type.FullyQualifiedName}})"/>, where the magnitude is expressed in
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
                /// <code>{ScalarReference} x = 2.3 * <see cref="{Type.FullyQualifiedName}.One{ExampleBase.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }

    public string InUnit() => $"""
        /// <summary>The magnitude of <see langword="this"/>, expressed in <paramref name="{UnitParameterName}"/>.</summary>
        /// <param name="{UnitParameterName}">The {UnitReference} in which the magnitude of <see langword="this"/> is expressed.</param>
        """;

    public string InConstantMultiples(IScalarConstant constant) => $"""
        /// <summary>The magnitude of <see langword="this", expressed in multiples of <see cref="{Type.FullyQualifiedName}.{constant.Name}"/>.</summary>
        """;

    public string InSpecifiedUnit(IUnresolvedUnitInstance unitInstance) => $"""
        /// <summary>The magnitude of <see langword="this"/>, expressed in <see cref="{Unit.Type.FullyQualifiedName}.{unitInstance.Name}"/>.</summary>
        """;

    public string Conversion(IUnresolvedScalarType scalar) => $"""
        /// <summary>Converts <see langword="this"/> to the equivalent <see cref="{scalar.Type.FullyQualifiedName}"/>.</summary>
        """;

    public string CastConversion(IUnresolvedScalarType scalar) => $"""
        /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="{scalar.Type.FullyQualifiedName}"/>.</summary>
        /// <param name="x">This {ScalarReference} is converted to the equivalent <see cref="{scalar.Type.FullyQualifiedName}"/>.</param>
        """;
    
    public string IsNaN() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsNaN"/>""";
    public string IsZero() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsZero"/>""";
    public string IsPositive() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsPositive"/>""";
    public string IsNegative() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsNegative"/>""";
    public string IsFinite() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsFinite"/>""";
    public string IsInfinite() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsInfinite"/>""";
    public string IsPositiveInfinity() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsPositiveInfinity"/>""";
    public string IsNegativeInfinity() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.IsNegativeInfinity"/>""";

    public string Absolute() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Absolute"/>""";
    public string Sign() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Sign"/>""";

    public string Reciprocal() => InheritDoc;
    public string Square() => InheritDoc;
    public string Cube() => InheritDoc;
    public string SquareRoot() => InheritDoc;
    public string CubeRoot() => InheritDoc;

    public string ToStringDocumentation()
    {
        var commonText = $"""/// <summary>Produces a description of <see langword="this"/> containing the type""";

        if (DefaultUnit is not null && DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the magnitude expressed in <see cref="{Unit.Type.FullyQualifiedName}.{DefaultUnit}"/>, and the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        if (DefaultUnit is not null)
        {
            return $"""{commonText} and the magnitude expressed in <see cref="{Unit.Type.FullyQualifiedName}.{DefaultUnit}"/>.</summary>""";
        }

        if (DefaultUnitSymbol is not null)
        {
            return $"""{commonText}, the magnitude expressed in an arbitrary unit, and the symbol [{DefaultUnitSymbol}].</summary>""";
        }

        return $"""{commonText} and the magnitude expressed in an arbitrary unit.</summary>""";
    }

    public string EqualsSameTypeMethod() => InheritDoc;
    public string EqualsObjectMethod() => InheritDoc;

    public string EqualitySameTypeOperator() => """
        /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent magnitudes.</summary>
        /// /// <param name="lhs">The left-hand side of the equality check.</param>
        /// <param name="rhs">The right-hand side of the equality check.</param>
        """;

    public string InequalitySameTypeOperator() => """
        /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent magnitudes.</summary>
        /// <param name="lhs">The left-hand side of the inequality check.</param>
        /// <param name="rhs">The right-hand side of the inequality check.</param>/// <param name="lhs">The left-hand side of the inequality check.</param>
        /// <param name="rhs">The right-hand side of the inequality check.</param>
        """;

    public string GetHashCodeDocumentation() => InheritDoc;

    public string UnaryPlusMethod() => InheritDoc;
    public string NegateMethod() => InheritDoc;

    public string AddSameTypeMethod() => InheritDoc;
    public string SubtractSameTypeMethod() => InheritDoc;

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

    public string CompareToSameType() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.CompareTo(global::SharpMeasures.Scalar)"/>""";

    public string LessThanSameType()=> $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string GreaterThanSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string LessThanOrEqualSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string GreaterThanOrEqualSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    private string ScalarReference => $"""<see cref="{Type.FullyQualifiedName}"/>""";
    private string UnitReference => $"""<see cref="{Unit.Type.FullyQualifiedName}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    public virtual bool Equals(DefaultDocumentation? other) => other is not null && Type == other.Type && Unit == other.Unit && DefaultUnit == other.DefaultUnit
        && DefaultUnitSymbol == other.DefaultUnitSymbol && UnitParameterName == other.UnitParameterName && ExampleBase == other.ExampleBase && ExampleUnit == other.ExampleUnit;

    public override bool Equals(object? obj) => obj is DefaultDocumentation other && Equals(other);

    public static bool operator ==(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Type, Unit, DefaultUnit, DefaultUnitSymbol, UnitParameterName, ExampleBase, ExampleUnit).GetHashCode();
}
