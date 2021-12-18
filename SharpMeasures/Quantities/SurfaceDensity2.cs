using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class SurfaceDensity2 : IEquatable<SurfaceDensity2>, IQuantity2
    {
        public SurfaceDensity X { get; }
        public SurfaceDensity Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public SurfaceDensity2(Scalar2 components)
        {
            X = new SurfaceDensity(components.X);
            Y = new SurfaceDensity(components.Y);
        }

        public SurfaceDensity2(Scalar2 components, UnitOfMass massUnit, UnitOfLength lengthUnit)
        {
            X = new SurfaceDensity(components.X, massUnit, lengthUnit);
            Y = new SurfaceDensity(components.Y, massUnit, lengthUnit);
        }

        public SurfaceDensity2(Scalar2 components, (UnitOfMass unit, MetricPrefix prefix) mass, (UnitOfLength unit, MetricPrefix prefix) length)
        {
            X = new SurfaceDensity(components.X, mass, length);
            Y = new SurfaceDensity(components.Y, mass, length);
        }

        public SurfaceDensity2(SurfaceDensity x, SurfaceDensity y)
        {
            X = x;
            Y = y;
        }

        public SurfaceDensity2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public SurfaceDensity Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(SurfaceDensity2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(SurfaceDensity2 a, SurfaceDensity2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(SurfaceDensity2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is SurfaceDensity2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.KilogramsPerSquareMetre}, {Y.KilogramsPerSquareMetre}) [kg/(m^2)]";

        public static bool operator ==(SurfaceDensity2? a, SurfaceDensity2? b)
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

        public static bool operator !=(SurfaceDensity2? a, SurfaceDensity2? b) => !(a == b);

        public static SurfaceDensity2 operator +(SurfaceDensity2 a) => a;
        public static SurfaceDensity2 operator -(SurfaceDensity2 a) => new(-a.X, -a.Y);
        public static SurfaceDensity2 operator +(SurfaceDensity2 a, SurfaceDensity2 b) => new(a.X + b.X, a.Y + b.Y);
        public static SurfaceDensity2 operator -(SurfaceDensity2 a, SurfaceDensity2 b) => new(a.X - b.X, a.Y - b.Y);
        public static SurfaceDensity2 operator %(SurfaceDensity2 a, SurfaceDensity2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(SurfaceDensity2 a, SurfaceDensity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(SurfaceDensity2 a, SurfaceDensity2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Mass2 operator *(SurfaceDensity2 a, Area2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Density2 operator /(SurfaceDensity2 a, Length2 b) => new(a.X / b.X, a.Y / b.Y);

        public static SurfaceDensity2 operator *(SurfaceDensity2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static SurfaceDensity2 operator *(Scalar2 a, SurfaceDensity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static SurfaceDensity2 operator /(SurfaceDensity2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(SurfaceDensity2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(SurfaceDensity2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Mass2 operator *(SurfaceDensity2 a, Area b) => new(a.X * b, a.Y * b);
        public static Density2 operator /(SurfaceDensity2 a, Length b) => new(a.X / b, a.Y / b);

        public static Scalar2 operator /(SurfaceDensity2 a, SurfaceDensity b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(SurfaceDensity a, SurfaceDensity2 b) => new(a / b.X, a / b.Y);

        public static SurfaceDensity2 operator *(SurfaceDensity2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static SurfaceDensity2 operator *(Scalar a, SurfaceDensity2 b) => new(a * b.X, a * b.Y);
        public static SurfaceDensity2 operator /(SurfaceDensity2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(SurfaceDensity2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, SurfaceDensity2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(SurfaceDensity2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, SurfaceDensity2 b) => new(b.X / a, b.Y / a);
    }
}
