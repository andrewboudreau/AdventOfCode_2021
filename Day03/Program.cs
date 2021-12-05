using static Day00.ReadInputs;
using Day00;

var AsEnumerableInts = (string src)
    => src.Select(ch => ch == '1' ? 1 : 0);

Read(AsEnumerableInts)
    .Aggregate((acc, current) =>
    {
        List<int> next = new();
        using var accEnumerator = acc.GetEnumerator();
        using var currentEnumerator = current.GetEnumerator();
        while (accEnumerator.MoveNext() && currentEnumerator.MoveNext())
        {
            next.Add(accEnumerator.Current + (currentEnumerator.Current == 0 ? -1 : 1));
        }
        return next;
    })
    .Select(x => x >= 0)
    .ToConsole(bits =>
    {
        var gamma = bits.ToInt32();
        var epsilon = bits.Select(x => !x).ToInt32();
        return new[]
        {
            $"gamma: {gamma}",
            $"epsilon: {epsilon}",
            $"part1: {gamma * epsilon}"
        };
    });
