namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as the square of a quantity <typeparamref name="TSquareRoot"/>.</summary>
/// <typeparam name="TSquareRoot">The scalar quantity that is the square root of <see langword="this"/>.</typeparam>
public interface ISquareScalarQuantity<out TSquareRoot> :
    IScalarQuantity
    where TSquareRoot : IScalarQuantity
{
    /// <summary>Computes { √ <see langword="this"/> }.</summary>
    public abstract TSquareRoot SquareRoot();
}

/// <summary><inheritdoc path="/summary"/></summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TSquareRoot"><inheritdoc path="/typeparam[@name='TSquareRoot']"/></typeparam>
public interface ISquareScalarQuantity<out TSelf, TSquareRoot> :
    ISquareScalarQuantity<TSquareRoot>
    where TSelf : ISquareScalarQuantity<TSelf, TSquareRoot>
    where TSquareRoot : IScalarQuantity
{
    /// <summary>Computes { <paramref name="squareRoot"/> ² }.</summary>
    /// <param name="squareRoot">The base of { <paramref name="squareRoot"/> ² }.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TSquareRoot squareRoot);
}
