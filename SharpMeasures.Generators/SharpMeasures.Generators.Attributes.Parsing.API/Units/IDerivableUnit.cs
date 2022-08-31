namespace SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IDerivableUnit
{
    public abstract string? DerivationID { get; }

    public abstract string Expression { get; }
    public abstract IReadOnlyList<NamedType> Signature { get; }

    public abstract bool Permutations { get; }
}
