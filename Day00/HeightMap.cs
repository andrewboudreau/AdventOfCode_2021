using System.Linq.Expressions;

public class HeightMap
{
    private readonly List<int> heights;
    private readonly int width;

    public HeightMap(IEnumerable<string> rows)
       : this(rows.Select(row => row.Select(c => (int)char.GetNumericValue(c))))
    {
    }

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

    public IEnumerable<((int X, int Y) Position, int Height)> Neighbors((int X, int Y) position, bool withDiagonals = false)
    {
        if (withDiagonals)
        {
            var upLeft = this[position.X - 1, position.Y + 1];
            if (upLeft is not null)
            {
                yield return ((position.X - 1, position.Y + 1), upLeft.Value);
            }
        }

        var up = this[position.X, position.Y + 1];
        if (up is not null)
        {
            yield return ((position.X, position.Y + 1), up.Value);
        }

        if (withDiagonals)
        {
            var upRight = this[position.X + 1, position.Y + 1];
            if (upRight is not null)
            {
                yield return ((position.X + 1, position.Y + 1), upRight.Value);
            }
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

        if (withDiagonals)
        {
            var downLeft = this[position.X - 1, position.Y + 1];
            if (downLeft is not null)
            {
                yield return ((position.X - 1, position.Y + 1), downLeft.Value);
            }
        }

        var down = this[position.X, position.Y - 1];
        if (down is not null)
        {
            yield return ((position.X, position.Y - 1), down.Value);
        }

        if (withDiagonals)
        {
            var downRight = this[position.X + 1, position.Y + 1];
            if (downRight is not null)
            {
                yield return ((position.X + 1, position.Y + 1), downRight.Value);
            }
        }
    }

    public IEnumerable<((int X, int Y) Position, int Height)> Positions()
    {
        for (var offset = 0; offset < heights.Count; offset++)
        {
            yield return ((offset / width, offset % width), heights[offset]);
        }
    }

    //public  Where(Expression<Func<((int X, int Y) Position, int Height), bool>> filter)
    //{
    //    var del = filter.Compile();
    //    foreach(var node in Positions())
    //    {

    //    }
        
    //}

    public IEnumerable<IEnumerable<int>> Rows()
    {
        for (var row = 0; row < heights.Count / width; row++)
        {
            yield return heights.Skip(row * width).Take(width);
        }
    }

    public void Render(int x = 0, int y = 0, Action<string>? draw = default)
    {
        draw ??= Console.WriteLine;
        foreach (var row in Rows())
        {
            Console.SetCursorPosition(x, y++);
            draw(string.Join("", row));
        }
    }

    public void Write(Action<string>? draw = default)
    {
        draw ??= Console.WriteLine;
        foreach (var row in Rows())
        {
            draw(string.Join("", row));
        }
    }
}