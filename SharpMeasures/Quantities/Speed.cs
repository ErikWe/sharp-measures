namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Speed
{
    /// <summary>Computes average <see cref="Speed"/> according to { <paramref name="distance"/> / <paramref name="time"/> },
    /// where <paramref name="distance"/> is the <see cref="Distance"/> covered over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Speed From(Distance distance, Time time) => new(distance.Magnitude / time.Magnitude);

    /// <summary>Computes change in <see cref="Speed"/> according to { <paramref name="acceleration"/> ∙ <paramref name="time"/> },
    /// where <paramref name="acceleration"/> is the average <see cref="Acceleration"/>over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Speed From(Acceleration acceleration, Time time) => new(acceleration.Magnitude * time.Magnitude);

    /// <summary>Computes total <see cref="Speed"/> according to { <see langword="this"/> + <paramref name="speed"/> }.</summary>
    public Speed Add(Speed speed) => new(Magnitude + speed.Magnitude);
    /// <summary>Computes total <see cref="Speed"/> according to { <paramref name="speed1"/> + <paramref name="speed2"/> }.</summary>
    public static Speed operator +(Speed speed1, Speed speed2) => speed1.Add(speed2);

    /// <summary>Computes difference in <see cref="Speed"/> according to { <see langword="this"/> - <paramref name="speed"/> }.</summary>
    public Speed Subtract(Speed speed) => new(Magnitude - speed.Magnitude);
    /// <summary>Computes difference in <see cref="Speed"/> according to { <see langword="speed1"/> - <paramref name="speed2"/> }.</summary>
    public static Speed operator -(Speed speed1, Speed speed2) => speed1.Subtract(speed2);

    /// <summary>Computes <see cref="Distance"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Distance Multiply(Time time) => Distance.From(this, time);
    /// <summary>Computes <see cref="Distance"/> according to { <paramref name="speed"/> ∙ <paramref name="time"/> }.</summary>
    public static Distance operator *(Speed speed, Time time) => speed.Multiply(time);

    /// <summary>Computes average <see cref="Acceleration"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Acceleration Divide(Time time) => Acceleration.From(this, time);
    /// <summary>Computes average <see cref="Acceleration"/> according to { <paramref name="speed"/> / <paramref name="time"/> }.</summary>
    public static Acceleration operator /(Speed speed, Time time) => speed.Divide(time);

    /// <summary>Computes <see cref="Time"/> according to { <see langword="this"/> / <paramref name="acceleration"/> }.</summary>
    public Time Divide(Acceleration acceleration) => Time.From(this, acceleration);
    /// <summary>Computes <see cref="Time"/> according to { <paramref name="speed"/> / <paramref name="acceleration"/> }.</summary>
    public static Time operator /(Speed speed, Acceleration acceleration) => speed.Divide(acceleration);
}
