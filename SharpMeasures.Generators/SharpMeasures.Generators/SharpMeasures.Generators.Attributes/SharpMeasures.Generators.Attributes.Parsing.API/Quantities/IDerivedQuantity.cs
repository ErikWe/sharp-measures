namespace SharpMeasures.Generators.Quantities;

public interface IDerivedQuantity
{
    public abstract string Expression { get; }
    public abstract QuantityDerivationSignature Signature { get; }
}
