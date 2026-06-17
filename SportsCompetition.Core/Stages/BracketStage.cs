using SportsCompetition.Core.Events;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.Strategies.Pairing;
using SportsCompetition.Core.Factory;

namespace SportsCompetition.Core.Stages;

public class BracketStage<TParty, TMatch, TEvent, TScore> : PlayStage<TParty, TMatch, TEvent, TScore>
where TMatch : IMatch<TEvent, TScore, TParty>
where TEvent : IEvent<TScore, TParty>
where TScore : class, IScore<TScore>
where TParty : IParty
{
    protected override List<TParty> _particpants { get; }

    public override List<TMatch> Matches { get; }

    public BracketStage(
        IReadOnlyList<TParty> participants,
        IPairingStrategy<TParty> pairing,
        IMatchFactory<TMatch, TEvent, TScore, TParty> matchFactory)
    {
        _particpants = [.. participants];

        Matches = [];

        foreach ((TParty p1, TParty p2) in pairing.CreatePairs(participants))
        {
            TMatch match = matchFactory.CreateMatch();

            match.AddParticipant(p1);
            match.AddParticipant(p2);

            Matches.Add(match);
        }
    }

    public override IReadOnlyList<TParty> GetRanking()
    {
        List<TParty> ranking = [];

        foreach (TMatch match in Matches)
            ranking.Add(match.Winner!);

        return ranking;
    }
}