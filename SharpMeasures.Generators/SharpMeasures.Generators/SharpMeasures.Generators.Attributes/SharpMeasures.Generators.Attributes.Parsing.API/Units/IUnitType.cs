namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

public interface IUnitType : ISharpMeasuresObjectType
{
    new public abstract IUnit Definition { get; }

    public abstract IFixedUnit? FixedUnit { get; }
    public abstract IReadOnlyList<IDerivableUnit> UnitDerivations { get; }

    public abstract IReadOnlyList<IUnitAlias> UnitAliases { get; }
    public abstract IReadOnlyList<IBiasedUnit> BiasedUnits { get; }
    public abstract IReadOnlyList<IDerivedUnit> DerivedUnits { get; }
    public abstract IReadOnlyList<IPrefixedUnit> PrefixedUnits { get; }
    public abstract IReadOnlyList<IScaledUnit> ScaledUnits { get; }

    public abstract IReadOnlyDictionary<string, IUnitInstance> UnitsByName { get; }
    public abstract IReadOnlyDictionary<string, IUnitInstance> UnitsByPluralName { get; }
    public abstract IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }
}
