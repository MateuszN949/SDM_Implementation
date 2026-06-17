using SportsCompetition.Core.Events;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Factory;

public interface IMatchFactory<TMatch, TEvent, TScore, TParty> where TMatch : IMatch<TEvent, TScore, TParty>
where TEvent : IEvent<TScore, TParty>
where TScore : class, IScore<TScore>
where TParty : IParty
{
    TMatch CreateMatch();
}