namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IDerivedQuantity : IAttributeDefinition
{
    public abstract string Expression { get; }
    public abstract IReadOnlyList<NamedType> Signature { get; }

    public abstract bool ImplementOperators { get; }
    public abstract bool ImplementAlgebraicallyEquivalentDerivations { get; }

    new public abstract IDerivedQuantityLocations Locations { get; }
}

public interface IDerivedQuantityLocations : IAttributeLocations
{
    public abstract MinimalLocation? Expression { get; }

    public abstract MinimalLocation? SignatureCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> SignatureElements { get; }

    public abstract MinimalLocation? ImplementOperators { get; }
    public abstract MinimalLocation? ImplementAlgebraicallyEquivalentDerivations { get; }

    public abstract bool ExplicitlySetExpression { get; }
    public abstract bool ExplicitlySetSignature { get; }

    public abstract bool ExplicitlySetImplementOperators { get; }
    public abstract bool ExplicitlySetImplementAlgebraicallyEquivalentDerivations { get; }
}
