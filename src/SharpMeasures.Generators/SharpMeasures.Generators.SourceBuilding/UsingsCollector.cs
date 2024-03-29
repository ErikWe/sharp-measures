﻿namespace SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

public sealed class UsingsCollector
{
    public static UsingsCollector Start(StringBuilder source, string fromNamespace)
    {
        UsingsCollector collector = new(source, fromNamespace);

        return collector;
    }

    public static UsingsCollector Delayed(StringBuilder source, string fromNamespace) => new(source, fromNamespace);

    private StringBuilder Source { get; }
    private string FromNamespace { get; }

    private int StartIndex { get; set; }
    private List<string> CollectedUsings { get; } = new();

    private UsingsCollector(StringBuilder source, string fromNamespace)
    {
        Source = source;
        FromNamespace = fromNamespace;

        StartIndex = source.Length;
    }

    public void MarkInsertionPoint()
    {
        StartIndex = Source.Length;
    }

    public void AddUsing(string name) => CollectedUsings.Add(name);
    public void AddUsings(params string[] names) => AddUsings(names as IEnumerable<string>);
    public void AddUsings(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            AddUsing(name);
        }
    }

    public void InsertUsings()
    {
        if (CollectedUsings.Count > 0)
        {
            UsingsBuilding.InsertUsings(Source, FromNamespace, StartIndex, CollectedUsings);
        }
    }
}
