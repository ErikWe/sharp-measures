namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfFrequencyDrift(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfFrequencyDrift>
{
    public static UnitOfFrequencyDrift From(UnitOfFrequency unitOfFrequency, UnitOfTime unitOfTime) => new(unitOfFrequency.Factor / unitOfTime.Factor);

    public static UnitOfFrequencyDrift HertzPerSecond { get; } = From(UnitOfFrequency.Hertz, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfFrequencyDrift(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfFrequencyDrift other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfFrequencyDrift x, UnitOfFrequencyDrift y) => x.Factor >= y.Factor;
}