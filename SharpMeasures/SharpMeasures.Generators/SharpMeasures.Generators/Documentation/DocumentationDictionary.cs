namespace SharpMeasures.Generators.Documentation;

using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

internal class DocumentationDictionary : ReadOnlyDictionary<string, DocumentationFile>
{
    public static DocumentationDictionary Construct(IReadOnlyDictionary<string, DocumentationFileBuilder> builders)
    {
        return new(builders.Values.ToDictionary(static (file) => file.Name, static (file) => file.Finalize()));
    }

    public DocumentationDictionary(IDictionary<string, DocumentationFile> dictionary) : base(dictionary) { }
}
