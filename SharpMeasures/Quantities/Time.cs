namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Time
{
    /// <summary>Computes <see cref="Time"/> according to { <paramref name="distance"/> / <paramref name="speed"/> },
    /// where <paramref name="speed"/> is the average <see cref="Speed"/> over a <see cref="Distance"/> <paramref name="distance"/>.</summary>
    public static Time From(Distance distance, Speed speed) => new(distance.Magnitude / speed.Magnitude);

    /// <summary>Computes <see cref="Time"/> according to { <paramref name="speed"/> / <paramref name="acceleration"/> },
    /// where <paramref name="speed"/> is the change in <see cref="Speed"/> resulting from some <see cref="Acceleration"/> <paramref name="acceleration"/>.</summary>
    public static Time From(Speed speed, Acceleration acceleration) => new(speed.Magnitude / acceleration.Magnitude);

    /// <summary>Computes total <see cref="Time"/> according to { <see langword="this"/> + <paramref name="time"/> }.</summary>
    public Time Add(Time time) => new(Magnitude + time.Magnitude);
    /// <summary>Computes total <see cref="Time"/> according to { <paramref name="time1"/> + <paramref name="time2"/> }.</summary>
    public static Time operator +(Time time1, Time time2) => new(time1.Magnitude + time2.Magnitude);

    /// <summary>Computes difference in <see cref="Time"/> according to { <see langword="this"/> - <paramref name="time"/> }.</summary>
    public Time Subtract(Time time) => new(Magnitude - time.Magnitude);
    /// <summary>Computes difference in <see cref="Time"/> according to { <see langword="time1"/> - <paramref name="time2"/> }.</summary>
    public static Time operator -(Time time1, Time time2) => time1.Subtract(time2);
}
