namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <see langword="this"/> + <typeparamref name="TAddend"/> }, resulting in a quantity of type
/// <typeparamref name="TSum"/>.</summary>
/// <typeparam name="TSum">The scalar quantity that represents the result of { <see langword="this"/> + <typeparamref name="TAddend"/> }.</typeparam>
/// <typeparam name="TAddend">The scalar quantity that represents the second addend of { <see langword="this"/> + <typeparamref name="TAddend"/> }.</typeparam>
public interface IAddendScalarQuantity<out TSum, in TAddend>
    : IScalarQuantity
    where TSum : IScalarQuantity
    where TAddend : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
    /// <param name="addend">The second addend of { <see langword="this"/> + <paramref name="addend"/> }.</param>
    public abstract TSum Add(TAddend addend);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> + <typeparamref name="TAddend"/> }, resulting in a quantity of type
/// <typeparamref name="TSum"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TSum">The scalar quantity that represents the result of { <typeparamref name="TSelf"/> + <typeparamref name="TAddend"/> }.</typeparam>
/// <typeparam name="TAddend">The scalar quantity that represents the second addend of { <typeparamref name="TSelf"/> + <typeparamref name="TAddend"/> }.</typeparam>
public interface IAddendScalarQuantity<in TSelf, out TSum, in TAddend>
    : IAddendScalarQuantity<TSum, TAddend>
    where TSelf : IAddendScalarQuantity<TSelf, TSum, TAddend>
    where TSum : IScalarQuantity
    where TAddend : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> + <paramref name="y"/> }.</summary>
    /// <param name="x">The first addend of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    /// <param name="y">The second addend of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TSum operator +(TSelf x, TAddend y);

    /// <summary>Computes { <paramref name="x"/> + <paramref name="y"/> }.</summary>
    /// <param name="x">The first addend of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    /// <param name="y">The second addend of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TSum operator +(TAddend x, TSelf y);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> + <typeparamref name="TSelf"/> }, resulting in a quantity
/// of type <typeparamref name="TSelf"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IAddendScalarQuantity<TSelf> : IAddendScalarQuantity<TSelf, TSelf, TSelf> where TSelf : IAddendScalarQuantity<TSelf> { }
