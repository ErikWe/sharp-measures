namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Acceleration :
    IAddableScalarQuantity<Acceleration, Acceleration>,
    ISubtractableScalarQuantity<Acceleration, Acceleration>
{
    public static Acceleration From(Velocity velocity, Time time) => new(velocity.Magnitude / time.Magnitude);
    public static Acceleration From(Jerk jerk, Time time) => new(jerk.Magnitude * time.Magnitude);

    public Velocity Multiply(Time time) => Velocity.From(this, time);
    public VelocitySquared Multiply(Length length) => VelocitySquared.From(length, this);
    public static Velocity operator *(Acceleration x, Time y) => x.Multiply(y);
    public static VelocitySquared operator *(Acceleration x, Length y) => x.Multiply(y);
}
