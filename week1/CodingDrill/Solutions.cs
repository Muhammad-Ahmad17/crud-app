namespace CodingDrill;

/// <summary>REFERENCE ONLY — cover this file. Rewrite from memory after studying.</summary>
public static class Solutions
{
    public static int[] TwoSum(int[] nums, int target)
    {
        var map = new Dictionary<int, int>();
        for (var i = 0; i < nums.Length; i++)
        {
            var need = target - nums[i];
            if (map.TryGetValue(need, out var j))
                return [j, i];
            map[nums[i]] = i;
        }
        throw new InvalidOperationException("No solution");
    }

    public static string ReverseString(string s)
    {
        var chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public static bool IsPalindrome(string s)
    {
        var left = 0;
        var right = s.Length - 1;
        while (left < right)
        {
            while (left < right && !char.IsLetterOrDigit(s[left])) left++;
            while (left < right && !char.IsLetterOrDigit(s[right])) right--;
            if (char.ToLowerInvariant(s[left]) != char.ToLowerInvariant(s[right]))
                return false;
            left++;
            right--;
        }
        return true;
    }

    public static int FindMax(int[] nums)
    {
        if (nums.Length == 0) throw new ArgumentException("Empty");
        var max = nums[0];
        foreach (var n in nums)
            if (n > max) max = n;
        return max;
    }

    public static int RemoveDuplicates(int[] nums)
    {
        if (nums.Length == 0) return 0;
        var write = 1;
        for (var read = 1; read < nums.Length; read++)
        {
            if (nums[read] != nums[write - 1])
                nums[write++] = nums[read];
        }
        return write;
    }

    public static void MoveZeroes(int[] nums)
    {
        var write = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
                nums[write++] = nums[i];
        }
        while (write < nums.Length)
            nums[write++] = 0;
    }

    public static bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length) return false;
        var counts = new int[26];
        for (var i = 0; i < s.Length; i++)
        {
            counts[s[i] - 'a']++;
            counts[t[i] - 'a']--;
        }
        return counts.All(c => c == 0);
    }

    public static int FirstUniqChar(string s)
    {
        var counts = new int[26];
        foreach (var c in s) counts[c - 'a']++;
        for (var i = 0; i < s.Length; i++)
            if (counts[s[i] - 'a'] == 1) return i;
        return -1;
    }

    public static int[] MergeSorted(int[] a, int[] b)
    {
        var result = new int[a.Length + b.Length];
        var i = 0; var j = 0; var k = 0;
        while (i < a.Length && j < b.Length)
            result[k++] = a[i] <= b[j] ? a[i++] : b[j++];
        while (i < a.Length) result[k++] = a[i++];
        while (j < b.Length) result[k++] = b[j++];
        return result;
    }

    public static bool ContainsDuplicate(int[] nums)
    {
        var seen = new HashSet<int>();
        foreach (var n in nums)
            if (!seen.Add(n)) return true;
        return false;
    }

    public static string LongestCommonPrefix(string[] strs)
    {
        if (strs.Length == 0) return "";
        var prefix = strs[0];
        for (var i = 1; i < strs.Length; i++)
        {
            while (!strs[i].StartsWith(prefix))
            {
                prefix = prefix[..^1];
                if (prefix.Length == 0) return "";
            }
        }
        return prefix;
    }

    public static void Rotate(int[] nums, int k)
    {
        if (nums.Length == 0) return;
        k %= nums.Length;
        Reverse(nums, 0, nums.Length - 1);
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, nums.Length - 1);
    }

    private static void Reverse(int[] nums, int l, int r)
    {
        while (l < r)
        {
            (nums[l], nums[r]) = (nums[r], nums[l]);
            l++; r--;
        }
    }

    public static int CountVowels(string s)
    {
        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
        return s.Count(c => vowels.Contains(char.ToLowerInvariant(c)));
    }

    public static int[] Intersection(int[] a, int[] b)
    {
        var setB = new HashSet<int>(b);
        return a.Where(setB.Contains).Distinct().ToArray();
    }

    public static int[] PlusOne(int[] digits)
    {
        for (var i = digits.Length - 1; i >= 0; i--)
        {
            if (digits[i] < 9)
            {
                digits[i]++;
                return digits;
            }
            digits[i] = 0;
        }
        var result = new int[digits.Length + 1];
        result[0] = 1;
        return result;
    }
}
