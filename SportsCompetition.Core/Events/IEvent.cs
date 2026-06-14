using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;

namespace SportsCompetition.Core.Events;

public interface IEvent<TScore, TParty>
    where TScore : IScore<TScore>
    where TParty : IParty
{
    void Apply(Dictionary<TParty, TScore> scores);
}