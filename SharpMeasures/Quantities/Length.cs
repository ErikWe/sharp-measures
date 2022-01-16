namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Length :
    IAddableScalarQuantity<Length, Length>,
    ISubtractableScalarQuantity<Length, Length>,
    IMultiplicableScalarQuantity<VelocitySquared, Acceleration>
{
    public static Length From(Velocity velocity, Time time) => new(velocity.Magnitude * time.Magnitude);
    public static Length From(Velocity velocity, Frequency frequency) => new(velocity.Magnitude / frequency.Magnitude);
    public static Length From(Acceleration acceleration, FrequencyDrift frequencyDrift) => new(acceleration.Magnitude / frequencyDrift.Magnitude);

    public static Length From(Mass mass, LinearDensity linearDensity) => new(mass.Magnitude / linearDensity.Magnitude);

    public VelocitySquared Multiply(Acceleration factor) => VelocitySquared.From(this, factor);
    public static VelocitySquared operator *(Length x, Acceleration y) => x.Multiply(y);
}
