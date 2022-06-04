namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as the cube of a quantity <typeparamref name="TCubeRoot"/>.</summary>
/// <typeparam name="TCubeRoot">The scalar quantity that is the cube root of <see langword="this"/>.</typeparam>
public interface ICubeScalarQuantity<out TCubeRoot> :
    IScalarQuantity
    where TCubeRoot : IScalarQuantity
{
    /// <summary>Computes { ³√ <see langword="this"/> }.</summary>
    public abstract TCubeRoot CubeRoot();
}

/// <summary><inheritdoc path="/summary"/></summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TCubeRoot"><inheritdoc path="/typeparam[@name='TCubeRoot']"/></typeparam>
public interface ICubeScalarQuantity<out TSelf, TCubeRoot> :
    ICubeScalarQuantity<TCubeRoot>
    where TSelf : ICubeScalarQuantity<TSelf, TCubeRoot>
    where TCubeRoot : IScalarQuantity
{
    /// <summary>Computes { <paramref name="cubeRoot"/> ³ }.</summary>
    /// <param name="cubeRoot">The base of { <paramref name="cubeRoot"/> ³ }.</param>
    [SuppressMessage("Design", "CA1000", Justification = "Implementing type is not necessarily generic")]
    public static abstract TSelf From(TCubeRoot cubeRoot);
}
