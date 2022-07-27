namespace SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedQuantitySpecializationType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedQuantitySpecialization Definition { get; }
}
