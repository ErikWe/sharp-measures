namespace SharpMeasures.Generators.Tests;

using System.Runtime.CompilerServices;

internal static class ProjectPath
{
    private static string? CachedPath { get; set; }
    public static string Path => CachedPath ??= CalculatePath();

    private static string CalculatePath()
    {
        string pathName = GetSourceFilePathName();
        return pathName[..pathName.LastIndexOf('\\')];
    }

    private static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;
}
