namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfSpecificAngularMomentum(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfSpecificAngularMomentum>
{
    public static UnitOfSpecificAngularMomentum From(UnitOfAngularMomentum unitOfAngularMomentum, UnitOfMass unitOfMass) => new(unitOfAngularMomentum.Factor / unitOfMass.Factor);

    public static UnitOfSpecificAngularMomentum SquareMetrePerSecond { get; } = From(UnitOfAngularMomentum.KilogramMetreSquaredPerSecond, UnitOfMass.Kilogram);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfSpecificAngularMomentum(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfSpecificAngularMomentum other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.Factor >= y.Factor;
}