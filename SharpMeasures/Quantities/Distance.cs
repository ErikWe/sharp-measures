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

    /// <summary>Computes <see cref="Absement"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Absement Multiply(Time time) => Absement.From(this, time);
    /// <summary>Computes <see cref="Absement"/> according to { <paramref name="distance"/> ∙ <paramref name="time"/> }.</summary>
    public static Absement operator *(Distance distance, Time time) => distance.Multiply(time);

    /// <summary>Computes <see cref="GravitationalEnergy"/> according to { <see langword="this"/> ∙ <paramref name="weight"/> }.</summary>
    public GravitationalEnergy Multiply(Weight weight) => GravitationalEnergy.From(weight, this);
    /// <summary>Computes <see cref="GravitationalEnergy"/> according to { <paramref name="distance"/> ∙ <paramref name="weight"/> }.</summary>
    public static GravitationalEnergy operator *(Distance distance, Weight weight) => distance.Multiply(weight);

    /// <summary>Computes <see cref="Work"/> according to { <see langword="this"/> ∙ <paramref name="force"/> }.</summary>
    public Work Multiply(Force force) => Work.From(force, this);
    /// <summary>Computes <see cref="Work"/> according to { <paramref name="distance"/> ∙ <paramref name="force"/> }.</summary>
    public static Work operator *(Distance distance, Force force) => distance.Multiply(force);

    /// <summary>Computes average <see cref="Speed"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Speed Divide(Time time) => Speed.From(this, time);
    /// <summary>Computes average <see cref="Speed"/> according to { <paramref name="distance"/> / <paramref name="time"/> }.</summary>
    public static Speed operator /(Distance distance, Time time) => distance.Divide(time);

    /// <summary>Computes <see cref="Time"/> according to { <see langword="this"/> / <paramref name="speed"/> }.</summary>
    public Time Divide(Speed speed) => Time.From(this, speed);
    /// <summary>Computes <see cref="Time"/> according to { <paramref name="distance"/> / <paramref name="speed"/> }.</summary>
    public static Time operator /(Distance distance, Speed speed) => distance.Divide(speed);

    /// <summary>Computes <see cref="TimeSquared"/> according to { <see langword="this"/> / <paramref name="acceleration"/> }.</summary>
    public TimeSquared Divide(Acceleration acceleration) => TimeSquared.From(this, acceleration);
    /// <summary>Computes <see cref="TimeSquared"/> according to { <paramref name="distance"/> / <paramref name="acceleration"/> }.</summary>
    public static TimeSquared operator /(Distance distance, Acceleration acceleration) => distance.Divide(acceleration);
}
