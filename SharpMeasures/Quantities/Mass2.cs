using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Mass2 : IEquatable<Mass2>, IQuantity2<Mass>
    {
        public Mass X { get; }
        public Mass Y { get; }

        Scalar IQuantity2.XMagnitude => X.Magnitude;
        Scalar IQuantity2.YMagnitude => Y.Magnitude;

        public Mass2(Scalar2 components)
        {
            X = new Mass(components.X);
            Y = new Mass(components.Y);
        }

        public Mass2(Scalar2 components, UnitOfMass unit)
        {
            X = new Mass(components.X, unit);
            Y = new Mass(components.Y, unit);
        }

        public Mass2(Scalar2 components, UnitOfMass unit, MetricPrefix prefix)
        {
            X = new Mass(components.X, unit, prefix);
            Y = new Mass(components.Y, unit, prefix);
        }

        public Mass2(Mass x, Mass y)
        {
            X = x;
            Y = y;
        }

        public Mass2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Mass Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Mass2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Mass2 a, Mass2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Mass2? other) => X.Equals(other?.X) && Y.Equals(other?.Y);
        public override bool Equals(object? obj) => Equals(obj as Mass2);

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.Kilograms}, {Y.Kilograms}) [kg]";

        public static bool operator ==(Mass2? a, Mass2? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Mass2? a, Mass2? b) => !(a == b);

        public static Mass2 operator +(Mass2 a) => a;
        public static Mass2 operator -(Mass2 a) => new(-a.X, -a.Y);
        public static Mass2 operator +(Mass2 a, Mass2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Mass2 operator -(Mass2 a, Mass2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Mass2 operator %(Mass2 a, Mass2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Mass2 a, Mass2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Mass2 a, Mass2 b) => new(a.X / b.X, a.Y / b.Y);

        public static SurfaceDensity2 operator /(Mass2 a, Area2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Density2 operator /(Mass2 a, Volume2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Mass2 operator *(Mass2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Mass2 operator *(Scalar2 a, Mass2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Mass2 operator /(Mass2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Mass2 a, IQuantity2 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude);
        public static UnhandledQuantity2 operator /(Mass2 a, IQuantity2 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude);

        public static SurfaceDensity2 operator /(Mass2 a, Area b) => new(a.X / b, a.Y / b);
        public static Density2 operator /(Mass2 a, Volume b) => new(a.X / b, a.Y / b);

        public static Scalar2 operator /(Mass2 a, Mass b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Mass a, Mass2 b) => new(a / b.X, a / b.Y);

        public static Mass2 operator *(Mass2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Mass2 operator *(Scalar a, Mass2 b) => new(a * b.X, a * b.Y);
        public static Mass2 operator /(Mass2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Mass2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Mass2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Mass2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Mass2 b) => new(b.X / a, b.Y / a);
    }
}
