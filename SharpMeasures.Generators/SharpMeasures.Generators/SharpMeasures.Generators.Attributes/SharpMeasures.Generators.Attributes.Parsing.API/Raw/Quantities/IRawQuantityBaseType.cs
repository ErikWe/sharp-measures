namespace SharpMeasures.Generators.Raw.Quantities;

public interface IRawQuantityBaseType : IRawQuantityType
{
    new public abstract IRawQuantityBase Definition { get; }
}
