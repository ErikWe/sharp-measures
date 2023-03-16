namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis.Diagnostics;

using System.Collections.Generic;

public record class DriverConstructionConfiguration(IReadOnlyDictionary<string, string> DocumentationFiles, AnalyzerConfigOptionsProvider OptionsProvider)
{
    public static DriverConstructionConfiguration Empty { get; } = new(new Dictionary<string, string>(), CustomAnalyzerConfigOptionsProvider.Empty);
}
