namespace SharpMeasures.Generators.SourceBuilding;

using System.Text;

public class NewlineSeparationHandler
{
    private StringBuilder Builder { get; }

    private int BuilderLengthAtLastAddedSeparation { get; set; }

    public bool HasSeparation => Builder.Length == BuilderLengthAtLastAddedSeparation;
    public bool LacksSeparation => HasSeparation is false;

    public NewlineSeparationHandler(StringBuilder builder, bool initialState = true)
    {
        Builder = builder;

        BuilderLengthAtLastAddedSeparation = initialState
            ? Builder.Length
            : -1;
    }

    public void AddIfNecessary()
    {
        if (LacksSeparation)
        {
            Add();
        }
    }

    public void Add()
    {
        Builder.AppendLine();

        MarkUnncecessary();
    }

    public void MarkUnncecessary()
    {
        BuilderLengthAtLastAddedSeparation = Builder.Length;
    }
}
