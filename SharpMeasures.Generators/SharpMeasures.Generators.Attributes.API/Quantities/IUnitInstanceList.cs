namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IUnitInstanceList
{
    public abstract IReadOnlyList<string> UnitInstances { get; }

    public abstract IUnitInstanceListLocations Locations { get; }
}

public interface IUnitInstanceListLocations
{
    public abstract MinimalLocation? UnitInstancesCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> UnitInstancesElements { get; }
    
    public abstract bool ExplicitlySetUnitInstances { get; }
}
