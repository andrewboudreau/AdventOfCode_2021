using System.Collections.Immutable;
using System.Numerics;
using System.Text;

using static Day00.ReadInputs;

var polymerizer = ReadTo<Polymerizer>(input =>
{
    var poly = new Polymerizer();
    foreach (var line in input)
    {
        if (string.IsNullOrEmpty(line))
        {
            continue;
        }
        else if (line.Contains("->"))
        {
            poly.AddRule(line.Split(" -> "));
        }
        else
        {
            poly.AddTemplate(line);
        }
    }

    return poly;
});

//Console.Write(polymerizer);
Console.WriteLine(polymerizer.Step(10).Solution());

public class Polymerizer
{
    private string template;
    private Dictionary<string, char> rules;

    public Polymerizer()
    {
        template = string.Empty;
        rules = new Dictionary<string, char>();
    }

    public void AddTemplate(string template)
    {
        this.template = template;
    }

    public void AddRule(string[] rule)
    {
        rules.Add(rule[0], rule[1].First());
    }

    public Polymerizer Step(int steps = 1)
    {
        var buffer = new StringBuilder();
        for (var step = 0; step < steps; step++)
        {
            Console.WriteLine($"step: {step}");
            for (var i = 0; i < template.Length - 1; i++)
            {
                buffer.Append(template[i]);
                buffer.Append(rules[template[i..(i + 2)]]);
            }

            buffer.Append(template[^1]);
            template = buffer.ToString();
            buffer.Clear();
        }

        return this;
    }

    public BigInteger Solution()
    {
        var counts = new Dictionary<char, BigInteger>();
        foreach (var chr in template)
        {
            if (!counts.ContainsKey(chr))
            {
                counts.Add(chr, 0);
            }

            counts[chr] += 1;
        }

        var sorts = counts.Values.ToImmutableSortedSet();
        return sorts.Last() - sorts.First();
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine(template);
        foreach (var (Pair, Insert) in rules)
        {
            builder.AppendLine($"{Pair} -> {Insert}");
        }
        return builder.ToString();
    }
}
