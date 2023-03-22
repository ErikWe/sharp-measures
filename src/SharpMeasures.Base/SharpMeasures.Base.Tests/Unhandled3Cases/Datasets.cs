namespace SharpMeasures.Tests.Unhandled3Cases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static Unhandled3 GetValidUnhandled3() => Unhandled3Values.Valid.First();

    public static ReferenceScalarQuantity GetNullScalarQuantity() => null!;
    public static ReferenceVector3Quantity GetNullVector3Quantity() => null!;

    public static class ScalarTupleValues
    {
        public static IEnumerable<(Scalar, Scalar, Scalar)> Valid => Vector3Cases.Datasets.ScalarTupleValues.Valid;
    }

    public static class UnhandledTupleValues
    {
        public static IEnumerable<(Unhandled, Unhandled, Unhandled)> Valid
        {
            get
            {
                foreach (var (x, y, z) in ScalarTupleValues.Valid)
                {
                    yield return (new(x), new(y), new(z));
                }
            }
        }
    }

    public static class Unhandled3Values
    {
        public static IEnumerable<Unhandled3> Valid
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
    public sealed class ValidUnhandledTuple : ATestDataset<(Unhandled, Unhandled, Unhandled)>
    {
        protected override IEnumerable<(Unhandled, Unhandled, Unhandled)> GetSamples() => UnhandledTupleValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled3 : ATestDataset<Unhandled3>
    {
        protected override IEnumerable<Unhandled3> GetSamples() => Unhandled3Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled3_ValidUnhandled3 : ATestDataset<(Unhandled3, Unhandled3)>
    {
        protected override IEnumerable<(Unhandled3, Unhandled3)> GetSamples() => Permutate(Unhandled3Values.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled3_ValidScalar : ATestDataset<(Unhandled3, Scalar)>
    {
        protected override IEnumerable<(Unhandled3, Scalar)> GetSamples() => Permutate(Unhandled3Values.Valid, ScalarCases.Datasets.ScalarValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidUnhandled3_ValidUnhandled : ATestDataset<(Unhandled3, Unhandled)>
    {
        protected override IEnumerable<(Unhandled3, Unhandled)> GetSamples() => Permutate(Unhandled3Values.Valid, UnhandledCases.Datasets.UnhandledValues.Valid);
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    [SuppressMessage("Minor Code Smell", "S101: Types should be named in PascalCase")]
    public sealed class ValidUnhandled3_ValidMatrix4x4 : ATestDataset<(Unhandled3, System.Numerics.Matrix4x4)>
    {
        protected override IEnumerable<(Unhandled3, System.Numerics.Matrix4x4)> GetSamples() => Permutate(Unhandled3Values.Valid, Matrix4x4Values.Valid);
    }
}
