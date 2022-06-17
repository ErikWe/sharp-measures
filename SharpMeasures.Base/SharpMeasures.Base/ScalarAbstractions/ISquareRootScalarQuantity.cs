namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as the square root of a quantity <typeparamref name="TSquare"/>.</summary>
/// <typeparam name="TSquare">The scalar quantity that is the square of <see langword="this"/>.</typeparam>
public interface ISquareRootScalarQuantity<out TSquare> :
    IScalarQuantity
    where TSquare : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> ² }.</summary>
    public abstract TSquare Square();
}

/// <summary><inheritdoc path="/summary"/></summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TSquare"><inheritdoc path="/typeparam[@name='TSquare']"/></typeparam>
public interface ISquareRootScalarQuantity<out TSelf, TSquare> :
    ISquareRootScalarQuantity<TSquare>
    where TSelf : ISquareRootScalarQuantity<TSelf, TSquare>
    where TSquare : IScalarQuantity
{
    /// <summary>Computes { √ <paramref name="square"/> }.</summary>
    /// <param name="square">The radicand of { √ <paramref name="square"/> }.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TSquare square);
}
