namespace SharpMeasures.Generators.Analyzers.Tests.SM0001.Units;

using SharpMeasures.Generators.Analyzers.Tests.Utility;
using SharpMeasures.Generators.Analyzers;

using System.Threading.Tasks;

using Xunit;

public class NoDiagnostics
{
    [Fact]
    public async Task PublicReadonlyPartialRecordStruct()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedUnit(typeof(string))]
public readonly partial record struct UnitOfLength { }
";

        await CSharpAnalyzerVerifier.VerifyNoDiagnostics<SharpMeasuresGeneratorsAnalyzer>(source).ConfigureAwait(false);
    }
}
