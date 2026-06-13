using System.Collections.Immutable;

namespace SportsCompetition.Core.Parties;

public class Team(string name, IEnumerable<Player> members) : Party
{
    public string Name { get; } = name;

    private readonly ImmutableList<Player> _players = [.. members];
    public IReadOnlyList<Player> Players => _players;
}