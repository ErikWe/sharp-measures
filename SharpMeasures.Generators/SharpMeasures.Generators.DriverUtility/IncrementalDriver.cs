namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators;

using System.Linq;
using System.Threading.Tasks;

public class IncrementalDriver
{
    public static async Task<IncrementalDriver> Build(string initialText, string documentationDirectory)
    {
        AdhocWorkspace workspace = new();

        GeneratorDriver driver = DriverConstruction.Construct<SharpMeasuresGenerator>(documentationDirectory);

        var solutionInfo = SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Default);
        var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, "Test", "testassembly", "C#",
            parseOptions: ParseOptions,
            compilationOptions: CompilationOptions,
            metadataReferences: AssemblyLoader.ReferencedAssemblies
                .Where(static (assembly) => assembly.IsDynamic is false)
                .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
                .Cast<MetadataReference>()
        );

        var documentID = DocumentId.CreateNewId(projectInfo.Id);

        Solution solution = workspace.AddSolution(solutionInfo);
        solution = solution.AddProject(projectInfo);
        solution = solution.AddDocument(documentID, "File.cs", SourceText.From(initialText));

        var compilation = await solution.Projects.First().GetCompilationAsync().ConfigureAwait(false);

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation!, out compilation, out var _);

        return new IncrementalDriver(workspace, solution, documentID, driver);
    }

    private static ParseOptions ParseOptions { get; } = new CSharpParseOptions(LanguageVersion.Preview);
    private static CompilationOptions CompilationOptions { get; } = new CSharpCompilationOptions(outputKind: OutputKind.DynamicallyLinkedLibrary);

    private Workspace Workspace { get; }
    private Solution Solution { get; set; }
    private DocumentId DocumentID { get; }

    public GeneratorDriver Driver { get; private set; }

    private IncrementalDriver(Workspace workspace, Solution solution, DocumentId documentID, GeneratorDriver driver)
    {
        Workspace = workspace;
        Solution = solution;
        DocumentID = documentID;

        Driver = driver;
    }

    public async void ApplyChange(string updatedText) => await ApplyChangeAndRetrieveCompilation(updatedText).ConfigureAwait(false);

    public async Task<Compilation?> ApplyChangeAndRetrieveCompilation(string updatedText)
    {
        Solution = Solution.WithDocumentText(DocumentID, SourceText.From(updatedText));

        Workspace.TryApplyChanges(Solution);

        var updatedCompilation = await Solution.Projects.First().GetCompilationAsync().ConfigureAwait(false);

        if (updatedCompilation is null)
        {
            return null;
        }

        Driver = Driver.RunGeneratorsAndUpdateCompilation(updatedCompilation, out updatedCompilation, out _);

        return updatedCompilation;
    }
}
