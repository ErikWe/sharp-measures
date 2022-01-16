namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct VelocitySquared :
    IAddableScalarQuantity<VelocitySquared, VelocitySquared>,
    ISubtractableScalarQuantity<VelocitySquared, VelocitySquared>
{
    public static VelocitySquared From(Length length, Acceleration acceleration) => new(length.Magnitude * acceleration.Magnitude);
}
