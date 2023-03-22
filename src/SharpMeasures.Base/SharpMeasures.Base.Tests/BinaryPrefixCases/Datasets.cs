namespace SharpMeasures.Tests.BinaryPrefixCases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static BinaryPrefix GetNullBinaryPrefix() => null!;
    public static BinaryPrefix GetValidBinaryPrefix() => BinaryPrefixValues.Valid.First();

    public static class ExponentInt32Values
    {
        public static IEnumerable<int> Invalid => new[]
        {
            int.MaxValue
        };

        public static IEnumerable<int> Valid => new[]
        {
            0,
            1,
            -1,
            100,
            int.MinValue
        };
    }

    public static class BinaryPrefixValues
    {
        public static IEnumerable<BinaryPrefix> Valid
        {
            get
            {
                foreach (var exponent in ExponentInt32Values.Valid)
                {
                    yield return BinaryPrefix.TwoToThePower(exponent);
                    yield return BinaryPrefix.ThousandTwentyFourToThePower(exponent);
                }
            }
        }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class InvalidExponentInt32 : ATestDataset<int>
    {
        protected override IEnumerable<int> GetSamples() => ExponentInt32Values.Invalid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidExponentInt32 : ATestDataset<int>
    {
        protected override IEnumerable<int> GetSamples() => ExponentInt32Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidBinaryPrefix : ATestDataset<BinaryPrefix>
    {
        protected override IEnumerable<BinaryPrefix> GetSamples() => BinaryPrefixValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidBinaryPrefix_ValidBinaryPrefix : ATestDataset<(BinaryPrefix, BinaryPrefix)>
    {
        protected override IEnumerable<(BinaryPrefix, BinaryPrefix)> GetSamples() => Permutate(BinaryPrefixValues.Valid);
    }
}
