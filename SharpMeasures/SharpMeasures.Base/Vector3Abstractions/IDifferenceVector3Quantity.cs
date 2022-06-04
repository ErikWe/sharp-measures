namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TMinuend"/> - <typeparamref name="TSubtrahend"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TMinuend">The three-dimensional vector quantity that represents the minuend of { <typeparamref name="TMinuend"/> -
/// <typeparamref name="TSubtrahend"/> }.</typeparam>
/// <typeparam name="TSubtrahend">The three-dimensional vector quantity that represents the subtrahend of { <typeparamref name="TMinuend"/> -
/// <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IDifferenceVector3Quantity<out TSelf, in TMinuend, in TSubtrahend> :
    IVector3Quantity
    where TSelf : IDifferenceVector3Quantity<TSelf, TMinuend, TSubtrahend>
    where TMinuend : IVector3Quantity
    where TSubtrahend : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> - <paramref name="b"/> }.</summary>
    /// <param name="a">The minuend of { <paramref name="a"/> - <paramref name="b"/> }.</param>
    /// <param name="b">The subtrahend of { <paramref name="a"/> - <paramref name="b"/> }</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TMinuend a, TSubtrahend b);
}

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TSelf"/> - <typeparamref name="TSubtrahend"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TSubtrahend">The three-dimensional vector quantity that represents the subtrahend of { <typeparamref name="TSelf"/> -
/// <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IDifferenceVector3Quantity<TSelf, in TSubtrahend> :
    IDifferenceVector3Quantity<TSelf, TSelf, TSubtrahend>
    where TSelf : IDifferenceVector3Quantity<TSelf, TSubtrahend>
    where TSubtrahend : IVector3Quantity
{ } 

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TSelf"/> - <typeparamref name="TSelf"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IDifferenceVector3Quantity<TSelf> : IDifferenceVector3Quantity<TSelf, TSelf> where TSelf : IDifferenceVector3Quantity<TSelf> { } 
