using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Area2 : IEquatable<Area2>, IQuantity2
    {
        public Area X { get; }
        public Area Y { get; }

        Scalar IQuantity2.X => X.Magnitude;
        Scalar IQuantity2.Y => Y.Magnitude;

        public Area2(Scalar2 components)
        {
            X = new Area(components.X);
            Y = new Area(components.Y);
        }

        public Area2(Scalar2 components, UnitOfArea unit)
        {
            X = new Area(components.X, unit);
            Y = new Area(components.Y, unit);
        }

        public Area2(Scalar2 components, UnitOfArea unit, MetricPrefix prefix)
        {
            X = new Area(components.X, unit, prefix);
            Y = new Area(components.Y, unit, prefix);
        }

        public Area2(Scalar2 components, UnitOfLength unit)
        {
            X = new Area(components.X, unit);
            Y = new Area(components.Y, unit);
        }

        public Area2(Scalar2 components, UnitOfLength unit, MetricPrefix prefix)
        {
            X = new Area(components.X, unit, prefix);
            Y = new Area(components.Y, unit, prefix);
        }

        public Area2(Area x, Area y)
        {
            X = x;
            Y = y;
        }

        public Area2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Area Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Area2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Area2 a, Area2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Area2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        public override bool Equals(object? obj)
        {
            if (obj is Area2 other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.SquareMetres}, {Y.SquareMetres}) [m^2]";

        public static bool operator ==(Area2? a, Area2? b)
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

        public static bool operator !=(Area2? a, Area2? b) => !(a == b);

        public static Area2 operator +(Area2 a) => a;
        public static Area2 operator -(Area2 a) => new(-a.X, -a.Y);
        public static Area2 operator +(Area2 a, Area2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Area2 operator -(Area2 a, Area2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Area2 operator %(Area2 a, Area2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Area2 a, Area2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Area2 a, Area2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Length2 operator /(Area2 a, Length2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Area2 operator *(Area2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Area2 operator *(Scalar2 a, Area2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Area2 operator /(Area2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Area2 a, IQuantity2 b) => new((a.X * b.X).Magnitude, (a.Y * b.Y).Magnitude);
        public static UnhandledQuantity2 operator /(Area2 a, IQuantity2 b) => new((a.X / b.X).Magnitude, (a.Y / b.Y).Magnitude);

        public static Scalar2 operator /(Area2 a, Area b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Area a, Area2 b) => new(a / b.X, a / b.Y);

        public static Length2 operator /(Area2 a, Length b) => new(a.X / b, a.Y / b);

        public static Area2 operator *(Area2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Area2 operator *(Scalar a, Area2 b) => new(a * b.X, a * b.Y);
        public static Area2 operator /(Area2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Area2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Area2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Area2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Area2 b) => new(b.X / a, b.Y / a);
    }
}
