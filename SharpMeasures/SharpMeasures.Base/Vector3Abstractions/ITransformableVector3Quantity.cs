namespace SharpMeasures.Vector3Abstractions;

using System.Numerics;

/// <summary>Describes a three-dimensional vector quantity that may be transformed according to a transformation matrix.</summary>
/// <typeparam name="TTransformedVector3Quantity">The three-dimensional vector quantity that is the result of transformation of the original quantity,
/// generally <see langword="this"/>.</typeparam>
public interface ITransformableVector3Quantity<out TTransformedVector3Quantity> :
    IVector3Quantity
    where TTransformedVector3Quantity : IVector3Quantity
{
    /// <summary>Transforms the quantity according to the transformation matrix <paramref name="transform"/>.</summary>
    /// <param name="transform">The quantity is transformed based on this transformation matrix.</param>
    public abstract TTransformedVector3Quantity Transform(Matrix4x4 transform);
}
