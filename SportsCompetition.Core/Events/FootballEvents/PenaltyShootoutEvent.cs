using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Core.Events;

public class PenaltyShootoutEvent(Team team1, int penaltyScore1, Team team2, int penaltyScore2) : FootballEvent, ITimeOverEvent, IWinningEvent<Team>
{
    private Team? _winner;
    public Team? Winner => _winner;

    public override void Apply(Dictionary<Team, FootballScore> scores)
    {
        if (penaltyScore1 > penaltyScore2)
        {
            _winner = team1;
        }
        else if (penaltyScore2 > penaltyScore1)
        {
            _winner = team2;
        }
    }
}