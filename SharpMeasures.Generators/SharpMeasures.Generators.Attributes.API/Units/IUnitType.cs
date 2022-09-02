namespace SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IUnitType : ISharpMeasuresObjectType
{
    new public abstract IUnit Definition { get; }

    public abstract IFixedUnitInstance? FixedUnitInstance { get; }
    public abstract IReadOnlyList<IDerivableUnit> UnitDerivations { get; }

    public abstract IReadOnlyList<IUnitInstanceAlias> UnitInstanceAliases { get; }
    public abstract IReadOnlyList<IBiasedUnitInstance> BiasedUnitInstances { get; }
    public abstract IReadOnlyList<IDerivedUnitInstance> DerivedUnitInstances { get; }
    public abstract IReadOnlyList<IPrefixedUnitInstance> PrefixedUnitInstances { get; }
    public abstract IReadOnlyList<IScaledUnitInstance> ScaledUnitInstances { get; }

    public abstract IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByName { get; }
    public abstract IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByPluralForm { get; }
    public abstract IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }
}
