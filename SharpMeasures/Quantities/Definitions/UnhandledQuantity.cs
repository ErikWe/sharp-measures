namespace ErikWe.SharpMeasures.Quantities.Definitions
{
    public struct UnhandledQuantity : IQuantity
    {
        public Scalar Magnitude { get; }

        public UnhandledQuantity(Scalar magnitude)
        {
            Magnitude = magnitude;
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

        public override bool Equals(object? obj)
        {
            if (obj is UnhandledQuantity other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Magnitude} [undef]";

        public int CompareTo(UnhandledQuantity other)
        {
            if (this > other)
            {
                return 1;
            }
            else if (this < other)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public static bool operator ==(UnhandledQuantity? x, UnhandledQuantity? y)
        {
            if (x is null)
            {
                return y is null;
            }
            else
            {
                return x.Equals(y);
            }
        }

        public static bool operator !=(UnhandledQuantity? x, UnhandledQuantity? y) => !(x == y);

        public static UnhandledQuantity operator +(UnhandledQuantity x) => x;
        public static UnhandledQuantity operator -(UnhandledQuantity x) => new(-x.Magnitude);
        public static UnhandledQuantity operator +(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude + y.Magnitude);
        public static UnhandledQuantity operator -(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude - y.Magnitude);
        public static UnhandledQuantity operator %(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(UnhandledQuantity x, UnhandledQuantity y) => new(x.Magnitude / y.Magnitude);

        public static UnhandledQuantity operator *(UnhandledQuantity x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(UnhandledQuantity x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude < y.Magnitude;
        public static bool operator >(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(UnhandledQuantity x, UnhandledQuantity y) => x.Magnitude >= y.Magnitude;
    }
}
