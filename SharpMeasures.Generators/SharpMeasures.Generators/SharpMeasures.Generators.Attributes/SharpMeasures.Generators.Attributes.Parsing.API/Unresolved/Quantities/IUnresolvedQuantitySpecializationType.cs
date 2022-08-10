namespace SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedQuantitySpecializationType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedQuantitySpecialization Definition { get; }
}
