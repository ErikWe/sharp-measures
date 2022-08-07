namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.PostResolutionFilter;

using SharpMeasures.Generators.Quantities.Parsing.Contexts.PostResolutionFilter;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal record class ScalarConstantPostResolutionFilterContext : QuantityConstantPostResolutionFilterContext, IScalarConstantPostResolutionFilterContext
{
    public HashSet<string> IncludedBases { get; }

    public ScalarConstantPostResolutionFilterContext(DefinedType type, HashSet<string> inheritedConstantNames, HashSet<string> inheritedConstantMuliplesNames, HashSet<string> includedBases,
        HashSet<string> includedUnits) : base(type, inheritedConstantNames, inheritedConstantMuliplesNames, includedUnits)
    {
        IncludedBases = includedBases;
    }
}
