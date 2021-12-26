using static Day00.ReadInputs;
using Day00;

var AsListInts = (string src)
    => src.Select(ch => ch == '1' ? 1 : 0).ToList();

var report = Read(AsListInts).ToList();

report
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

report
    .Aggregate(
        seed: report,
        func: (acc, current) =>
        {
            List<int> next = new();
            using var accEnumerator = acc.GetEnumerator();
            while (accEnumerator.MoveNext())
            {
                //next.Add(accEnumerator.Current + (currentEnumerator.Current == 0 ? -1 : 1));
            }
            return acc;
        });
