namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Distance
{
    /// <summary>Computes <see cref="Distance"/> according to { <paramref name="speed"/> ∙ <paramref name="time"/> },
    /// where <paramref name="speed"/> is the average <see cref="Speed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Distance From(Speed speed, Time time) => new(speed.Magnitude * time.Magnitude);

    /// <summary>Computes total <see cref="Distance"/> according to { <see langword="this"/> + <paramref name="distance"/> }.</summary>
    public Distance Add(Distance distance) => new(Magnitude + distance.Magnitude);
    /// <summary>Computes total <see cref="Distance"/> according to { <paramref name="distance1"/> + <paramref name="distance2"/> }.</summary>
    public static Distance operator +(Distance distance1, Distance distance2) => distance1.Add(distance2);

    /// <summary>Computes difference in <see cref="Distance"/> according to { <see langword="this"/> - <paramref name="distance"/> }.</summary>
    public Distance Subtract(Distance distance) => new(Magnitude - distance.Magnitude);
    /// <summary>Computes difference in <see cref="Distance"/> according to { <see langword="speed1"/> - <paramref name="distance2"/> }.</summary>
    public static Distance operator -(Distance distance1, Distance distance2) => distance1.Subtract(distance2);

    /// <summary>Computes average <see cref="Speed"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Speed Divide(Time time) => Speed.From(this, time);
    /// <summary>Computes average <see cref="Speed"/> according to { <paramref name="distance"/> / <paramref name="time"/> }.</summary>
    public static Speed operator /(Distance distance, Time time) => distance.Divide(time);

    /// <summary>Computes <see cref="Time"/> according to { <see langword="this"/> / <paramref name="speed"/> }.</summary>
    public Time Divide(Speed speed) => Time.From(this, speed);
    /// <summary>Computes <see cref="Time"/> according to { <paramref name="distance"/> / <paramref name="speed"/> }.</summary>
    public static Time operator /(Distance distance, Speed speed) => distance.Divide(speed);
}
