namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Torque
{
    /// <summary>Computes <see cref="Torque"/> according to { <see cref="Torque"/> = <paramref name="distance"/> ∗ <paramref name="force"/> ∗ sin(<paramref name="angle"/>) },
    /// where <paramref name="force"/> is the <see cref="Force"/> being applied at a <see cref="Distance"/> <paramref name="distance"/>, and <paramref name="angle"/>
    /// is the <see cref="Angle"/> between the direction of <paramref name="force"/> and the vector to the point where the <see cref="Force"/> is applied.</summary>
    public static Torque From(Distance distance, Force force, Angle angle) => new(distance.Magnitude * force.Magnitude * angle.Sin().Magnitude);
}
