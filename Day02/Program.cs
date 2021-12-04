using static Day00.ReadInputs;

var (X, Y) = Read(Command.Create)
    .Aggregate(
        seed: (X: 0, Y: 0),
        func: (acc, next) =>
        {
            return next.Direction switch
            {
                "forward" => (acc.X + next.Distance, acc.Y),
                "down" => (acc.X, acc.Y - next.Distance),
                "up" => (acc.X, acc.Y + next.Distance),
                _ => throw new NotImplementedException(),
            };
        });

Console.WriteLine($"Part 1: {X * Y}");

(int Aim, int X, int Y) part2 = Read(Command.Create)
    .Aggregate(
        seed: (Aim: 0, X: 0, Y: 0),
        func: (acc, next) =>
        {
            return next.Direction switch
            {
                "forward" => (acc.Aim, acc.X + next.Distance, acc.Y + next.Distance * acc.Aim),
                "down" => (acc.Aim + next.Distance, acc.X, acc.Y),
                "up" => (acc.Aim - next.Distance, acc.X, acc.Y),
                _ => throw new NotImplementedException(),
            };
        });

Console.WriteLine($"Part2: {part2.X * part2.Y}");

record Command(string Direction, int Distance)
{
    public static Command Create(string? input) =>
        new(
            input?.Split(' ')[0] ?? throw new NullReferenceException(nameof(input)),
            int.Parse(input!.Split(' ')[1]));
}