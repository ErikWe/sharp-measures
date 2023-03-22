namespace SharpMeasures.Tests.UnhandledCases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Unhandled GetNaNUnhandled() => Unhandled.NaN;
    public static Unhandled GetValidUnhandled() => UnhandledValues.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;

    public static ReferenceVector2Quantity GetNullVector2Quantity() => null!;
    public static ReferenceVector3Quantity GetNullVector3Quantity() => null!;
    public static ReferenceVector4Quantity GetNullVector4Quantity() => null!;

    public static class UnhandledScalarValues
    {
        public static IEnumerable<Scalar> Valid => ScalarCases.Datasets.ScalarValues.Valid;
        public static IEnumerable<Scalar> ValidExceptNaN => ScalarCases.Datasets.ScalarValues.ValidExceptNaN;
    }

    public static class UnhandledValues
    {
        public static IEnumerable<Unhandled> Valid
        {
            get
            {
                foreach (var magnitude in UnhandledScalarValues.Valid)
                {
                    yield return new(magnitude);
                }
            }
        }

        public static IEnumerable<Unhandled> ValidExceptNaN
        {
            get
            {
                foreach (var magnitude in UnhandledScalarValues.ValidExceptNaN)
                {
                    yield return new(magnitude);
                }
            }
        }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandledScalar : ATestDataset<Scalar>
    {
        protected override IEnumerable<Scalar> GetSamples() => UnhandledScalarValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled : ATestDataset<Unhandled>
    {
        protected override IEnumerable<Unhandled> GetSamples() => UnhandledValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidExceptNaNUnhandled : ATestDataset<Unhandled>
    {
        protected override IEnumerable<Unhandled> GetSamples() => UnhandledValues.ValidExceptNaN;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidUnhandled : ATestDataset<(Unhandled, Unhandled)>
    {
        protected override IEnumerable<(Unhandled, Unhandled)> GetSamples() => Permutate(UnhandledValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidUnhandled2 : ATestDataset<(Unhandled, Unhandled2)>
    {
        protected override IEnumerable<(Unhandled, Unhandled2)> GetSamples() => Permutate(UnhandledValues.Valid, Unhandled2Cases.Datasets.Unhandled2Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidUnhandled3 : ATestDataset<(Unhandled, Unhandled3)>
    {
        protected override IEnumerable<(Unhandled, Unhandled3)> GetSamples() => Permutate(UnhandledValues.Valid, Unhandled3Cases.Datasets.Unhandled3Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidUnhandled4 : ATestDataset<(Unhandled, Unhandled4)>
    {
        protected override IEnumerable<(Unhandled, Unhandled4)> GetSamples() => Permutate(UnhandledValues.Valid, Unhandled4Cases.Datasets.Unhandled4Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidScalar : ATestDataset<(Unhandled, Scalar)>
    {
        protected override IEnumerable<(Unhandled, Scalar)> GetSamples() => Permutate(UnhandledValues.Valid, ScalarCases.Datasets.ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidVector2 : ATestDataset<(Unhandled, Vector2)>
    {
        protected override IEnumerable<(Unhandled, Vector2)> GetSamples() => Permutate(UnhandledValues.Valid, Vector2Cases.Datasets.Vector2Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidVector3 : ATestDataset<(Unhandled, Vector3)>
    {
        protected override IEnumerable<(Unhandled, Vector3)> GetSamples() => Permutate(UnhandledValues.Valid, Vector3Cases.Datasets.Vector3Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled_ValidVector4 : ATestDataset<(Unhandled, Vector4)>
    {
        protected override IEnumerable<(Unhandled, Vector4)> GetSamples() => Permutate(UnhandledValues.Valid, Vector4Cases.Datasets.Vector4Values.Valid);
    }
}
