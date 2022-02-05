namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Area
{
    /// <summary>Computes <see cref="Area"/> according to { <see cref="Area"/> = <paramref name="length1"/> ∗ <paramref name="length2"/> },
    /// where <paramref name="length1"/> and <paramref name="length2"/> are perpendicular measures of <see cref="Length"/>.</summary>
    public static Area From(Length length1, Length length2) => new(length1.Magnitude * length2.Magnitude);
}
