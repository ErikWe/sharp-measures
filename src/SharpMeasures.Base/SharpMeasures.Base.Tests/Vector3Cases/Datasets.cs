namespace SharpMeasures.Tests.Vector3Cases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Vector3 GetValidVector3() => Vector3Values.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;

    public static class ScalarTupleValues
    {
        public static IEnumerable<(Scalar, Scalar, Scalar)> Valid => new (Scalar, Scalar, Scalar)[]
        {
            (0, 0, 0),
            (1, 0.217, 9817.15),
            (double.Epsilon, double.MinValue, double.MaxValue),
            (double.NaN, -4.56, 0),
            (3.56, double.NaN, -3.56),
            (-0.007, 0, double.NaN),
            (double.NaN, double.NaN, double.NaN),
            (double.PositiveInfinity, 0, 4.57),
            (-0.0089, double.NegativeInfinity, 0.0089),
            (56717.3, -1, double.PositiveInfinity),
            (double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity)
        };
    }

    public static class Vector3Values
    {
        public static IEnumerable<Vector3> Valid
        {
            get
            {
                foreach (var (x, y, z) in ScalarTupleValues.Valid)
                {
                    yield return new(x, y, z);
                }
            }
        }
    }

    [SuppressMessage("Minor Code Smell", "S101: Types should be named in PascalCase")]
    public static class Matrix4x4Values
    {
        public static IEnumerable<System.Numerics.Matrix4x4> Valid => new System.Numerics.Matrix4x4[]
        {
            new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
            System.Numerics.Matrix4x4.Identity,
            System.Numerics.Matrix4x4.CreateRotationX(2),
            System.Numerics.Matrix4x4.CreateTranslation(5, -9, 3.14f)
        };
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalarTuple : ATestDataset<(Scalar, Scalar, Scalar)>
    {
        protected override IEnumerable<(Scalar, Scalar, Scalar)> GetSamples() => ScalarTupleValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector3 : ATestDataset<Vector3>
    {
        protected override IEnumerable<Vector3> GetSamples() => Vector3Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector3_ValidVector3 : ATestDataset<(Vector3, Vector3)>
    {
        protected override IEnumerable<(Vector3, Vector3)> GetSamples() => Permutate(Vector3Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector3_ValidScalar : ATestDataset<(Vector3, Scalar)>
    {
        protected override IEnumerable<(Vector3, Scalar)> GetSamples() => Permutate(Vector3Values.Valid, ScalarCases.Datasets.ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector3_ValidUnhandled : ATestDataset<(Vector3, Unhandled)>
    {
        protected override IEnumerable<(Vector3, Unhandled)> GetSamples() => Permutate(Vector3Values.Valid, UnhandledCases.Datasets.UnhandledValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    [SuppressMessage("Minor Code Smell", "S101: Types should be named in PascalCase")]
    public sealed class ValidVector3_ValidMatrix4x4 : ATestDataset<(Vector3, System.Numerics.Matrix4x4)>
    {
        protected override IEnumerable<(Vector3, System.Numerics.Matrix4x4)> GetSamples() => Permutate(Vector3Values.Valid, Matrix4x4Values.Valid);
    }
}
