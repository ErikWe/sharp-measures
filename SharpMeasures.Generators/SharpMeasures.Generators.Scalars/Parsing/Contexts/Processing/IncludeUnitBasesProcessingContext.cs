namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeUnitBasesProcessingContext : SimpleProcessingContext, IUniqueItemListProcessingContext<string>
{
    public HashSet<string> ListedItems { get; } = new();

    public IncludeUnitBasesProcessingContext(DefinedType type) : base(type) { }
}
