namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TMinuend"/> - <see langword="this"/> },
/// resulting in a quantity of type <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TDifference">The three-dimensional vector quantity that represents the result of { <typeparamref name="TMinuend"/> -
/// <see langword="this"/> }.</typeparam>
/// <typeparam name="TMinuend">The three-dimensional vector quantity that represents the minuend of { <typeparamref name="TMinuend"/> -
/// <see langword="this"/> }.</typeparam>
public interface ISubtrahendVector3Quantity<out TDifference, in TMinuend>
    : IVector3Quantity
    where TDifference : IVector3Quantity
    where TMinuend : IVector3Quantity
{
    /// <summary>Computes { <paramref name="minuend"/> - <see langword="this"/> }.</summary>
    /// <param name="minuend">The minuend of { <paramref name="minuend"/> - <see langword="this"/> }.</param>
    public abstract TDifference SubtractFrom(TMinuend minuend);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TMinuend"/> - <typeparamref name="TSelf"/> },
/// resulting in a quantity of type <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TDifference">The three-dimensional vector quantity that represents the result of { <typeparamref name="TMinuend"/> -
/// <typeparamref name="TSelf"/> }.</typeparam>
/// <typeparam name="TMinuend">The three-dimensional vector quantity that represents the minuend of { <typeparamref name="TMinuend"/> -
/// <typeparamref name="TSelf"/> }.</typeparam>
public interface ISubtrahendVector3Quantity<in TSelf, out TDifference, in TMinuend>
    : ISubtrahendVector3Quantity<TDifference, TMinuend>
    where TSelf : ISubtrahendVector3Quantity<TSelf, TDifference, TMinuend>
    where TDifference : IVector3Quantity
    where TMinuend : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> - <paramref name="b"/> }.</summary>
    /// <param name="a">The minuend of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    /// <param name="b">The subtrahend of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "Available as 'SubtractFrom'")]
    public static abstract TDifference operator -(TMinuend a, TSelf b);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TSelf"/> - <typeparamref name="TSelf"/> },
/// resulting in a quantity of type <typeparamref name="TSelf"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface ISubtrahendVector3Quantity<TSelf> : ISubtrahendVector3Quantity<TSelf, TSelf, TSelf> where TSelf : ISubtrahendVector3Quantity<TSelf> { }
