namespace ErikWe.SharpMeasures.Maths;

using ErikWe.SharpMeasures.Quantities;

using System.Numerics;

internal static class Vectors
{
    public static Scalar Dot<TLHSVector3, TRHSVector3>(TLHSVector3 lhs, TRHSVector3 rhs)
        where TLHSVector3 : IVector3Quantity
        where TRHSVector3 : IVector3Quantity
    {
        return new(lhs.MagnitudeX * rhs.MagnitudeX + lhs.MagnitudeY * rhs.MagnitudeY + lhs.MagnitudeZ * rhs.MagnitudeZ);
    }

    public static Quantities.Vector3 Cross<TLHSVector3, TRHSVector3>(TLHSVector3 lhs, TRHSVector3 rhs)
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

    public static Quantities.Vector3 Transform<TVector3>(TVector3 vector, Matrix4x4 transform)
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
