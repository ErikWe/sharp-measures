﻿namespace SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

public sealed class InterfaceCollector
{
    public static InterfaceCollector Start(StringBuilder source)
    {
        InterfaceCollector collector = new(source);
        collector.MarkInsertionPoint();
        return collector;
    }

    public static InterfaceCollector Delayed(StringBuilder source) => new(source);

    private StringBuilder Source { get; }

    private int StartIndex { get; set; }
    private List<string> CollectedInterfaces { get; } = new();

    private InterfaceCollector(StringBuilder source)
    {
        Source = source;

        StartIndex = source.Length;
    }

    public void MarkInsertionPoint()
    {
        StartIndex = Source.Length;
    }

    public void AddInterface(string name) => CollectedInterfaces.Add(name);
    public void AddInterfaces(params string[] names) => AddInterfaces(names as IEnumerable<string>);
    public void AddInterfaces(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            AddInterface(name);
        }
    }

    public void InsertInterfaces()
    {
        if (CollectedInterfaces.Count > 0)
        {
            InterfaceBuilding.InsertInterfaceImplementation(Source, StartIndex, CollectedInterfaces);
        }
    }

    public void InsertInterfacesOnNewLines(Indentation indentation)
    {
        if (CollectedInterfaces.Count > 0)
        {
            InterfaceBuilding.InsertInterfaceImplementationOnNewLines(Source, indentation, StartIndex, CollectedInterfaces);
        }
    }
}
