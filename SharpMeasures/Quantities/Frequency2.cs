using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public class Frequency2 : IEquatable<Frequency2>, IQuantity2<Frequency>
    {
        public Frequency X { get; }
        public Frequency Y { get; }

        Scalar IQuantity2.XMagnitude => X.Magnitude;
        Scalar IQuantity2.YMagnitude => Y.Magnitude;

        public Frequency2(Scalar2 components)
        {
            X = new Frequency(components.X);
            Y = new Frequency(components.Y);
        }

        public Frequency2(Scalar2 components, UnitOfFrequency unit)
        {
            X = new Frequency(components.X, unit);
            Y = new Frequency(components.Y, unit);
        }

        public Frequency2(Scalar2 components, UnitOfFrequency unit, MetricPrefix prefix)
        {
            X = new Frequency(components.X, unit, prefix);
            Y = new Frequency(components.Y, unit, prefix);
        }

        public Frequency2(Frequency x, Frequency y)
        {
            X = x;
            Y = y;
        }

        public Frequency2 Normalize() => this / Magnitude().Magnitude;
        public UnhandledQuantity SquaredMagnitude() => Dot(this);
        public Frequency Magnitude() => new(SquaredMagnitude().Sqrt().Magnitude);
        public UnhandledQuantity Dot(Frequency2 other) => Dot(this, other);

        public static UnhandledQuantity Dot(Frequency2 a, Frequency2 b) => a.X * b.X + a.Y * b.Y;

        public bool Equals(Frequency2? other) => X.Equals(other?.X) && Y.Equals(other?.Y);
        public override bool Equals(object? obj) => Equals(obj as Frequency2);

        public override int GetHashCode() => (X, Y).GetHashCode();
        public override string ToString() => $"({X.Hertz}, {Y.Hertz}) [Hz]";

        public static bool operator ==(Frequency2? a, Frequency2? b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(Frequency2? a, Frequency2? b) => !(a == b);

        public static Frequency2 operator +(Frequency2 a) => a;
        public static Frequency2 operator -(Frequency2 a) => new(-a.X, -a.Y);
        public static Frequency2 operator +(Frequency2 a, Frequency2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Frequency2 operator -(Frequency2 a, Frequency2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Frequency2 operator %(Frequency2 a, Frequency2 b) => new(a.X % b.X, a.Y % b.Y);
        public static UnhandledQuantity2 operator *(Frequency2 a, Frequency2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Scalar2 operator /(Frequency2 a, Frequency2 b) => new(a.X / b.X, a.Y / b.Y);

        public static Frequency2 operator *(Frequency2 a, Scalar2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Frequency2 operator *(Scalar2 a, Frequency2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Frequency2 operator /(Frequency2 a, Scalar2 b) => new(a.X / b.X, a.Y / b.Y);

        public static UnhandledQuantity2 operator *(Frequency2 a, IQuantity2 b) => new(a.X * b.XMagnitude, a.Y * b.YMagnitude);
        public static UnhandledQuantity2 operator /(Frequency2 a, IQuantity2 b) => new(a.X / b.XMagnitude, a.Y / b.YMagnitude);

        public static Velocity2 operator *(Frequency2 a, Length2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Acceleration2 operator *(Frequency2 a, Velocity2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Jerk2 operator *(Frequency2 a, Acceleration2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Scalar2 operator /(Frequency2 a, Frequency b) => new(a.X / b, a.Y / b);
        public static Scalar2 operator /(Frequency a, Frequency2 b) => new(a / b.X, a / b.Y);

        public static Frequency2 operator *(Frequency2 a, Scalar b) => new(a.X * b, a.Y * b);
        public static Frequency2 operator *(Scalar a, Frequency2 b) => new(a * b.X, a * b.Y);
        public static Frequency2 operator /(Frequency2 a, Scalar b) => new(a.X / b, a.Y / b);

        public static UnhandledQuantity2 operator *(Frequency2 a, IQuantity b) => new(a.X * b, a.Y * b);
        public static UnhandledQuantity2 operator *(IQuantity a, Frequency2 b) => new(b.X * a, b.Y * a);
        public static UnhandledQuantity2 operator /(Frequency2 a, IQuantity b) => new(a.X / b, a.Y / b);
        public static UnhandledQuantity2 operator /(IQuantity a, Frequency2 b) => new(b.X / a, b.Y / a);
    }
}
