namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.SourceBuilding;

using System;

internal class DefaultDocumentation : IDocumentationStrategy, IEquatable<DefaultDocumentation>
{
    private DefinedType UnitType { get; }
    private ScalarInterface Quantity { get; }

    private bool SupportsBiasedQuantities { get; }

    private string QuantityParameterName { get; }

    public DefaultDocumentation(DataModel model)
    {
        UnitType = model.UnitData.UnitType;
        Quantity = model.UnitDefinition.Quantity;

        SupportsBiasedQuantities = model.UnitDefinition.SupportsBiasedQuantities;

        QuantityParameterName = SourceBuildingUtility.ToParameterName(UnitType.Name);
    }

    public string Header() => SupportsBiasedQuantities switch
    {
        true => $"""/// <summary>A unit of measurement, describing {QuantityReference} together with a <see cref="Scalar"/> offset.</summary>""",
        false => $"""/// <summary>A unit of measurement, primarily describing {QuantityReference}.</summary>"""
    };

    public string Derivation(DerivableSignature signature)
    {
        var text = $"""/// <summary>Constructs a new {UnitReference}, derived from other units.</summary>""";

        foreach (var unit in signature)
        {
            text = $"""
                {text}
                /// <param name="{SourceBuildingUtility.ToParameterName(unit.Name)}">This <see cref="{unit.Name}"/> is used to derive a {UnitReference}.</param>
                """;
        }

        return text;
    }

    public string Definition(IUnitDefinition definition) => SupportsBiasedQuantities switch
    {
        true => $"""/// <summary>A {UnitReference}, describing a certain {QuantityReference} together with a <see cref="Scalar"/> offset.</summary>""",
        false => $"/// <summary>A {UnitReference}, describing a certain {QuantityReference}.</summary>"
    };

    public string RepresentedQuantity() => $"""/// <summary>The {QuantityReference} described by <see langword="this"/>.</summary>""";
    public string Offset() => """/// <summary>The <see cref="Scalar"/> offset described by <see langword="this"/>.</summary>""";

    public string Constructor() => SupportsBiasedQuantities switch
    {
        true => BiasedConstructor(),
        false => UnbiasedConstructor()
    };

    private string BiasedConstructor() => $"""
        /// <summary>Constructs a new {UnitReference}, describing <paremref name="{QuantityParameterName}"/>, together with a offset <paramref name="offset"/>.</summary>
        /// <param name="{QuantityParameterName}">The {QuantityReference} described by the constructed {UnitReference}.</param>
        /// <param name="offset">The <see cref="Scalar"/> offset described by the constructed {UnitReference}.</param>
        """;
    
    private string UnbiasedConstructor() => $"""
        /// <summary>Constructs a new {UnitReference}, describing <paremref name="{QuantityParameterName}"/>.</summary>
        /// <param name="{QuantityParameterName}">The {QuantityReference} described by the constructed {UnitReference}.</param>
        """;

    public string ScaledBy() => SupportsBiasedQuantities switch
    {
        true => BiasedScaledBy(),
        false => UnbiasedScaledBy()
    };

    private string BiasedScaledBy() => $$"""
        /// <summary>Scales the {{QuantityReference}} described by <see langword="this"/> by <paramref name="scale"/> to derive a new {{UnitReference}}.</summary>
        /// <param name="scale">The described {{QuantityReference}} is scaled by this value.
        /// <para>The described offset is also scaled, but by the reciprocal / inverse of this value.</para></param>
        /// <remarks>The offset described by <see langword="this"/> is also scaled, but by { 1 / <paramref name="scale"/> }.
        /// <para>When used together with <see cref="OffsetBy(Scalar)"/>, the order matters.</para></remarks>
        """;

    private string UnbiasedScaledBy() => $"""
        /// <summary>Scales <see langword="this"/> by <paramref name="scale"/> to derive a new {UnitReference}.</summary>
        /// <param name="scale">The described {QuantityReference} is scaled by this value.</param>
        """;

    public string OffsetBy() => $"""
        /// <summary>Offsets <see langword="this"/> by <paramref name="offset"/> to derive a new {UnitReference}.</summary>
        /// <param name="offset">The difference in offset between <see langword="this"/> and the derived {UnitReference}.</param>
        /// <remarks>When used together with <see cref="ScaledBy(Scalar)"/> or <see cref="WithPrefix(IPrefix)"/>, the order matters.</remarks>
        """;

    public string WithPrefix() => SupportsBiasedQuantities switch
    {
        true => BiasedWithPrefix(),
        false => UnbiasedWithPrefix()
    };
    
    private string BiasedWithPrefix() => $"""
        /// <summary>Prefixes <see langword="this"/> with <paramref name="prefix"/> to derive a new {UnitReference}.</summary>
        /// <param name="prefix">The described {QuantityReference} is prefixed by this <see cref="IPrefix"/>.</param>
        /// <remarks>The offset described by <see langword="this"/> is also affected, but by the reciprocal / inverse of the scale-factor of <paramref name="prefix"/>.
        /// <para>Repeated invokation of <see cref="WithPrefix(IPrefix)"/> will not <i>replace</i> the previously applied <see cref="IPrefix"/>, but
        /// <i>stack</i> them.</para>
        /// <para>When used together with <see cref="OffsetBy(Scalar)"/>, the order matters.</para></remarks>
        """;

    private string UnbiasedWithPrefix() => $"""
        /// <summary>Prefixes <see langword="this"/> with <paramref name="prefix"/> to derive a new {UnitReference}.</summary>
        /// <param name="prefix">The described {QuantityReference} is prefixed by this <see cref="IPrefix"/>.</param>
        /// <remarks>Repeated invokation of <see cref="WithPrefix(IPrefix)"/> will not <i>replace</i> the previously applied <see cref="IPrefix"/>, but
        /// <i>stack</i> them.</remarks>
        """;

    public string ToStringDocumentation() => SupportsBiasedQuantities switch
    {
        true => BiasedToStringDocumentation(),
        false => UnbiasedToStringDocumentation()
    };

    private string BiasedToStringDocumentation() => $"""
        ///<summary>Produces a description of <see langword="this"/> containing the type, the described {QuantityReference}, and the described offset.</summary>
        """;

    private string UnbiasedToStringDocumentation() => $"""
        ///<summary>Produces a description of <see langword="this"/> containing the type and the described {QuantityReference}.</summary>
        """;

    public string CompareToSameType() => """/// <inheritdoc cref="Scalar.CompareTo(Scalar)"/>""";

    public string LessThanSameType() => $$"""/// <inheritdoc cref="Scalar.operator &lt;(Scalar, Scalar)"/>""";
    public string GreaterThanSameType() => $$"""/// <inheritdoc cref="Scalar.operator &gt;(Scalar, Scalar)"/>""";
    public string LessThanOrEqualSameType() => $$"""/// <inheritdoc cref="Scalar.operator &lt=;(Scalar, Scalar)"/>""";
    public string GreaterThanOrEqualSameType() => $$"""/// <inheritdoc cref="Scalar.operator &gt=;(Scalar, Scalar)"/>""";

    private string UnitReference => $"""<see cref="{UnitType.Name}"/>""";
    private string QuantityReference => $"""<see cref="{Quantity.ScalarType.Name}"/>""";

    public virtual bool Equals(DefaultDocumentation? other)
    {
        if (other is null)
        {
            return false;
        }

        return UnitType == other.UnitType && Quantity == other.Quantity && SupportsBiasedQuantities == other.SupportsBiasedQuantities;
    }

    public override bool Equals(object obj)
    {
        if (obj is DefaultDocumentation other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultDocumentation? lhs, DefaultDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (UnitType, Quantity, SupportsBiasedQuantities).GetHashCode();
}
