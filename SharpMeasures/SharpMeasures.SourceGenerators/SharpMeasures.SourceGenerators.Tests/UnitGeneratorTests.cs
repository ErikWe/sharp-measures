namespace SharpMeasures.SourceGenerators.Tests;

using SharpMeasures.SourceGenerators.Units;

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
using SharpMeasures.Attributes;
using SharpMeasures.Attributes.Utility;

using System;

namespace Foo
{
    namespace Bar
    {
        public readonly record struct Length { }
        public readonly record struct Time { }
        public readonly record struct Speed { }

        [Unit(typeof(Length))]
        [FixedUnitInstance(""Metre"", UnitPluralCodes.AppendS, 1)]
        public readonly partial record struct UnitOfLength { }

        [Unit(typeof(Time))]
        [FixedUnitInstance(""Second"", UnitPluralCodes.AppendS, 1)]
        public readonly partial record struct UnitOfTime { }

        [Unit(typeof(Speed))]
        [DerivedUnit(""{0} / {1}"", typeof(UnitOfLength), typeof(UnitOfTime)]
        [DerivedUnitInstance(""MetrePerSecond"", UnitPluralCodes.InsertSBeforePer, new Type[] { typeof(UnitOfLength), typeof(UnitOfTime) }, new string[] { ""Metre"", ""Second"" })]
        public readonly partial record struct UnitOfSpeed { }
    }
}";

        return Utility.VerifyGenerator<UnitGenerator>(source);
    }
}