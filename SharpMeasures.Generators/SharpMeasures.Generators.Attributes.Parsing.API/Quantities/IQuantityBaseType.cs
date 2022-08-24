namespace SharpMeasures.Generators.Quantities;

public interface IQuantityBaseType : IQuantityType
{
    new public abstract IQuantityBase Definition { get; }
}
