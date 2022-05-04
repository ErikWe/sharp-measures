namespace SharpMeasures.Generators.Tests.Diagnostics.TypeNotPartial;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitsWithDiagnostics
{
    [Fact]
    public Task Class_ExactMatch()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }
";

        return GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics).Verify();
    }

    [Fact]
    public void Struct()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public struct UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void Record()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public record UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void RecordClass()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public record class UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void RecordStruct()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public record struct UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics);
    }

    [Fact]
    public void ReadonlyRecordStruct()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public readonly record struct UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).AllListedDiagnosticIDsReported(ExpectedDiagnostics);
    }

    private static IReadOnlyCollection<string> ExpectedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotPartial };
}
