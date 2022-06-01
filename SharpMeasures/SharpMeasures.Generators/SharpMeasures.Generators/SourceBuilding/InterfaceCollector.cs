namespace SharpMeasures.Generators.SourceBuilding;

using System.Collections.Generic;
using System.Text;

internal class InterfaceCollector
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

    public void AddInterface(string name)
    {
        CollectedInterfaces.Add(name);
    }

    public void AddInterfaces(IEnumerable<string> names)
    {
        foreach (string name in names)
        {
            AddInterface(name);
        }
    }

    public void AddInterfaces(params string[] names)
    {
        AddInterfaces(names as IEnumerable<string>);
    }

    public void InsertInterfaces()
    {
        if (CollectedInterfaces.Count > 0)
        {
            InterfaceBuilding.InsertInterfaceImplementation(Source, StartIndex, CollectedInterfaces);
        }
    }
}
