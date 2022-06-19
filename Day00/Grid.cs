using System.Collections;

public class Node<T>
{
    public Node(int x, int y, T value)
    {
        X = x;
        Y = y;
        Value = value;
    }

    public int X { get; init; }
    public int Y { get; init; }
    public T Value { get; private set; }

    public T SetValue(T value)
        => Value = value;
    public T SetValue(Func<T, T> setter)
        => Value = setter(Value);

    public static implicit operator T(Node<T> node)
    {
        return node.Value;
    }

    public void Deconstruct(out int x, out int y, out T value)
    {
        x = X;
        y = Y;
        value = Value;
    }

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}

public class Grid<T> : IEnumerable<Node<T>>
{
    private readonly List<Node<T>> nodes;
    private readonly int width;

    public Grid(IEnumerable<string> rows, Func<string, IEnumerable<T>> factory)
       : this(rows.Select(factory))
    {
    }

    public Grid(IEnumerable<IEnumerable<T>> map)
    {
        nodes = new List<Node<T>>();
        int x = 0;
        int y = 0;

        foreach (var row in map)
        {
            foreach (var node in row)
            {
                nodes.Add(new Node<T>(x, y++, node));
            }

            if (width == 0)
            {
                width = y;
            }
            x++;
            y = 0;
        }
    }

    public Node<T>? this[int x, int y]
    {
        get
        {
            if (x < 0) return default;
            if (x >= width) return default;
            if (y < 0) return default;
            if (y >= width) return default;

            int offset = x * width + y;
            if (offset < 0 || offset >= nodes.Count) return default;
            return new Node<T>(x, y, nodes[offset]);
        }
    }

    public IEnumerable<Node<T>> Neighbors(Node<T> position, bool withDiagonals = true)
    {
        if (withDiagonals && this[position.X - 1, position.Y + 1] is Node<T> upLeft)
        {
            yield return upLeft;
        }

        if (this[position.X, position.Y + 1] is Node<T> up)
        {
            yield return up; ;
        }

        if (withDiagonals && this[position.X + 1, position.Y + 1] is Node<T> upRight)
        {
            yield return upRight;
        }

        if (this[position.X - 1, position.Y] is Node<T> left)
        {
            yield return left;
        }

        if (this[position.X + 1, position.Y] is Node<T> right)
        {
            yield return right;
        }

        if (withDiagonals && this[position.X - 1, position.Y - 1] is Node<T> downLeft)
        {
            yield return downLeft;
        }

        if (this[position.X, position.Y - 1] is Node<T> down)
        {
            yield return down;
        }

        if (withDiagonals && this[position.X + 1, position.Y - 1] is Node<T> downRight)
        {
            yield return downRight;
        }
    }

    public IEnumerable<Node<T>> Nodes()
    {
        for (var offset = 0; offset < nodes.Count; offset++)
        {
            yield return nodes[offset];
        }
    }

    public Grid<T> WhileTrue(Func<Grid<T>, bool> operation)
    {
        while (operation(this)) ;
        return this;
    }

    public Grid<T> Each(Action<Node<T>> action)
    {
        foreach (var node in Nodes())
        {
            action(node);
        }

        return this;
    }

    public IEnumerable<IEnumerable<Node<T>>> Rows()
    {
        for (var row = 0; row < nodes.Count / width; row++)
        {
            yield return nodes.Skip(row * width).Take(width);
        }
    }

    public Grid<T> Render(int x = 0, int y = 0, Action<string>? draw = default)
    {
        draw ??= Console.WriteLine;
        foreach (var row in Rows())
        {
            Console.SetCursorPosition(x, y++);
            draw(string.Join("", row.Select(x => x.Value)));
        }

        return this;
    }

    public Grid<T> WriteTo(Action<string>? draw = default)
    {
        draw ??= Console.WriteLine;
        foreach (var row in Rows())
        {
            draw(string.Join("", row));
        }

        return this;
    }

    public IEnumerator<Node<T>> GetEnumerator()
        => Nodes().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}