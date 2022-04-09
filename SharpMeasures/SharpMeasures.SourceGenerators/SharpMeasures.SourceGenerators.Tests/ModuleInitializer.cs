namespace SharpMeasures.SourceGeneration.Tests;

using System.IO;
using System.Runtime.CompilerServices;

using VerifyTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Enable();

        VerifierSettings.DerivePathInfo(
            static (sourceFile, projectDirectory, type, method) =>
            {
                return new(
                    directory: Path.Combine(projectDirectory, "Verify"),
                    typeName: type.Name,
                    methodName: method.Name);
            });
    }
}