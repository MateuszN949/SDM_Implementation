using SportsCompetition.Core.Events;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Factory;

public class FootballMatchFactory : IMatchFactory<FootballMatch, FootballEvent, FootballScore, Team>
{
    public FootballMatch CreateMatch()
    {
        return new FootballMatch();
    }
}