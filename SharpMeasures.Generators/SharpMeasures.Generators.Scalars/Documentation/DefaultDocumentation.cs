namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

using System;
using System.Globalization;
using System.Text;

internal class DefaultDocumentation : IDocumentationStrategy, IEquatable<DefaultDocumentation>
{
    private DefinedType Type { get; }
    private IUnitType Unit { get; }

    private IUnitInstance? DefaultUnitInstance { get; }
    private string? DefaultUnitInstanceSymbol { get; }

    private string UnitParameterName { get; }

    private IUnitInstance? ExampleUnitBaseInstance { get; }
    private IUnitInstance? ExampleUnitInstance { get; }

    public DefaultDocumentation(DataModel model)
    {
        Type = model.Scalar.Type;
        Unit = model.UnitPopulation.Units[model.Scalar.Unit];

        DefaultUnitInstance = GetDefaultUnitInstance(model);
        DefaultUnitInstanceSymbol = model.Scalar.DefaultUnitInstanceSymbol;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.Type.Name);

        ExampleUnitBaseInstance = model.Scalar.IncludedUnitBaseInstancesNames.Count > 0 ? Unit.UnitInstancesByName[model.Scalar.IncludedUnitBaseInstancesNames[0]] : null;
        ExampleUnitInstance = model.Scalar.IncludedUnitInstanceNames.Count > 0 ? Unit.UnitInstancesByName[model.Scalar.IncludedUnitInstanceNames[0]] : null;
    }

    private IUnitInstance? GetDefaultUnitInstance(DataModel model)
    {
        if (model.Scalar.DefaultUnitInstanceName is not null && Unit.UnitInstancesByName.TryGetValue(model.Scalar.DefaultUnitInstanceName, out var defaultUnitInstance))
        {
            return defaultUnitInstance;
        }

        return null;
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

        return $$"""/// <summary>The {{ScalarReference}} representing the constant {{constant.Name}}, equivalent to { {{value}} [<see cref="{{Unit.Type.FullyQualifiedName}}.{{constant.UnitInstanceName}}"/>] }.</summary>""";
    }

    public string UnitBase(IUnitInstance unitInstance)
        => $$"""/// <summary>The {{ScalarReference}} representing { 1 [<see cref="{{Unit.Type.FullyQualifiedName}}.{{unitInstance.Name}}"/>] }.</summary>""";

    public string WithMagnitude() => InheritDoc;

    public string FromReciprocal(NamedType reciprocal)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(reciprocal.Name);
    
        return $$"""
            /// <summary>Computes { 1 / <paramref name="{{parameterName}}"/> }.</summary>
            /// <param name="{{parameterName}}">The reciprocal of the computed {{ScalarReference}}.</param>
            """;
    }

    public string FromSquare(NamedType square)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(square.Name);

        return $$"""
            /// <summary>Computes { √ <paramref name="{{parameterName}}"/> }.</summary>
            /// <param name="{{parameterName}}">The square of the computed {{ScalarReference}}.</param>
            """;
    }

    public string FromCube(NamedType cube)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(cube.Name);

        return $$"""
            /// <summary>Computes { ³√ <paramref name="{{parameterName}}"/> }.</summary>
            /// <param name="{{parameterName}}">The cube of the computed {{ScalarReference}}.</param>
            """;
    }

    public string FromSquareRoot(NamedType squareRoot)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(squareRoot.Name);

        return $$"""
            /// <summary>Computes { <paramref name="{{parameterName}}"/> ² }.</summary>
            /// <param name="{{parameterName}}">The square root of the computed {{ScalarReference}}.</param>
            """;
    }

    public string FromCubeRoot(NamedType cubeRoot)
    {
        var parameterName = SourceBuildingUtility.ToParameterName(cubeRoot.Name);

        return $$"""
            /// <summary>Computes { <paramref name="{{parameterName}}"/> ³ }.</summary>
            /// <param name="{{parameterName}}">The cube root of the computed {{ScalarReference}}.</param>
            """;
    }

    public string Derivation(DerivedQuantitySignature signature)
    {
        StringBuilder source = new();
        source.Append($"""/// <summary>Constructs a new {ScalarReference}, derived from other quantities.</summary>""");

        foreach (var quantity in signature)
        {
            source.AppendLine();

            source.Append($"""
                /// <param name="{SourceBuildingUtility.ToParameterName(quantity.Name)}">This <see cref="{quantity.FullyQualifiedName}"/> is used to derive a {ScalarReference}.</param>
                """);
        }

        return source.ToString();
    }

    public string OperatorDerivationLHS(OperatorDerivation derivation)
    {
        var operatorSymbol = GetOperatorSymbol(derivation.OperatorType);
        (var firstComponentName, var secondComponentName) = GetOperatorComponentNames(derivation.OperatorType);

        return $$"""
            /// <summary>Computes { <paramref name="a"/> {{operatorSymbol}} <paramref name="b"/> }, resulting in a <see cref="{{derivation.Result.FullyQualifiedName}}"/>.</summary>
            /// <param name="a">The {{(firstComponentName == secondComponentName ? string.Empty : "first ")}}{{firstComponentName}} of { <paramref name="a"/> {{operatorSymbol}} <paramref name="b"/> }.</param>
            /// <param name="b">The {{(firstComponentName == secondComponentName ? string.Empty : "second ")}}{{secondComponentName}} of { <paramref name="a"/> {{operatorSymbol}} <paramref name="b"/> }.</param>
            """;
    }

    public string Magnitude()
    {
        var commonText = $"""
            /// <summary>The magnitude of <see langword="this"/>, expressed in an arbitrary unit.</summary>
            /// <remarks>In most cases, expressing the magnitude in a specified {UnitReference} should be preferred. This is achieved through <see cref="InUnit({Unit.Type.FullyQualifiedName})"/>
            """;

        if (ExampleUnitInstance is not null)
        {
            commonText = $"""{commonText} or an associated property - for example, <see cref="{ExampleUnitInstance.PluralForm}"/>""";
        }

        return $"{commonText}.</remarks>";
    }

    public string ScalarConstructor() => $$"""
        /// <summary>Constructs a new {{ScalarReference}} representing { <paramref name="magnitude"/> }, expressed in an arbitrary unit.</summary>
        /// <param name="magnitude">The magnitude represented by the constructed {{ScalarReference}}, expressed in an arbitrary unit.</param>
        /// <remarks>Consider preferring construction through <see cref="{{Type.FullyQualifiedName}}(global::SharpMeasures.Scalar, {{Unit.Type.FullyQualifiedName}})"/>, where the magnitude is expressed in
        /// a specified {{UnitReference}}.</remarks>
        """;

    public string ScalarAndUnitConstructor()
    {
        var commonText = $$"""
            /// <summary>Constructs a new {{ScalarReference}} representing { <paramref name="magnitude"/> [<paramref name="{{UnitParameterName}}"/>] }.</summary>
            /// <param name="magnitude">The magnitude represented by the constructed {{ScalarReference}}, when expressed in <paramref name="{{UnitParameterName}}"/>.</param>
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which <paramref name="magnitude"/> is expressed.</param>
            """;

        if (ExampleUnitBaseInstance is not null)
        {
            return $"""
                {commonText}
                /// <remarks>A {ScalarReference} may also be constructed as demonstrated below.
                /// <code>{ScalarReference} x = 2.3 * <see cref="{Type.FullyQualifiedName}.One{ExampleUnitBaseInstance.Name}"/>;</code>
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

    public string InSpecifiedUnit(IUnitInstance unitInstance) => $"""
        /// <summary>The magnitude of <see langword="this"/>, expressed in <see cref="{Unit.Type.FullyQualifiedName}.{unitInstance.Name}"/>.</summary>
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

    public string Reciprocal() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Reciprocal()"/>""";
    public string Square() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Square()"/>""";
    public string Cube() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Cube()"/>""";
    public string SquareRoot() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.SquareRoot()"/>""";
    public string CubeRoot() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.CubeRoot()"/>""";

    public string ToStringDocumentation()
    {
        var commonText = $"""/// <summary>Produces a description of <see langword="this"/> containing the""";

        if (DefaultUnitInstance is not null && DefaultUnitInstanceSymbol is not null)
        {
            return $"""{commonText} magnitude expressed in <see cref="{Unit.Type.FullyQualifiedName}.{DefaultUnitInstance.Name}"/>, followed by the symbol [{DefaultUnitInstanceSymbol}].</summary>""";
        }

        if (DefaultUnitInstance is not null)
        {
            return $"""{commonText} magnitude expressed in <see cref="{Unit.Type.FullyQualifiedName}.{DefaultUnitInstance.Name}"/>.</summary>""";
        }

        return $"""{commonText} magnitude expressed in an arbitrary unit.</summary>""";
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

    public string MultiplySameTypeMethod() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Multiply(global::SharpMeasures.Scalar)"/>""";
    public string DivideSameTypeMethod() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.Divide(global::SharpMeasures.Scalar)"/>""";

    public string MultiplyVectorMethod(int dimension) => $$"""/// <inheritdoc cref="Scalar.Multiply(global::SharpMeasures.Vector{{dimension}})"/>""";

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
    public string DivideScalarOperatorRHS() => InheritDoc;

    public string MultiplySameTypeOperator() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string DivideSameTypeOperator() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    public string MultiplyVectorOperatorLHS(int dimension) => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{dimension}}.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Vector{{dimension}})"/>""";
    public string MultiplyVectorOperatorRHS(int dimension) => $$"""/// <inheritdoc cref="global::SharpMeasures.Vector{{dimension}}.operator *(global::SharpMeasures.Vector{{dimension}}, global::SharpMeasures.Scalar)"/>""";

    public string CompareToSameType() => """/// <inheritdoc cref="global::SharpMeasures.Scalar.CompareTo(global::SharpMeasures.Scalar)"/>""";

    public string LessThanSameType()=> $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string GreaterThanSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string LessThanOrEqualSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";
    public string GreaterThanOrEqualSameType() => $$"""/// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>""";

    private string ScalarReference => $"""<see cref="{Type.FullyQualifiedName}"/>""";
    private string UnitReference => $"""<see cref="{Unit.Type.FullyQualifiedName}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    private static string GetOperatorSymbol(OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Addition => "+",
            OperatorType.Subtraction => "-",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            _ => throw new NotSupportedException($"Invalid {typeof(OperatorType).Name}: {operatorType}")
        };
    }

    private static (string First, string Second) GetOperatorComponentNames(OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Addition => ("term", "term"),
            OperatorType.Subtraction => ("term", "term"),
            OperatorType.Multiplication => ("factor", "factor"),
            OperatorType.Division => ("dividend", "divisor"),
            _ => throw new NotSupportedException($"Invalid {typeof(OperatorType).Name}: {operatorType}")
        };
    }

    public virtual bool Equals(DefaultDocumentation? other) => other is not null && Type == other.Type && Unit == other.Unit && DefaultUnitInstance == other.DefaultUnitInstance
        && DefaultUnitInstanceSymbol == other.DefaultUnitInstanceSymbol && UnitParameterName == other.UnitParameterName && ExampleUnitBaseInstance == other.ExampleUnitBaseInstance && ExampleUnitInstance == other.ExampleUnitInstance;

    public override bool Equals(object? obj) => obj is DefaultDocumentation other && Equals(other);

    public static bool operator ==(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Type, Unit, DefaultUnitInstance, DefaultUnitInstanceSymbol, UnitParameterName, ExampleUnitBaseInstance, ExampleUnitInstance).GetHashCode();
}
