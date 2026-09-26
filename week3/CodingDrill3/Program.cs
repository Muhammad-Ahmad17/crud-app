using CodingDrill3;

Console.WriteLine("Week 3 Coding Drill (31-40). Implement Problems stubs yourself.\n");
if (args.Length == 0) { Solutions.SmokeTest(); return; }
if (!int.TryParse(args[0], out var n) || n is < 31 or > 40) { Console.WriteLine("31-40"); return; }
Solutions.Demo(n);
