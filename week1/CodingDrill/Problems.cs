namespace CodingDrill;

/// <summary>
/// RULE: implement each method yourself from a blank mind.
/// Only peek at Solutions.cs after 20+ minutes of honest effort, then rewrite from scratch.
/// </summary>
public static class Problems
{
    // 1. Two Sum — return indices of two numbers that add to target (assume exactly one solution)
    public static int[] TwoSum(int[] nums, int target)
    {
        throw new NotImplementedException("Implement TwoSum");
    }

    // 2. Reverse a string
    public static string ReverseString(string s)
    {
        throw new NotImplementedException("Implement ReverseString");
    }

    // 3. Check if string is a palindrome (ignore case, ignore non-alphanumeric)
    public static bool IsPalindrome(string s)
    {
        throw new NotImplementedException("Implement IsPalindrome");
    }

    // 4. Find max in array (throw if empty)
    public static int FindMax(int[] nums)
    {
        throw new NotImplementedException("Implement FindMax");
    }

    // 5. Remove duplicates from sorted array — return new length (in-place)
    public static int RemoveDuplicates(int[] nums)
    {
        throw new NotImplementedException("Implement RemoveDuplicates");
    }

    // 6. Move all zeros to end, keep relative order of non-zeros
    public static void MoveZeroes(int[] nums)
    {
        throw new NotImplementedException("Implement MoveZeroes");
    }

    // 7. Valid anagram — same letters different order?
    public static bool IsAnagram(string s, string t)
    {
        throw new NotImplementedException("Implement IsAnagram");
    }

    // 8. First unique character index, or -1
    public static int FirstUniqChar(string s)
    {
        throw new NotImplementedException("Implement FirstUniqChar");
    }

    // 9. Merge two sorted arrays into one sorted array
    public static int[] MergeSorted(int[] a, int[] b)
    {
        throw new NotImplementedException("Implement MergeSorted");
    }

    // 10. Contains duplicate?
    public static bool ContainsDuplicate(int[] nums)
    {
        throw new NotImplementedException("Implement ContainsDuplicate");
    }

    // 11. Longest common prefix of string array
    public static string LongestCommonPrefix(string[] strs)
    {
        throw new NotImplementedException("Implement LongestCommonPrefix");
    }

    // 12. Rotate array right by k steps
    public static void Rotate(int[] nums, int k)
    {
        throw new NotImplementedException("Implement Rotate");
    }

    // 13. Count vowels in a string
    public static int CountVowels(string s)
    {
        throw new NotImplementedException("Implement CountVowels");
    }

    // 14. Intersection of two arrays (unique values)
    public static int[] Intersection(int[] a, int[] b)
    {
        throw new NotImplementedException("Implement Intersection");
    }

    // 15. Plus one — digits array representing a number, add 1
    public static int[] PlusOne(int[] digits)
    {
        throw new NotImplementedException("Implement PlusOne");
    }

    public static void RunProblem(int n)
    {
        Console.WriteLine($"Running reference demo for problem {n} (uses Solutions, not your stubs):");
        switch (n)
        {
            case 1: Console.WriteLine(string.Join(",", Solutions.TwoSum([2, 7, 11, 15], 9))); break;
            case 2: Console.WriteLine(Solutions.ReverseString("hello")); break;
            case 3: Console.WriteLine(Solutions.IsPalindrome("A man, a plan, a canal: Panama")); break;
            case 4: Console.WriteLine(Solutions.FindMax([3, 1, 4, 1, 5])); break;
            case 5:
                var d = new[] { 1, 1, 2 };
                Console.WriteLine(Solutions.RemoveDuplicates(d) + " -> " + string.Join(",", d.Take(2)));
                break;
            case 6:
                var z = new[] { 0, 1, 0, 3, 12 };
                Solutions.MoveZeroes(z);
                Console.WriteLine(string.Join(",", z));
                break;
            case 7: Console.WriteLine(Solutions.IsAnagram("anagram", "nagaram")); break;
            case 8: Console.WriteLine(Solutions.FirstUniqChar("leetcode")); break;
            case 9: Console.WriteLine(string.Join(",", Solutions.MergeSorted([1, 3, 5], [2, 4, 6]))); break;
            case 10: Console.WriteLine(Solutions.ContainsDuplicate([1, 2, 3, 1])); break;
            case 11: Console.WriteLine(Solutions.LongestCommonPrefix(["flower", "flow", "flight"])); break;
            case 12:
                var r = new[] { 1, 2, 3, 4, 5, 6, 7 };
                Solutions.Rotate(r, 3);
                Console.WriteLine(string.Join(",", r));
                break;
            case 13: Console.WriteLine(Solutions.CountVowels("Education")); break;
            case 14: Console.WriteLine(string.Join(",", Solutions.Intersection([1, 2, 2, 1], [2, 2]))); break;
            case 15: Console.WriteLine(string.Join(",", Solutions.PlusOne([9, 9, 9]))); break;
        }
    }

    public static void RunSelfTests()
    {
        Console.WriteLine("Self-test your Implementations by filling Problems stubs, then replace this");
        Console.WriteLine("call to invoke your methods. For now, Solutions pass:");
        Assert(Solutions.TwoSum([2, 7, 11, 15], 9) is [0, 1] or [1, 0], "TwoSum");
        Assert(Solutions.ReverseString("ab") == "ba", "ReverseString");
        Assert(Solutions.IsPalindrome("race a car") == false, "IsPalindrome");
        Assert(Solutions.FindMax([1, 9, 2]) == 9, "FindMax");
        Assert(Solutions.IsAnagram("rat", "car") == false, "IsAnagram");
        Assert(Solutions.ContainsDuplicate([1, 2, 3]) == false, "ContainsDuplicate");
        Console.WriteLine("All reference solution smoke tests passed.");
    }

    private static void Assert(bool condition, string name)
    {
        if (!condition) throw new Exception($"FAIL: {name}");
        Console.WriteLine($"  OK {name}");
    }
}
