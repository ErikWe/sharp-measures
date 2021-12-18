using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Density2 : IEquatable<Density2>, IQuantity2
    {
        public Density X { get; }
        public Density Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Density2(Scalar2 components)
        {
            X = new Density(components.X);
            Y = new Density(components.Y);
        }

        public Density2(Scalar2 components, UnitOfMass massUnit, UnitOfLength lengthUnit)
        {
            X = new Density(components.X, massUnit, lengthUnit);
            Y = new Density(components.Y, massUnit, lengthUnit);
        }

        public Density2(Scalar2 components, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
        {
            X = new Density(components.X, mass, length);
            Y = new Density(components.Y, mass, length);
        }

        public Density2(Density x, Density y)
        {
            X = x;
            Y = y;
        }

        public Density2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Density Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Density2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Density2 a, Density2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Density2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Density2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.KilogramsPerCubicMetre}, {Y.KilogramsPerCubicMetre}) [kg/(m^3)]";

        public static bool operator ==(Density2? a, Density2? b)
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

        public static bool operator !=(Density2? a, Density2? b) => !(a == b);

        public static Density2 operator +(Density2 a) => a;
        public static Density2 operator -(Density2 a) => new(-a.X, -a.Y);
        public static Density2 operator +(Density2 a, Density2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Density2 operator -(Density2 a, Density2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Density2 operator %(Density2 a, Density2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Density2 a, Density2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Density2 a, Density2 b) => new(a.X / b.X, a.Y / b.Y);

        public static SurfaceDensity2 operator *(Density2 a, Length2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Mass2 operator *(Density2 a, Volume2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Density2 operator *(Density2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Density2 operator *(Scalar2 a, Density2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Density2 operator /(Density2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Density2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(Density2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static SurfaceDensity2 operator *(Density2 a, Length b) => new(a.X * b, a.Y * b);
        public static Mass2 operator *(Density2 a, Volume b) => new(a.X * b, a.Y * b);

        public static Scalar2 operator /(Density2 a, Density b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Density a, Density2 b) => new(a / b.X, a / b.Y);

        public static Density2 operator *(Density2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Density2 operator *(Scalar a, Density2 b) => new(a * b.X, a * b.Y);
        public static Density2 operator /(Density2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Density2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Density2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Density2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Density2 b) => new(b.X / a, b.Y / a);
    }
}
