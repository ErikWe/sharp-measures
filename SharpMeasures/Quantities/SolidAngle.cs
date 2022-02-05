namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SolidAngle
{
    /// <summary>Computes <see cref="SolidAngle"/> according to { <see cref="SolidAngle"/> = <paramref name="angle1"/> ∗ <paramref name="angle2"/> },
    /// where <paramref name="angle1"/> and <paramref name="angle2"/> are perpendicular measures of <see cref="Angle"/>.</summary>
    public static SolidAngle From(Angle angle1, Angle angle2) => new(angle1.Magnitude * angle2.Magnitude);
}
