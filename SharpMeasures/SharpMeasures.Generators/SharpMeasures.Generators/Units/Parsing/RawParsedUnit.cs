namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class RawParsedUnit
{
    public DefinedType UnitType { get; }
    public MinimalLocation UnitLocation { get; }
    public GeneratedUnitDefinition UnitDefinition { get; }

    public IEnumerable<RawDerivableUnitDefinition> DerivableUnitDefinitions { get; }

    public IEnumerable<RawUnitAliasDefinition> UnitAliasDefinitions { get; }
    public IEnumerable<RawDerivedUnitDefinition> DerivedUnitDefinitions { get; }
    public IEnumerable<RawFixedUnitDefinition> FixedUnitDefinitions { get; }
    public IEnumerable<RawOffsetUnitDefinition> OffsetUnitDefinitions { get; }
    public IEnumerable<RawPrefixedUnitDefinition> PrefixedUnitDefinitions { get; }
    public IEnumerable<RawScaledUnitDefinition> ScaledUnitDefinitions { get; }

    public RawParsedUnit(DefinedType unitType, MinimalLocation unitLocation, GeneratedUnitDefinition unitDefinition,
        IEnumerable<RawDerivableUnitDefinition> derivableUnitDefinitions, IEnumerable<RawUnitAliasDefinition> unitAliasDefinitions,
        IEnumerable<RawDerivedUnitDefinition> derivedUnitDefinitions, IEnumerable<RawFixedUnitDefinition> fixedUnitDefinitions,
        IEnumerable<RawOffsetUnitDefinition> offsetUnitDefinitions, IEnumerable<RawPrefixedUnitDefinition> prefixedUnitDefinitions,
        IEnumerable<RawScaledUnitDefinition> scaledUnitDefinitions)
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

    public IEnumerable<IRawUnitDefinition> GetUnitList()
    {
        return (UnitAliasDefinitions as IEnumerable<IRawUnitDefinition>).Concat(DerivedUnitDefinitions).Concat(FixedUnitDefinitions).Concat(OffsetUnitDefinitions)
            .Concat(PrefixedUnitDefinitions).Concat(ScaledUnitDefinitions);
    }

    public IEnumerable<IRawUnitDefinition> GetNonDependantUnitList()
    {
        return (DerivedUnitDefinitions as IEnumerable<IRawUnitDefinition>).Concat(FixedUnitDefinitions);
    }

    public IEnumerable<IRawDependantUnitDefinition> GetDependantUnitList()
    {
        return (UnitAliasDefinitions as IEnumerable<IRawDependantUnitDefinition>).Concat(OffsetUnitDefinitions).Concat(PrefixedUnitDefinitions).Concat(ScaledUnitDefinitions);
    }

    public virtual bool Equals(RawParsedUnit other)
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
