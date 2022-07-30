namespace SharpMeasures.Generators.Unresolved.Units;

public interface IUnresolvedDerivableUnit
{
    public abstract string? DerivationID { get; }

    public abstract string Expression { get; }
    public abstract UnresolvedUnitDerivationSignature Signature { get; }
}
