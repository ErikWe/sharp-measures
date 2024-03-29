﻿namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Globalization;

internal sealed class DefaultDocumentation : IDocumentationStrategy, IEquatable<DefaultDocumentation>
{
    private DefinedType Type { get; }
    private NamedType Unit { get; }

    private string? DefaultUnitInstancePluralForm { get; }
    private string? DefaultUnitInstanceSymbol { get; }

    private string UnitParameterName { get; }

    private string? ExampleUnitBaseInstanceName { get; }
    private string? ExampleUnitInstancePluralForm { get; }

    public DefaultDocumentation(ResolvedScalarType scalar, IUnitPopulation unitPopulation)
    {
        Type = scalar.Type;
        Unit = scalar.Unit;

        DefaultUnitInstancePluralForm = GetDefaultUnitPluralForm(scalar, unitPopulation);
        DefaultUnitInstanceSymbol = scalar.DefaultUnitInstanceSymbol;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.Name);

        ExampleUnitBaseInstanceName = GetExampleUnitInstance(scalar, unitPopulation, scalar.IncludedUnitBaseInstanceNames)?.Name;
        ExampleUnitInstancePluralForm = GetExampleUnitInstance(scalar, unitPopulation, scalar.IncludedUnitInstanceNames)?.PluralForm;
    }

    private static string? GetDefaultUnitPluralForm(ResolvedScalarType scalar, IUnitPopulation unitPopulation)
    {
        if (scalar.DefaultUnitInstanceName is null || unitPopulation.Units.TryGetValue(scalar.Unit, out var unit) is false || unit.UnitInstancesByName.TryGetValue(scalar.DefaultUnitInstanceName, out var defaultUnit) is false)
        {
            return null;
        }

        return defaultUnit.PluralForm;
    }

    private static IUnitInstance? GetExampleUnitInstance(ResolvedScalarType scalar, IUnitPopulation unitPopulation, IReadOnlyList<string> includedUnitInstanceNames)
    {
        if (unitPopulation.Units.TryGetValue(scalar.Unit, out var unit) is false)
        {
            return null;
        }

        foreach (var includedUnitInstanceName in includedUnitInstanceNames)
        {
            if (unit.UnitInstancesByName.TryGetValue(includedUnitInstanceName, out var unitInstance))
            {
                return unitInstance;
            }
        }

        return null;
    }

    public string Header() => $"""/// <summary>A measure of the scalar quantity {SourceBuildingUtility.ToQuantityName(Type.Name)}, expressed in {UnitReference}.</summary>""";
    public string Zero() => $$"""/// <summary>The {{ScalarReference}} representing { 0 }.</summary>""";

    public string Constant(IScalarConstant constant)
    {
        return $$"""/// <summary>The {{ScalarReference}} representing { {{produceDescription()}} } <see cref="{{Unit.FullyQualifiedName}}.{{constant.UnitInstanceName}}"/>.</summary>""";

        string produceDescription()
        {
            if (constant.Locations.ExplicitlySetValue)
            {
                return constant.Value!.Value switch
                {
                    > 10000 or (< 0.0001 and > -0.0001) or < -10000 => constant.Value.Value.ToString("0.000E0", CultureInfo.InvariantCulture),
                    _ => constant.Value.Value.ToString("0.####", CultureInfo.InvariantCulture)
                };
            }

            return constant.Expression!;
        }
    }

    public string UnitBase(IUnitInstance unitInstance) => $$"""/// <summary>The {{ScalarReference}} representing { 1 } <see cref="{{Unit.FullyQualifiedName}}.{{unitInstance.Name}}"/>.</summary>""";

    public string WithMagnitude() => InheritDoc;

    public string Magnitude()
    {
        var commonText = $"""
            /// <summary>The magnitude of <see langword="this"/>, expressed in an arbitrary unit.</summary>
            /// <remarks>In most cases, expressing the magnitude in a specified {UnitReference} should be preferred. This is achieved through <see cref="InUnit({Unit.FullyQualifiedName})"/>
            """;

        if (ExampleUnitInstancePluralForm is not null)
        {
            commonText = $"""{commonText} or an associated property - for example, <see cref="{ExampleUnitInstancePluralForm}"/>""";
        }

        return $"{commonText}.</remarks>";
    }

    public string ScalarConstructor() => $$"""
        /// <summary>Constructs a new {{ScalarReference}} representing { <paramref name="magnitude"/> }, expressed in an arbitrary unit.</summary>
        /// <param name="magnitude">The magnitude represented by the constructed {{ScalarReference}}, expressed in an arbitrary unit.</param>
        /// <remarks>Consider preferring construction through <see cref="{{Type.FullyQualifiedName}}(global::SharpMeasures.Scalar, {{Unit.FullyQualifiedName}})"/>, where the magnitude is expressed in
        /// a specified {{UnitReference}}.</remarks>
        """;

    public string ScalarAndUnitConstructor()
    {
        var commonText = $$"""
            /// <summary>Constructs a new {{ScalarReference}} representing { <paramref name="magnitude"/> }, when expressed in <paramref name="{{UnitParameterName}}"/>.</summary>
            /// <param name="magnitude">The magnitude represented by the constructed {{ScalarReference}}, when expressed in <paramref name="{{UnitParameterName}}"/>.</param>
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which <paramref name="magnitude"/> is expressed.</param>
            """;

        if (ExampleUnitBaseInstanceName is not null)
        {
            return $"""
                {commonText}
                /// <remarks>A new {ScalarReference} may also be constructed as demonstrated below.
                /// <code>{ScalarReference} x = 4.2 * <see cref="{Type.FullyQualifiedName}.{UnitBaseInstanceNameInterpreter.InterpretName(ExampleUnitBaseInstanceName)}"/>;</code>
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
        /// <summary>The magnitude of <see langword="this"/>, expressed in multiples of <see cref="{Type.FullyQualifiedName}.{constant.Name}"/>.</summary>
        """;

    public string InSpecifiedUnit(IUnitInstance unitInstance) => $"""
        /// <summary>The magnitude of <see langword="this"/>, expressed in <see cref="{Unit.FullyQualifiedName}.{unitInstance.Name}"/>.</summary>
        """;

    public string Conversion(NamedType scalar) => $"""
        /// <summary>Converts <see langword="this"/> to the equivalent <see cref="{scalar.FullyQualifiedName}"/>.</summary>
        """;

    public string AntidirectionalConversion(NamedType scalar) => $"""
        /// <summary>Converts <paramref name="{SourceBuildingUtility.ToParameterName(scalar.Name)}"/> to the equivalent {ScalarReference}.</summary>
        /// <param name="{SourceBuildingUtility.ToParameterName(scalar.Name)}">This <see cref="{scalar.FullyQualifiedName}"/> is converted to the original {ScalarReference}.</param>
        """;

    public string CastConversion(NamedType scalar) => $"""
        /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="{scalar.FullyQualifiedName}"/>.</summary>
        /// <param name="x">This {ScalarReference} is converted to the equivalent <see cref="{scalar.FullyQualifiedName}"/>.</param>
        """;

    public string AntidirectionalCastConversion(NamedType scalar) => $"""
        /// <summary>Converts <paramref name="x"/> to the equivalent {ScalarReference}.</summary>
        /// <param name="x">This <see cref="{scalar.FullyQualifiedName}"/> is converted to the equivalent {ScalarReference}.</param>
        """;

    public string OperationMethod(IQuantityOperation operation, NamedType other) => OperationMethod(operation, other, mirrored: false);
    public string MirroredOperationMethod(IQuantityOperation operation, NamedType other) => OperationMethod(operation, other, mirrored: true);
    private static string OperationMethod(IQuantityOperation operation, NamedType other, bool mirrored)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(other.Name);

        if (operation.OperatorType is OperatorType.Addition)
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> + <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The second term of { <see langword="this"/> + <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Multiplication)
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> ∙ <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The second factor of { <see langword="this"/> ∙ <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Subtraction)
        {
            if ((operation.Position is OperatorPosition.Left && mirrored is false) || (operation.Position is OperatorPosition.Right && mirrored))
            {
                return $$"""
                    /// <summary>Computes { <see langword="this"/> - <paramref name="{{parameterName}}"/> }.</summary>
                    /// <param name="{{parameterName}}">The subtrahend of { <see langword="this"/> - <paramref name="{{parameterName}}"/> }.</param>
                    """;
            }

            return $$"""
                /// <summary>Computes { <paramref name="{{parameterName}}"/> - <see langword="this"/> }.</summary>
                /// <param name="{{parameterName}}">The minuend of { <paramref name="{{parameterName}}"/> - <see langword="this"/> }.</param>
                """;
        }

        if ((operation.Position is OperatorPosition.Left && mirrored is false) || (operation.Position is OperatorPosition.Right && mirrored))
        {
            return $$"""
                /// <summary>Computes { <see langword="this"/> / <paramref name="{{parameterName}}"/> }.</summary>
                /// <param name="{{parameterName}}">The divisor of { <see langword="this"/> / <paramref name="{{parameterName}}"/> }.</param>
                """;
        }

        return $$"""
            /// <summary>Computes { <paramref name="{{parameterName}}"/> / <see langword="this"/> }.</summary>
            /// <param name="{{parameterName}}">The dividend of { <paramref name="{{parameterName}}"/> / <see langword="this"/> }.</param>
            """;
    }

    public string MirroredOperationOperator(IQuantityOperation operation, NamedType other) => OperationOperator(operation, other);
    public string OperationOperator(IQuantityOperation operation, NamedType other)
    {
        if (operation.OperatorType is OperatorType.Addition)
        {
            return """
                /// <summary>Computes { <see langword="a"/> + <paramref name="b"/> }.</summary>
                /// <param name="a">The first term of { <see langword="a"/> + <paramref name="b"/> }.</param>
                /// <param name="b">The second term of { <see langword="a"/> + <paramref name="b"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Multiplication)
        {
            return """
                /// <summary>Computes { <see langword="a"/> * <paramref name="b"/> }.</summary>
                /// <param name="a">The first factor of { <see langword="a"/> * <paramref name="b"/> }.</param>
                /// <param name="b">The second factor of { <see langword="a"/> * <paramref name="b"/> }.</param>
                """;
        }

        if (operation.OperatorType is OperatorType.Subtraction)
        {
            return """
                /// <summary>Computes { <see langword="a"/> - <paramref name="b"/> }.</summary>
                /// <param name="a">The minuend of { <see langword="a"/> - <paramref name="b"/> }.</param>
                /// <param name="b">The subtrahend of { <see langword="a"/> - <paramref name="b"/> }.</param>
                """;
        }

        return """
            /// <summary>Computes { <see langword="a"/> / <paramref name="b"/> }.</summary>
            /// <param name="a">The dividend of { <see langword="a"/> / <paramref name="b"/> }.</param>
            /// <param name="b">The divisor of { <see langword="a"/> / <paramref name="b"/> }.</param>
            """;
    }

    public string Process(IQuantityProcess process) => $"""/// <summary>Executes a custom process.</summary>""";

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

    public string ToStringMethod()
    {
        if (DefaultUnitInstancePluralForm is not null && DefaultUnitInstanceSymbol is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> using the current culture, followed by the symbol [{DefaultUnitInstanceSymbol}].</summary>""";
        }

        if (DefaultUnitInstancePluralForm is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> using the current culture.</summary>""";
        }

        return $"""/// <summary>Formats the represented <see cref="Magnitude"/> using the current culture.</summary>""";
    }

    public string ToStringFormat()
    {
        if (DefaultUnitInstancePluralForm is not null && DefaultUnitInstanceSymbol is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> according to <paramref name="format"/>, using the current culture - and followed by the symbol [{DefaultUnitInstanceSymbol}].</summary>""";
        }

        if (DefaultUnitInstancePluralForm is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> according to <paramref name="format"/>, using the current culture.</summary>""";
        }

        return $"""/// <summary>Formats the represented <see cref="Magnitude"/> according to <paramref name="format"/>, using the current culture.</summary>""";
    }

    public string ToStringProvider()
    {
        if (DefaultUnitInstancePluralForm is not null && DefaultUnitInstanceSymbol is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> using the culture-specific formatting information provided by <paramref name="formatProvider"/> - and followed by the symbol [{DefaultUnitInstanceSymbol}].</summary>""";
        }

        if (DefaultUnitInstancePluralForm is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>""";
        }

        return $"""/// <summary>Formats the represented <see cref="Magnitude"/> using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>""";
    }

    public string ToStringFormatAndProvider()
    {
        if (DefaultUnitInstancePluralForm is not null && DefaultUnitInstanceSymbol is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> according to <paramref name="format"/>, using the culture-specific formatting information provided by <paramref name="formatProvider"/> - and followed by the symbol [{DefaultUnitInstanceSymbol}].</summary>""";
        }

        if (DefaultUnitInstancePluralForm is not null)
        {
            return $"""/// <summary>Formats the represented <see cref="{DefaultUnitInstancePluralForm}"/> according to <paramref name="format"/>, using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>""";
        }

        return $"""/// <summary>Formats the represented <see cref="Magnitude"/> according to <paramref name="format"/>, using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>""";
    }

    public string EqualsSameTypeMethod() => InheritDoc;
    public string EqualsObjectMethod() => InheritDoc;

    public string EqualitySameTypeOperator() => """
        /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent magnitudes.</summary>
        /// <param name="lhs">The left-hand side of the equality check.</param>
        /// <param name="rhs">The right-hand side of the equality check.</param>
        """;

    public string InequalitySameTypeOperator() => """
        /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent magnitudes.</summary>
        /// <param name="lhs">The left-hand side of the inequality check.</param>
        /// <param name="rhs">The right-hand side of the inequality check.</param>
        """;

    public string GetHashCodeDocumentation() => InheritDoc;

    public string UnaryPlusMethod() => InheritDoc;
    public string NegateMethod() => InheritDoc;

    public string AddSameTypeMethod() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Add(global::SharpMeasures.Scalar)"/>""";
    public string SubtractSameTypeMethod() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Subtract(global::SharpMeasures.Scalar)"/>""";

    public string AddDifferenceMethod() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Add(global::SharpMeasures.Scalar)"/>""";
    public string SubtractDifferenceMethod() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Subtract(global::SharpMeasures.Scalar)"/>""";

    public string MultiplyScalarMethod() => InheritDoc;
    public string DivideScalarMethod() => InheritDoc;

    public string DivideSameTypeMethod() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Divide(global::SharpMeasures.Scalar)"/>""";

    public string MultiplyVectorMethod(int dimension) => $$"""/// <inheritdoc cref="Scalar.Multiply(global::SharpMeasures.Vector{{dimension}})"/>""";

    public string AddTScalarMethod() => """
        /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
        /// <typeparam name="TScalar">The type of <paramref name="addend"/>.</typeparam>
        /// <param name="addend">The second term of { <see langword="this"/> + <paramref name="addend"/> }.</param>
        """;

    public string SubtractTScalarMethod() => """
        /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
        /// <typeparam name="TScalar">The type of <paramref name="subtrahend"/>.</typeparam>
        /// <param name="subtrahend">The second term of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
        """;

    public string SubtractFromTScalarMethod() => """
        /// <summary>Computes { <paramref name="minuend"/> - <see langword="this"/> }.</summary>
        /// <typeparam name="TScalar">The type of <paramref name="minuend"/>.</typeparam>
        /// <param name="minuend">The first term of { <paramref name="minuend"/> - <see langword="this"/> }.</param>
        """;

    public string MultiplyTScalarMethod() => """
        /// <summary>Computes { <see langword="this"/> * <paramref name="factor"/> }.</summary>
        /// <typeparam name="TScalar">The type of <paramref name="factor"/>.</typeparam>
        /// <param name="factor">The second factor of { <see langword="this"/> * <paramref name="factor"/> }.</param>
        """;

    public string DivideTScalarMethod() => """
        /// <summary>Computes { <see langword="this"/> / <paramref name="divisor"/> }.</summary>
        /// <typeparam name="TScalar">The type of <paramref name="divisor"/>.</typeparam>
        /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
        """;

    public string DivideIntoTScalarMethod() => """
        /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
        /// <typeparam name="TScalar">The type of <paramref name="dividend"/>.</typeparam>
        /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
        """;

    public string UnaryPlusOperator() => InheritDoc;
    public string NegateOperator() => InheritDoc;

    public string AddSameTypeOperator() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string SubtractSameTypeOperator() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    public string AddDifferenceOperatorLHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string AddDifferenceOperatorRHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string SubtractDifferenceOperatorLHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    public string MultiplyScalarOperatorLHS() => InheritDoc;
    public string MultiplyScalarOperatorRHS() => InheritDoc;
    public string DivideScalarOperatorLHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    public string DivideSameTypeOperator() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    public string MultiplyVectorOperatorLHS(int dimension) => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{dimension}}.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Vector{{dimension}})"/>""";
    public string MultiplyVectorOperatorRHS(int dimension) => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{dimension}}.operator *(global::SharpMeasures.Vector{{dimension}}, global::SharpMeasures.Scalar)"/>""";

    public string AddIScalarOperator() => """
        /// <summary>Computes { <paramref name="x"/> + <paramref name="y"/> }.</summary>
        /// <param name="x">The first term of { <paramref name="x"/> + <paramref name="y"/> }.</param>
        /// <param name="y">The second term of { <paramref name="x"/> + <paramref name="y"/> }.</param>
        /// <remarks>Consider preferring <see cref="Add{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
        """;

    public string SubtractIScalarOperator() => """
        /// <summary>Computes { <paramref name="x"/> - <paramref name="y"/> }.</summary>
        /// <param name="x">The first term of { <paramref name="x"/> - <paramref name="y"/> }.</param>
        /// <param name="y">The second term of { <paramref name="x"/> - <paramref name="y"/> }.</param>
        /// <remarks>Consider preferring <see cref="Subtract{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
        """;

    public string MultiplyIScalarOperator() => """
        /// <summary>Computes { <paramref name="x"/> * <paramref name="y"/> }.</summary>
        /// <param name="x">The first factor of { <paramref name="x"/> * <paramref name="y"/> }.</param>
        /// <param name="y">The second factor of { <paramref name="x"/> * <paramref name="y"/> }.</param>
        /// <remarks>Consider preferring <see cref="Multiply{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
        """;

    public string DivideIScalarOperator() => """
        /// <summary>Computes { <paramref name="x"/> / <paramref name="y"/> }.</summary>
        /// <param name="x">The dividend of { <paramref name="x"/> / <paramref name="y"/> }.</param>
        /// <param name="y">The divisor of { <paramref name="x"/> / <paramref name="y"/> }.</param>
        /// <remarks>Consider preferring <see cref="Divide{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
        """;

    public string AddUnhandledOperatorLHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string AddUnhandledOperatorRHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string SubtractUnhandledOperatorLHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string SubtractUnhandledOperatorRHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string MultiplyUnhandledOperatorLHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string MultiplyUnhandledOperatorRHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string DivideUnhandledOperatorLHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string DivideUnhandledOperatorRHS() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    public string CompareToSameType() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.CompareTo(global::SharpMeasures.Scalar)"/>""";

    public string LessThanSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string GreaterThanSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string LessThanOrEqualSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string GreaterThanOrEqualSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    private string ScalarReference => $"""<see cref="{Type.FullyQualifiedName}"/>""";
    private string UnitReference => $"""<see cref="{Unit.FullyQualifiedName}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    public bool Equals(DefaultDocumentation? other) => other is not null && Type == other.Type && Unit == other.Unit && DefaultUnitInstancePluralForm == other.DefaultUnitInstancePluralForm && DefaultUnitInstanceSymbol == other.DefaultUnitInstanceSymbol && UnitParameterName == other.UnitParameterName
        && ExampleUnitBaseInstanceName == other.ExampleUnitBaseInstanceName && ExampleUnitInstancePluralForm == other.ExampleUnitInstancePluralForm;

    public override bool Equals(object? obj) => obj is DefaultDocumentation other && Equals(other);

    public static bool operator ==(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Type, Unit, DefaultUnitInstancePluralForm, DefaultUnitInstanceSymbol, UnitParameterName, ExampleUnitBaseInstanceName, ExampleUnitInstancePluralForm).GetHashCode();
}
