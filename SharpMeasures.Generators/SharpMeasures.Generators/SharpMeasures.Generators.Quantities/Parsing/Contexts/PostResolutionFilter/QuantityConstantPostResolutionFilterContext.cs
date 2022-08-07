namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.PostResolutionFilter;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

public record class QuantityConstantPostResolutionFilterContext : IQuantityConstantPostResolutionFilterContext
{
    public DefinedType Type { get; }

    public HashSet<string> InheritedConstantNames { get; }
    public HashSet<string> InheritedConstantMultiplesNames { get; }

    public HashSet<string> IncludedUnits { get; }

    public QuantityConstantPostResolutionFilterContext(DefinedType type, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMultiplesNames, HashSet<string> includedUnits)
    {
        Type = type;

        InheritedConstantNames = inheritedConstantNames;
        InheritedConstantMultiplesNames = inheritedConstantMultiplesNames;

        IncludedUnits = includedUnits;
    }
}
