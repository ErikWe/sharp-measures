using ErikWe.SharpMeasures.Quantities;

using System;

namespace ErikWe.SharpMeasures.Units
{
    public struct MetricPrefix : IEquatable<MetricPrefix>, IComparable<MetricPrefix>
    {
        public static readonly MetricPrefix Yotta = new(Math.Pow(10, 24));
        public static readonly MetricPrefix Zetta = new(Math.Pow(10, 21));
        public static readonly MetricPrefix Exa = new(Math.Pow(10, 18));
        public static readonly MetricPrefix Peta = new(Math.Pow(10, 15));
        public static readonly MetricPrefix Tera = new(Math.Pow(10, 12));
        public static readonly MetricPrefix Giga = new(Math.Pow(10, 9));
        public static readonly MetricPrefix Mega = new(Math.Pow(10, 6));
        public static readonly MetricPrefix Kilo = new(Math.Pow(10, 3));
        public static readonly MetricPrefix Hecto = new(Math.Pow(10, 2));
        public static readonly MetricPrefix Deca = new(Math.Pow(10, 1));
        public static readonly MetricPrefix Identity = new(Math.Pow(10, 0));
        public static readonly MetricPrefix Deci = new(Math.Pow(10, -1));
        public static readonly MetricPrefix Centi = new(Math.Pow(10, -2));
        public static readonly MetricPrefix Milli = new(Math.Pow(10, -3));
        public static readonly MetricPrefix Micro = new(Math.Pow(10, -6));
        public static readonly MetricPrefix Nano = new(Math.Pow(10, -9));
        public static readonly MetricPrefix Pico = new(Math.Pow(10, -12));
        public static readonly MetricPrefix Femto = new(Math.Pow(10, -15));
        public static readonly MetricPrefix Atto = new(Math.Pow(10, -18));
        public static readonly MetricPrefix Zepto = new(Math.Pow(10, -21));
        public static readonly MetricPrefix Yocto = new(Math.Pow(10, -24));

        public Scalar Scale { get; }

        private MetricPrefix(Scalar scale)
        {
            Scale = scale;
        }

        public bool Equals(MetricPrefix other) => Scale.Equals(other.Scale);

        public override bool Equals(object? obj)
        {
            if (obj is MetricPrefix other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Scale.GetHashCode();
        public override string ToString() => $"{Scale}x";

        public int CompareTo(MetricPrefix other)
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

        public static bool operator ==(MetricPrefix? x, MetricPrefix? y)
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

        public static bool operator !=(MetricPrefix? x, MetricPrefix? y) => !(x == y);

        public static Scalar operator *(MetricPrefix x, MetricPrefix y) => x.Scale * y.Scale;
        public static Scalar operator *(MetricPrefix x, Scalar y) => x.Scale * y;
        public static Scalar operator *(Scalar x, MetricPrefix y) => x * y.Scale;
        public static Scalar operator /(MetricPrefix x, MetricPrefix y) => x.Scale / y.Scale;
        public static Scalar operator /(MetricPrefix x, Scalar y) => x.Scale / y;
        public static Scalar operator /(Scalar x, MetricPrefix y) => x / y.Scale;

        public static bool operator <(MetricPrefix x, MetricPrefix y) => x.Scale < y.Scale;
        public static bool operator >(MetricPrefix x, MetricPrefix y) => x.Scale > y.Scale;
        public static bool operator <=(MetricPrefix x, MetricPrefix y) => x.Scale <= y.Scale;
        public static bool operator >=(MetricPrefix x, MetricPrefix y) => x.Scale >= y.Scale;

        public static implicit operator double(MetricPrefix x) => x.Scale;
        public static implicit operator Scalar(MetricPrefix x) => x.Scale;
    }
}
