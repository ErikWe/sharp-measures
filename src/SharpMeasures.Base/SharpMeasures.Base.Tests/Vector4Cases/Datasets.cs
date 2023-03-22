namespace SharpMeasures.Tests.Vector4Cases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Vector4 GetValidVector4() => Vector4Values.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;

    public static class ScalarTupleValues
    {
        public static IEnumerable<(Scalar, Scalar, Scalar, Scalar)> Valid => new (Scalar, Scalar, Scalar, Scalar)[]
        {
            (0, 0, 0, 0),
            (1, 0.217, 8765.13, -9.391),
            (double.Epsilon, double.MinValue, double.MaxValue, -1),
            (double.NaN, 456.88, -9763.007, 0),
            (-4.56, double.NaN, 0, 1009.3),
            (-1, 0, double.NaN, -93837.39),
            (0.0007, 317.0009, 5, double.NaN),
            (double.NaN, double.NaN, double.NaN, double.NaN),
            (double.PositiveInfinity, 0, 4.57, 1),
            (-0.0089, double.NegativeInfinity, 0.0089, 57),
            (0, 3, double.PositiveInfinity, 45.998),
            (38, 7.7756, 0.0000005, double.NegativeInfinity),
            (double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity, double.PositiveInfinity)
        };
    }

    public static class Vector4Values
    {
        public static IEnumerable<Vector4> Valid
        {
            get
            {
                foreach (var (x, y, z, w) in ScalarTupleValues.Valid)
                {
                    yield return new(x, y, z, w);
                }
            }
        }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalarTuple : ATestDataset<(Scalar, Scalar, Scalar, Scalar)>
    {
        protected override IEnumerable<(Scalar, Scalar, Scalar, Scalar)> GetSamples() => ScalarTupleValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector4 : ATestDataset<Vector4>
    {
        protected override IEnumerable<Vector4> GetSamples() => Vector4Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector4_ValidVector4 : ATestDataset<(Vector4, Vector4)>
    {
        protected override IEnumerable<(Vector4, Vector4)> GetSamples() => Permutate(Vector4Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector4_ValidScalar : ATestDataset<(Vector4, Scalar)>
    {
        protected override IEnumerable<(Vector4, Scalar)> GetSamples() => Permutate(Vector4Values.Valid, ScalarCases.Datasets.ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector4_ValidUnhandled : ATestDataset<(Vector4, Unhandled)>
    {
        protected override IEnumerable<(Vector4, Unhandled)> GetSamples() => Permutate(Vector4Values.Valid, UnhandledCases.Datasets.UnhandledValues.Valid);
    }
}
