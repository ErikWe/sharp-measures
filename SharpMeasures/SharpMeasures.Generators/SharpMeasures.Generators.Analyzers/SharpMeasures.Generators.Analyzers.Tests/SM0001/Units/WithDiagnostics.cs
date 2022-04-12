namespace SharpMeasures.Generators.Analyzers.Tests.SM0001.Units;

using Microsoft.CodeAnalysis.Testing;

using SharpMeasures.Generators.Analyzers.Tests.Utility;
using SharpMeasures.Generators.Analyzers;

using System.Threading.Tasks;

using Xunit;

public class WithDiagnostics
{
    [Fact]
    public async Task PublicReadonlyRecordStruct()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedUnit(typeof(string))]
public readonly record struct UnitOfLength { }
";

        DiagnosticResult expected = CSharpAnalyzerVerifier.Diagnostic<SharpMeasuresGeneratorsAnalyzer>("SharpMeasures0001")
            .WithLocation(0).WithArguments("TypeName");
        
        await CSharpAnalyzerVerifier.VerifyDiagnostics<SharpMeasuresGeneratorsAnalyzer>(source, expected).ConfigureAwait(false);
    }
}
