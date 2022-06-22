using static Day00.ReadInputs;

static int AddOne(int value) => value + 1;
static int ToZero(int value) => 0;

var grid = new Grid<int>(ReadAsRowsOfInts());

HashSet<Node<int>> flashed = new();
List<Node<int>> flashing;

int Step()
{
    List<Node<int>> query() => grid.Where(n => n.Value > 9 && flashed.Add(n)).ToList();
    grid.Each(n => n.SetValue(AddOne));

    while ((flashing = query()).Any())
    {
        foreach (var flash in flashing)
        {
            foreach (var neighbor in grid.Neighbors(flash))
            {
                neighbor.SetValue(AddOne);
            }
        }
    }

    foreach (var (X, Y) in flashed)
    {
        _ = grid[X, Y]?.SetValue(ToZero) 
            ?? throw new InvalidOperationException($"Invalid grid position ({X},{Y}).");
    }

    var count = flashed.Count;
    flashed.Clear();
    return count;
}

var flashes = 0;
var step = 1;
while (Console.ReadKey().KeyChar != 'q')
{
    Console.WriteLine("");
    grid.WriteTo();
    flashes += Step();
    Console.WriteLine(step++ + " steps ------------ flashses: " + flashes);
    grid.WriteTo();
    Console.WriteLine("");
    Console.WriteLine("Press 'q' to quit, any other key to tick.");
}