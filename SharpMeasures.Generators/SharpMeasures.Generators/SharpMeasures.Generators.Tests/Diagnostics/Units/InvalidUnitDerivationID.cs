﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUnitDerivationID
{
    private static string Text(string id) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit({{id}}, "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    [Fact]
    public Task Null_ExactListAndVerify()
    {
        string source = Text("null");

        return AssertExactlyInvalidUnitDerivationIDDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Empty_ExactListAndVerify()
    {
        string source = Text("\"\"");

        return AssertExactlyInvalidUnitDerivationIDDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInvalidUnitDerivationIDDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitderivationIDDiagnostics);

    private static IReadOnlyCollection<string> InvalidUnitderivationIDDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidUnitDerivationID };
}
