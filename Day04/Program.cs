using static Day00.ReadInputs;
using Day00;
using System.Collections.ObjectModel;

var reader = Read().GetEnumerator();
reader.MoveNext();
List<int> numbers = reader.Current!.Split(",").Select(int.Parse).ToList();

List<Card> cards = new();
Card card = new();
reader.MoveNext();

while (reader.MoveNext())
{
    if (reader.Current == string.Empty)
    {
        cards.Add(card);
        card = new();
        reader.MoveNext();
    }

    card.AddRow(reader.Current);
}

foreach (var number in numbers)
{
    foreach (var c in cards)
    {
        c.Mark(number);
    }
}

Console.WriteLine("Test");

class Card
{
    private record struct Number(int Value, int Row, int Column, bool Marked);
    readonly Dictionary<int, Number> card = new();
    int row = 0;

    public void AddRow(string input)
    {
        var numbers = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select((value, index) => (int.Parse(value), new Number(int.Parse(value), row, index, false)));

        foreach (var number in numbers)
        {
            card.Add(number.Item1, number.Item2);
        }

        row++;
    }

    public void Mark(int number)
    {
        if (card.ContainsKey(number))
        {
            card[number] = card[number] with { Marked = true };
        }
    }

    public void Render(Action<string> writer)
    {
        foreach (var cell in card)
        {
            if (cell.Value.Marked)
            {
                writer($"{cell.Value.Value}. ");
            }
            else
            {
                writer($"{cell.Value.Value}  ");
            }
        }
    }
}