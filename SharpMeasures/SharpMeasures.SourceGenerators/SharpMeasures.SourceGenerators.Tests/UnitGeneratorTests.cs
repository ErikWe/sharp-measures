namespace SharpMeasures.SourceGeneration.Tests;

using SharpMeasures.SourceGeneration.Units;

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
using SharpMeasures.SourceGeneration;
using SharpMeasures.SourceGeneration.Attributes.Utility;

using System;

namespace Foo
{
    namespace Bar
    {
        public readonly record struct Length { }
        public readonly record struct Time { }
        public readonly record struct Speed { }

        [GeneratedUnit(typeof(Length))]
        [FixedUnitInstance(""Metre"", UnitPluralCodes.AppendS, 1)]
        public readonly partial record struct UnitOfLength { }

        [GeneratedUnit(typeof(Time))]
        [FixedUnitInstance(""Second"", UnitPluralCodes.AppendS, 1)]
        public readonly partial record struct UnitOfTime { }

        [GeneratedUnit(typeof(Speed))]
        [DerivedUnit(""{0} / {1}"", typeof(UnitOfLength), typeof(UnitOfTime)]
        [DerivedUnitInstance(""MetrePerSecond"", UnitPluralCodes.InsertSBeforePer, new Type[] { typeof(UnitOfLength), typeof(UnitOfTime) }, new string[] { ""Metre"", ""Second"" })]
        public readonly partial record struct UnitOfSpeed { }
    }
}";

        return Utility.VerifyGenerator<UnitGenerator>(source);
    }
}