namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Work
{
    /// <summary>Computes <see cref="Work"/> according to { <see cref="Work"/> = <paramref name="force"/> ∗ <paramref name="distance"/> },
    /// where <paramref name="force"/> is a <see cref="Force"/> causing a displacement <paramref name="distance"/> in the same direction.</summary>
    /// <remarks>If the displacement is not in the direction of <paramref name="force"/>, use <see cref="From(Force, Distance, Angle)"/>.</remarks>
    public static Work From(Force force, Distance distance) => new(force.Magnitude * distance.Magnitude);

    /// <summary>Computes <see cref="Work"/> according to { <see cref="Work"/> = <paramref name="force"/> ∗ <paramref name="distance"/> ∗ cos(<paramref name="angle"/>) },
    /// where <paramref name="force"/> is a <see cref="Force"/> causing a displacement <paramref name="distance"/>, and <paramref name="angle"/>
    /// is the <see cref="Angle"/> between the direction of <paramref name="force"/> and the direction of the displacement is applied.</summary>
    public static Work From(Force force, Distance distance, Angle angle) => new(force.Magnitude * distance.Magnitude * angle.Cos().Magnitude);
}
