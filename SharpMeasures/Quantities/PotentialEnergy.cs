namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct PotentialEnergy
{
    /// <summary>Computes final <see cref="PotentialEnergy"/> according to { <paramref name="initialPotentialEnergy"/> - <paramref name="work"/> }.</summary>
    public static PotentialEnergy From(PotentialEnergy initialPotentialEnergy, Work work) => new(initialPotentialEnergy.Magnitude - work.Magnitude);

    /// <summary>Computes <see cref="Energy"/> according to { <see langword="this"/> + <paramref name="kineticEnergy"/> }.</summary>
    public Energy Add(KineticEnergy kineticEnergy) => Energy.From(this, kineticEnergy);
    /// <summary>Computes <see cref="Energy"/> according to { <paramref name="potentialEnergy"/> + <paramref name="kineticEnergy"/> }.</summary>
    public static Energy operator +(PotentialEnergy potentialEnergy, KineticEnergy kineticEnergy) => potentialEnergy.Add(kineticEnergy);

    /// <summary>Computes final <see cref="PotentialEnergy"/> according to { <see langword="this"/> - <paramref name="work"/> }.</summary>
    public PotentialEnergy Subtract(Work work) => PotentialEnergy.From(this, work);
    /// <summary>Computes final <see cref="PotentialEnergy"/> according to { <paramref name="initialPotentialEnergy"/> - <paramref name="work"/> }.</summary>
    public static PotentialEnergy operator -(PotentialEnergy initialPotentialEnergy, Work work) => initialPotentialEnergy.Subtract(work);
}
