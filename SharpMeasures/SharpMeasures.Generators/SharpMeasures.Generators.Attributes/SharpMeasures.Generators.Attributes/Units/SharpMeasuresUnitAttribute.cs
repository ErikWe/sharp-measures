namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Marks the type as a unit, and allows a source generator to implement relevant functionality.</summary>
/// <remarks>Accompanying attributes, such as <see cref="Units.DerivableUnitAttribute"/> and <see cref="Units.FixedUnitAttribute"/>, may be used to extend
/// the source generation.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SharpMeasuresUnitAttribute : Attribute
{
    /// <summary>The scalar quantity that the unit describes.</summary>
    /// <remarks>For units that support biased quantities, this should represent an associated unbiased quantity.</remarks>
    public Type Quantity { get; }
    
    /// <summary>Dictates whether this unit should include a bias term.</summary>
    public bool SupportsBiasedQuantities { get; init; }

    /// <summary>Dictates whether documentation should be generated for this unit.</summary>
    /// <remarks>If this property is not explicitly set, the entry [<i>SharpMeasures_GenerateDocumentation</i>] in the global AnalyzerConfig
    /// file is used to determine whether documentation is generated - which in turn is <see langword="true"/> by default.</remarks>
    public bool GenerateDocumentation { get; init; }

    /// <summary>Marks the type as a unit, and allows a source generator to implement relevant functionality.</summary>
    /// <param name="quantity">The scalar quantity that the unit describes.
    /// <para>For units that support biased quantities, this should represent an associated unbiased quantity.</para></param>
    public SharpMeasuresUnitAttribute(Type quantity)
    {
        Quantity = quantity;
    }
}
