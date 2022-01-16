namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfFrequency(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfFrequency>
{
    public static UnitOfFrequency From(UnitOfTime unitOfTime) => new(1 / unitOfTime.Factor);

    public static UnitOfFrequency PerSecond { get; } = From(UnitOfTime.Second);

    public static UnitOfFrequency Hertz { get; } = PerSecond;
    public static UnitOfFrequency Kilohertz { get; } = Hertz with { Prefix = MetricPrefix.Kilo };
    public static UnitOfFrequency Megahertz { get; } = Hertz with { Prefix = MetricPrefix.Mega };
    public static UnitOfFrequency Gigahertz { get; } = Hertz with { Prefix = MetricPrefix.Giga };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfFrequency(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfFrequency other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfFrequency x, UnitOfFrequency y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfFrequency x, UnitOfFrequency y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfFrequency x, UnitOfFrequency y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfFrequency x, UnitOfFrequency y) => x.Factor >= y.Factor;
}