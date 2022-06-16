using static Day00.ReadInputs;

var values = Read(row => row.Select(x => (int)char.GetNumericValue(x)))
    ?? throw new NullReferenceException("Height values are required");

var heights = new HeightMap(values);

var lows = heights.Positions()
    .Where(p => heights.Neighbors(p.Position).All(n => n.Height > p.Height))
    .ToList();

Console.WriteLine($"# of lows: {lows.Count}");
Console.WriteLine($"risks: {lows.Select(x => x.Height + 1).Sum()}");
Console.WriteLine($"Lows: {string.Join("|", lows)}");

var basinSizes = new List<int>();

foreach (var basin in lows)
{
    Console.WriteLine($"Starting basin at {basin.Position}");
    var visited = new HashSet<(int X, int Y)> { basin.Position };
    var pending = new Queue<(int X, int Y)>();
    pending.Enqueue(basin.Position);

    while (pending.TryDequeue(out var current))
    {
        foreach (var (Position, Height) in heights.Neighbors(current))
        {
            //Console.WriteLine($"checking height {Height} of {Position}");
            if (Height < 9 && visited.Add(Position))
            {
                //Console.WriteLine($"traveling to {Height}");
                pending.Enqueue(Position);
            }
        }
    }
    basinSizes.Add(visited.Count);
    Console.WriteLine($"Completed basin at {basin.Position}, it was {visited.Count} cells big.");
    Console.WriteLine($"Lows: {string.Join("|", visited)}");
    Console.WriteLine("");
}

var threeLargest = basinSizes
    .OrderByDescending(x => x)
    .Take(3).ToList();

Console.WriteLine($"Largest Basins: {string.Join(", ", threeLargest)}");

var solution2 = basinSizes
    .OrderByDescending(x => x)
    .Take(3)
    .Aggregate((product, next) => product * next);

Console.WriteLine($"Solution 2 is : {solution2}");


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