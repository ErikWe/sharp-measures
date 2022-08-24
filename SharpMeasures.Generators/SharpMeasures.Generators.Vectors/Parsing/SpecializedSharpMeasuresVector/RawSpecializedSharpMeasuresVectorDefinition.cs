﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class RawSpecializedSharpMeasuresVectorDefinition : ARawAttributeDefinition<RawSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations>, IDefaultUnitDefinition
{
    public static RawSpecializedSharpMeasuresVectorDefinition Empty => new();

    public NamedType? OriginalVector { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public NamedType? Scalar { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresVectorDefinition Definition => this;

    IDefaultUnitLocations IDefaultUnitDefinition.DefaultUnitLocations => Locations;

    private RawSpecializedSharpMeasuresVectorDefinition() : base(SpecializedSharpMeasuresVectorLocations.Empty) { }
}