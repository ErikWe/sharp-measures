namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as { <typeparamref name="TMinuend"/> - <typeparamref name="TSubtrahend"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TMinuend">The scalar quantity that represents the minuend of { <typeparamref name="TMinuend"/> - <typeparamref name="TSubtrahend"/> }.</typeparam>
/// <typeparam name="TSubtrahend">The scalar quantity that represents the subtrahend of { <typeparamref name="TMinuend"/> - <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IDifferenceScalarQuantity<out TSelf, in TMinuend, in TSubtrahend> :
    IScalarQuantity
    where TSelf : IDifferenceScalarQuantity<TSelf, TMinuend, TSubtrahend>
    where TMinuend : IScalarQuantity
    where TSubtrahend : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> - <paramref name="y"/> }.</summary>
    /// <param name="x">The minuend of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    /// <param name="y">The subtrahend of { <paramref name="x"/> - <paramref name="y"/> }</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TMinuend x, TSubtrahend y);
}

/// <summary>Describes a scalar quantity as { <typeparamref name="TSelf"/> - <typeparamref name="TSubtrahend"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TSubtrahend">The scalar quantity that represents the subtrahend of { <typeparamref name="TSelf"/> - <typeparamref name="TSubtrahend"/> }.</typeparam>
public interface IDifferenceScalarQuantity<TSelf, in TSubtrahend> :
    IDifferenceScalarQuantity<TSelf, TSelf, TSubtrahend>
    where TSelf : IDifferenceScalarQuantity<TSelf, TSubtrahend>
    where TSubtrahend : IScalarQuantity
{ } 

/// <summary>Describes a scalar quantity as { <typeparamref name="TSelf"/> - <typeparamref name="TSelf"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IDifferenceScalarQuantity<TSelf> : IDifferenceScalarQuantity<TSelf, TSelf> where TSelf : IDifferenceScalarQuantity<TSelf> { } 
