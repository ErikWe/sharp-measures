namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as { <typeparamref name="TAddend1"/> + <typeparamref name="TAddend2"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TAddend1">The scalar quantity that represents the first addend of { <typeparamref name="TAddend1"/> + <typeparamref name="TAddend2"/> }.</typeparam>
/// <typeparam name="TAddend2">The scalar quantity that represents the second addend of { <typeparamref name="TAddend1"/> + <typeparamref name="TAddend2"/> }.</typeparam>
public interface ISumScalarQuantity<out TSelf, in TAddend1, in TAddend2> :
    IScalarQuantity
    where TSelf : ISumScalarQuantity<TSelf, TAddend1, TAddend2>
    where TAddend1 : IScalarQuantity
    where TAddend2 : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> + <paramref name="y"/> }.</summary>
    /// <param name="x">The first addend of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    /// <param name="y">The second addend of { <paramref name="x"/> + <paramref name="y"/> }</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TAddend1 x, TAddend2 y);
}

/// <summary>Describes a scalar quantity as { <typeparamref name="TSelf"/> + <typeparamref name="TAddend"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TAddend">The scalar quantity that represents the second addend of { <typeparamref name="TSelf"/> + <typeparamref name="TAddend"/> }.</typeparam>
public interface ISumScalarQuantity<TSelf, in TAddend> :
    ISumScalarQuantity<TSelf, TSelf, TAddend>
    where TSelf : ISumScalarQuantity<TSelf, TAddend>
    where TAddend : IScalarQuantity
{ }

/// <summary>Describes a scalar quantity as { <typeparamref name="TSelf"/> + <typeparamref name="TSelf"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
public interface ISumScalarQuantity<TSelf> :
    ISumScalarQuantity<TSelf, TSelf, TSelf>
    where TSelf : ISumScalarQuantity<TSelf>
{ }
