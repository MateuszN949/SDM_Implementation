using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Strategies.Grouping;

public class SequentialGroupingStrategy<TParty> : IGroupingStrategy<TParty> where TParty : IParty
{
    public IReadOnlyList<IReadOnlyList<TParty>> CreateGroups(IReadOnlyList<TParty> participants, int groupCount)
    {
        List<List<TParty>> groups = [];

        for (int i = 0; i < groupCount; i++)
            groups.Add([]);

        for (int i = 0; i < participants.Count; i++)
            groups[i % groupCount].Add(participants[i]);

        return groups;
    }
}