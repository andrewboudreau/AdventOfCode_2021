using static Day00.ReadInputs;

var location = Read(Command.Create)
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

Console.WriteLine("Part1: " + location);
Console.WriteLine("Part1: " + location.X * location.Y);

//location = Read(Command.Create)
//    .Aggregate(
//        seed: (Aim: 0, X: 0, Y: 0),
//        func: (acc, next) =>
//        {
//            return next.Direction switch
//            {
//                "forward" => (acc.X + next.Distance, acc.Y),
//                "down" => (acc.X, acc.Y - next.Distance),
//                "up" => (acc.X, acc.Y + next.Distance),
//                _ => throw new NotImplementedException(),
//            };
//        });

record Command(string Direction, int Distance)
{
    public static Command Create(string? input) =>
        new(
            input?.Split(' ')[0] ?? throw new NullReferenceException(nameof(input)),
            int.Parse(input!.Split(' ')[1]));
}