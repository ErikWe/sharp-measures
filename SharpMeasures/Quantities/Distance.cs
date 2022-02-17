namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Distance
{
    /// <summary>Computes <see cref="Distance"/> according to { <paramref name="speed"/> ∙ <paramref name="time"/> },
    /// where <paramref name="speed"/> is the average <see cref="Speed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Distance From(Speed speed, Time time) => new(speed.Magnitude * time.Magnitude);

    public static Distance operator +(Distance x, Distance y) => new(x.Magnitude + y.Magnitude);
}
