namespace ErikWe.SharpMeasures.Maths;

using ErikWe.SharpMeasures.Quantities;

using System.Numerics;

internal static class Vectors
{
    public static Scalar Dot<TLHSVector3, TRHSVector3>(TLHSVector3 lhs, TRHSVector3 rhs)
        where TLHSVector3 : IVector3Quantity
        where TRHSVector3 : IVector3Quantity
    {
        return new(lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z);
    }

    public static Quantities.Vector3 Cross<TLHSVector3, TRHSVector3>(TLHSVector3 lhs, TRHSVector3 rhs)
        where TLHSVector3 : IVector3Quantity
        where TRHSVector3 : IVector3Quantity
    {
        return new
        (
            lhs.Y * rhs.Z - lhs.Z * rhs.Y,
            lhs.Z * rhs.X - lhs.X * rhs.Z,
            lhs.X * rhs.Y - lhs.Y * rhs.X
        );
    }

    public static Quantities.Vector3 Transform<TVector3>(TVector3 vector, Matrix4x4 transform)
        where TVector3 : IVector3Quantity
    {
        return new
        (
            vector.X * transform.M11 + vector.Y * transform.M21 + vector.Z * transform.M31 + transform.M41,
            vector.X * transform.M12 + vector.Y * transform.M22 + vector.Z * transform.M32 + transform.M42,
            vector.X * transform.M13 + vector.Y * transform.M23 + vector.Z * transform.M33 + transform.M43
        );
    }
}
