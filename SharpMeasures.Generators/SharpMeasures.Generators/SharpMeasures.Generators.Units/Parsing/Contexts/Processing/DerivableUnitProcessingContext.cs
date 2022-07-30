namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;

internal record class DerivableUnitProcessingContext : SimpleProcessingContext, IDerivableUnitProcessingContext
{
    public bool MultipleDefinitions { get; private set; }
    public HashSet<string> ReservedIDs { get; } = new();

    public DerivableUnitProcessingContext(DefinedType type) : base(type) { }

    public void MarkMultipleDefinitions() => MultipleDefinitions = true;
}
