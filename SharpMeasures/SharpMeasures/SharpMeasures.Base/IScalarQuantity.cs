namespace SharpMeasures;

using System;

/// <summary>Describes a scalar quantity, having only magnitude.</summary>
public interface IScalarQuantity
{
    /// <summary>The magnitude of the scalar quantity.</summary>
    public abstract double Magnitude { get; }
}

/// <summary>Describes a scalar quantity that may be scaled by a <see cref="Scalar"/> or <see cref="double"/>.</summary>
/// <typeparam name="TScaledScalarQuantity">The scalar quantity that is the scaled version of the original quantity, generally <see langword="this"/>.</typeparam>
public interface IScalableScalarQuantity<out TScaledScalarQuantity> :
    IScalarQuantity
    where TScaledScalarQuantity : IScalarQuantity
{
    /// <summary>Scales the scalar quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The scalar quantity is scaled by this value.</param>
    public abstract TScaledScalarQuantity Multiply(double factor);
    /// <summary>Scales the scalar quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The scalar quantity is scaled by this value.</param>
    public abstract TScaledScalarQuantity Multiply(Scalar factor);

    /// <summary>Scales the scalar quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The scalar quantity is scaled through division by this value.</param>
    public abstract TScaledScalarQuantity Divide(double divisor);

    /// <summary>Scales the scalar quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The scalar quantity is scaled through division by this value.</param>
    public abstract TScaledScalarQuantity Divide(Scalar divisor);
}

/// <summary>Describes a scalar quantity that may be inverted, to produce a quantity of type
/// <typeparamref name="TInverseScalarQuantity"/>.</summary>
/// <typeparam name="TInverseScalarQuantity">The scalar quantity that is the inverse of the original quantity.</typeparam>
public interface IInvertibleScalarQuantity<out TInverseScalarQuantity> :
    IScalarQuantity
    where TInverseScalarQuantity : IScalarQuantity
{
    /// <summary>Inverts the quantity, resulting in a <typeparamref name="TInverseScalarQuantity"/>.</summary>
    public abstract TInverseScalarQuantity Invert();
}

/// <summary>Describes a scalar quantity that may be squared, to produce a quantity of type
/// <typeparamref name="TSquareScalarQuantity"/>.</summary>
/// <typeparam name="TSquareScalarQuantity">The scalar quantity that is the square of the original quantity.</typeparam>
public interface ISquarableScalarQuantity<out TSquareScalarQuantity> :
    IScalarQuantity
    where TSquareScalarQuantity : IScalarQuantity
{
    /// <summary>Squares the quantity, resulting in a <typeparamref name="TSquareScalarQuantity"/>.</summary>
    public abstract TSquareScalarQuantity Square();
}

/// <summary>Describes a scalar quantity that may be cubed, to produce a quantity of type
/// <typeparamref name="TCubeScalarQuantity"/>.</summary>
/// <typeparam name="TCubeScalarQuantity">The scalar quantity that is the cube of the original quantity.</typeparam>
public interface ICubableScalarQuantity<out TCubeScalarQuantity> :
    IScalarQuantity
    where TCubeScalarQuantity : IScalarQuantity
{
    /// <summary>Cubes the quantity, resulting in a <typeparamref name="TCubeScalarQuantity"/>.</summary>
    public abstract TCubeScalarQuantity Cube();
}

/// <summary>Describes a scalar quantity to which the square root may be applied, to produce a quantity of type
/// <typeparamref name="TSquareRootScalarQuantity"/>.</summary>
/// <typeparam name="TSquareRootScalarQuantity">The scalar quantity that is the square root of the original quantity.</typeparam>
public interface ISquareRootableScalarQuantity<out TSquareRootScalarQuantity> :
    IScalarQuantity
    where TSquareRootScalarQuantity : IScalarQuantity
{
    /// <summary>Takes the square root of the quantity, resulting in a <typeparamref name="TSquareRootScalarQuantity"/>.</summary>
    public abstract TSquareRootScalarQuantity SquareRoot();
}

/// <summary>Describes a scalar quantity to which the cube root may be applied, to produce a quantity of type
/// <typeparamref name="TCubeRootScalarQuantity"/>.</summary>
/// <typeparam name="TCubeRootScalarQuantity">The scalar quantity that is the cube root of the original quantity.</typeparam>
public interface ICubeRootableScalarQuantity<out TCubeRootScalarQuantity> :
    IScalarQuantity
    where TCubeRootScalarQuantity : IScalarQuantity
{
    /// <summary>Takes the cube root of the quantity, resulting in a <typeparamref name="TCubeRootScalarQuantity"/>.</summary>
    public abstract TCubeRootScalarQuantity CubeRoot();
}

/// <summary>Describes a scalar quantity to which a quantity of type <typeparamref name="TTermVector3Quantity"/> may be added to produce a quantity
/// of type <typeparamref name="TSumVector3Quantity"/>.</summary>
/// <typeparam name="TSumVector3Quantity">The scalar quantity that is the sum of the two quantities.</typeparam>
/// <typeparam name="TTermVector3Quantity">The tscalar quantity that may be added to the original quantity.</typeparam>
public interface IAddableScalarQuantity<out TSumVector3Quantity, in TTermVector3Quantity> :
    IScalarQuantity
    where TSumVector3Quantity : IScalarQuantity
    where TTermVector3Quantity : IScalarQuantity
{
    /// <summary>Adds the quantity <paramref name="term"/> to the original quantity, resulting in a <typeparamref name="TSumVector3Quantity"/>.</summary>
    /// <param name="term">The quantity that is added to the original quantity.</param>
    public abstract TSumVector3Quantity Add(TTermVector3Quantity term);
}

/// <summary>Describes a scalar quantity from which a quantity of type <typeparamref name="TTermScalarQuantity"/> may be subtracted to produce
/// a quantity of type <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
/// <typeparam name="TDifferenceScalarQuantity">The scalar quantity that is the difference of the two quantities.</typeparam>
/// <typeparam name="TTermScalarQuantity">The scalar quantity that may be subtracted from the original quantity.</typeparam>
public interface ISubtractableScalarQuantity<out TDifferenceScalarQuantity, in TTermScalarQuantity> :
    IScalarQuantity
    where TDifferenceScalarQuantity : IScalarQuantity
    where TTermScalarQuantity : IScalarQuantity
{
    /// <summary>Subtracts the quantity <paramref name="term"/> from the original quantity, resulting in a <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
    /// <param name="term">The quantity that is subtracted from the original quantity.</param>
    public abstract TDifferenceScalarQuantity Subtract(TTermScalarQuantity term);
}

/// <summary>Describes a scalar quantity that may be multiplied by a quantity of type <typeparamref name="TFactorScalarQuantity"/>
/// to produce a quantity of type <typeparamref name="TProductScalarQuantity"/>.</summary>
/// <typeparam name="TProductScalarQuantity">The scalar quantity that is the product of the two quantities.</typeparam>
/// <typeparam name="TFactorScalarQuantity">The scalar quantity by which the original quantity may be multiplied.</typeparam>
public interface IMultiplicableScalarQuantity<out TProductScalarQuantity, in TFactorScalarQuantity> :
    IScalarQuantity
    where TProductScalarQuantity : IScalarQuantity
    where TFactorScalarQuantity : IScalarQuantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/>, resulting in a <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    public abstract TProductScalarQuantity Multiply(TFactorScalarQuantity factor);
}

/// <summary>Describes a scalar quantity that may be divided by a quantity of type <typeparamref name="TDivisorScalarQuantity"/>
/// to produce a quantity of type <typeparamref name="TQuotientScalarQuantity"/>.</summary>
/// <typeparam name="TQuotientScalarQuantity">The scalar quantity that is the quotient of the two quantities.</typeparam>
/// <typeparam name="TDivisorScalarQuantity">The scalar quantity by which the original quantity may be divided.</typeparam>
public interface IDivisibleScalarQuantity<out TQuotientScalarQuantity, in TDivisorScalarQuantity> :
    IScalarQuantity
    where TQuotientScalarQuantity : IScalarQuantity
    where TDivisorScalarQuantity : IScalarQuantity
{
    /// <summary>Divides the original quantity by the quantity <paramref name="divisor"/>, resulting in a <typeparamref name="TQuotientScalarQuantity"/>.</summary>
    /// <param name="divisor">The quantity by which the original quantity is divided.</param>
    public abstract TQuotientScalarQuantity Divide(TDivisorScalarQuantity divisor);
}

/// <summary>Describes a scalar quantity that may be multiplied by a quantity of a generically provided type, to produce a quantity of another generically
/// provided type.</summary>
public interface IGenericallyMultiplicableScalarQuantity :
    IScalarQuantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/> of type <typeparamref name="TFactorScalarQuantity"/>, resulting in a
    /// <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <typeparam name="TProductScalarQuantity">The scalar quantity that is the product of the two quantities.</typeparam>
    /// <typeparam name="TFactorScalarQuantity">The scalar quantity by which the original quantity may be multiplied.</typeparam>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TProductScalarQuantity"/> from a <see langword="double"/>.</param>
    public abstract TProductScalarQuantity Multiply<TProductScalarQuantity, TFactorScalarQuantity>(TFactorScalarQuantity factor, Func<double, TProductScalarQuantity> factory)
        where TProductScalarQuantity : IScalarQuantity
        where TFactorScalarQuantity : IScalarQuantity;
}

/// <summary>Describes a scalar quantity that may be divided by a quantity of a generically provided type, to produce a iquantity of another generically
/// provided type.</summary>
public interface IGenericallyDivisibleScalarQuantity :
    IScalarQuantity
{
    /// <summary>Divides the original quantity by the quantity <paramref name="divisor"/> of type <typeparamref name="TDivisorScalarQuantity"/>, resulting in a
    /// <typeparamref name="TQuotientScalarQuantity"/>.</summary>
    /// <typeparam name="TQuotientScalarQuantity">The scalar quantity that is the quotient of the two quantities.</typeparam>
    /// <typeparam name="TDivisorScalarQuantity">The type of the scalar quantity by which the original quantity may be divided.</typeparam>
    /// <param name="divisor">The quantity by which the original quantity is divided.</param>
    /// <param name="factory">Delegate for constructing a <typeparamref name="TQuotientScalarQuantity"/> from a <see langword="double"/>.</param>
    public abstract TQuotientScalarQuantity Divide<TQuotientScalarQuantity, TDivisorScalarQuantity>(TDivisorScalarQuantity divisor, Func<double, TQuotientScalarQuantity> factory)
        where TQuotientScalarQuantity : IScalarQuantity
        where TDivisorScalarQuantity : IScalarQuantity;
}