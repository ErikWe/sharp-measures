﻿using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Scalar : IEquatable<Scalar>, IComparable<Scalar>, IQuantity
    {
        Scalar IQuantity.Magnitude => this;

        public double Magnitude { get; }

        public Scalar(double magnitude)
        {
            Magnitude = magnitude;
        }

        public bool IsNaN => double.IsNaN(Magnitude);
        public bool IsZero => Magnitude == 0;
        public bool IsPositive => Magnitude > 0;
        public bool IsNegative => double.IsNegative(Magnitude);
        public bool IsFinite => double.IsFinite(Magnitude);
        public bool IsInfinity => double.IsInfinity(Magnitude);
        public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
        public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

        public Scalar Abs() => new(Math.Abs(Magnitude));
        public Scalar Floor() => new(Math.Floor(Magnitude));
        public Scalar Ceiling() => new(Math.Ceiling(Magnitude));
        public Scalar Round() => new(Math.Round(Magnitude));
        public Scalar Sqrt() => new(Math.Sqrt(Magnitude));
        public Scalar Square() => new(Magnitude * Magnitude);
        public Scalar Pow(double x) => new(Math.Pow(Magnitude, x));

        public bool Equals(Scalar other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Scalar other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{Magnitude}";

        public int CompareTo(Scalar other)
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

        public static bool operator ==(Scalar? x, Scalar? y)
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

        public static bool operator !=(Scalar? x, Scalar? y) => !(x == y);

        public static Scalar operator +(Scalar x) => x;
        public static Scalar operator -(Scalar x) => new(-x.Magnitude);
        public static Scalar operator +(Scalar x, Scalar y) => new(x.Magnitude + y.Magnitude);
        public static Scalar operator -(Scalar x, Scalar y) => new(x.Magnitude - y.Magnitude);
        public static Scalar operator %(Scalar x, Scalar y) => new(x.Magnitude % y.Magnitude);
        public static Scalar operator *(Scalar x, Scalar y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Scalar x, Scalar y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Scalar x, Scalar y) => x.Magnitude > y.Magnitude;
        public static bool operator >(Scalar x, Scalar y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Scalar x, Scalar y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Scalar x, Scalar y) => x.Magnitude >= y.Magnitude;

        public static implicit operator double(Scalar x) => x.Magnitude;
        public static implicit operator Scalar(double x) => new(x);
    }
}
