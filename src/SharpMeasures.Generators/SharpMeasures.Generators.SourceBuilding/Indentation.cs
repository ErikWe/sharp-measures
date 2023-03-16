namespace SharpMeasures.Generators.SourceBuilding;

using System.Text;

public readonly record struct Indentation(int Level, string Text)
{
    public static Indentation Zero { get; } = new(0);

    private static string IndentationString { get; } = "    ";

    public Indentation(int level) : this(level, GetIndentationString(level)) { }

    public Indentation Increased => new(Level + 1);
    public Indentation Decreased => new(Level - 1);

    public override string ToString() => Text;

    private static string GetIndentationString(int level)
    {
        StringBuilder builder = new();

        for (var i = 0; i < level; i++)
        {
            builder.Append(IndentationString);
        }

        return builder.ToString();
    }
}
