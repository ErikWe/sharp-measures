namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IDerivedQuantity : IAttributeDefinition
{
    public abstract string Expression { get; }
    public abstract IReadOnlyList<NamedType> Signature { get; }

    public abstract DerivationOperatorImplementation OperatorImplementation { get; }

    public abstract bool Permutations { get; }

    public abstract IReadOnlyList<IExpandedDerivedQuantity> ExpandedScalarResults { get; }
    public abstract IReadOnlyDictionary<int, IReadOnlyList<IExpandedDerivedQuantity>> ExpandedVectorResults { get; }

    new public abstract IDerivedQuantityLocations Locations { get; }
}

public interface IDerivedQuantityLocations : IAttributeLocations
{
    public abstract MinimalLocation? Expression { get; }

    public abstract MinimalLocation? SignatureCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> SignatureElements { get; }

    public abstract MinimalLocation? OperatorImplementation { get; }

    public abstract MinimalLocation? Permutations { get; }

    public abstract bool ExplicitlySetExpression { get; }
    public abstract bool ExplicitlySetSignature { get; }

    public abstract bool ExplicitlySetOperatorImplementation { get; }

    public abstract bool ExplicitlySetPermutations { get; }
}
