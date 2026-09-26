using CodingDrill;

Console.WriteLine("Week 1 Coding Drill — run: dotnet run -- <problemNumber>");
Console.WriteLine("Example: dotnet run -- 1");
Console.WriteLine("Problems 1-15 are arrays/strings. Implement stubs in Problems.cs yourself first.\n");

if (args.Length == 0)
{
    Problems.RunSelfTests();
    return;
}

if (!int.TryParse(args[0], out var n) || n is < 1 or > 15)
{
    Console.WriteLine("Pass a problem number 1-15.");
    return;
}

Problems.RunProblem(n);
