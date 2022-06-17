namespace SharpMeasures.Generators.Tests.Diagnostics.TypeNotPartial;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class SharpMeasuresUnit
{
    [Fact]
    public Task Class_ExactListAndVerify()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public class UnitOfLength { }
";

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpectedDiagnostics).Verify();
    }

    [Fact]
    public void Struct_ExactList()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public struct UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void Record_ExactList()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public record UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void RecordClass_ExactList()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public record class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void RecordStruct_ExactList()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public record struct UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void ReadonlyRecordStruct_ExactList()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public readonly record struct UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpectedDiagnostics);
    }

    private static IReadOnlyCollection<string> ExpectedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotPartial };
}
