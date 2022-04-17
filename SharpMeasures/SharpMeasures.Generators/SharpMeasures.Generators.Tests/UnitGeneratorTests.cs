namespace SharpMeasures.Generators.Tests;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Utility;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitGeneratorTests
{
    [Fact]
    public Task UnitGenerator_ShouldBeExactCode()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Utility;

using System;

namespace Foo
{
    namespace Bar
    {
        public readonly record struct Length { }
        public readonly record struct Time { }
        public readonly record struct Speed { }

        [GeneratedUnit(typeof(Length))]
        [FixedUnit(""Metre"", UnitPluralCodes.AppendS, 1)]
        public readonly partial record struct UnitOfLength { }

        [GeneratedUnit(typeof(Time))]
        [FixedUnit(""Second"", UnitPluralCodes.AppendS, 1)]
        public readonly partial record struct UnitOfTime { }

        [GeneratedUnit(typeof(Speed))]
        [DerivableUnit(""{0} / {1}"", typeof(UnitOfLength), typeof(UnitOfTime)]
        [DerivedUnit(""MetrePerSecond"", UnitPluralCodes.InsertSBeforePer, new Type[] { typeof(UnitOfLength), typeof(UnitOfTime) }, new string[] { ""Metre"", ""Second"" })]
        public readonly partial record struct UnitOfSpeed { }
    }
}";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }
}