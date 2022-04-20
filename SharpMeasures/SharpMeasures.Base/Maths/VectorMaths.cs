namespace SharpMeasures.Maths;

using System.Numerics;

/// <summary>Provides mathematical operations related to vector quantities.</summary>
public static class VectorMaths
{
    /// <summary>Computes the dot-product { <paramref name="lhs"/> ∙ <paramref name="rhs"/> }, resulting in a <see cref="Scalar"/>.</summary>
    /// <typeparam name="TLHSVector3">The type of the first factor, <paramref name="lhs"/>.</typeparam>
    /// <typeparam name="TRHSVector3">The type of the second factor, <paramref name="rhs"/>.</typeparam>
    /// <param name="lhs">The first factor, which is dot-multiplied by <paramref name="rhs"/>.</param>
    /// <param name="rhs">The second factor, which is dot-multiplied by <paramref name="lhs"/>.</param>
    public static Scalar Dot<TLHSVector3, TRHSVector3>(TLHSVector3 lhs, TRHSVector3 rhs)
        where TLHSVector3 : IVector3Quantity
        where TRHSVector3 : IVector3Quantity
    {
        return new(lhs.XMagnitude * rhs.XMagnitude + lhs.YMagnitude * rhs.YMagnitude + lhs.ZMagnitude * rhs.ZMagnitude);
    }

    /// <summary>Computes the cross-product { <paramref name="lhs"/> x <paramref name="rhs"/> }, resulting in a <see cref="SharpMeasures.Vector3"/>.</summary>
    /// <typeparam name="TLHSVector3">The type of the first factor, <paramref name="lhs"/>.</typeparam>
    /// <typeparam name="TRHSVector3">The type of the second factor, <paramref name="rhs"/>.</typeparam>
    /// <param name="lhs">The first factor, which is cross-multiplied by <paramref name="rhs"/>.</param>
    /// <param name="rhs">The second factor, which is cross-multiplied by <paramref name="lhs"/>.</param>
    public static SharpMeasures.Vector3 Cross<TLHSVector3, TRHSVector3>(TLHSVector3 lhs, TRHSVector3 rhs)
        where TLHSVector3 : IVector3Quantity
        where TRHSVector3 : IVector3Quantity
    {
        return new
        (
            lhs.YMagnitude * rhs.ZMagnitude - lhs.ZMagnitude * rhs.YMagnitude,
            lhs.ZMagnitude * rhs.XMagnitude - lhs.XMagnitude * rhs.ZMagnitude,
            lhs.XMagnitude * rhs.YMagnitude - lhs.YMagnitude * rhs.XMagnitude
        );
    }

    /// <summary>Transforms <paramref name="vector"/> according to <paramref name="transform"/>.</summary>
    /// <typeparam name="TVector3">The type of the vector quantity.</typeparam>
    /// <param name="vector">The vector, which is transformed according to <paramref name="transform"/>.</param>
    /// <param name="transform">The transform, which is used to transform <paramref name="vector"/>.</param>
    public static SharpMeasures.Vector3 Transform<TVector3>(TVector3 vector, Matrix4x4 transform)
        where TVector3 : IVector3Quantity
    {
        return new
        (
            vector.XMagnitude * transform.M11 + vector.YMagnitude * transform.M21 + vector.ZMagnitude * transform.M31 + transform.M41,
            vector.XMagnitude * transform.M12 + vector.YMagnitude * transform.M22 + vector.ZMagnitude * transform.M32 + transform.M42,
            vector.XMagnitude * transform.M13 + vector.YMagnitude * transform.M23 + vector.ZMagnitude * transform.M33 + transform.M43
        );
    }
}
