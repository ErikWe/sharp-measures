namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct KineticEnergy
{
    /// <summary>Computes the total <see cref="KineticEnergy"/> of an object, according to { <see cref="KineticEnergy"/>
    /// = <paramref name="translationalKineticEnergy"/> + <paramref name="rotationalKineticEnergy"/> }.</summary>
    public static KineticEnergy From(TranslationalKineticEnergy translationalKineticEnergy, RotationalKineticEnergy rotationalKineticEnergy)
        => new(translationalKineticEnergy.Magnitude + rotationalKineticEnergy.Magnitude);
}
