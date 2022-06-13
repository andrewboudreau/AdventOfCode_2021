using static Day00.ReadInputs;
using Day00;

var positions = Read()!.Split(',').ToList();
var max = positions.Max();

Console.WriteLine($"There are {positions.Count}");
Console.WriteLine($"max: {max}");
var costs1 = new List<int>(max);
var costs2 = new List<int>(max);

for (var current = 0; current <= max; current++)
{
    var cost1 = 0;
    var cost2 = 0;
    foreach (var position in positions)
    {
        var n = Math.Abs(position - current);
        cost1 += n;
        cost2 += (n * (n + 1)) / 2;
    }
    costs1.Add(cost1);
    costs2.Add(cost2);
}

Console.WriteLine($"lowest cost is {costs1.Min()} at {costs1.IndexOf(costs1.Min())}");
Console.WriteLine($"lowest cost is {costs2.Min()} at {costs2.IndexOf(costs2.Min())}");