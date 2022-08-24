namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IDerivedQuantity
{
    public abstract string Expression { get; }
    public abstract IReadOnlyList<NamedType> Signature { get; }
}
