using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Core.Events;

public class CoinTossEvent(Team party) : FootballEvent, ITimeOverEvent, IWinningEvent<Team>
{
    private Team? _winner;
    public Team? Winner => _winner;

    public override void Apply(Dictionary<Team, FootballScore> scores)
    {
        _winner = party;
    }
}