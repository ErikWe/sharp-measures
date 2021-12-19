using System;

namespace ErikWe.SharpMeasures.Quantities.Definitions
{
    public struct UnhandledQuantity : IQuantity, IEquatable<UnhandledQuantity>, IComparable<UnhandledQuantity>
    {
        public Scalar Magnitude { get; }

        public UnhandledQuantity(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public UnhandledQuantity(IQuantity quantity)
        {
            Magnitude = quantity.Magnitude;
        }

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public UnhandledQuantity Abs() => new(Magnitude.Abs());
        public UnhandledQuantity Floor() => new(Magnitude.Floor());
        public UnhandledQuantity Ceiling() => new(Magnitude.Ceiling());
        public UnhandledQuantity Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public UnhandledQuantity Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(UnhandledQuantity other) => Magnitude.Equals(other.Magnitude);
        public override bool Equals(object? obj) => obj is UnhandledQuantity other && Equals(other);
        public int CompareTo(UnhandledQuantity other) => Magnitude.CompareTo(other.Magnitude);

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Magnitude} [undef]";

        public static bool operator ==(UnhandledQuantity? x, UnhandledQuantity? y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(UnhandledQuantity? x, UnhandledQuantity? y) => !(x == y);

        public static UnhandledQuantity operator +(UnhandledQuantity x) => x;
        public static UnhandledQuantity operator -(UnhandledQuantity x) => new(-x.Magnitude);
        public static UnhandledQuantity operator +(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude + y.Magnitude);
        public static UnhandledQuantity operator -(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude - y.Magnitude);
        public static UnhandledQuantity operator %(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude / y.Magnitude);

        public static UnhandledQuantity operator *(UnhandledQuantity x, Scalar y) => new(x.Magnitude * y);
        public static UnhandledQuantity operator *(Scalar x, UnhandledQuantity y) => new(x * y.Magnitude);
        public static UnhandledQuantity operator /(UnhandledQuantity x, Scalar y) => new(x.Magnitude / y);
        public static UnhandledQuantity operator /(Scalar x, UnhandledQuantity y) => new(x / y.Magnitude);

        public static UnhandledQuantity operator *(UnhandledQuantity x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(UnhandledQuantity x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude < y.Magnitude;
        public static bool operator >(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude >= y.Magnitude;
    }
}
