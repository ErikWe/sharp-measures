namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures vector quantities, describing the quantity as associated with a scalar quantity, <typeparamref name="TScalar"/>.</summary>
/// <typeparam name="TScalar">The scalar quantity associated with the vector quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class ScalarAssociationAttribute<TScalar> : Attribute
{
    /// <summary>Dictates whether the provided <typeparamref name="TScalar"/> should be used to describe each Cartesian component of the vector quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool AsComponents { get; init; }

    /// <summary>Dictates whether the provided <typeparamref name="TScalar"/> should be used to describe the magnitude of the vector quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool AsMagnitude { get; init; }
}
