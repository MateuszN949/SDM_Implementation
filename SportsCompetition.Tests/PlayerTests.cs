using System.Reflection.Metadata;
using SportsCompetition.Core.Parties;

namespace SportsCompetition.Tests;

public class PlayerTests
{
    [Fact]
    public void Player_Should_Store_Full_Name()
    {
        const string name = "unwijnvqqpijn";
        Player player = new(name);
        Assert.Equal(name, player.FullName);
    }

    [Fact]
    public void Players_Should_Have_Unique_Ids()
    {
        Player p1 = new("A");
        Player p2 = new("B");
        Player p3 = new("C");

        Assert.Equal(3, new[] { p1.ID, p2.ID, p3.ID }.Distinct().Count());
    }

    [Fact]
    public void Team_Should_Contain_Provided_Players()
    {
        string[] names = ["a", "b", "c", "a1", "b1", "c1"];
        
        List<Player> players = [];

        foreach (string name in names)
        {
            players.Add(new Player(name));
        }

        Team team = new("abc", players);

        Assert.Equal(players.Count, team.Players.Count);

        foreach (string name in names)
        {
            Assert.Contains(team.Players, p => p.FullName == name);
        }
    }

    [Fact]
    public void Team_Should_Not_Be_Affected_By_Source_List_Modification()
    {
        List<Player> players = [new("A"), new("B")];
        int count = players.Count;

        Team team = new("Team", players);

        players.Add(new Player("C"));

        Assert.Equal(count, team.Players.Count);
    }
}
