namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Jerk :
    IAddableScalarQuantity<Jerk, Jerk>,
    ISubtractableScalarQuantity<Jerk, Jerk>
{
    public static Jerk From(Acceleration acceleration, Time time) => new(acceleration.Magnitude / time.Magnitude);

    public Acceleration Multiply(Time time) => Acceleration.From(this, time);
    public static Acceleration operator *(Jerk x, Time y) => x.Multiply(y);
}
