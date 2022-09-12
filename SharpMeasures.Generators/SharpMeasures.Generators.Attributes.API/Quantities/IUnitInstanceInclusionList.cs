namespace SharpMeasures.Generators.Quantities;

public interface IUnitInstanceInclusionList : IUnitInstanceList
{
    public abstract InclusionStackingMode StackingMode { get; }

    new public abstract IUnitInstanceInclusionListLocations Locations { get; }
}

public interface IUnitInstanceInclusionListLocations : IUnitInstanceListLocations
{
    public abstract MinimalLocation? StackingMode { get; }

    public abstract bool ExplicitlySetStackingMode { get; }
}
