namespace CodingDrill3;

public static class Solutions
{
    public static int[] RunningSum(int[] nums)
    {
        for (var i = 1; i < nums.Length; i++) nums[i] += nums[i - 1];
        return nums;
    }

    public static int[] Shuffle(int[] nums, int n)
    {
        var result = new int[2 * n];
        for (var i = 0; i < n; i++)
        {
            result[2 * i] = nums[i];
            result[2 * i + 1] = nums[n + i];
        }
        return result;
    }

    public static int NumIdenticalPairs(int[] nums)
    {
        var map = new Dictionary<int, int>();
        var pairs = 0;
        foreach (var n in nums)
        {
            if (map.TryGetValue(n, out var c)) { pairs += c; map[n] = c + 1; }
            else map[n] = 1;
        }
        return pairs;
    }

    public static string DefangIPaddr(string address) => address.Replace(".", "[.]");

    public static int[] SmallerNumbersThanCurrent(int[] nums) =>
        nums.Select(n => nums.Count(x => x < n)).ToArray();

    public static int[] DecompressRLElist(int[] nums)
    {
        var list = new List<int>();
        for (var i = 0; i < nums.Length; i += 2)
            for (var j = 0; j < nums[i]; j++)
                list.Add(nums[i + 1]);
        return list.ToArray();
    }

    public static int[] CreateTargetArray(int[] nums, int[] index)
    {
        var list = new List<int>();
        for (var i = 0; i < nums.Length; i++)
            list.Insert(index[i], nums[i]);
        return list.ToArray();
    }

    public static bool CheckIfPangram(string sentence) =>
        sentence.ToLowerInvariant().Where(char.IsLetter).Distinct().Count() == 26;

    public static int CountMatches(IList<IList<string>> items, string ruleKey, string ruleValue)
    {
        var idx = ruleKey switch { "type" => 0, "color" => 1, _ => 2 };
        return items.Count(it => it[idx] == ruleValue);
    }

    public static string Interpret(string command) =>
        command.Replace("()", "o").Replace("(al)", "al");

    public static void SmokeTest()
    {
        Assert(RunningSum([1, 2, 3, 4]) is [1, 3, 6, 10], "31");
        Assert(NumIdenticalPairs([1, 2, 3, 1, 1, 3]) == 4, "33");
        Assert(DefangIPaddr("1.1.1.1") == "1[.]1[.]1[.]1", "34");
        Assert(CheckIfPangram("thequickbrownfoxjumpsoverthelazydog"), "38");
        Assert(Interpret("G()(al)") == "Goal", "40");
        Console.WriteLine("Week 3 reference smoke tests passed.");
    }

    public static void Demo(int n)
    {
        switch (n)
        {
            case 31: Console.WriteLine(string.Join(",", RunningSum([1, 1, 1, 1, 1]))); break;
            case 32: Console.WriteLine(string.Join(",", Shuffle([2, 5, 1, 3, 4, 7], 3))); break;
            case 33: Console.WriteLine(NumIdenticalPairs([1, 1, 1, 1])); break;
            case 34: Console.WriteLine(DefangIPaddr("255.100.50.0")); break;
            case 35: Console.WriteLine(string.Join(",", SmallerNumbersThanCurrent([8, 1, 2, 2, 3]))); break;
            case 36: Console.WriteLine(string.Join(",", DecompressRLElist([1, 2, 3, 4]))); break;
            case 37: Console.WriteLine(string.Join(",", CreateTargetArray([0, 1, 2, 3, 4], [0, 1, 2, 2, 1]))); break;
            case 38: Console.WriteLine(CheckIfPangram("leetcode")); break;
            case 39:
                IList<IList<string>> items = [["phone", "blue", "pixel"], ["computer", "silver", "lenovo"]];
                Console.WriteLine(CountMatches(items, "color", "silver"));
                break;
            case 40: Console.WriteLine(Interpret("G()()()()(al)")); break;
        }
    }

    private static void Assert(bool ok, string name)
    {
        if (!ok) throw new Exception($"FAIL {name}");
        Console.WriteLine($"  OK {name}");
    }
}
