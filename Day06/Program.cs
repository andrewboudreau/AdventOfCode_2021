using static Day00.ReadInputs;
using Day00;
using System.Text;

var school = new Population(Read()!.Split(','));

Console.WriteLine(school);
for (var i = 1; i < 257; i++)
{
    school.Tick();
    //Console.WriteLine($"Day {i} has {school.Sum()}: {school}");
    Console.WriteLine($"Day {i} has {school.Sum()}");
}

public class Population
{
    private readonly long[] ages;

    public Population(IEnumerable<int> fishes)
    {
        ages = new long[9];
        foreach (var fish in fishes)
        {
            ages[fish] += 1;
        }
    }

    public void Tick()
    {
        var spawners = ages[0];
        for (var i = 1; i < ages.Length; i++)
        {
            ages[i - 1] = ages[i];
        }

        ages[6] += spawners;
        ages[8] = spawners;
    }

    public long Sum() => ages.Sum();

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < 9; i++)
        {
            if (ages[i] > 0)
            {
                foreach (var x in Enumerable.Range(0, (int)ages[i]))
                {
                    sb.Append(i);
                    sb.Append(' ');
                }
            }
        }

        return sb.ToString();
    }
}

