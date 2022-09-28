namespace SharpMeasures.Generators.Quantities;

public interface IQuantitySpecialization : IQuantity
{
    public abstract NamedType OriginalQuantity { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }

    public abstract ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; }
    public abstract ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; }

    new public abstract IQuantitySpecializationLocations Locations { get; }
}

public interface IQuantitySpecializationLocations : IQuantityLocations
{
    public abstract MinimalLocation? OriginalQuantity { get; }

    public abstract MinimalLocation? InheritDerivations { get; }
    public abstract MinimalLocation? InheritConversions { get; }
    public abstract MinimalLocation? InheritUnits { get; }

    public abstract MinimalLocation? ForwardsCastOperatorBehaviour { get; }
    public abstract MinimalLocation? BackwardsCastOperatorBehaviour { get; }

    public abstract bool ExplicitlySetOriginalQuantity { get; }

    public abstract bool ExplicitlySetInheritDerivations { get; }
    public abstract bool ExplicitlySetInheritConversions { get; }
    public abstract bool ExplicitlySetInheritUnits { get; }

    public abstract bool ExplicitlySetForwardsCastOperatorBehaviour { get; }
    public abstract bool ExplicitlySetBackwardsCastOperatorBehaviour { get; }
}
