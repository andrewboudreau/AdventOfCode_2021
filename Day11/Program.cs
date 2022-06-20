using static Day00.ReadInputs;

static int AddOne(int value) => value + 1;
static int ToZero(int value) => 0;

var map = new Grid<int>(ReadAsRowsOfInts());

HashSet<(int, int)> flashed = new();
List<Node<int>> flashing;

int Tick()
{
    map.Each(n => n.SetValue(AddOne));
    while ((flashing = map.Where(n => n.Value > 9 && !flashed.Contains((n.X, n.Y))).ToList()).Any())
    {
        foreach (var flash in flashing)
        {
            if (flashed.Add((flash.X, flash.Y)))
            {
                foreach (var neighbor in map.Neighbors(flash))
                {
                    neighbor.SetValue(AddOne);
                }

                flash.SetValue(ToZero);
            }
        }
    }

    foreach (var node in flashed)
    {
        map[node.Item1, node.Item2].SetValue(ToZero);
    }

    var count = flashed.Count;
    flashed.Clear();
    return count;
}

while (Console.ReadKey().KeyChar != 'q')
{
    Console.WriteLine("");
    map.WriteTo();
    var flashes = Tick();
    Console.WriteLine("------------ " + flashes);
    map.WriteTo();
    Console.WriteLine("");
    Console.WriteLine("Press 'q' to quit, any other key to tick.");
}