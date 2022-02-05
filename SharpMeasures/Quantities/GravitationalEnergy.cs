namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct GravitationalEnergy
{
    /// <summary>Computes <see cref="GravitationalEnergy"/> according to { <see cref="GravitationalAcceleration"/>
    /// = <paramref name="weight"/> ∗ <paramref name="distance"/> }, where <paramref name="weight"/> is the <see cref="Weight"/> of an
    /// object at height <paramref name="distance"/> above a chosen reference point in a uniform gravitational field - for example an object
    /// relatively close to the Earth, with the surface as reference point.</summary>
    public static GravitationalEnergy From(Weight weight, Distance distance) => new(weight.Magnitude * distance.Magnitude);
}
