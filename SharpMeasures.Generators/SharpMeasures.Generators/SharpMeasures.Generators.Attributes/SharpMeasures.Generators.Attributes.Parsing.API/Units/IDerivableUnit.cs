namespace SharpMeasures.Generators.Units;

public interface IDerivableUnit
{
    public abstract string? DerivationID { get; }

    public abstract string Expression { get; }
    public abstract UnitDerivationSignature Signature { get; }
}
