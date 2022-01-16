namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Velocity :
    IAddableScalarQuantity<Velocity, Velocity>,
    ISubtractableScalarQuantity<Velocity, Velocity>
{
    public static Velocity From(Acceleration acceleration, Time time) => new(acceleration.Magnitude * time.Magnitude);

    public Length Multiply(Time time) => Length.From(this, time);
    public Acceleration Divide(Time time) => Acceleration.From(this, time);
    public static Length operator *(Velocity x, Time y) => x.Multiply(y);
    public static Acceleration operator /(Velocity x, Time y) => x.Divide(y);
}
