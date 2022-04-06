namespace ErikWe.SharpMeasures.SourceGenerators.Tests;

using System.Runtime.CompilerServices;
using System.IO;

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