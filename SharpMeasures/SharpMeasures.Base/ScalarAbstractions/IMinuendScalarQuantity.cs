namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <see langword="this"/> - <typeparamref name="TSubtrahend"/> }, resulting in a quantity of type
/// <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TDifference">The scalar quantity that represents the result of { <see langword="this"/> - <typeparamref name="TSubtrahend"/> }.</typeparam>
/// <typeparam name="TSubtrahend">The scalar quantity that represents the subtrahend of { <see langword="this"/> - <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IMinuendScalarQuantity<out TDifference, in TSubtrahend>
    : IScalarQuantity
    where TDifference : IScalarQuantity
    where TSubtrahend : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
    /// <param name="subtrahend">The subtrahend of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
    public abstract TDifference Subtract(TSubtrahend subtrahend);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> - <typeparamref name="TSubtrahend"/> }, resulting in a quantity of type
/// <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TDifference">The scalar quantity that represents the result of { <typeparamref name="TSelf"/> - <typeparamref name="TSubtrahend"/> }.</typeparam>
/// <typeparam name="TSubtrahend">The scalar quantity that represents the subtrahend of { <typeparamref name="TSelf"/> - <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IMinuendScalarQuantity<in TSelf, out TDifference, in TSubtrahend>
    : IMinuendScalarQuantity<TDifference, TSubtrahend>
    where TSelf : IMinuendScalarQuantity<TSelf, TDifference, TSubtrahend>
    where TDifference : IScalarQuantity
    where TSubtrahend : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> - <paramref name="y"/> }.</summary>
    /// <param name="x">The minuend of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    /// <param name="y">The subtrahend of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TDifference operator -(TSelf x, TSubtrahend y);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> - <typeparamref name="TSelf"/> }, resulting in a quantity
/// of type <typeparamref name="TSelf"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IMinuendScalarQuantity<TSelf> : IMinuendScalarQuantity<TSelf, TSelf, TSelf> where TSelf : IMinuendScalarQuantity<TSelf> { }
