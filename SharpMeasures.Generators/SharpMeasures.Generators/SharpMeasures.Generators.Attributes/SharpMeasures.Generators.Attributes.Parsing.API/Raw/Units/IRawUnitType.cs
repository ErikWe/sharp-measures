namespace SharpMeasures.Generators.Raw.Units;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

public interface IRawUnitType : ISharpMeasuresObjectType
{
    new public abstract IRawUnit Definition { get; }

    public abstract IRawFixedUnit? FixedUnit { get; }
    public abstract IReadOnlyList<IRawDerivableUnit> UnitDerivations { get; }

    public abstract IReadOnlyList<IRawUnitAlias> UnitAliases { get; }
    public abstract IReadOnlyList<IRawBiasedUnit> BiasedUnits { get; }
    public abstract IReadOnlyList<IRawDerivedUnit> DerivedUnits { get; }
    public abstract IReadOnlyList<IRawPrefixedUnit> PrefixedUnits { get; }
    public abstract IReadOnlyList<IRawScaledUnit> ScaledUnits { get; }

    public abstract IReadOnlyDictionary<string, IRawUnitInstance> UnitsByName { get; }
    public abstract IReadOnlyDictionary<string, IRawUnitInstance> UnitsByPluralName { get; }
    public abstract IReadOnlyDictionary<string, IRawDerivableUnit> DerivationsByID { get; }
}
