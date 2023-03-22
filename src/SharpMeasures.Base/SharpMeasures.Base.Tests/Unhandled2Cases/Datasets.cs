namespace SharpMeasures.Tests.Unhandled2Cases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Unhandled2 GetValidUnhandled2() => Unhandled2Values.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;
    public static ReferenceVector2Quantity GetNullVector2Quantity() => null!;

    public static class ScalarTupleValues
    {
        public static IEnumerable<(Scalar, Scalar)> Valid => Vector2Cases.Datasets.ScalarTupleValues.Valid;
    }

    public static class UnhandledTupleValues
    {
        public static IEnumerable<(Unhandled, Unhandled)> Valid
        {
            get
            {
                foreach (var (x, y) in ScalarTupleValues.Valid)
                {
                    yield return (new(x), new(y));
                }
            }
        }
    }

    public static class Unhandled2Values
    {
        public static IEnumerable<Unhandled2> Valid
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
    public sealed class ValidUnhandledTuple : ATestDataset<(Unhandled, Unhandled)>
    {
        protected override IEnumerable<(Unhandled, Unhandled)> GetSamples() => UnhandledTupleValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled2 : ATestDataset<Unhandled2>
    {
        protected override IEnumerable<Unhandled2> GetSamples() => Unhandled2Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled2_ValidUnhandled2 : ATestDataset<(Unhandled2, Unhandled2)>
    {
        protected override IEnumerable<(Unhandled2, Unhandled2)> GetSamples() => Permutate(Unhandled2Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled2_ValidScalar : ATestDataset<(Unhandled2, Scalar)>
    {
        protected override IEnumerable<(Unhandled2, Scalar)> GetSamples() => Permutate(Unhandled2Values.Valid, ScalarCases.Datasets.ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled2_ValidUnhandled : ATestDataset<(Unhandled2, Unhandled)>
    {
        protected override IEnumerable<(Unhandled2, Unhandled)> GetSamples() => Permutate(Unhandled2Values.Valid, UnhandledCases.Datasets.UnhandledValues.Valid);
    }
}
