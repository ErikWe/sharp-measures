namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfPower(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfPower>
{
    public static UnitOfPower From(UnitOfEnergy unitOfEnergy, UnitOfTime unitOfTime) => new(unitOfEnergy.Factor / unitOfTime.Factor);

    public static UnitOfPower Watt { get; } = From(UnitOfEnergy.Joule, UnitOfTime.Second);
    public static UnitOfPower Kilowatt { get; } = Watt with { Prefix = MetricPrefix.Kilo };
    public static UnitOfPower Megawatt { get; } = Watt with { Prefix = MetricPrefix.Mega };
    public static UnitOfPower Gigawatt { get; } = Watt with { Prefix = MetricPrefix.Giga };
    public static UnitOfPower Terawatt { get; } = Watt with { Prefix = MetricPrefix.Tera };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfPower(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfPower other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfPower x, UnitOfPower y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfPower x, UnitOfPower y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfPower x, UnitOfPower y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfPower x, UnitOfPower y) => x.Factor >= y.Factor;
}