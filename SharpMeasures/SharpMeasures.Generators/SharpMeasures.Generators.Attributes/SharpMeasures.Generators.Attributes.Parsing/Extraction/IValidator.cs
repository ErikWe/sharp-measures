namespace SharpMeasures.Generators.Attributes.Parsing.Extraction;

using Microsoft.CodeAnalysis;

public interface IValidator<TParameters>
{
    public abstract ExtractionValidity Check(AttributeData attributeData, TParameters parameters);
}
