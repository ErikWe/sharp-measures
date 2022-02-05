namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct PotentialEnergy
{
    /// <summary>Computes final <see cref="PotentialEnergy"/> according to { <see cref="PotentialEnergy"/>
    /// = <paramref name="initiaPotentialEnergy"/> - <paramref name="work"/> }.</summary>
    public static PotentialEnergy From(PotentialEnergy initiaPotentialEnergy, Work work) => new(initiaPotentialEnergy.Magnitude - work.Magnitude);
}
