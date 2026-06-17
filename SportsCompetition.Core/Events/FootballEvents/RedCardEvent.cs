using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Core.Events;

public class RedCardEvent(Team party) : FootballEvent, IInProgressEvent, ITimeOverEvent
{
    private readonly Team _party = party;

    public override void Apply(Dictionary<Team, FootballScore> scores)
    {
        scores[_party].AddRedCard();
    }
}