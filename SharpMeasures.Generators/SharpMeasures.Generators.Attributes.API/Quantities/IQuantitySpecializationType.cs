namespace SharpMeasures.Generators.Quantities;

public interface IQuantitySpecializationType : IQuantityType
{
    new public abstract IQuantitySpecialization Definition { get; }
}
