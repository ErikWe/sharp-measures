namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, indicating that two instances of the implementing quantity can be added - resulting in a quantity of type <typeparamref name="TSum"/>.</summary>
/// <typeparam name="TSum">The quantity that represents the sum of two instances of the implementing quantity.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantitySumAttribute<TSum> : Attribute { }
