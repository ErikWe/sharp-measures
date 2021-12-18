using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Volume2 : IEquatable<Volume2>, IQuantity2
    {
        public Volume X { get; }
        public Volume Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Volume2(Scalar2 components)
        {
            X = new Volume(components.X);
            Y = new Volume(components.Y);
        }

        public Volume2(Scalar2 components, UnitOfVolume unit)
        {
            X = new Volume(components.X, unit);
            Y = new Volume(components.Y, unit);
        }

        public Volume2(Scalar2 components, UnitOfVolume unit, MetricPrefix prefix)
        {
            X = new Volume(components.X, unit, prefix);
            Y = new Volume(components.Y, unit, prefix);
        }

        public Volume2(Scalar2 components, UnitOfLength unit)
        {
            X = new Volume(components.X, unit);
            Y = new Volume(components.Y, unit);
        }

        public Volume2(Scalar2 components, UnitOfLength unit, MetricPrefix prefix)
        {
            X = new Volume(components.X, unit, prefix);
            Y = new Volume(components.Y, unit, prefix);
        }

        public Volume2(Volume x, Volume y)
        {
            X = x;
            Y = y;
        }

        public Volume2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Volume Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Volume2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Volume2 a, Volume2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Volume2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Volume2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.CubicMetres}, {Y.CubicMetres}) [m^3]";

        public static bool operator ==(Volume2? a, Volume2? b)
        {
            if (a is null)
            {
                return b is null;
            }
            else
            {
                return a.Equals(b);
            }
        }

        public static bool operator !=(Volume2? a, Volume2? b) => !(a == b);

        public static Volume2 operator +(Volume2 a) => a;
        public static Volume2 operator -(Volume2 a) => new(-a.X, -a.Y);
        public static Volume2 operator +(Volume2 a, Volume2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Volume2 operator -(Volume2 a, Volume2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Volume2 operator %(Volume2 a, Volume2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Volume2 a, Volume2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Volume2 a, Volume2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Area2 operator /(Volume2 a, Length2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Length2 operator /(Volume2 a, Area2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Volume2 operator *(Volume2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Volume2 operator *(Scalar2 a, Volume2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Volume2 operator /(Volume2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Volume2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(Volume2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Scalar2 operator /(Volume2 a, Volume b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Volume a, Volume2 b) => new(a / b.X, a / b.Y);

        public static Area2 operator /(Volume2 a, Length b) => new(a.X / b, a.Y / b);
        public static Length2 operator /(Volume2 a, Area b) => new(a.X / b, a.Y / b);

        public static Volume2 operator *(Volume2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Volume2 operator *(Scalar a, Volume2 b) => new(a * b.X, a * b.Y);
        public static Volume2 operator /(Volume2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Volume2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Volume2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Volume2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Volume2 b) => new(b.X / a, b.Y / a);
    }
}
