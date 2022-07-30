namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;

internal record class DerivableUnitProcessingContext : SimpleProcessingContext, IDerivableUnitProcessingContext
{
    public bool MultipleDefinitions { get; }
    public HashSet<string> ReservedIDs { get; } = new();

    public DerivableUnitProcessingContext(DefinedType type, bool multipleDefinitions) : base(type)
    {
        MultipleDefinitions = multipleDefinitions;
    }
}
