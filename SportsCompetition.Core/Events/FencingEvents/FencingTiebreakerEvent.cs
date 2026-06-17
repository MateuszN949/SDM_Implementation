using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Core.Events;

public class FencingTiebreakerEvent(Player priorityWinner, Player? touchScorer = null) : FencingEvent, ITimeOverEvent, IWinningEvent<Player>
{
    private Player? _winner;
    public Player? Winner => _winner;

    public override void Apply(Dictionary<Player, FencingScore> scores)
    {
        _winner = priorityWinner;

        if (touchScorer is not null)
            scores[touchScorer].AddPoint();
    }
}