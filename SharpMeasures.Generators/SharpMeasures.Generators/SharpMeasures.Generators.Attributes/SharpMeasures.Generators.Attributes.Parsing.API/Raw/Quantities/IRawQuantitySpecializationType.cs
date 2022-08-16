namespace SharpMeasures.Generators.Raw.Quantities;

public interface IRawQuantitySpecializationType : IRawQuantityType
{
    new public abstract IRawQuantitySpecialization Definition { get; }
}
