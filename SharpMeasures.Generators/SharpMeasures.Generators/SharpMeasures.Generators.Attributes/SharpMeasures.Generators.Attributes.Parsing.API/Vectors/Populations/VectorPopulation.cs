namespace SharpMeasures.Generators.Vectors.Populations;

public class VectorPopulation
{
    public BaseVectorPopulation BaseVectorPopulation { get; }
    public ResizedVectorPopulation ResizedVectorPopulation { get; }

    public ResizedGroupPopulation ResizedGroupPopulation { get; }

    public VectorPopulation(BaseVectorPopulation baseVectorPopulation, ResizedVectorPopulation resizedVectorPopulation,
        ResizedGroupPopulation resizedGroupPopulation)
    {
        BaseVectorPopulation = baseVectorPopulation;
        ResizedVectorPopulation = resizedVectorPopulation;

        ResizedGroupPopulation = resizedGroupPopulation;
    }
}
