namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

using System.Linq;
using System.Threading.Tasks;

public static class CrossAssemblyDriverConstruction
{
    private static LanguageVersion LanguageVersion { get; } = LanguageVersion.Preview;

    private static CSharpParseOptions ParseOptions { get; } = new(languageVersion: LanguageVersion);
    private static CSharpCompilationOptions CompilationOptions { get; } = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

    public static Task<(GeneratorDriver Driver, Compilation Compilation)?> ConstructAndRun<TGenerator>(string localSource, string foreignSource) where TGenerator : IIncrementalGenerator, new() => ConstructAndRun<TGenerator>(localSource, foreignSource, DriverConstructionConfiguration.Empty);
    public static Task<(GeneratorDriver Driver, Compilation Compilation)?> ConstructAndRun<TGenerator>(string localSource, string foreignSource, DriverConstructionConfiguration configuration) where TGenerator : IIncrementalGenerator, new() => ConstructAndRun(localSource, foreignSource, new TGenerator(), configuration);
    public static Task<(GeneratorDriver Driver, Compilation Compilation)?> ConstructAndRun(string localSource, string foreignSource, IIncrementalGenerator generator) => ConstructAndRun(localSource, foreignSource, generator, DriverConstructionConfiguration.Empty);
    public static Task<(GeneratorDriver Driver, Compilation Compilation)?> ConstructAndRun(string localSource, string foreignSource, IIncrementalGenerator generator, DriverConstructionConfiguration configuration) => RunAndUpdateCompilation(localSource, foreignSource, Construct(generator, configuration));

    public static GeneratorDriver Construct<TGenerator>() where TGenerator : IIncrementalGenerator, new() => Construct<TGenerator>(DriverConstructionConfiguration.Empty);
    public static GeneratorDriver Construct<TGenerator>(DriverConstructionConfiguration configuration) where TGenerator : IIncrementalGenerator, new() => Construct(new TGenerator(), configuration);

    public static GeneratorDriver Construct(IIncrementalGenerator generator) => Construct(generator, DriverConstructionConfiguration.Empty);
    public static GeneratorDriver Construct(IIncrementalGenerator generator, DriverConstructionConfiguration configuration)
    {
        return DriverConstruction.Construct(generator, configuration).WithUpdatedParseOptions(ParseOptions);
    }

    public async static Task<Compilation?> CreateCompilation(string localSource, string foreignSource)
    {
        using AdhocWorkspace workspace = new();

        var solutionInfo = SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Default);

        var localProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, "LocalTest", "localtestassembly", "C#",
            parseOptions: ParseOptions,
            compilationOptions: CompilationOptions,
            metadataReferences: AssemblyLoader.ReferencedAssemblies
                .Where(static (assembly) => assembly.IsDynamic is false)
                .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
                .Cast<MetadataReference>()
        );

        var localDocumentID = DocumentId.CreateNewId(localProjectInfo.Id);

        var foreignProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, "ForeignTest", "foreigntestassembly", "C#",
            parseOptions: ParseOptions,
            compilationOptions: CompilationOptions,
            metadataReferences: AssemblyLoader.ReferencedAssemblies
                .Where(static (assembly) => assembly.IsDynamic is false)
                .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
                .Cast<MetadataReference>()
        );

        var foreignDocumentID = DocumentId.CreateNewId(foreignProjectInfo.Id);

        Solution solution = workspace.AddSolution(solutionInfo);
        solution = solution.AddProject(localProjectInfo);
        solution = solution.AddProject(foreignProjectInfo);
        solution = solution.AddProjectReference(localProjectInfo.Id, new ProjectReference(foreignProjectInfo.Id));
        solution = solution.AddDocument(localDocumentID, "File.cs", SourceText.From(localSource));
        solution = solution.AddDocument(foreignDocumentID, "File.cs", SourceText.From(foreignSource));

        return await solution.GetProject(localProjectInfo.Id)!.GetCompilationAsync().ConfigureAwait(false);
    }

    public async static Task<GeneratorDriver?> Run(string localSource, string foreignSource, GeneratorDriver driver)
    {
        if (await CreateCompilation(localSource, foreignSource).ConfigureAwait(false) is not Compilation compilation)
        {
            return null;
        }

        return driver.RunGenerators(compilation);
    }

    public async static Task<(GeneratorDriver Driver, Compilation Compilation)?> RunAndUpdateCompilation(string localSource, string foreignSource, GeneratorDriver driver)
    {
        if (await CreateCompilation(localSource, foreignSource).ConfigureAwait(false) is not Compilation compilation)
        {
            return null;
        }

        return (driver.RunGeneratorsAndUpdateCompilation(compilation, out compilation, out _), compilation);
    }
}
