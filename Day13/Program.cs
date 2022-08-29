using System.Drawing;
using System.Text;

using static Day00.ReadInputs;

var paper = ReadTo<Paper>(input =>
{
    var manual = new Paper();
    foreach (var line in input)
    {
        if (string.IsNullOrEmpty(line))
        {
            continue;
        }
        else if (line.Length < 11)
        {
            manual.AddPoint(line.Split(','));
        }
        else
        {
            var f = line.Split('=');
            manual.Fold(f[0][^1] == 'y', int.Parse(f[1]));
        }
    }

    return manual;
});

Console.Write(paper);

public class Paper
{
    private bool first = true;
    private int xmax;
    private int ymax;

    public Paper()
    {
        Points = new List<Point>();
        Folds = new List<(bool, int)>();
    }

    public List<Point> Points { get; }

    public List<(bool Horizontal, int Offset)> Folds { get; }

    public void AddPoint(string[] coords)
    {
        Points.Add(new Point(int.Parse(coords[0]), int.Parse(coords[1])));
        xmax = Math.Max(int.Parse(coords[0]), xmax);
        ymax = Math.Max(int.Parse(coords[1]), ymax);
    }

    public void Fold(bool horizontal, int offset)
    {
        if (horizontal)
        {
            foreach (var point in Points.Where(p => p.Y > offset).ToList())
            {
                Points.Remove(point);
                Points.Add(point with { Y = offset - (point.Y - offset) });
            }
            ymax = offset;
        }
        else
        {
            foreach (var point in Points.Where(p => p.X > offset).ToList())
            {
                Points.Remove(point);
                Points.Add(point with { X = offset - (point.X - offset) });
            }
            xmax = offset;
        }

        if (first)
        {
            first = false;
            Console.WriteLine("First fold leaves {0} dots.", Points.Distinct().Count());
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendFormat("x:{0} y:{1}", xmax, ymax);
        builder.AppendLine();

        var point = new Point();
        for (var y = 0; y <= ymax; y++)
        {
            if (Folds.Any(f => f == (true, y)))
            {
                builder.Append(new string('-', xmax));
                builder.AppendLine();
                continue;
            }

            for (var x = 0; x <= xmax; x++)
            {
                if (Folds.Any(f => f == (true, x)))
                {
                    builder.Append('|');
                }
                else
                {
                    point.X = x;
                    point.Y = y;
                    builder.Append(Points.Contains(point) ? '#' : '.');
                }
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }
}
