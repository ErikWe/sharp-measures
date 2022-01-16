namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct MassFlowRate :
    IAddableScalarQuantity<MassFlowRate, MassFlowRate>,
    ISubtractableScalarQuantity<MassFlowRate, MassFlowRate>
{
    public static MassFlowRate From(Mass mass, Time time) => new(mass.Magnitude / time.Magnitude);
    public static MassFlowRate From(Mass mass, Frequency frequency) => new(mass.Magnitude / frequency.Magnitude);
}
