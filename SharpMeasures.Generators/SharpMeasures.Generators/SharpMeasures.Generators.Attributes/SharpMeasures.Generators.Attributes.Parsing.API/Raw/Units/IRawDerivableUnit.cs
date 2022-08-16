namespace SharpMeasures.Generators.Raw.Units;

public interface IRawDerivableUnit
{
    public abstract string? DerivationID { get; }

    public abstract string Expression { get; }
    public abstract RawUnitDerivationSignature Signature { get; }
}
