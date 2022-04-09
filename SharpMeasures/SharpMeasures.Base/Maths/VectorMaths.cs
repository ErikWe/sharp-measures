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
        return new(lhs.MagnitudeX * rhs.MagnitudeX + lhs.MagnitudeY * rhs.MagnitudeY + lhs.MagnitudeZ * rhs.MagnitudeZ);
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
            lhs.MagnitudeY * rhs.MagnitudeZ - lhs.MagnitudeZ * rhs.MagnitudeY,
            lhs.MagnitudeZ * rhs.MagnitudeX - lhs.MagnitudeX * rhs.MagnitudeZ,
            lhs.MagnitudeX * rhs.MagnitudeY - lhs.MagnitudeY * rhs.MagnitudeX
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
            vector.MagnitudeX * transform.M11 + vector.MagnitudeY * transform.M21 + vector.MagnitudeZ * transform.M31 + transform.M41,
            vector.MagnitudeX * transform.M12 + vector.MagnitudeY * transform.M22 + vector.MagnitudeZ * transform.M32 + transform.M42,
            vector.MagnitudeX * transform.M13 + vector.MagnitudeY * transform.M23 + vector.MagnitudeZ * transform.M33 + transform.M43
        );
    }
}
