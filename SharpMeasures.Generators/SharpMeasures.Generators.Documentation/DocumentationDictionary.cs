namespace SharpMeasures.Generators.Documentation;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public sealed class DocumentationDictionary : ReadOnlyEquatableDictionary<string, DocumentationFile>
{
    public DocumentationDictionary(IReadOnlyDictionary<string, DocumentationFile> dictionary) : base(dictionary) { }
}
