namespace SharpMeasures.Tests.ScalarCases;

using SharpMeasures.Tests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Scalar GetNaNScalar() => Scalar.NaN;
    public static Scalar GetValidScalar() => ScalarValues.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;

    public static ReferenceVector2Quantity GetNullVector2Quantity() => null!;
    public static ReferenceVector3Quantity GetNullVector3Quantity() => null!;
    public static ReferenceVector4Quantity GetNullVector4Quantity() => null!;

    public static class ScalarDoubleValues
    {
        public static IEnumerable<double> Valid => new[]
        {
            0,
            1,
            -1,
            0.217,
            -0.314,
            double.Epsilon,
            double.MinValue,
            double.MaxValue,
            double.NaN,
            double.PositiveInfinity,
            double.NegativeInfinity
        };

        public static IEnumerable<double> ValidExceptNaN
        {

            get
            {
                foreach (var value in Valid)
                {
                    if (double.IsNaN(value) is false)
                    {
                        yield return value;
                    }
                }
            }
        }
    }

    public static class ScalarValues
    {
        public static IEnumerable<Scalar> Valid
        {
            get
            {
                foreach (var value in ScalarDoubleValues.Valid)
                {
                    yield return new(value);
                }
            }
        }

        public static IEnumerable<Scalar> ValidExceptNaN
        {
            get
            {
                foreach (var value in ScalarDoubleValues.ValidExceptNaN)
                {
                    yield return new(value);
                }
            }
        }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalarDouble : ATestDataset<double>
    {
        protected override IEnumerable<double> GetSamples() => ScalarDoubleValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar : ATestDataset<Scalar>
    {
        protected override IEnumerable<Scalar> GetSamples() => ScalarValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidExceptNaNScalar : ATestDataset<Scalar>
    {
        protected override IEnumerable<Scalar> GetSamples() => ScalarValues.ValidExceptNaN;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidScalar : ATestDataset<(Scalar, Scalar)>
    {
        protected override IEnumerable<(Scalar, Scalar)> GetSamples() => Permutate(ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidVector2 : ATestDataset<(Scalar, Vector2)>
    {
        protected override IEnumerable<(Scalar, Vector2)> GetSamples() => Permutate(ScalarValues.Valid, Vector2Cases.Datasets.Vector2Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidVector3 : ATestDataset<(Scalar, Vector3)>
    {
        protected override IEnumerable<(Scalar, Vector3)> GetSamples() => Permutate(ScalarValues.Valid, Vector3Cases.Datasets.Vector3Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidVector4 : ATestDataset<(Scalar, Vector4)>
    {
        protected override IEnumerable<(Scalar, Vector4)> GetSamples() => Permutate(ScalarValues.Valid, Vector4Cases.Datasets.Vector4Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidUnhandled : ATestDataset<(Scalar, Unhandled)>
    {
        protected override IEnumerable<(Scalar, Unhandled)> GetSamples() => Permutate(ScalarValues.Valid, UnhandledCases.Datasets.UnhandledValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidUnhandled2 : ATestDataset<(Scalar, Unhandled2)>
    {
        protected override IEnumerable<(Scalar, Unhandled2)> GetSamples() => Permutate(ScalarValues.Valid, Unhandled2Cases.Datasets.Unhandled2Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidUnhandled3 : ATestDataset<(Scalar, Unhandled3)>
    {
        protected override IEnumerable<(Scalar, Unhandled3)> GetSamples() => Permutate(ScalarValues.Valid, Unhandled3Cases.Datasets.Unhandled3Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidScalar_ValidUnhandled4 : ATestDataset<(Scalar, Unhandled4)>
    {
        protected override IEnumerable<(Scalar, Unhandled4)> GetSamples() => Permutate(ScalarValues.Valid, Unhandled4Cases.Datasets.Unhandled4Values.Valid);
    }
}
