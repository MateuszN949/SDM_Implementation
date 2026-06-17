using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;
using System.Text.RegularExpressions;

namespace SportsCompetition.Core.Matches;

public class FootballMatch : IMatch<FootballEvent, FootballScore, Team>
{
    private readonly List<Team> _participants = [];
    public IReadOnlyList<Team> Participants => _participants;

    private readonly List<FootballEvent> _events = [];
    public IReadOnlyList<FootballEvent> Events => _events;

    private IMatchState _state = new CreatingState();
    public IMatchState State => _state;

    private readonly Dictionary<Team, FootballScore> _scores = [];
    public IReadOnlyDictionary<Team, FootballScore> Scores => _scores;

    private Team? _winner;
    public Team? Winner => _winner;

    private const int teamCount = 2;

    public void Start()
    {
        if (_participants.Count != teamCount)
            throw new InvalidOperationException("Football match needs exactly two teams.");

        if (_state is CreatingState)
            _state = new InProgressState();
    }

    public void TimeOver()
    {
        if (_state is InProgressState)
            _state = new TimeOverState();
    }

    public void End()
    {
        if (_state is InProgressState || _state is TimeOverState)
            _state = new CompletedState();
    }

    public bool IsDraw()
    {
        List<FootballScore> values = [.. _scores.Values];

        return values[0].CompareTo(values[1]) == 0 && Winner is null;
    }

    private void Apply(FootballEvent matchEvent)
    {
        matchEvent.Apply(_scores);

        if (matchEvent is IWinningEvent<Team> winningEvent)
        {
            if (winningEvent.Winner is not null)
                _winner = winningEvent.Winner;
        }
    }

    public void AddEvent(FootballEvent matchEvent)
    {
        if (!_state.CanAddEvent(matchEvent))
            throw new InvalidOperationException("Cannot add this event in current state.");

        Apply(matchEvent);

        _events.Add(matchEvent);
    }

    public void CancelEvent(FootballEvent canceledEvent)
    {
        if (!_events.Remove(canceledEvent))
            throw new InvalidOperationException("Cannot cancel an event that has not taken place.");

        RecalculateScores();
    }

    private void RecalculateScores()
    {
        _winner = null;

        foreach (Team team in _scores.Keys.ToList())
            _scores[team] = new FootballScore();

        foreach (FootballEvent footballEvent in _events)
            Apply(footballEvent);
    }

    public void AddParticipant(Team participant)
    {
        if (!_state.CanAddParticipant)
            throw new InvalidOperationException("Cannot add participant in current state.");

        _participants.Add(participant);

        _scores.Add(participant, new FootballScore());
    }
}