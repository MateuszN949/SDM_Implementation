using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.Strategies.Grouping;
using SportsCompetition.Core.Strategies.Pairing;
using SportsCompetition.Core.Factory;

namespace SportsCompetition.Core.Stages;

public class GroupStage<TParty, TMatch, TEvent, TScore> : PlayStage<TParty, TMatch, TEvent, TScore>
where TMatch : IMatch<TEvent, TScore, TParty>
where TEvent : IEvent<TScore, TParty>
where TScore : class, IScore<TScore>, new()
where TParty : IParty
{
    protected override List<TParty> _particpants { get; }

    public override List<TMatch> Matches { get; }

    private readonly List<Group> _groups = [];

    private readonly int _advancingPerGroup;

    public GroupStage(
        IReadOnlyList<TParty> participants,
        IGroupingStrategy<TParty> grouping,
        IPairingStrategy<TParty> pairing,
        IMatchFactory<TMatch, TEvent, TScore, TParty> matchFactory,
        int groupCount,
        int advancingPerGroup)
    {
        _particpants = [.. participants];
        _advancingPerGroup = advancingPerGroup;
        _groups = [];

        foreach (var participantGroup in grouping.CreateGroups(participants, groupCount))
        {
            List<TMatch> matches = [];

            foreach ((TParty p1, TParty p2) in pairing.CreatePairs(participantGroup))
            {
                TMatch match = matchFactory.CreateMatch();

                match.AddParticipant(p1);
                match.AddParticipant(p2);

                matches.Add(match);
            }

            _groups.Add(new Group(participantGroup, matches));
        }

        Matches = [.. _groups.SelectMany(g => g.Matches)];
    }

    public override IReadOnlyList<TParty> GetRanking()
    {
        List<TParty> ranking = [];

        foreach (var group in _groups)
            ranking.AddRange(GetGroupRanking(group).Take(_advancingPerGroup));

        return ranking;
    }

    private static IReadOnlyList<TParty> GetGroupRanking(Group group)
    {
        Dictionary<TParty, TScore> scores = [];

        foreach (TParty participant in group.Participants)
            scores[participant] = new();

        foreach (TMatch match in group.Matches)
        {
            foreach (var pair in match.Scores)
                scores[pair.Key] = scores[pair.Key].Aggregate(pair.Value);
        }

        return [.. scores.OrderByDescending(x => x.Value).Select(x => x.Key)];
    }

    private class Group(IEnumerable<TParty> participants, IEnumerable<TMatch> matches)
    {
        private readonly List<TParty> _participants = [.. participants];
        public IReadOnlyList<TParty> Participants => _participants;

        private readonly List<TMatch> _matches = [.. matches];
        public IReadOnlyList<TMatch> Matches => _matches;
    }
}