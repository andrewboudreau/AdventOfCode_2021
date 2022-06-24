using static Day00.ReadInputs;

var grid = new List<Node<Cave>>();
var f = Read(x =>
{
    var parts = x.Split("-");
    grid.Add(new Node<Cave>(1, 1, new Cave(parts[0])));
    grid.Add(new Node<Cave>(1, 1, new Cave(parts[1])));
});


public class Cave
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
}