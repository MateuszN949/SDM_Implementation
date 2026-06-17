using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Core.Events;

public class TouchEvent(Player party) : FencingEvent, IInProgressEvent
{
    private readonly Player _party = party;

    public override void Apply(Dictionary<Player, FencingScore> scores)
    {
        scores[_party].AddPoint();
    }
}