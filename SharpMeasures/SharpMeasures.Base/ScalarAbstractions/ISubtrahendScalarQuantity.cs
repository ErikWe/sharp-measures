namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TMinuend"/> - <see langword="this"/> }, resulting in a quantity of type
/// <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TDifference">The scalar quantity that represents the result of { <typeparamref name="TMinuend"/> - <see langword="this"/> }.</typeparam>
/// <typeparam name="TMinuend">The scalar quantity that represents the minuend of { <typeparamref name="TMinuend"/> - <see langword="this"/> }.</typeparam>
public interface ISubtrahendScalarQuantity<out TDifference, in TMinuend>
    : IScalarQuantity
    where TDifference : IScalarQuantity
    where TMinuend : IScalarQuantity
{
    /// <summary>Computes { <paramref name="minuend"/> - <see langword="this"/> }.</summary>
    /// <param name="minuend">The minuend of { <paramref name="minuend"/> - <see langword="this"/> }.</param>
    public abstract TDifference SubtractFrom(TMinuend minuend);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TMinuend"/> - <typeparamref name="TSelf"/> }, resulting in a quantity of type
/// <typeparamref name="TDifference"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TDifference">The scalar quantity that represents the result of { <typeparamref name="TMinuend"/> - <typeparamref name="TSelf"/> }.</typeparam>
/// <typeparam name="TMinuend">The scalar quantity that represents the minuend of { <typeparamref name="TMinuend"/> - <typeparamref name="TSelf"/> }.</typeparam>
public interface ISubtrahendScalarQuantity<in TSelf, out TDifference, in TMinuend>
    : ISubtrahendScalarQuantity<TDifference, TMinuend>
    where TSelf : ISubtrahendScalarQuantity<TSelf, TDifference, TMinuend>
    where TDifference : IScalarQuantity
    where TMinuend : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> - <paramref name="y"/> }.</summary>
    /// <param name="x">The minuend of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    /// <param name="y">The subtrahend of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "Available as 'SubtractFrom'")]
    public static abstract TDifference operator -(TMinuend x, TSelf y);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> - <typeparamref name="TSelf"/> }, resulting in a quantity
/// of type <typeparamref name="TSelf"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface ISubtrahendScalarQuantity<TSelf> : ISubtrahendScalarQuantity<TSelf, TSelf, TSelf> where TSelf : ISubtrahendScalarQuantity<TSelf> { }
