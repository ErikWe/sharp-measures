using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Angle2 : IEquatable<Angle2>, IQuantity2<Angle>
    {
        public Angle X { get; }
        public Angle Y { get; }

        Scalar IQuantity2.XMagnitude => X.Magnitude;
        Scalar IQuantity2.YMagnitude => Y.Magnitude;

        public Angle2(Scalar2 components)
        {
            X = new Angle(components.X);
            Y = new Angle(components.Y);
        }

        public Angle2(Scalar2 components, UnitOfAngle unit)
        {
            X = new Angle(components.X, unit);
            Y = new Angle(components.Y, unit);
        }

        public Angle2(Scalar2 components, UnitOfAngle unit, MetricPrefix prefix)
        {
            X = new Angle(components.X, unit, prefix);
            Y = new Angle(components.Y, unit, prefix);
        }

        public Angle2(Angle x, Angle y)
        {
            X = x;
            Y = y;
        }

        public Angle2(Scalar x, Scalar y)
        {
            X = new Angle(x.Magnitude);
            Y = new Angle(y.Magnitude);
        }

        public Angle2(IQuantity x, IQuantity y)
        {
            X = new Angle(x.Magnitude);
            Y = new Angle(y.Magnitude);
        }

        public Angle2 Normalize() => this / Magnitude().Magnitude;
        public Angle2 NormalizeComponents()
        {
            return new(
                X.Normalize(),
                Y.Normalize()
            );
        }

        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Angle Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Angle2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Angle2 a, Angle2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Angle2? other) => X.Equals(other?.X) && Y.Equals(other?.Y);
        public override bool Equals(object? obj) => Equals(obj as Angle2);

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.Radians}, {Y.Radians}) [rad]";

        public static bool operator ==(Angle2? a, Angle2? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Angle2? a, Angle2? b) => !(a == b);

        public static Angle2 operator +(Angle2 a) => a;
        public static Angle2 operator -(Angle2 a) => new(-a.X, -a.Y);
        public static Angle2 operator +(Angle2 a, Angle2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Angle2 operator -(Angle2 a, Angle2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Angle2 operator %(Angle2 a, Angle2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Angle2 a, Angle2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Angle2 a, Angle2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Angle2 operator *(Angle2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Angle2 operator *(Scalar2 a, Angle2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Angle2 operator /(Angle2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Angle2 a, IQuantity2 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude);
        public static UnhandledQuantity2 operator /(Angle2 a, IQuantity2 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude);

        public static Scalar2 operator /(Angle2 a, Angle b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Angle a, Angle2 b) => new(a / b.X, a / b.Y);

        public static Angle2 operator *(Angle2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Angle2 operator *(Scalar a, Angle2 b) => new(a * b.X, a * b.Y);
        public static Angle2 operator /(Angle2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Angle2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Angle2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Angle2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Angle2 b) => new(b.X / a, b.Y / a);
    }
}
