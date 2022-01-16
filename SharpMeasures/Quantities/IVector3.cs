namespace ErikWe.SharpMeasures.Quantities;

using System.Numerics;

public interface IVector3
{
    public abstract double X { get; }
    public abstract double Y { get; }
    public abstract double Z { get; }

    public abstract Vector3 AsVector3();

    public abstract Scalar Magnitude();
    public abstract Scalar SquaredMagnitude();

    public abstract Unhandled3 Multiply(Unhandled factor);
    public abstract Unhandled3 Divide(Unhandled divisor);

    public abstract Unhandled3 Multiply<TScalar>(TScalar factor) where TScalar : IScalarQuantity;
    public abstract Unhandled3 Divide<TScalar>(TScalar divisor) where TScalar : IScalarQuantity;

    public abstract Unhandled Dot(Unhandled3 factor);
    public abstract Unhandled3 Cross(Unhandled3 factor);

    public abstract Unhandled Dot<TVector3>(TVector3 factor) where TVector3 : IVector3;
    public abstract Unhandled3 Cross<TVector3>(TVector3 factor) where TVector3 : IVector3;
}

public interface IVector3<out TVector3> :
    IVector3
    where TVector3 : IVector3
{
    public abstract TVector3 Normalize();
    public abstract TVector3 Transform(Matrix4x4 transform);

    public abstract TVector3 Plus();
    public abstract TVector3 Negate();

    public abstract TVector3 Multiply(double factor);
    public abstract TVector3 Divide(double divisor);

    public abstract TVector3 Multiply(Scalar factor);
    public abstract TVector3 Divide(Scalar divisor);
}

public interface IVector3izableScalarQuantity<out TVector3>
    where TVector3 : IVector3
{
    public abstract TVector3 Multiply(Vector3 vector);
    public abstract TVector3 Multiply((double x, double y, double z) vector);
}

public interface IVector3MultiplicableScalar<out TProductVector3, in TFactorVector3>
    where TProductVector3 : IVector3
    where TFactorVector3 : IVector3
{
    public abstract TProductVector3 Multiply(TFactorVector3 factor);
}

public interface IAddableVector3<out TSumVector3, in TTermVector3>
    where TSumVector3 : IVector3
    where TTermVector3 : IVector3
{
    public abstract TSumVector3 Add(TTermVector3 term);
}

public interface ISubtractableVector3<out TDifferenceVector3, in TTermVector3>
    where TDifferenceVector3 : IVector3
    where TTermVector3 : IVector3
{
    public abstract TDifferenceVector3 Subtract(TTermVector3 term);
}

public interface IMultiplicableVector3<out TProductVector3, in TFactorScalar>
    where TProductVector3 : IVector3
    where TFactorScalar : IScalarQuantity
{
    public abstract TProductVector3 Multiply(TFactorScalar factor);
}

public interface IDivisibleVector3<out TQuotientVector3, in TDivisorScalar>
    where TQuotientVector3 : IVector3
    where TDivisorScalar : IScalarQuantity
{
    public abstract TQuotientVector3 Divide(TDivisorScalar divisor);
}

public interface IDotableVector3<out TResultScalar, in TFactorVector3>
    where TResultScalar : IScalarQuantity
    where TFactorVector3 : IVector3
{
    public abstract TResultScalar Dot(TFactorVector3 factor);
}

public interface ICrossableVector3<out TResultVector3, in TFactorVector3>
    where TResultVector3 : IVector3
    where TFactorVector3 : IVector3
{
    public abstract TResultVector3 Cross(TFactorVector3 factor);
}