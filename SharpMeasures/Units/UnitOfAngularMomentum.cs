namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfAngularMomentum(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfAngularMomentum>
{
    public static UnitOfAngularMomentum From(UnitOfLength unitOfLength, UnitOfMomentum unitOfMomentum) => new(unitOfLength.Factor * unitOfMomentum.Factor);

    public static UnitOfAngularMomentum KilogramMetreSquaredPerSecond { get; } = From(UnitOfLength.Metre, UnitOfMomentum.KilogramMetrePerSecond);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfAngularMomentum(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfAngularMomentum other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.Factor >= y.Factor;
}