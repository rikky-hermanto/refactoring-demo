using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

namespace DataStrucutreAlgo
{
    public class VotingSystemTests
    {
        [Fact]
        public void BasicSorting_WorksCorrectly()
        {
            var ballots = new List<List<string>>
        {
            new List<string> { "Alice", "Bob", "Charlie" },
            new List<string> { "Bob", "Alice", "Charlie" },
            new List<string> { "Charlie", "Alice", "Bob" }
        };

            var result = VotingSystem.GetResults(ballots, TiedStrategy.FirstToReachWinningPoints);
            Assert.Equal(new List<string> { "Alice", "Bob", "Charlie" }, result);
        }

        [Fact]
        public void Strategy1_FirstToReachWinningPoints()
        {
            var ballots = new List<List<string>>
        {
            new List<string> { "Alice", "Bob", "Charlie" },
            new List<string> { "Bob", "Alice", "Charlie" },
            new List<string> { "Charlie", "Alice", "Bob" },
            new List<string> { "Alice", "Bob", "Charlie" } // Alice reaches max points first
        };

            var result = VotingSystem.GetResults(ballots,  TiedStrategy.FirstToReachWinningPoints);
            Assert.Equal(new List<string> { "Alice", "Bob", "Charlie" }, result);
        }

        [Fact]
        public void Strategy2_MajorityVotesInEarlyPosition()
        {
            var ballots = new List<List<string>>
        {
            new List<string> { "Alice", "Charlie", "Bob" },
            new List<string> { "Bob", "Alice", "Charlie" },
            new List<string> { "Charlie", "Alice", "Bob" },
            new List<string> { "Charlie", "Bob", "Alice" } // Charlie wins due to early 0th index votes
        };

            var result = VotingSystem.GetResults(ballots,  TiedStrategy.MajorityVotesInEarlyPostion);
            Assert.Equal(new List<string> { "Charlie", "Alice", "Bob" }, result);
        }

        [Fact]
        public void Handles_Tie_In_TotalPoints_With_Strategy1()
        {
            var ballots = new List<List<string>>
        {
            new List<string> { "Alice", "Bob" },  // Alice gets 3, Bob gets 2
            new List<string> { "Bob", "Alice" }   // Bob gets 3, Alice gets 2
        };

            var result = VotingSystem.GetResults(ballots,  TiedStrategy.FirstToReachWinningPoints);
            Assert.Equal(new List<string> { "Alice", "Bob" }, result); // Alice reached winning points first
        }

        [Fact]
        public void Handles_Tie_In_TotalPoints_With_Strategy2()
        {
            var ballots = new List<List<string>>
            {
                new List<string> { "Alice", "Bob" },  // Alice gets 3, Bob gets 2
                new List<string> { "Bob", "Alice" }   // Bob gets 3, Alice gets 2
            };

            var result = VotingSystem.GetResults(ballots,  TiedStrategy.MajorityVotesInEarlyPostion);

            // Step-by-step breakdown:
            // - Total points: Alice = 5, Bob = 5 (Tied)
            // - Votes at 0th index: Alice = 1, Bob = 1 (Tied)
            // - Votes at 1st index: Alice = 1, Bob = 1 (Tied)
            // - No further votes -> Keep initial encounter order (Alice first)

            Assert.Equal(new List<string> { "Alice", "Bob" }, result); // Alice should be ranked first
        }


        [Fact]
        public void Handles_Empty_Input()
        {
            var ballots = new List<List<string>>();
            var result = VotingSystem.GetResults(ballots,  TiedStrategy.FirstToReachWinningPoints);
            Assert.Empty(result);
        }

        [Fact]
        public void Handles_Single_Ballot()
        {
            var ballots = new List<List<string>> { new List<string> { "Alice", "Bob", "Charlie" } };
            var result = VotingSystem.GetResults(ballots,  TiedStrategy.FirstToReachWinningPoints);
            Assert.Equal(new List<string> { "Alice", "Bob", "Charlie" }, result);
        }

        [Fact]
        public void Handles_Single_Candidate_Multiple_Votes()
        {
            var ballots = new List<List<string>>
        {
            new List<string> { "Alice" },
            new List<string> { "Alice" },
            new List<string> { "Alice" }
        };

            var result = VotingSystem.GetResults(ballots,  TiedStrategy.FirstToReachWinningPoints);
            Assert.Equal(new List<string> { "Alice" }, result);
        }

        [Fact]
        public void Handles_Tie_With_Three_Candidates()
        {
            var ballots = new List<List<string>>
        {
            new List<string> { "Alice", "Bob", "Charlie" },
            new List<string> { "Bob", "Charlie", "Alice" },
            new List<string> { "Charlie", "Alice", "Bob" }
        };

            var result = VotingSystem.GetResults(ballots,  TiedStrategy.MajorityVotesInEarlyPostion);
            Assert.Equal(new List<string> { "Alice", "Bob", "Charlie" }, result);
        }
    }

    internal class VotingSystem
    {
        internal static IEnumerable<string> GetResults(List<List<string>> ballots, TiedStrategy strategy)
        {
            var rankMap = new Dictionary<string, (int[] votesPosition, int score, int firstApperance)>();

            foreach (var ballot in ballots)
            {
                for(int i = 0; i<ballot.Count; i++)
                {
                    var candidate = ballot[i];

                    if (!rankMap.ContainsKey(candidate))
                    {
                        rankMap[candidate] = (new int[ballot.Count], 0, i);
                    }

                    rankMap[candidate].votesPosition[i]++;
                }
            }
             
            var winners = new List<string>(rankMap.Keys);
            winners.Sort((candidate1, candidate2) => {
                (int[] votesPosition, int score, int firstApperance) candidate1Data = rankMap[candidate1];
                (int[] votesPosition, int score, int firstApperance) candidate2Data = rankMap[candidate2];

                // Compare by score
                int compareScores = candidate1Data.score.CompareTo(candidate2Data.score);
                if (compareScores != 0) 
                    return compareScores;

                if (strategy == TiedStrategy.FirstToReachWinningPoints)
                {
                    //Strategy 1: First to Reach Winning Points
                    //If two candidates(A and B) have the same total points, the winner is the one who reached that point total first. The candidate who reached the points first wins
                    return candidate1Data.firstApperance.CompareTo(candidate2Data.firstApperance);
                }
                else
                {
                    //Strategy 2: Majority Votes in Early Positions
                    //If two candidates have the same total points, the winner is determined by the number of times they appear at each position in the ballots:
                    //The candidate who appears most frequently at index 0(first choice) wins.
                    //If there is still a tie, the candidate who appears most at index 1 wins.
                    //If there is still a tie, the candidate who appears most at index 2 wins.
                    for (int i = 0; i < candidate1Data.votesPosition.Length; i++)
                    {
                        if (candidate1Data.votesPosition[i] != candidate2Data.votesPosition[i])
                            return candidate1Data.votesPosition[i] > candidate2Data.votesPosition[i] ? -1 : 1; //min-heap
                    }
                }

                // Default to alphabetical order if everything else is tied
                return candidate1.CompareTo(candidate2);
            });

            return winners;
        }
    }

    enum TiedStrategy
    {
        FirstToReachWinningPoints = 1,
        MajorityVotesInEarlyPostion = 2
    }
}
