namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(Length), Vector = typeof(DisplacementN))]
public readonly partial record struct Distance { }
