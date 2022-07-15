namespace SharpMeasures.Generators.Scalars.Pipelines.Convertible;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IConvertibleScalar> ConvertibleScalars => convertibleScalars;

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IConvertibleScalar> convertibleScalars { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IConvertibleScalar> convertibleScalars, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        this.convertibleScalars = convertibleScalars.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
