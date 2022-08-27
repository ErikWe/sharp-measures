namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeBasesProcessingContext : SimpleProcessingContext, IUniqueItemListProcessingContext<string>
{
    public HashSet<string> ListedItems { get; } = new();

    public IncludeBasesProcessingContext(DefinedType type) : base(type) { }
}
