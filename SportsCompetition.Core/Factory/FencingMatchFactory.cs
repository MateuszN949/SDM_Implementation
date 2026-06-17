using SportsCompetition.Core.Events;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Factory;

public class FencingMatchFactory : IMatchFactory<FencingMatch, FencingEvent, FencingScore, Player>
{
    public FencingMatch CreateMatch()
    {
        return new FencingMatch();
    }
}