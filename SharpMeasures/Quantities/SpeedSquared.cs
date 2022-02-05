namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpeedSquared
{
    /// <summary>Computes average <see cref="SpeedSquared"/> according to { <see cref="SpeedSquared"/> = <paramref name="speed1"/> ∗ <paramref name="speed2"/> }.</summary>
    public static SpeedSquared From(Speed speed1, Speed speed2) => new(speed1.Magnitude * speed2.Magnitude);
}
