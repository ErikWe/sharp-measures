namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnitsProcessingContext : SimpleProcessingContext, IUniqueItemListProcessingContext<string>
{
    public HashSet<string> ListedItems { get; } = new();

    public ExcludeUnitsProcessingContext(DefinedType type) : base(type) { }
}
