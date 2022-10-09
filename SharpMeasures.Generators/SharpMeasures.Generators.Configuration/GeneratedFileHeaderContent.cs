namespace SharpMeasures.Generators.Configuration;

using System;

[Flags]
public enum GeneratedFileHeaderContent
{
    None,
    All = Tool | Version | Date | Time,
    Tool = 1,
    Version = 2,
    Date = 4,
    Time = 8
}
