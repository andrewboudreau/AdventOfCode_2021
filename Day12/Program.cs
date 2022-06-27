using static Day00.ReadInputs;

var edges = Read(x =>
{
    var parts = x.Split("-");
    return (
        new Cave(parts[0]),
        new Cave(parts[1])
    );
});

var caves = new Graph<Cave>(edges);
caves.WriteTo();
foreach(var cave in caves)
{
    Console.WriteLine(cave.Value.Name);
    foreach(var n in cave.Neighbors)
    {
        Console.WriteLine("  "+n.Value);
    }
}    

public class Cave : IEquatable<Cave>
{
    public Cave(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public int Visited { get; private set; }

    public bool Big => char.IsUpper(Name[0]);

    public bool Small => !Big;

    public bool Visit()
    {
        if (Small && Visited > 0)
        {
            throw new InvalidOperationException($"Cannot visit small '{Name}' cave twice.");
        }

        return ++Visited > 0;
    }

    public bool Start => Name == "start";

    public bool End => Name == "end";

    public bool Equals(Cave? other)
        => other?.Name?.Equals(Name) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as Cave);

    public override int GetHashCode()
        => Name.GetHashCode();

    public override string ToString()
    {
        return Name;
    }
}