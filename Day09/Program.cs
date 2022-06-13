using static Day00.ReadInputs;
using Day00;


var values = Read(row => row.Select(x => (int)char.GetNumericValue(x)))
    ?? throw new NullReferenceException("Height values are required");

var heights = new HeightMap(values);

var lows = heights.Positions()
    .Where(p => heights.Neighbors(p.Position).All(n => n.Height > p.Height))
    .ToList();

var basin = new List<(int X, int Y)>();
var f = lows.First();

int a = 0;
foreach (var n in heights.Neighbors(f.Position))
{
    if (n.Height < 9)
    {

    }
}


int Go(List<((int X, int Y), int Height)> heights)
{
    foreach (var h in heights)
    {
        if (h <)
    }
}


//heights.Render();
Console.WriteLine($"lows: {lows.Count}");
Console.WriteLine($"risks: {lows.Select(x => x.Height + 1).Sum()}");

//Console.WriteLine($"{string.Join("|", heights.Neighbors((0,0)))}");
//Console.WriteLine($"{string.Join("|", lows)}");


public class HeightMap
{
    private readonly List<int> heights;
    private readonly int width;

    public HeightMap(IEnumerable<IEnumerable<int>> map)
    {
        heights = new List<int>(512);
        foreach (var row in map)
        {
            heights.AddRange(row);
            if (width == default)
            {
                width = heights.Count;
            }
        }
    }
    public int? this[(int X, int Y) position]
        => this[position.X, position.Y];

    public int? this[int x, int y]
    {
        get
        {
            if (x < 0) return null;
            if (x >= width) return null;
            if (y < 0) return null;
            if (y >= width) return null;

            int offset = x * width + y;
            if (offset < 0 || offset >= heights.Count) return null;
            return heights[offset];
        }
    }

    public IEnumerable<((int X, int Y) Position, int Height)> Neighbors((int X, int Y) position)
    {
        var up = this[position.X, position.Y + 1];
        if (up is not null)
        {
            yield return ((position.X, position.Y + 1), up.Value);
        }

        var down = this[position.X, position.Y - 1];
        if (down is not null)
        {
            yield return ((position.X, position.Y - 1), down.Value);
        }

        var left = this[position.X - 1, position.Y];
        if (left is not null)
        {
            yield return ((position.X - 1, position.Y), left.Value);
        }

        var right = this[position.X + 1, position.Y];
        if (right is not null)
        {
            yield return ((position.X + 1, position.Y), right.Value);
        }
    }

    public IEnumerable<((int X, int Y) Position, int Height)> Positions()
    {
        for (var offset = 0; offset < heights.Count; offset++)
        {
            yield return ((offset / width, offset % width), heights[offset]);
        }
    }
    public IEnumerable<IEnumerable<int>> Rows()
    {
        for (var row = 0; row < heights.Count / width; row++)
        {
            yield return heights.Skip(row * width).Take(width);
        }
    }
    public void Render()
    {
        foreach (var row in Rows())
        {
            Console.WriteLine(string.Join("", row));
        }
    }
}