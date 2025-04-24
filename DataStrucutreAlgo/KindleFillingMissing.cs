namespace DataStrucutreAlgo;

public class KindleFillingMissing
{
    public static int Handle(string s) {
        int n = s.Length;
        int count = 0;

        //prefix sum, suffixSum
        int[][] s1 = new int[s.Length][]; //s1[i][0]: (, s1[i][1]= )
        int[][] s2 = new int[s.Length][]; //s1[i][0]: (, s1[i][1]= )

        //count every index s1[i][5] and s2[i][5], 5 is total characters per each split by index
        for (int i = 0; i < s.Length; i++) {
            s1[i] = new int[5];
            s2[^(i+1)] = new int[5];

            //copy prefix sum
            if (i > 0) {
                for (int j = 0; j < 5; j++) {
                    s1[i][j] = s1[i - 1][j];
                    s2[^(i+1)][j] = s2[^i][j];
                }
            }

            switch (s[i]) {
                case '(':
                    s1[i][0]++;
                    s2[^(i+1)][0]++;
                    break;
                case ')':
                    s1[i][1]++;
                    s2[^(i+1)][1]++;
                    break;
                case '[':
                    s1[i][2]++;
                    s2[^(i+1)][2]++;
                    break;
                case ']':
                    s1[i][3]++;
                    s2[^(i+1)][3]++;
                    break;
                case '?':
                    s1[i][4]++;
                    s2[^(i+1)][4]++;
                    break;
            }
        }

        //evaluate each subs: s1 and s2
        for (int i = 1; i < s.Length-1; i++) {
            //Console.WriteLine(s1[i-1]);
            //do comparison, if total selisih antara pasangan > dari wildcard = invalid
            if (Math.Abs(s1[i - 1][0] - s1[i - 1][1]) + Math.Abs(s1[i - 1][2] - s1[i - 1][3]) > s1[i-1][4])
                continue;

            if (Math.Abs(s2[i][0] - s2[i][1]) + Math.Abs(s2[i][2] - s2[i][3]) <= s2[i][4])
                count++;
        }

        return count;
    }
}

public class GetBalancedSplitCountTests
{
    [Fact]
    public void TestSample0() {
        string s = "(?][";
        int expected = 1;
        Assert.Equal(expected, KindleFillingMissing.Handle(s));
    }

    [Fact]
    public void TestSample1() {
        string s = "(((?";
        int expected = 0;
        Assert.Equal(expected, KindleFillingMissing.Handle(s));
    }

    [Fact]
    public void TestTwoValidSplits() {
        string s = "(?)[]";
        int expected = 2; // split after index 2 and 3
        Assert.Equal(expected, KindleFillingMissing.Handle(s));
    }

    [Fact]
    public void TestLongBalanced() {
        string s = "([?])([?])";
        int expected = 3;
        Assert.Equal(expected, KindleFillingMissing.Handle(s));
    }
}