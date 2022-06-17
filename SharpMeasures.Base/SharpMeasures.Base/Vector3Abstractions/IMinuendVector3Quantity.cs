namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <see langword="this"/> - <typeparamref name="TSubtrahend"/> },
/// resulting in a quantity of type <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TDifference">The three-dimensional vector quantity that represents the result of { <see langword="this"/> -
/// <typeparamref name="TSubtrahend"/> }.</typeparam>
/// <typeparam name="TSubtrahend">The three-dimensional vector quantity that represents the subtrahend of { <see langword="this"/> -
/// <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IMinuendVector3Quantity<out TDifference, in TSubtrahend>
    : IVector3Quantity
    where TDifference : IVector3Quantity
    where TSubtrahend : IVector3Quantity
{
    /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
    /// <param name="subtrahend">The subtrahend of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
    public abstract TDifference Subtract(TSubtrahend subtrahend);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TSelf"/> - <typeparamref name="TSubtrahend"/> },
/// resulting in a quantity of type <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TDifference">The three-dimensional vector quantity that represents the result of { <typeparamref name="TSelf"/> -
/// <typeparamref name="TSubtrahend"/> }.</typeparam>
/// <typeparam name="TSubtrahend">The three-dimensional vector quantity that represents the subtrahend of { <typeparamref name="TSelf"/> -
/// <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IMinuendVector3Quantity<in TSelf, out TDifference, in TSubtrahend>
    : IMinuendVector3Quantity<TDifference, TSubtrahend>
    where TSelf : IMinuendVector3Quantity<TSelf, TDifference, TSubtrahend>
    where TDifference : IVector3Quantity
    where TSubtrahend : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> - <paramref name="b"/> }.</summary>
    /// <param name="a">The minuend of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    /// <param name="b">The subtrahend of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TDifference operator -(TSelf a, TSubtrahend b);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TSelf"/> - <typeparamref name="TSelf"/> },
/// resulting in a quantity of type <typeparamref name="TSelf"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IMinuendVector3Quantity<TSelf> : IMinuendVector3Quantity<TSelf, TSelf, TSelf> where TSelf : IMinuendVector3Quantity<TSelf> { }
