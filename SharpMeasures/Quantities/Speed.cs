namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Speed
{
    /// <summary>Computes average <see cref="Speed"/> according to { <paramref name="distance"/> / <paramref name="time"/> },
    /// where <paramref name="distance"/> is the <see cref="Distance"/> covered over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Speed From(Distance distance, Time time) => new(distance.Magnitude / time.Magnitude);

    public Speed Subtract(Speed term) => new(Magnitude - term.Magnitude);
    public static Speed operator -(Speed x, Speed y) => x.Subtract(y);

    public Acceleration Divide(Time divisor) => new(Magnitude / divisor.Magnitude);
    public static Acceleration operator /(Speed x, Time y) => x.Divide(y);

    public Distance Multiply(Time factor) => new(Magnitude * factor.Magnitude);
    public static Distance operator *(Speed x, Time y) => x.Multiply(y);
}
