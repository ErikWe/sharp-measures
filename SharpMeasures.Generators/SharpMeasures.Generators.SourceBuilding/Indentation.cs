namespace SharpMeasures.Generators.SourceBuilding;

public readonly record struct Indentation(int Level, string Text)
{
    public static Indentation Zero { get; } = new(0);

    public Indentation(int level) : this(level, GetIndentationString(level)) { }

    public Indentation Increased => new(Level + 1);
    public Indentation Decreased => new(Level - 1);

    public override string ToString() => Text;

    private static string GetIndentationString(int level)
    {
        string text = "";
        for (int i = 0; i < level; i++)
        {
            text += "    ";
        }

        return text;
    }
}
