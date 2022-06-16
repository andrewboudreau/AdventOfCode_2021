using static Day00.ReadInputs;

var lines = Read(x => x)
    ?? throw new NullReferenceException("string values are required");


List<string> incompletes = new();
int points = 0;
int row = 0;

foreach (var line in lines)
{
    row++;
    int column = 0;

    var stack = new Stack<char>(line.Length);

    foreach (var operation in line)
    {
        column++;
        if (operation == '(' || operation == '{' || operation == '[' || operation == '<')
        {
            stack.Push(operation);
            continue;
        }

        var start = stack.Pop();
        if ((start == '(' && operation == ')') ||
            (start == '{' && operation == '}') ||
            (start == '[' && operation == ']') ||
            (start == '<' && operation == '>'))
            continue;

        Console.WriteLine("Got {4} {3} broken line at row {0}, column {1}: {2}", row, column, line, operation, start);

        points += (operation) switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => throw new NotSupportedException($"'{operation}' is an invalid bracket character")
        };

        stack.Clear();
        break;
    }

    if (stack.Any())
    {
        var add = new string(stack.Select(x => x).ToArray());
        incompletes.Add(add);

        Console.WriteLine($"adding incomplete for row {row}: {add}");
    }
}

Console.WriteLine($"Points: {points}");

var scores = new List<long>();
foreach (var line in incompletes)
{
    Console.WriteLine("Handling incmplete {0}", line);
    scores.Add(
        line.Select(op =>
        {
            if (op == '(') return 1;
            else if (op == '[') return 2;
            else if (op == '{') return 3;
            else if (op == '<') return 4;
            else throw new NotSupportedException($"'{op}' is an invalid bracket character");
        })
        .Aggregate<int, long>(0, (total, next) => (total * 5) + next));
}

Console.WriteLine(string.Join(", ", scores.OrderBy(x => x)));
Console.WriteLine(scores.OrderBy(x => x).Skip(scores.Count() / 2).First());

