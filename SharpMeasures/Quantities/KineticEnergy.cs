namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct KineticEnergy
{
    /// <summary>Computes the total <see cref="KineticEnergy"/> of an object, according to { <paramref name="translationalKineticEnergy"/>
    /// + <paramref name="rotationalKineticEnergy"/> }.</summary>
    public static KineticEnergy From(TranslationalKineticEnergy translationalKineticEnergy, RotationalKineticEnergy rotationalKineticEnergy)
        => new(translationalKineticEnergy.Magnitude + rotationalKineticEnergy.Magnitude);

    /// <summary>Computes <see cref="Energy"/> according to { <see langword="this"/> + <paramref name="potentialEnergy"/> }.</summary>
    public Energy Add(PotentialEnergy potentialEnergy) => Energy.From(potentialEnergy, this);
    /// <summary>Computes <see cref="Energy"/> according to { <paramref name="kineticEnergy"/> + <paramref name="potentialEnergy"/> }.</summary>
    public static Energy operator +(KineticEnergy kineticEnergy, PotentialEnergy potentialEnergy) => kineticEnergy.Add(potentialEnergy);
}
