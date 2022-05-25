namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Text.RegularExpressions;

public abstract record class AUnitParsingData<TLocations> : AAttributeParsingData<TLocations>, IUnitParsingData
    where TLocations : AUnitLocations
{
    IUnitLocations IUnitParsingData.Locations => Locations;

    public string InterpretedPlural { get; init; } = string.Empty;

    protected AUnitParsingData(TLocations locations) : base(locations) { }
}
