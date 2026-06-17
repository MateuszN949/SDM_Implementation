using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Strategies.Grouping;

public interface IGroupingStrategy<TParty> where TParty : IParty
{
    IReadOnlyList<IReadOnlyList<TParty>> CreateGroups(IReadOnlyList<TParty> participants, int groupCount);
}