namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Energy
{
    /// <summary>Computes <see cref="Energy"/> according to { <paramref name="potentialEnergy"/> + <paramref name="kineticEnergy"/> }.</summary>
    public static Energy From(PotentialEnergy potentialEnergy, KineticEnergy kineticEnergy) => new(potentialEnergy.Magnitude + kineticEnergy.Magnitude);
}
