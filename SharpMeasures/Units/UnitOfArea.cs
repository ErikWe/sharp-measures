namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfArea(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfArea>
{
    public static UnitOfArea From(UnitOfLength unitOfLength) => new(Math.Pow(unitOfLength.Factor, 2));

    public static UnitOfArea SquareMetre { get; } = From(UnitOfLength.Metre);
    public static UnitOfArea SquareKilometre { get; } = From(UnitOfLength.Kilometre);
    public static UnitOfArea SquareMile { get; } = From(UnitOfLength.Mile);

    public static UnitOfArea Are { get; } = SquareMetre with { BaseScale = Math.Pow(10, 2) };
    public static UnitOfArea Hectare { get; } = Are with { BaseScale = Math.Pow(10, 2) };

    public static UnitOfArea Acre { get; } = SquareMile with { BaseScale = 1d / 640 };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfArea(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfArea other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfArea x, UnitOfArea y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfArea x, UnitOfArea y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfArea x, UnitOfArea y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfArea x, UnitOfArea y) => x.Factor >= y.Factor;
}