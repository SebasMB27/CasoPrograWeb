namespace CP2.Models;

public class PuzzleViewModel
{
    public string PuzzleName { get; set; } = string.Empty;

    public int Rows { get; set; }

    public int Columns { get; set; }

    public List<FoundWordDto> FoundWords { get; set; } = new();

    public override string ToString() => string.Join(",", FoundWords.Select(word => word.ToString()));
}

public class FoundWordDto
{
    public string Word { get; set; } = string.Empty;

    public CoordinateDto Start { get; set; } = new();

    public CoordinateDto End { get; set; } = new();

    public override string ToString()
    {
        return $"{Word} {Start.Row} {Start.Column} {End.Row} {End.Column}";
    }
}

public class CoordinateDto
{
    public string Column { get; set; } = string.Empty;

    public int Row { get; set; }
}
