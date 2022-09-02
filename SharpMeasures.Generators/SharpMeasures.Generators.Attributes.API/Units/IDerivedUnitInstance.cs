namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators;

using System.Collections.Generic;

public interface IDerivedUnitInstance : IUnitInstance
{
    public abstract string? DerivationID { get; }
    public abstract IReadOnlyList<string> Units { get; }

    new public abstract IDerivedUnitInstanceLocations Locations { get; }
}

public interface IDerivedUnitInstanceLocations : IUnitInstanceLocations
{
    public abstract MinimalLocation? DerivationID { get; }
    public abstract MinimalLocation? UnitsCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> UnitsElements { get; }

    public abstract bool ExplicitlySetDerivationID { get; }
    public abstract bool ExplicitlySetUnits { get; }
}
