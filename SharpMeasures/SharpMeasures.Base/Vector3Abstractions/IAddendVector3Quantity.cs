namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <see langword="this"/> + <typeparamref name="TAddend"/> }, resulting
/// in a quantity of type <typeparamref name="TSum"/>.</summary>
/// <typeparam name="TSum">The three-dimensional vector quantity that represents the result of { <see langword="this"/> + <typeparamref name="TAddend"/> }.</typeparam>
/// <typeparam name="TAddend">The three-dimensional vector quantity that represents the second addend of { <see langword="this"/> +
/// <typeparamref name="TAddend"/> }.</typeparam>
public interface IAddendVector3Quantity<out TSum, in TAddend> :
    IVector3Quantity
    where TSum : IVector3Quantity
    where TAddend : IVector3Quantity
{
    /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
    /// <param name="addend">The second addend of { <see langword="this"/> + <paramref name="addend"/> }.</param>
    public abstract TSum Add(TAddend addend);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TSelf"/> + <typeparamref name="TAddend"/> },
/// resulting in a quantity of type <typeparamref name="TSum"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TSum">The three-dimensional vector quantity that represents the result of { <typeparamref name="TSelf"/> +
/// <typeparamref name="TAddend"/> }.</typeparam>
/// <typeparam name="TAddend">The three-dimensional vector quantity that represents the second addend of { <typeparamref name="TSelf"/> +
/// <typeparamref name="TAddend"/> }.</typeparam>
public interface IAddendVector3Quantity<in TSelf, out TSum, in TAddend>
    : IAddendVector3Quantity<TSum, TAddend>
    where TSelf : IAddendVector3Quantity<TSelf, TSum, TAddend>
    where TSum : IVector3Quantity
    where TAddend : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> + <paramref name="b"/> }.</summary>
    /// <param name="a">The first addend of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    /// <param name="b">The second addend of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TSum operator +(TSelf a, TAddend b);

    /// <summary>Computes { <paramref name="a"/> + <paramref name="b"/> }.</summary>
    /// <param name="a">The first addend of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    /// <param name="b">The second addend of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TSum operator +(TAddend a, TSelf b);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TSelf"/> + <typeparamref name="TSelf"/> },
/// resulting in a quantity of type <typeparamref name="TSelf"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface IAddendVector3Quantity<TSelf> : IAddendVector3Quantity<TSelf, TSelf, TSelf> where TSelf : IAddendVector3Quantity<TSelf> { }
