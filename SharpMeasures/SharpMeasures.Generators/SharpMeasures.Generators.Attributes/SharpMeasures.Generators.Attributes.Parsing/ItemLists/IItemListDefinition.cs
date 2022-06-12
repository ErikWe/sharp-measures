﻿namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public interface IItemListDefinition<TItem> : IAttributeDefinition
{
    public abstract IReadOnlyList<TItem> Items { get; }

    new public abstract IItemListLocations Locations { get; }
}