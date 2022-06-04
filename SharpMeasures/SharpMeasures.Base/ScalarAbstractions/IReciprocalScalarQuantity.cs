namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as the reciprocal of a quantity <typeparamref name="TReciprocal"/>.</summary>
/// <typeparam name="TReciprocal">The scalar quantity that is the reciprocal of <see langword="this"/>.</typeparam>
public interface IReciprocalScalarQuantity<out TReciprocal> :
    IScalarQuantity
    where TReciprocal : IScalarQuantity
{
    /// <summary>Computes { 1 / <see langword="this"/> }.</summary>
    public abstract TReciprocal Reciprocal();
}

/// <summary><inheritdoc path="/summary"/></summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TReciprocal"><inheritdoc path="/typeparam[@name='TReciprocal']"/></typeparam>
public interface IReciprocalScalarQuantity<out TSelf, TReciprocal> :
    IReciprocalScalarQuantity<TReciprocal>
    where TSelf : IReciprocalScalarQuantity<TSelf, TReciprocal>
    where TReciprocal : IScalarQuantity
{
    /// <summary>Computes { 1 / <paramref name="reciprocal"/> }.</summary>
    /// <param name="reciprocal">The divisor of { 1 / <paramref name="reciprocal"/> }.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TReciprocal reciprocal);
}
