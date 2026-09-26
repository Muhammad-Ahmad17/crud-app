using CodingDrill2;

Console.WriteLine("Week 2 Coding Drill (16-30). Implement Problems stubs yourself.");
Console.WriteLine("dotnet run --project week2/CodingDrill2 -- 16\n");

if (args.Length == 0)
{
    Solutions.SmokeTest();
    return;
}

if (!int.TryParse(args[0], out var n) || n is < 16 or > 30)
{
    Console.WriteLine("Pass problem number 16-30.");
    return;
}

Solutions.Demo(n);
