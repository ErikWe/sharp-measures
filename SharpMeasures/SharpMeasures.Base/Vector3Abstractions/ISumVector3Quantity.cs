namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TAddend1"/> + <typeparamref name="TAddend2"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TAddend1">The three-dimensional vector quantity that represents the first addend of { <typeparamref name="TAddend1"/> +
/// <typeparamref name="TAddend2"/> }.</typeparam>
/// <typeparam name="TAddend2">The three-dimensional vector quantity that represents the second addend of { <typeparamref name="TAddend1"/> +
/// <typeparamref name="TAddend2"/> }.</typeparam>
public interface ISumVector3Quantity<out TSelf, in TAddend1, in TAddend2> :
    IVector3Quantity
    where TSelf : ISumVector3Quantity<TSelf, TAddend1, TAddend2>
    where TAddend1 : IVector3Quantity
    where TAddend2 : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> + <paramref name="b"/> }.</summary>
    /// <param name="a">The first addend of { <paramref name="a"/> + <paramref name="b"/> }.</param>
    /// <param name="b">The second addend of { <paramref name="a"/> + <paramref name="b"/> }</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TAddend1 a, TAddend2 b);
}

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TSelf"/> + <typeparamref name="TAddend"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TAddend">The three-dimensional vector quantity that represents the second addend of { <typeparamref name="TSelf"/> +
/// <typeparamref name="TAddend"/> }.</typeparam>
public interface ISumVector3Quantity<TSelf, in TAddend> :
    ISumVector3Quantity<TSelf, TSelf, TAddend>
    where TSelf : ISumVector3Quantity<TSelf, TAddend>
    where TAddend : IVector3Quantity
{ }

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TSelf"/> + <typeparamref name="TSelf"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface ISumVector3Quantity<TSelf> :
    ISumVector3Quantity<TSelf, TSelf, TSelf>
    where TSelf : ISumVector3Quantity<TSelf>
{ }
