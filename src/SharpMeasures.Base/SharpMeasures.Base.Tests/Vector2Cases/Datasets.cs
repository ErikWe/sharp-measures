namespace SharpMeasures.Tests.Vector2Cases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Vector2 GetValidVector2() => Vector2Values.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;

    public static class ScalarTupleValues
    {
        public static IEnumerable<(Scalar, Scalar)> Valid => new (Scalar, Scalar)[]
        {
            (0, 0),
            (1, 0.217),
            (double.Epsilon, -1),
            (double.MinValue, double.MaxValue),
            (double.NaN, -4.56),
            (0, double.NaN),
            (double.NaN, double.NaN),
            (double.PositiveInfinity, 0),
            (-0.0089, double.NegativeInfinity),
            (double.PositiveInfinity, double.NegativeInfinity)
        };
    }

    public static class Vector2Values
    {
        public static IEnumerable<Vector2> Valid
        {
            get
            {
                foreach (var (x, y) in ScalarTupleValues.Valid)
                {
                    yield return new(x, y);
                }
            }
        }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalarTuple : ATestDataset<(Scalar, Scalar)>
    {
        protected override IEnumerable<(Scalar, Scalar)> GetSamples() => ScalarTupleValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector2 : ATestDataset<Vector2>
    {
        protected override IEnumerable<Vector2> GetSamples() => Vector2Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector2_ValidVector2 : ATestDataset<(Vector2, Vector2)>
    {
        protected override IEnumerable<(Vector2, Vector2)> GetSamples() => Permutate(Vector2Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector2_ValidScalar : ATestDataset<(Vector2, Scalar)>
    {
        protected override IEnumerable<(Vector2, Scalar)> GetSamples() => Permutate(Vector2Values.Valid, ScalarCases.Datasets.ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidVector2_ValidUnhandled : ATestDataset<(Vector2, Unhandled)>
    {
        protected override IEnumerable<(Vector2, Unhandled)> GetSamples() => Permutate(Vector2Values.Valid, UnhandledCases.Datasets.UnhandledValues.Valid);
    }
}
