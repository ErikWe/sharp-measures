namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators;

using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ExistingSolutionDriver
{
    public static async Task<ExistingSolutionDriver> Build(string solutionPath)
    {
        var driver = DriverConstruction.Construct<SharpMeasuresGenerator>();

        var references = AssemblyLoader.ReferencedAssemblies
                .Where(static (assembly) => assembly.IsDynamic is false)
                .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
                .Cast<MetadataReference>();

        MSBuildLocator.RegisterDefaults();

        var workspace = MSBuildWorkspace.Create();

        var solution = await workspace.OpenSolutionAsync(solutionPath).ConfigureAwait(false);

        var projectID = solution.Projects.Where(static (project) => project.Name is "SharpMeasures").Single().Id;

        //solution = solution.AddMetadataReferences(projectID, references);

        var compilation = await solution.GetProject(projectID)!.GetCompilationAsync().ConfigureAwait(false);

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation!, out compilation, out var _);

        return new(workspace, solution, projectID, driver);
    }

    private Workspace Workspace { get; }
    private Solution Solution { get; set; }
    private ProjectId ProjectID { get; }

    public GeneratorDriver Driver { get; private set; }

    private ExistingSolutionDriver(Workspace workspace, Solution solution, ProjectId projectID, GeneratorDriver driver)
    {
        Workspace = workspace;
        Solution = solution;
        ProjectID = projectID;

        Driver = driver;
    }

    public async void ApplyChange(string filePath, string updatedText) => await ApplyChangeAndRetrieveCompilation(filePath, updatedText).ConfigureAwait(false);

    public async Task<Compilation?> ApplyChangeAndRetrieveCompilation(string filePath, string updatedText)
    {
        filePath = Path.GetFullPath(filePath);

        var documentID = Solution.GetProject(ProjectID)!.Documents.Where((document) => document.FilePath == filePath).Single().Id;

        Solution = Solution.WithDocumentText(documentID, SourceText.From(updatedText));

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
