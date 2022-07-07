namespace SharpMeasures.Generators.Unresolved.Units;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

public interface IUnresolvedUnitType
{
    public abstract DefinedType Type { get; }
    public abstract IUnresolvedUnit UnitDefinition { get; }

    public abstract IUnresolvedFixedUnit? FixedUnit { get; }
    public abstract IReadOnlyList<IUnresolvedDerivableUnit> UnitDerivations { get; }

    public abstract IReadOnlyList<IUnresolvedUnitAlias> UnitAliases { get; }
    public abstract IReadOnlyList<IUnresolvedBiasedUnit> BiasedUnits { get; }
    public abstract IReadOnlyList<IUnresolvedDerivedUnit> DerivedUnits { get; }
    public abstract IReadOnlyList<IUnresolvedPrefixedUnit> PrefixedUnits { get; }
    public abstract IReadOnlyList<IUnresolvedScaledUnit> ScaledUnits { get; }

    public abstract IReadOnlyDictionary<string, IUnresolvedUnitInstance> UnitsByName { get; }
    public abstract IReadOnlyDictionary<string, IUnresolvedDerivableUnit> DerivationsByID { get; }
}
