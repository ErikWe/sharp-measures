namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Scalars;

using System;

internal class DefaultDocumentation : IDocumentationStrategy, IEquatable<DefaultDocumentation>
{
    private DefinedType UnitType { get; }
    private IUnresolvedScalarType Quantity { get; }

    private bool BiasTerm { get; }

    private string QuantityParameterName { get; }

    public DefaultDocumentation(DataModel model)
    {
        UnitType = model.Unit.Type;
        Quantity = model.Unit.Definition.Quantity;

        BiasTerm = model.Unit.Definition.BiasTerm;

        QuantityParameterName = SourceBuildingUtility.ToParameterName(Quantity.Type.Name);
    }

    public string Header() => BiasTerm switch
    {
        true => $"""/// <summary>A unit of measurement, describing {QuantityReference} together with a <see cref="Scalar"/> bias term.</summary>""",
        false => $"""/// <summary>A unit of measurement, primarily describing {QuantityReference}.</summary>"""
    };

    public string Derivation(UnitDerivationSignature signature)
    {
        var text = $"""/// <summary>Constructs a new {UnitReference}, derived from other units.</summary>""";

        foreach (var unit in signature)
        {
            text = $"""
                {text}
                /// <param name="{SourceBuildingUtility.ToParameterName(unit.Type.Name)}">This <see cref="{unit.Type.Name}"/> is used to derive a {UnitReference}.</param>
                """;
        }

        return text;
    }

    public string Definition(IUnitInstance definition) => BiasTerm switch
    {
        true => $"""/// <summary>A {UnitReference}, describing a certain {QuantityReference} together with a <see cref="Scalar"/> bias term.</summary>""",
        false => $"/// <summary>A {UnitReference}, describing a certain {QuantityReference}.</summary>"
    };

    public string RepresentedQuantity() => $"""/// <summary>The {QuantityReference} described by <see langword="this"/>.</summary>""";
    public string Bias() => """
        /// <summary>The <see cref="Scalar"/> bias term associated with <see langword="this"/>.</summary>
        /// <remarks>This is the value of <see langword="this"/> when a unit with bias { 0 } represents the value { 0 }.</remarks>
        """;

    public string Constructor() => BiasTerm switch
    {
        true => BiasedConstructor(),
        false => UnbiasedConstructor()
    };

    private string BiasedConstructor() => $"""
        /// <summary>Constructs a new {UnitReference}, describing <paremref name="{QuantityParameterName}"/>, together with a bias <paramref name="bias"/>.</summary>
        /// <param name="{QuantityParameterName}">The {QuantityReference} described by the constructed {UnitReference}.</param>
        /// <param name="bias">The <see cref="Scalar"/> bias associated with the constructed {UnitReference}.</param>
        """;
    
    private string UnbiasedConstructor() => $"""
        /// <summary>Constructs a new {UnitReference}, describing <paremref name="{QuantityParameterName}"/>.</summary>
        /// <param name="{QuantityParameterName}">The {QuantityReference} described by the constructed {UnitReference}.</param>
        """;

    public string ScaledBy() => BiasTerm switch
    {
        true => BiasedScaledBy(),
        false => UnbiasedScaledBy()
    };

    private string BiasedScaledBy() => $$"""
        /// <summary>Scales the {{QuantityReference}} described by <see langword="this"/> by <paramref name="scale"/> to derive a new {{UnitReference}}.</summary>
        /// <param name="scale">The described {{QuantityReference}} is scaled by this value.
        /// <para>The bias term associated with <see langword="this"/> is also scaled, but by the reciprocal / inverse of this value.</para></param>
        /// <remarks>The bias term associated with <see langword="this"/> is also scaled, but by { 1 / <paramref name="scale"/> }.
        /// <para>When used together with <see cref="WithBias(Scalar)"/>, the order matters.</para></remarks>
        """;

    private string UnbiasedScaledBy() => $"""
        /// <summary>Scales the {QuantityReference} described by <see langword="this"/> by <paramref name="scale"/> to derive a new {UnitReference}.</summary>
        /// <param name="scale">The described {QuantityReference} is scaled by this value.</param>
        """;

    public string WithBias() => $"""
        /// <summary>Derives a new {UnitReference} that has bias <paramref name="bias"/> relative to <see langword="this"/>.</summary>
        /// <param name="bias">The bias of the derived {UnitReference} relative to <see langword="this"/>.</param>
        /// <remarks>When used together with <see cref="ScaledBy(Scalar)"/> or <see cref="WithPrefix(IPrefix)"/>, the order matters.</remarks>
        """;

    public string WithPrefix() => BiasTerm switch
    {
        true => BiasedWithPrefix(),
        false => UnbiasedWithPrefix()
    };
    
    private string BiasedWithPrefix() => $$"""
        /// <summary>Prefixes the {{QuantityReference}} described by <see langword="this"/> with <paramref name="prefix"/> to derive a new {{UnitReference}}.</summary>
        /// <param name="prefix">The described {{QuantityReference}} is prefixed by this <see cref="IPrefix"/>.</param>
        /// <remarks>The bias term associated with <see langword="this"/> is also scaled, but by the reciprocal / inverse of the scale-factor of <paramref name="prefix"/>.
        /// <para>Repeated invokation will <i>stack</i> the prefixes, rather than <i>replace</i> the previously applied <see cref="IPrefix"/>.</para>
        /// <para>When used together with <see cref="WithBias(Scalar)"/>, the order matters.</para></remarks>
        """;

    private string UnbiasedWithPrefix() => $$"""
        /// <summary>Prefixes the {{QuantityReference}} described by <see langword="this"/> with <paramref name="prefix"/> to derive a new {{UnitReference}}.</summary>
        /// <param name="prefix">The described {{QuantityReference}} is prefixed by this <see cref="IPrefix"/>.</param>
        /// <remarks>Repeated invokation will <i>stack</i> the prefixes, rather than <i>replace</i> the previously applied <see cref="IPrefix"/>.</remarks>
        """;

    public string ToStringDocumentation() => BiasTerm switch
    {
        true => BiasedToStringDocumentation(),
        false => UnbiasedToStringDocumentation()
    };

    private string BiasedToStringDocumentation() => $"""
        ///<summary>Produces a description of <see langword="this"/> containing the type, the described {QuantityReference}, and the associated bias.</summary>
        """;

    private string UnbiasedToStringDocumentation() => $"""
        ///<summary>Produces a description of <see langword="this"/> containing the type and the described {QuantityReference}.</summary>
        """;

    public string EqualsSameTypeMethod() => InheritDoc;
    public string EqualsObjectMethod() => InheritDoc;

    public string EqualitySameTypeOperator()
    {
        string text = $"""/// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent {QuantityReference}""";

        if (BiasTerm)
        {
            text += " and bias";
        }

        return $"""
            {text}.</summary>
            /// <param name="lhs">The left-hand side of the equality check.</param>
            /// <param name="rhs">The right-hand side of the equality check.</param>
            """;
    }

    public string InequalitySameTypeOperator()
    {
        string text = $"""/// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent {QuantityReference}""";

        if (BiasTerm)
        {
            text += " or bias";
        }

        return $"""
            {text}.</summary>
            /// <param name="lhs">The left-hand side of the inequality check.</param>
            /// <param name="rhs">The right-hand side of the inequality check.</param>
            """;
    }

    public string GetHashCodeDocumentation() => InheritDoc;

    public string CompareToSameType() => """/// <inheritdoc cref="Scalar.CompareTo(Scalar)"/>""";

    public string LessThanSameType() => $$"""/// <inheritdoc cref="Scalar.operator &lt;(Scalar, Scalar)"/>""";
    public string GreaterThanSameType() => $$"""/// <inheritdoc cref="Scalar.operator &gt;(Scalar, Scalar)"/>""";
    public string LessThanOrEqualSameType() => $$"""/// <inheritdoc cref="Scalar.operator &lt;=(Scalar, Scalar)"/>""";
    public string GreaterThanOrEqualSameType() => $$"""/// <inheritdoc cref="Scalar.operator &gt;=(Scalar, Scalar)"/>""";

    private string UnitReference => $"""<see cref="{UnitType.Name}"/>""";
    private string QuantityReference => $"""<see cref="{Quantity.Type.FullyQualifiedName}"/>""";

    private static string InheritDoc => "/// <inheritdoc/>";

    public virtual bool Equals(DefaultDocumentation? other) => other is not null && UnitType == other.UnitType && Quantity == other.Quantity
        && BiasTerm == other.BiasTerm;

    public override bool Equals(object? obj) => obj is DefaultDocumentation other && Equals(other);

    public static bool operator ==(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (UnitType, Quantity, BiasTerm).GetHashCode();
}
