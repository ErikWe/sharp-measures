namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class ParsedUnit
{
    public DefinedType UnitType { get; }
    public MinimalLocation UnitLocation { get; }
    public GeneratedUnitDefinition UnitDefinition { get; }

    public IEnumerable<DerivableUnitDefinition> DerivableUnitDefinitions { get; }

    public IEnumerable<UnitAliasDefinition> UnitAliasDefinitions { get; }
    public IEnumerable<DerivedUnitDefinition> DerivedUnitDefinitions { get; }
    public IEnumerable<FixedUnitDefinition> FixedUnitDefinitions { get; }
    public IEnumerable<OffsetUnitDefinition> OffsetUnitDefinitions { get; }
    public IEnumerable<PrefixedUnitDefinition> PrefixedUnitDefinitions { get; }
    public IEnumerable<ScaledUnitDefinition> ScaledUnitDefinitions { get; }

    public ParsedUnit(DefinedType unitType, MinimalLocation unitLocation, GeneratedUnitDefinition unitDefinition,
        IEnumerable<DerivableUnitDefinition> derivableUnitDefinitions, IEnumerable<UnitAliasDefinition> unitAliasDefinitions,
        IEnumerable<DerivedUnitDefinition> derivedUnitDefinitions, IEnumerable<FixedUnitDefinition> fixedUnitDefinitions,
        IEnumerable<OffsetUnitDefinition> offsetUnitDefinitions, IEnumerable<PrefixedUnitDefinition> prefixedUnitDefinitions,
        IEnumerable<ScaledUnitDefinition> scaledUnitDefinitions)
    {
        UnitType = unitType;
        UnitLocation = unitLocation;
        UnitDefinition = unitDefinition;

        DerivableUnitDefinitions = derivableUnitDefinitions;
        UnitAliasDefinitions = unitAliasDefinitions;
        DerivedUnitDefinitions = derivedUnitDefinitions;
        FixedUnitDefinitions = fixedUnitDefinitions;
        OffsetUnitDefinitions = offsetUnitDefinitions;
        PrefixedUnitDefinitions = prefixedUnitDefinitions;
        ScaledUnitDefinitions = scaledUnitDefinitions;
    }

    public IEnumerable<IUnitDefinition> GetUnitList()
    {
        return (UnitAliasDefinitions as IEnumerable<IUnitDefinition>).Concat(DerivedUnitDefinitions).Concat(FixedUnitDefinitions).Concat(OffsetUnitDefinitions)
            .Concat(PrefixedUnitDefinitions).Concat(ScaledUnitDefinitions);
    }

    public IEnumerable<IUnitDefinition> GetNonDependantUnitList()
    {
        return (DerivedUnitDefinitions as IEnumerable<IUnitDefinition>).Concat(FixedUnitDefinitions);
    }

    public IEnumerable<IDependantUnitDefinition> GetDependantUnitList()
    {
        return (UnitAliasDefinitions as IEnumerable<IDependantUnitDefinition>).Concat(OffsetUnitDefinitions).Concat(PrefixedUnitDefinitions).Concat(ScaledUnitDefinitions);
    }

    public virtual bool Equals(ParsedUnit other)
    {
        if (other is null)
        {
            return false;
        }

        return UnitType == other.UnitType && UnitLocation == other.UnitLocation && UnitDefinition == other.UnitDefinition
            && DerivableUnitDefinitions.SequenceEqual(other.DerivableUnitDefinitions) && UnitAliasDefinitions.SequenceEqual(other.UnitAliasDefinitions)
            && DerivedUnitDefinitions.SequenceEqual(other.DerivedUnitDefinitions) && FixedUnitDefinitions.SequenceEqual(other.FixedUnitDefinitions)
            && OffsetUnitDefinitions.SequenceEqual(other.OffsetUnitDefinitions) && PrefixedUnitDefinitions.SequenceEqual(other.PrefixedUnitDefinitions)
            && ScaledUnitDefinitions.SequenceEqual(other.ScaledUnitDefinitions);
    }

    public override int GetHashCode()
    {
        return (UnitType, UnitLocation, UnitDefinition).GetHashCode() ^ DerivableUnitDefinitions.GetSequenceHashCode() ^ UnitAliasDefinitions.GetSequenceHashCode()
            ^ DerivedUnitDefinitions.GetSequenceHashCode() ^ FixedUnitDefinitions.GetSequenceHashCode() ^ OffsetUnitDefinitions.GetSequenceHashCode()
            ^ PrefixedUnitDefinitions.GetSequenceHashCode() ^ ScaledUnitDefinitions.GetSequenceHashCode();
    }
}
