namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as the cube root of a quantity <typeparamref name="TCube"/>.</summary>
/// <typeparam name="TCube">The scalar quantity that is the cube of <see langword="this"/>.</typeparam>
public interface ICubeRootScalarQuantity<out TCube> :
    IScalarQuantity
    where TCube : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> ³ }.</summary>
    public abstract TCube Cube();
}

/// <summary><inheritdoc path="/summary"/></summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TCube"><inheritdoc path="/typeparam[@name='TCube']"/></typeparam>
public interface ICubeRootScalarQuantity<out TSelf, TCube> :
    ICubeRootScalarQuantity<TCube>
    where TSelf : ICubeRootScalarQuantity<TSelf, TCube>
    where TCube : IScalarQuantity
{
    /// <summary>Computes { ³√ <paramref name="cube"/> }.</summary>
    /// <param name="cube">The radicand of { ³√ <paramref name="cube"/> }.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TCube cube);
}
