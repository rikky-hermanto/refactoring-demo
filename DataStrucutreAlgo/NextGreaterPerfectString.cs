namespace DataStrucutreAlgo;

public class NextGreaterPerfectStringHandler
{
    public static string NextGreaterPerfectString(String s) {
        //Greedy!
        //Prune: Duplicate adjacent
        var chars = s.ToCharArray();
        for (int i = 0; i < chars.Length; i++) {
            if (i + 1 < chars.Length && chars[i + i] == chars[i]) {
                //try bump, if bumpable (less z)
            }
        }

        return new string(chars);
    }

}

public class NextPerfectStringTests
{
    [Fact]
    public void Test1_BasicCase()
    {
        string input = "abzzzcd";
        string expected = "acababa";
        Assert.Equal(expected, NextGreaterPerfectStringHandler.NextGreaterPerfectString(input));
    }

    [Fact]
    public void Test2_NoPossibleAnswer()
    {
        string input = "zzab";
        string expected = "-1";
        Assert.Equal(expected, NextGreaterPerfectStringHandler.NextGreaterPerfectString(input));
    }

    [Fact]
    public void Test3_AlreadyPerfectButNeedsBump()
    {
        string input = "abcabc";
        string expected = "abcabd";
        Assert.Equal(expected, NextGreaterPerfectStringHandler.NextGreaterPerfectString(input));
    }
//ababab
    [Fact]
    public void Test4_PerfectWithLongSameSuffix()
    {
        string input = "aabaaa";
        string expected = "ababab";
        Assert.Equal(expected, NextGreaterPerfectStringHandler.NextGreaterPerfectString(input));
    }

    [Fact]
    public void Test5_SingleCharacter()
    {
        string input = "z";
        string expected = "-1";
        Assert.Equal(expected, NextGreaterPerfectStringHandler.NextGreaterPerfectString(input));
    }
}