namespace SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedSpecializedQuantityType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedSpecializedQuantity Definition { get; }
}
