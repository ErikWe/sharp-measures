namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, indicating that the difference between two instances of the implementing quantity results in a quantity of type <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TDifference">The quantity that represents the difference between two instances of the implementing quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantityDifferenceAttribute<TDifference> : Attribute { }
