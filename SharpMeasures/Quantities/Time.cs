namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Time :
    IAddableScalarQuantity<Time, Time>,
    ISubtractableScalarQuantity<Time, Time>
{ }