namespace CodingDrill2;

public static class Solutions
{
    public static bool IsValidParentheses(string s)
    {
        var stack = new Stack<char>();
        foreach (var c in s)
        {
            if (c is '(' or '[' or '{') stack.Push(c);
            else
            {
                if (stack.Count == 0) return false;
                var open = stack.Pop();
                if (c == ')' && open != '(') return false;
                if (c == ']' && open != '[') return false;
                if (c == '}' && open != '{') return false;
            }
        }
        return stack.Count == 0;
    }

    public static int MaxProfit(int[] prices)
    {
        var min = int.MaxValue;
        var best = 0;
        foreach (var p in prices)
        {
            if (p < min) min = p;
            best = Math.Max(best, p - min);
        }
        return best;
    }

    public static int[] TwoSumSorted(int[] numbers, int target)
    {
        var l = 0; var r = numbers.Length - 1;
        while (l < r)
        {
            var sum = numbers[l] + numbers[r];
            if (sum == target) return [l + 1, r + 1];
            if (sum < target) l++; else r--;
        }
        return [];
    }

    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var map = new Dictionary<string, IList<string>>();
        foreach (var s in strs)
        {
            var key = new string(s.OrderBy(c => c).ToArray());
            if (!map.ContainsKey(key)) map[key] = new List<string>();
            map[key].Add(s);
        }
        return map.Values.ToList();
    }

    public static int[] TopKFrequent(int[] nums, int k) =>
        nums.GroupBy(n => n)
            .OrderByDescending(g => g.Count())
            .Take(k)
            .Select(g => g.Key)
            .ToArray();

    public static bool IsSubsequence(string s, string t)
    {
        var i = 0;
        foreach (var c in t)
            if (i < s.Length && c == s[i]) i++;
        return i == s.Length;
    }

    public static string ReverseOnlyLetters(string s)
    {
        var chars = s.ToCharArray();
        var l = 0; var r = chars.Length - 1;
        while (l < r)
        {
            if (!char.IsLetter(chars[l])) { l++; continue; }
            if (!char.IsLetter(chars[r])) { r--; continue; }
            (chars[l], chars[r]) = (chars[r], chars[l]);
            l++; r--;
        }
        return new string(chars);
    }

    public static int MissingNumber(int[] nums)
    {
        var n = nums.Length;
        var expected = n * (n + 1) / 2;
        return expected - nums.Sum();
    }

    public static int SingleNumber(int[] nums)
    {
        var x = 0;
        foreach (var n in nums) x ^= n;
        return x;
    }

    public static int[] Intersect(int[] a, int[] b)
    {
        var counts = a.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        var result = new List<int>();
        foreach (var n in b)
        {
            if (counts.TryGetValue(n, out var c) && c > 0)
            {
                result.Add(n);
                counts[n] = c - 1;
            }
        }
        return result.ToArray();
    }

    public static void SortColors(int[] nums)
    {
        var low = 0; var mid = 0; var high = nums.Length - 1;
        while (mid <= high)
        {
            if (nums[mid] == 0)
            {
                (nums[low], nums[mid]) = (nums[mid], nums[low]);
                low++; mid++;
            }
            else if (nums[mid] == 1) mid++;
            else
            {
                (nums[mid], nums[high]) = (nums[high], nums[mid]);
                high--;
            }
        }
    }

    public static int BinarySearch(int[] nums, int target)
    {
        var l = 0; var r = nums.Length - 1;
        while (l <= r)
        {
            var m = l + (r - l) / 2;
            if (nums[m] == target) return m;
            if (nums[m] < target) l = m + 1; else r = m - 1;
        }
        return -1;
    }

    public static int ClimbStairs(int n)
    {
        if (n <= 2) return n;
        var a = 1; var b = 2;
        for (var i = 3; i <= n; i++)
        {
            var c = a + b;
            a = b; b = c;
        }
        return b;
    }

    public static int MajorityElement(int[] nums)
    {
        var count = 0; var candidate = 0;
        foreach (var n in nums)
        {
            if (count == 0) candidate = n;
            count += n == candidate ? 1 : -1;
        }
        return candidate;
    }

    public static bool IsHappy(int n)
    {
        var seen = new HashSet<int>();
        while (n != 1 && seen.Add(n))
        {
            var sum = 0;
            while (n > 0)
            {
                var d = n % 10;
                sum += d * d;
                n /= 10;
            }
            n = sum;
        }
        return n == 1;
    }

    public static void SmokeTest()
    {
        Assert(IsValidParentheses("()[]{}"), "16");
        Assert(MaxProfit([7, 1, 5, 3, 6, 4]) == 5, "17");
        Assert(TwoSumSorted([2, 7, 11, 15], 9) is [1, 2], "18");
        Assert(IsSubsequence("abc", "ahbgdc"), "21");
        Assert(MissingNumber([3, 0, 1]) == 2, "23");
        Assert(BinarySearch([1, 2, 3, 4, 5], 4) == 3, "27");
        Assert(ClimbStairs(5) == 8, "28");
        Assert(IsHappy(19), "30");
        Console.WriteLine("Week 2 reference smoke tests passed.");
    }

    public static void Demo(int n)
    {
        switch (n)
        {
            case 16: Console.WriteLine(IsValidParentheses("([)]")); break;
            case 17: Console.WriteLine(MaxProfit([7, 1, 5, 3, 6, 4])); break;
            case 18: Console.WriteLine(string.Join(",", TwoSumSorted([2, 7, 11, 15], 9))); break;
            case 19: Console.WriteLine(GroupAnagrams(["eat", "tea", "tan", "ate", "nat", "bat"]).Count); break;
            case 20: Console.WriteLine(string.Join(",", TopKFrequent([1, 1, 1, 2, 2, 3], 2))); break;
            case 21: Console.WriteLine(IsSubsequence("axc", "ahbgdc")); break;
            case 22: Console.WriteLine(ReverseOnlyLetters("a-bC-dEf-ghIj")); break;
            case 23: Console.WriteLine(MissingNumber([9, 6, 4, 2, 3, 5, 7, 0, 1])); break;
            case 24: Console.WriteLine(SingleNumber([4, 1, 2, 1, 2])); break;
            case 25: Console.WriteLine(string.Join(",", Intersect([1, 2, 2, 1], [2, 2]))); break;
            case 26:
                var c = new[] { 2, 0, 2, 1, 1, 0 };
                SortColors(c);
                Console.WriteLine(string.Join(",", c));
                break;
            case 27: Console.WriteLine(BinarySearch([-1, 0, 3, 5, 9, 12], 9)); break;
            case 28: Console.WriteLine(ClimbStairs(4)); break;
            case 29: Console.WriteLine(MajorityElement([2, 2, 1, 1, 1, 2, 2])); break;
            case 30: Console.WriteLine(IsHappy(2)); break;
        }
    }

    private static void Assert(bool ok, string name)
    {
        if (!ok) throw new Exception($"FAIL {name}");
        Console.WriteLine($"  OK {name}");
    }
}
