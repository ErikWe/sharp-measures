namespace SharpMeasures.Generators.Vectors.Documentation;

internal interface IGroupDocumentationStrategy
{
    public abstract string Header();

    public abstract string ScalarFactoryMethod(int dimension);
    public abstract string VectorFactoryMethod(int dimension);
    public abstract string ComponentsFactoryMethod(int dimension);
}
