namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Transform
{
    private static Vector3 Target(Vector3 vector, System.Numerics.Matrix4x4 transform) => vector.Transform(transform);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidMatrix4x4))]
    public void MatchArithmetic(Vector3 vector, System.Numerics.Matrix4x4 transform)
    {
        Vector3 expected =
        (
            (vector.X * transform.M11) + (vector.Y * transform.M21) + (vector.Z * transform.M31) + transform.M41,
            (vector.X * transform.M12) + (vector.Y * transform.M22) + (vector.Z * transform.M32) + transform.M42,
            (vector.X * transform.M13) + (vector.Y * transform.M23) + (vector.Z * transform.M33) + transform.M43
        );

        var actual = Target(vector, transform);

        Assert.Equal(expected, actual);
    }
}
