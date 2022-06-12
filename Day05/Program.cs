using static Day00.ReadInputs;
using Day00;

var lines = Read(Line.Create).ToList();

lines.ToConsole(lines =>
{
    var overlaps = lines
        .Where(x => !x.Diagonal)
        .SelectMany(x => x.Path())
        .GroupBy(x => x)
        .Count(x => x.Count() > 1);

    return new[]
    {
        $"overlaps: {overlaps}"
    };
}

);

lines.ToConsole(lines =>
    new[]
    {
        $"overlaps: {lines.SelectMany(x => x.Path()).GroupBy(x => x).Count(x => x.Count() > 1)}"
    }
);


