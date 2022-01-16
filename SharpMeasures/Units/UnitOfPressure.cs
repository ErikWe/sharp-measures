namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfPressure(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfPressure>
{
    public static UnitOfPressure From(UnitOfForce unitOfForce, UnitOfArea unitOfArea) => new(unitOfForce.Factor / unitOfArea.Factor);

    public static UnitOfPressure Pascal { get; } = From(UnitOfForce.Newton, UnitOfArea.SquareMetre);
    public static UnitOfPressure Kilopascal { get; } = Pascal with { Prefix = MetricPrefix.Kilo };

    public static UnitOfPressure Bar { get; } = Kilopascal with { BaseScale = 100 };

    public static UnitOfPressure PoundForcePerSquareInch { get; } = From(UnitOfForce.PoundForce, UnitOfArea.From(UnitOfLength.Inch));

    public static UnitOfPressure StandardAtmosphere { get; } = Pascal with { BaseScale = 101325 };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfPressure(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfPressure other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfPressure x, UnitOfPressure y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfPressure x, UnitOfPressure y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfPressure x, UnitOfPressure y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfPressure x, UnitOfPressure y) => x.Factor >= y.Factor;
}