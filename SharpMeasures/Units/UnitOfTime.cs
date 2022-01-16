namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfTime(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfTime>
{
    public static UnitOfTime Second { get; } = new() { BaseScale = 1 };

    public static UnitOfTime Femtosecond { get; } = Second with { Prefix = MetricPrefix.Femto };
    public static UnitOfTime Picosecond { get; } = Second with { Prefix = MetricPrefix.Pico };
    public static UnitOfTime Nanosecond { get; } = Second with { Prefix = MetricPrefix.Nano };
    public static UnitOfTime Microsecond { get; } = Second with { Prefix = MetricPrefix.Micro };
    public static UnitOfTime Millisecond { get; } = Second with { Prefix = MetricPrefix.Milli };

    public static UnitOfTime Minute { get; } = Second with { BaseScale = 60 };
    public static UnitOfTime Hour { get; } = Minute with { BaseScale = 60 };
    public static UnitOfTime Day { get; } = Hour with { BaseScale = 24 };
    public static UnitOfTime Week { get; } = Day with { BaseScale = 7 };
    public static UnitOfTime CommonYear { get; } = Day with { BaseScale = 365 };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfTime(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfTime other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfTime x, UnitOfTime y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfTime x, UnitOfTime y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfTime x, UnitOfTime y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfTime x, UnitOfTime y) => x.Factor >= y.Factor;
}