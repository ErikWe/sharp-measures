namespace SharpMeasures.Tests.Unhandled4Cases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Unhandled4 GetValidUnhandled4() => Unhandled4Values.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;
    public static ReferenceVector4Quantity GetNullVector4Quantity() => null!;

    public static class ScalarTupleValues
    {
        public static IEnumerable<(Scalar, Scalar, Scalar, Scalar)> Valid => Vector4Cases.Datasets.ScalarTupleValues.Valid;
    }

    public static class UnhandledTupleValues
    {
        public static IEnumerable<(Unhandled, Unhandled, Unhandled, Unhandled)> Valid
        {
            get
            {
                foreach (var (x, y, z, w) in ScalarTupleValues.Valid)
                {
                    yield return (new(x), new(y), new(z), new(w));
                }
            }
        }
    }

    public static class Unhandled4Values
    {
        public static IEnumerable<Unhandled4> Valid
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
    public sealed class ValidUnhandledTuple : ATestDataset<(Unhandled, Unhandled, Unhandled, Unhandled)>
    {
        protected override IEnumerable<(Unhandled, Unhandled, Unhandled, Unhandled)> GetSamples() => UnhandledTupleValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled4 : ATestDataset<Unhandled4>
    {
        protected override IEnumerable<Unhandled4> GetSamples() => Unhandled4Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled4_ValidUnhandled4 : ATestDataset<(Unhandled4, Unhandled4)>
    {
        protected override IEnumerable<(Unhandled4, Unhandled4)> GetSamples() => Permutate(Unhandled4Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled4_ValidScalar : ATestDataset<(Unhandled4, Scalar)>
    {
        protected override IEnumerable<(Unhandled4, Scalar)> GetSamples() => Permutate(Unhandled4Values.Valid, ScalarCases.Datasets.ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled4_ValidUnhandled : ATestDataset<(Unhandled4, Unhandled)>
    {
        protected override IEnumerable<(Unhandled4, Unhandled)> GetSamples() => Permutate(Unhandled4Values.Valid, UnhandledCases.Datasets.UnhandledValues.Valid);
    }
}
