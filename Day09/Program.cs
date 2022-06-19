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
