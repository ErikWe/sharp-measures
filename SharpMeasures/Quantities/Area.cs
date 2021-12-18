using ErikWe.SharpMeasures.Quantities.Definitions;
using ErikWe.SharpMeasures.Units;

using System;

namespace ErikWe.SharpMeasures.Quantities
{
    public struct Area : IEquatable<Area>, IComparable<Area>, IQuantity
    {
        public static readonly Area Zero = new(0);

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfArea unit, MetricPrefix prefix) => prefix * magnitude * unit.Scale;

        private static Scalar ComputeMagnitude(Scalar magnitude, UnitOfLength unit, MetricPrefix prefix) => Math.Pow(prefix, 2) * magnitude * Math.Pow(unit.Scale, 2);

        public Scalar Magnitude { get; }

        public Area(Scalar magnitude)
        {
            Magnitude = magnitude;
        }

        public Area(Scalar magnitude, UnitOfArea unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Area(Scalar magnitude, UnitOfArea unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Area(Scalar magnitude, UnitOfLength unit)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, MetricPrefix.Identity);
        }

        public Area(Scalar magnitude, UnitOfLength unit, MetricPrefix prefix)
        {
            Magnitude = ComputeMagnitude(magnitude, unit, prefix);
        }

        public Scalar InUnit(UnitOfArea unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfArea unit, MetricPrefix prefix) => Magnitude / (prefix * unit.Scale);
        public Scalar InUnit(UnitOfLength unit) => InUnit(unit, MetricPrefix.Identity);
        public Scalar InUnit(UnitOfLength unit, MetricPrefix prefix) => Magnitude / (Math.Pow(prefix, 2) * Math.Pow(unit.Scale, 2));

        public Scalar Ares => InUnit(UnitOfArea.Are);
        public Scalar Hectares => InUnit(UnitOfArea.Hectare);
        public Scalar Acres => InUnit(UnitOfArea.Acre);

        public Scalar SquareMillimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Milli);
        public Scalar SquareCentimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Centi);
        public Scalar SquareDecimetres => InUnit(UnitOfLength.Metre, MetricPrefix.Deci);
        public Scalar SquareMetres => InUnit(UnitOfLength.Metre);
        public Scalar SquareKilometres => InUnit(UnitOfLength.Metre, MetricPrefix.Kilo);

        public Scalar SquareInches => InUnit(UnitOfLength.Inch);
        public Scalar SquareFeet => InUnit(UnitOfLength.Foot);
        public Scalar SquareYards => InUnit(UnitOfLength.Yard);
        public Scalar SquareMiles => InUnit(UnitOfLength.Mile);

        public bool IsNaN => Magnitude.IsNaN;
        public bool IsZero => Magnitude.IsZero;
        public bool IsPositive => Magnitude.IsPositive;
        public bool IsNegative => Magnitude.IsNegative;
        public bool IsFinite => Magnitude.IsFinite;
        public bool IsInfinity => Magnitude.IsInfinity;
        public bool IsPositiveInfinity => Magnitude.IsPositiveInfinity;
        public bool IsNegativeInfinity => Magnitude.IsNegativeInfinity;

        public Area Abs() => new(Magnitude.Abs());
        public Area Floor() => new(Magnitude.Floor());
        public Area Ceiling() => new(Magnitude.Ceiling());
        public Area Round() => new(Magnitude.Round());
        public UnhandledQuantity Pow(double x) => new(Magnitude.Pow(x));
        public UnhandledQuantity Square() => new(Magnitude.Square());
        public Length Sqrt() => new(Magnitude.Sqrt());

        public bool Equals(Area other) => Magnitude.Equals(other.Magnitude);

        public override bool Equals(object? obj)
        {
            if (obj is Area other)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => Magnitude.GetHashCode();
        public override string ToString() => $"{SquareMetres} [m^2]";

        public int CompareTo(Area other)
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

        public static bool operator ==(Area? x, Area? y)
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

        public static bool operator !=(Area? x, Area? y) => !(x == y);

        public static Area operator +(Area x) => x;
        public static Area operator -(Area x) => new(-x.Magnitude);
        public static Area operator +(Area x, Area y) => new(x.Magnitude + y.Magnitude);
        public static Area operator -(Area x, Area y) => new(x.Magnitude - y.Magnitude);
        public static Area operator %(Area x, Area y) => new(x.Magnitude % y.Magnitude);
        public static UnhandledQuantity operator *(Area x, Area y) => new(x.Magnitude * y.Magnitude);
        public static Scalar operator /(Area x, Area y) => new(x.Magnitude / y.Magnitude);

        public static Volume operator *(Area x, Length y) => new(x.Magnitude * y.Magnitude);
        public static Length operator /(Area x, Length y) => new(x.Magnitude / y.Magnitude);

        public static Area operator *(Area x, Scalar y) => new(x.Magnitude * y);
        public static Area operator *(Scalar x, Area y) => new(x * y.Magnitude);
        public static Area operator /(Area x, Scalar y) => new(x.Magnitude / y);

        public static UnhandledQuantity operator *(Area x, IQuantity y) => new(x.Magnitude * y.Magnitude);
        public static UnhandledQuantity operator /(Area x, IQuantity y) => new(x.Magnitude / y.Magnitude);

        public static bool operator <(Area x, Area y) => x.Magnitude < y.Magnitude;
        public static bool operator >(Area x, Area y) => x.Magnitude > y.Magnitude;
        public static bool operator <=(Area x, Area y) => x.Magnitude <= y.Magnitude;
        public static bool operator >=(Area x, Area y) => x.Magnitude >= y.Magnitude;
    }
}
