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

    /// <summary>Computes <see cref="Absement"/> according to { <see langword="this"/> ∙ <paramref name="distance"/> }.</summary>
    public Absement Multiply(Distance distance) => Absement.From(distance, this);
    /// <summary>Computes <see cref="Absement"/> according to { <paramref name="time"/> ∙ <paramref name="distance"/> }.</summary>
    public static Absement operator *(Time time, Distance distance) => time.Multiply(distance);

    /// <summary>Computes change in <see cref="Speed"/> according to { <see langword="this"/> ∙ <paramref name="acceleration"/> }.</summary>
    public Speed Multiply(Acceleration acceleration) => Speed.From(acceleration, this);
    /// <summary>Computes change in <see cref="Speed"/> according to { <paramref name="time"/> ∙ <paramref name="acceleration"/> }.</summary>
    public static Speed operator *(Time time, Acceleration acceleration) => time.Multiply(acceleration);

    /// <summary>Computes <see cref="Absement3"/> according to { <see langword="this"/> ∙ <paramref name="displacement"/> }.</summary>
    public Absement3 Multiply(Displacement3 displacement) => Absement3.From(displacement, this);
    /// <summary>Computes <see cref="Absement3"/> according to { <paramref name="time"/> ∙ <paramref name="displacement"/> }.</summary>
    public static Absement3 operator *(Time time, Displacement3 displacement) => time.Multiply(displacement);

    /// <summary>Computes change in <see cref="Angle"/> according to { <see langword="this"/> ∙ <paramref name="angularSpeed"/> }.</summary>
    public Angle Multiply(AngularSpeed angularSpeed) => Angle.From(angularSpeed, this);
    /// <summary>Computes change in <see cref="Angle"/> according to { <paramref name="time"/> ∙ <paramref name="angularSpeed"/> }.</summary>
    public static Angle operator *(Time time, AngularSpeed angularSpeed) => time.Multiply(angularSpeed);

    /// <summary>Computes change in <see cref="Rotation3"/> according to { <see langword="this"/> ∙ <paramref name="angularVelocity"/> }.</summary>
    public Rotation3 Multiply(AngularVelocity3 angularVelocity) => Rotation3.From(angularVelocity, this);
    /// <summary>Computes change in <see cref="Rotation3"/> according to { <paramref name="time"/> ∙ <paramref name="angularVelocity"/> }.</summary>
    public static Rotation3 operator *(Time time, AngularVelocity3 angularVelocity) => time.Multiply(angularVelocity);

    /// <summary>Computes <see cref="Distance"/> according to { <see langword="this"/> ∙ <paramref name="speed"/> }.</summary>
    public Distance Multiply(Speed speed) => Distance.From(speed, this);
    /// <summary>Computes <see cref="Distance"/> according to { <paramref name="time"/> ∙ <paramref name="speed"/> }.</summary>
    public static Distance operator *(Time time, Speed speed) => time.Multiply(speed);

    /// <summary>Computes <see cref="Displacement3"/> according to { <see langword="this"/> ∙ <paramref name="velocity"/> }.</summary>
    public Displacement3 Multiply(Velocity3 velocity) => Displacement3.From(velocity, this);
    /// <summary>Computes <see cref="Displacement3"/> according to { <paramref name="time"/> ∙ <paramref name="velocity"/> }.</summary>
    public static Displacement3 operator *(Time time, Velocity3 velocity) => time.Multiply(velocity);

    /// <summary>Computes <see cref="Impulse"/> according to { <see langword="this"/> ∙ <paramref name="force"/> }.</summary>
    public Impulse Multiply(Force force) => Impulse.From(force, this);
    /// <summary>Computes <see cref="Impulse"/> according to { <paramref name="time"/> ∙ <paramref name="force"/> }.</summary>
    public static Impulse operator *(Time time, Force force) => time.Multiply(force);

    /// <summary>Computes <see cref="Impulse3"/> according to { <see langword="this"/> ∙ <paramref name="force"/> }.</summary>
    public Impulse3 Multiply(Force3 force) => Impulse3.From(force, this);
    /// <summary>Computes <see cref="Impulse3"/> according to { <paramref name="time"/> ∙ <paramref name="force"/> }.</summary>
    public static Impulse3 operator *(Time time, Force3 force) => time.Multiply(force);
}
