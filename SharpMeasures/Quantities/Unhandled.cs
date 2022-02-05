namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly record struct Unhandled(double Magnitude) :
    IComparable<Unhandled>,
    IScalarQuantity,
    IScalableScalarQuantity<Unhandled>,
    IInvertibleScalarQuantity<Unhandled>,
    ISquarableScalarQuantity<Unhandled>,
    ICubableScalarQuantity<Unhandled>,
    ISquareRootableScalarQuantity<Unhandled>,
    ICubeRootableScalarQuantity<Unhandled>,
    IAddableScalarQuantity<Unhandled, Unhandled>,
    ISubtractableScalarQuantity<Unhandled, Unhandled>,
    IMultiplicableScalarQuantity<Unhandled, Scalar>,
    IDivisibleScalarQuantity<Unhandled, Scalar>,
    IMultiplicableScalarQuantity<Unhandled, Unhandled>,
    IDivisibleScalarQuantity<Unhandled, Unhandled>,
    IGenericallyMultiplicableScalarQuantity,
    IGenericallyDivisibleScalarQuantity
{
    public static Unhandled Zero { get; } = new(0);
    public static Unhandled One { get; } = new(1);

    public bool IsNaN => double.IsNaN(Magnitude);
    public bool IsZero => Magnitude == 0;
    public bool IsPositive => Magnitude > 0;
    public bool IsNegative => double.IsNegative(Magnitude);
    public bool IsFinite => double.IsFinite(Magnitude);
    public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude);
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude);

    public Unhandled Absolute() => new(Math.Abs(Magnitude));
    public Unhandled Floor() => new(Math.Floor(Magnitude));
    public Unhandled Ceiling() => new(Math.Ceiling(Magnitude));
    public Unhandled Round() => new(Math.Round(Magnitude));

    public Unhandled Power(double exponent) => new(Math.Pow(Magnitude, exponent));
    public Unhandled Invert() => new(1 / Magnitude);
    public Unhandled Square() => new(Magnitude * Magnitude);
    public Unhandled Cube() => new(Magnitude * Magnitude * Magnitude);
    public Unhandled SquareRoot() => new(Math.Sqrt(Magnitude));
    public Unhandled CubeRoot() => new(Math.Cbrt(Magnitude));

    public int CompareTo(Unhandled other) => Magnitude.CompareTo(other.Magnitude);
    public override string ToString() => $"{Magnitude} [undef]";

    public Unhandled Plus() => this;
    public Unhandled Negate() => new(-Magnitude);
    public static Unhandled operator +(Unhandled x) => x;
    public static Unhandled operator -(Unhandled x) => new(-x.Magnitude);

    public Unhandled Add(Unhandled term) => new(Magnitude + term.Magnitude);
    public Unhandled Subtract(Unhandled term) => new(Magnitude - term.Magnitude);
    public static Unhandled operator +(Unhandled x, Unhandled y) => new(x.Magnitude + y.Magnitude);
    public static Unhandled operator -(Unhandled x, Unhandled y) => new(x.Magnitude - y.Magnitude);

    public Unhandled Multiply(Unhandled factor) => new(Magnitude * factor.Magnitude);
    public Unhandled Divide(Unhandled divisor) => new(Magnitude / divisor.Magnitude);
    public static Unhandled operator *(Unhandled x, Unhandled y) => new(x.Magnitude * y.Magnitude);
    public static Unhandled operator /(Unhandled x, Unhandled y) => new(x.Magnitude / y.Magnitude);

    public Unhandled Remainder(double divisor) => new(Magnitude % divisor);
    public Unhandled Multiply(double factor) => new(Magnitude * factor);
    public Unhandled Divide(double divisor) => new(Magnitude / divisor);
    public static Unhandled operator %(Unhandled x, double y) => new(x.Magnitude % y);
    public static Unhandled operator *(Unhandled x, double y) => new(x.Magnitude * y);
    public static Unhandled operator *(double x, Unhandled y) => new(x * y.Magnitude);
    public static Unhandled operator /(Unhandled x, double y) => new(x.Magnitude / y);
    public static Unhandled operator /(double x, Unhandled y) => new(x / y.Magnitude);

    public Unhandled Remainder(Scalar divisor) => new(Magnitude % divisor.Magnitude);
    public Unhandled Multiply(Scalar factor) => new(Magnitude * factor.Magnitude);
    public Unhandled Divide(Scalar divisor) => new(Magnitude / divisor.Magnitude);
    public static Unhandled operator %(Unhandled x, Scalar y) => new(x.Magnitude % y.Magnitude);
    public static Unhandled operator *(Unhandled x, Scalar y) => new(x.Magnitude * y.Magnitude);
    public static Unhandled operator /(Unhandled x, Scalar y) => new(x.Magnitude / y.Magnitude);

    public Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity => new(Magnitude * factor.Magnitude);
    public Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity => new(Magnitude / divisor.Magnitude);
    public static Unhandled operator *(Unhandled x, IScalarQuantity y) => new(x.Magnitude * (y?.Magnitude ?? double.NaN));
    public static Unhandled operator /(Unhandled x, IScalarQuantity y) => new(x.Magnitude / (y?.Magnitude ?? double.NaN));

    public static bool operator <(Unhandled x, Unhandled y) => x.Magnitude < y.Magnitude;
    public static bool operator >(Unhandled x, Unhandled y) => x.Magnitude > y.Magnitude;
    public static bool operator <=(Unhandled x, Unhandled y) => x.Magnitude <= y.Magnitude;
    public static bool operator >=(Unhandled x, Unhandled y) => x.Magnitude >= y.Magnitude;

    public double ToDouble() => Magnitude;
    public static implicit operator double(Unhandled x) => x.Magnitude;

    public Scalar ToScalar() => new(Magnitude);
    public static explicit operator Scalar(Unhandled x) => new(x.Magnitude);
    
    public static Unhandled FromDouble(double x) => new(x);
    public static explicit operator Unhandled(double x) => new(x);

    public static Unhandled FromScalar(Scalar x) => new(x);
    public static explicit operator Unhandled(Scalar x) => new(x);
}