namespace SharpMeasures.Generators.Configuration;

using System;

[Flags]
public enum GeneratedFileHeaderContent
{
    None = 0,
    Header = 1,
    Tool = 2,
    Version = 4,
    Date = 8,
    Time = 16,
    All = None | Header | Tool | Version | Date | Time
}
