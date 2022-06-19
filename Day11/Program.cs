using static Day00.ReadInputs;

static int AddOne(int value) => value + 1;

var map = new Grid<int>(ReadAsRowsOfInts());

void Tick()
{
    map.Each(n => n.SetValue(AddOne));

    var charged = map.Where(n => n.Value > 9);
    while (charged.Any())
    {
        charged = map.Where(n => n.Value > 9);
    }

map.WhileTrue(nodes =>
    {
        nodes
            .Each(n => n.SetValue(n.Value + 1))
            .Each(n =>
            {
                bool again;
                do
                {
                    again = false;
                    if (n.Value > 9)
                    {
                        var flashed =
                            map.Neighbors(n)
                            .Select(x => x.SetValue(x.Value + 1));


                        return;

                            ).ToList();

                n.SetValue(0);
            };
    } while (again) ;
});


Console.WriteLine("Press 'q' to quit, any other key to tick.");
while (Console.ReadKey().KeyChar != 'q')
{
    Tick();
    map.Render();
}