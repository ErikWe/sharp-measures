namespace SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IDerivableUnit : IAttributeDefinition
{
    public abstract string? DerivationID { get; }

    public abstract string Expression { get; }
    public abstract IReadOnlyList<NamedType> Signature { get; }

    public abstract bool Permutations { get; }

    new public abstract IDerivableUnitLocations Locations { get; }
}

public interface IDerivableUnitLocations : IAttributeLocations
{
    public abstract MinimalLocation? DerivationID { get; }

    public abstract MinimalLocation? Expression { get; }
    public abstract MinimalLocation? SignatureCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> SignatureElements { get; }

    public abstract MinimalLocation? Permutations { get; }

    public abstract bool ExplicitlySetDerivationID { get; }

    public abstract bool ExplicitlySetExpression { get; }
    public abstract bool ExplicitlySetSignature { get; }

    public abstract bool ExplicitlySetPermutations { get; }
}
