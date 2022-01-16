namespace ErikWe.SharpMeasures.Quantities;

/// <summary>Describes common properties of scalar quantities.</summary>
public interface IScalarQuantity
{
    /// <summary>The magnitude of the quantity.</summary>
    public abstract double Magnitude { get; }

    /// <summary>Indicates whether the magnitude of the quantity is NaN.</summary>
    public abstract bool IsNaN { get; }
    /// <summary>Indicates whether the magnitude of the quantity is zero.</summary>
    public abstract bool IsZero { get; }
    /// <summary>Indicates whether the magnitude of the quantity is positive.</summary>
    public abstract bool IsPositive { get; }
    /// <summary>Indicates whether the magnitude of the quantity is negative.</summary>
    public abstract bool IsNegative { get; }
    /// <summary>Indicates whether the magnitude of the quantity is finite.</summary>
    public abstract bool IsFinite { get; }
    /// <summary>Indicates whether the magnitude of the quantity is infinite.</summary>
    public abstract bool IsInfinite { get; }
    /// <summary>Indicates whether the magnitude of the quantity is infinite, and positive.</summary>
    public abstract bool IsPositiveInfinity { get; }
    /// <summary>Indicates whether the magnitude of the quantity is infinite, and negative.</summary>
    public abstract bool IsNegativeInfinity { get; }

    /// <summary>Multiplies the quantity by the <see cref="Unhandled"/> quantity <paramref name="factor"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="factor">The factor by which the quantity is multiplied.</param>
    public abstract Unhandled Multiply(Unhandled factor);
    /// <summary>Multiplies the quantity by the quantity <paramref name="factor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which multiplication is done.</typeparam>
    /// <param name="factor">The factor by which the quantity is multiplied.</param>
    public abstract Unhandled Multiply<TScalarQuantity>(TScalarQuantity factor) where TScalarQuantity : IScalarQuantity;

    /// <summary>Divides the quantity by the <see cref="Unhandled"/> quantity <paramref name="divisor"/> - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <param name="divisor">The divisor by which the quantity is divided.</param>
    public abstract Unhandled Divide(Unhandled divisor);
    /// <summary>Divides the quantity by the quantity <paramref name="divisor"/> of type <typeparamref name="TScalarQuantity"/>
    /// - resulting in an <see cref="Unhandled"/> quantity.</summary>
    /// <typeparam name="TScalarQuantity">The type of the quantity by which division is done.</typeparam>
    /// <param name="divisor">The divisor by which the quantity is divided.</param>
    public abstract Unhandled Divide<TScalarQuantity>(TScalarQuantity divisor) where TScalarQuantity : IScalarQuantity;
}

/// <summary>Describes common properties of scalar quantities.</summary>
/// <typeparam name="TScalarQuantity">Type of the scalar quantity.</typeparam>
public interface IScalarQuantity<out TScalarQuantity> :
    IScalarQuantity
    where TScalarQuantity : IScalarQuantity
{
    /// <summary>Produces a <typeparamref name="TScalarQuantity"/>, with magnitude equal to the absolute of the original magnitude.</summary>
    public abstract TScalarQuantity Absolute();
    /// <summary>Produces a <typeparamref name="TScalarQuantity"/>, with magnitude equal to the floor of the original magnitude.</summary>
    public abstract TScalarQuantity Floor();
    /// <summary>Produces a <typeparamref name="TScalarQuantity"/>, with magnitude equal to the ceiling of the original magnitude.</summary>
    public abstract TScalarQuantity Ceiling();
    /// <summary>Produces a <typeparamref name="TScalarQuantity"/>, with magnitude equal to the original magnitude, rounded to the nearest integer.</summary>
    public abstract TScalarQuantity Round();

    /// <summary>Unary plus, resulting in an unmodified <typeparamref name="TScalarQuantity"/>.</summary>
    public abstract TScalarQuantity Plus();
    /// <summary>Negation, resulting in a <typeparamref name="TScalarQuantity"/> with negated magnitude.</summary>
    public abstract TScalarQuantity Negate();

    /// <summary>Produces a <typeparamref name="TScalarQuantity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public abstract TScalarQuantity Remainder(double divisor);
    /// <summary>Produces a <typeparamref name="TScalarQuantity"/>, with magnitude equal to the remainder from division of the original
    /// magnitude by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, from division by which the remainder is retrieved.</param>
    public abstract TScalarQuantity Remainder(Scalar divisor);

    /// <summary>Scales the quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the quantity is scaled.</param>
    public abstract TScalarQuantity Multiply(double factor);
    /// <summary>Scales the quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The factor by which the quantity is scaled.</param>
    public abstract TScalarQuantity Multiply(Scalar factor);

    /// <summary>Scales the quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which the quantity is divided.</param>
    public abstract TScalarQuantity Divide(double divisor);
    /// <summary>Scales the quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The divisor, by which quantity is divided.</param>
    public abstract TScalarQuantity Divide(Scalar divisor);
}

/// <summary>Describes common properties of scalar quantities that may be inverted, to produce quantities of type
/// <typeparamref name="TInverseScalarQuantity"/>.</summary>
/// <typeparam name="TInverseScalarQuantity">The scalar quantity that is the inverse of the original quantity.</typeparam>
public interface IInvertibleScalarQuantity<out TInverseScalarQuantity>
    where TInverseScalarQuantity : IScalarQuantity
{
    /// <summary>Inverts the quantity, resulting in a <typeparamref name="TInverseScalarQuantity"/>.</summary>
    public abstract TInverseScalarQuantity Invert();
}

/// <summary>Describes common properties of scalar quantities that may be squared, to produce quantities of type
/// <typeparamref name="TSquareScalarQuantity"/>.</summary>
/// <typeparam name="TSquareScalarQuantity">The scalar quantity that is the square of the original quantity.</typeparam>
public interface ISquarableScalarQuantity<out TSquareScalarQuantity>
    where TSquareScalarQuantity : IScalarQuantity
{
    /// <summary>Squares the quantity, resulting in a <typeparamref name="TSquareScalarQuantity"/>.</summary>
    public abstract TSquareScalarQuantity Square();
}

/// <summary>Describes common properties of scalar quantities that may be cubed, to produce quantities of type
/// <typeparamref name="TCubeScalarQuantity"/>.</summary>
/// <typeparam name="TCubeScalarQuantity">The scalar quantity that is the cube of the original quantity.</typeparam>
public interface ICubableScalarQuantity<out TCubeScalarQuantity>
    where TCubeScalarQuantity : IScalarQuantity
{
    /// <summary>Cubes the quantity, resulting in a <typeparamref name="TCubeScalarQuantity"/>.</summary>
    public abstract TCubeScalarQuantity Cube();
}

/// <summary>Describes common properties of scalar quantities to which the square root may be applied, to produce quantities of type
/// <typeparamref name="TSquareRootScalarQuantity"/>.</summary>
/// <typeparam name="TSquareRootScalarQuantity">The scalar quantity that is the square root of the original quantity.</typeparam>
public interface ISquareRootableScalarQuantity<out TSquareRootScalarQuantity>
    where TSquareRootScalarQuantity : IScalarQuantity
{
    /// <summary>Takes the square root of the quantity, resulting in a <typeparamref name="TSquareRootScalarQuantity"/>.</summary>
    public abstract TSquareRootScalarQuantity SquareRoot();
}

/// <summary>Describes common properties of scalar quantities to which the cube root may be applied, to produce quantities of type
/// <typeparamref name="TCubeRootScalarQuantity"/>.</summary>
/// <typeparam name="TCubeRootScalarQuantity">The scalar quantity that is the cube root of the original quantity.</typeparam>
public interface ICubeRootableScalarQuantity<out TCubeRootScalarQuantity>
    where TCubeRootScalarQuantity : IScalarQuantity
{
    /// <summary>Takes the cube root of the quantity, resulting in a <typeparamref name="TCubeRootScalarQuantity"/>.</summary>
    public abstract TCubeRootScalarQuantity CubeRoot();
}

/// <summary>Describes common properties of scalar quantities to which quantities of type <typeparamref name="TTermScalarQuantity"/> may be added to produce quantities
/// of type <typeparamref name="TSumScalarQuantity"/>.</summary>
/// <typeparam name="TSumScalarQuantity">The scalar quantity that is the sum of the two quantities.</typeparam>
/// <typeparam name="TTermScalarQuantity">The scalar quantity that may be added.</typeparam>
public interface IAddableScalarQuantity<out TSumScalarQuantity, in TTermScalarQuantity>
    where TSumScalarQuantity : IScalarQuantity
    where TTermScalarQuantity : IScalarQuantity
{
    /// <summary>Adds the quantity <paramref name="term"/> to the original quantity, resulting in a <typeparamref name="TSumScalarQuantity"/>.</summary>
    /// <param name="term">The quantity that is added to the original quantity.</param>
    public abstract TSumScalarQuantity Add(TTermScalarQuantity term);
}

/// <summary>Describes common properties of scalar quantities from which quantities of type <typeparamref name="TTermScalarQuantity"/> may be subtracted to produce
/// quantities of type <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
/// <typeparam name="TDifferenceScalarQuantity">The scalar quantity that is the difference of the two quantities.</typeparam>
/// <typeparam name="TTermScalarQuantity">The scalar quantity that may be subtracted.</typeparam>
public interface ISubtractableScalarQuantity<out TDifferenceScalarQuantity, in TTermScalarQuantity>
    where TDifferenceScalarQuantity : IScalarQuantity
    where TTermScalarQuantity : IScalarQuantity
{
    /// <summary>Subtracts the quantity <paramref name="term"/> from the original quantity, resulting in a <typeparamref name="TDifferenceScalarQuantity"/>.</summary>
    /// <param name="term">The quantity that is subtracted from the original quantity.</param>
    public abstract TDifferenceScalarQuantity Subtract(TTermScalarQuantity term);
}

/// <summary>Describes common properties of scalar quantities that may be multiplied by quantities of type <typeparamref name="TFactorScalarQuantity"/>
/// to produce quantities of type <typeparamref name="TProductScalarQuantity"/>.</summary>
/// <typeparam name="TProductScalarQuantity">The scalar quantity that is the product of the two quantities.</typeparam>
/// <typeparam name="TFactorScalarQuantity">The scalar quantity by which the original quantity may be multiplied.</typeparam>
public interface IMultiplicableScalarQuantity<out TProductScalarQuantity, in TFactorScalarQuantity>
    where TProductScalarQuantity : IScalarQuantity
    where TFactorScalarQuantity : IScalarQuantity
{
    /// <summary>Multiplies the original quantity by the quantity <paramref name="factor"/>, resulting in a <typeparamref name="TProductScalarQuantity"/>.</summary>
    /// <param name="factor">The quantity by which the original quantity is multiplied.</param>
    public abstract TProductScalarQuantity Multiply(TFactorScalarQuantity factor);
}

/// <summary>Describes common properties of scalar quantities that may be divided by quantities of type <typeparamref name="TDivisorScalarQuantity"/>
/// to produce quantities of type <typeparamref name="TQuotientScalarQuantity"/>.</summary>
/// <typeparam name="TQuotientScalarQuantity">The scalar quantity that is the quotient of the two quantities.</typeparam>
/// <typeparam name="TDivisorScalarQuantity">The scalar quantity by which the original quantity may be divided.</typeparam>
public interface IDivisibleScalarQuantity<out TQuotientScalarQuantity, in TDivisorScalarQuantity>
    where TQuotientScalarQuantity : IScalarQuantity
    where TDivisorScalarQuantity : IScalarQuantity
{
    /// <summary>Divides the original quantity by the quantity <paramref name="divisor"/>, resulting in a <typeparamref name="TQuotientScalarQuantity"/>.</summary>
    /// <param name="divisor">The quantity by which the original quantity is divided.</param>
    public abstract TQuotientScalarQuantity Divide(TDivisorScalarQuantity divisor);
}