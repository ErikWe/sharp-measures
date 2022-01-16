namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfEnergy(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfEnergy>
{
    public static UnitOfEnergy From(UnitOfForce unitOfForce, UnitOfLength unitOfArea) => new(unitOfForce.Factor * unitOfArea.Factor);
    public static UnitOfEnergy From(UnitOfPower unitOfPower, UnitOfTime unitOfTime) => new(unitOfPower.Factor * unitOfTime.Factor);

    public static UnitOfEnergy Joule { get; } = From(UnitOfForce.Newton, UnitOfLength.Metre);
    public static UnitOfEnergy Kilojoule { get; } = Joule with { Prefix = MetricPrefix.Kilo };
    public static UnitOfEnergy Megajoule { get; } = Joule with { Prefix = MetricPrefix.Mega };
    public static UnitOfEnergy Gigajoule { get; } = Joule with { Prefix = MetricPrefix.Giga };

    public static UnitOfEnergy KilowattHour { get; } = From(UnitOfPower.Kilowatt, UnitOfTime.Hour);
    
    public static UnitOfEnergy Calorie { get; } = Joule with { BaseScale = 4.184 };
    public static UnitOfEnergy Kilocalorie { get; } = Calorie with { Prefix = MetricPrefix.Kilo };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfEnergy(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfEnergy other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfEnergy x, UnitOfEnergy y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfEnergy x, UnitOfEnergy y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfEnergy x, UnitOfEnergy y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfEnergy x, UnitOfEnergy y) => x.Factor >= y.Factor;
}