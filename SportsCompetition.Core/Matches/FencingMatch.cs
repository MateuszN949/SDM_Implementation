using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;
using System.Text.RegularExpressions;

namespace SportsCompetition.Core.Matches;

public class FencingMatch : IMatch<FencingEvent, FencingScore, Player>
{
    private readonly List<Player> _participants = [];
    public IReadOnlyList<Player> Participants => _participants;

    private readonly List<FencingEvent> _events = [];
    public IReadOnlyList<FencingEvent> Events => _events;

    private IMatchState _state = new CreatingState();
    public IMatchState State => _state;

    private readonly Dictionary<Player, FencingScore> _scores = [];
    public IReadOnlyDictionary<Player, FencingScore> Scores => _scores;

    private Player? _winner;
    public Player? Winner => _winner;

    private Player? priorityPlayer;

    private const int playerCount = 2;

    public void Start()
    {
        if (_participants.Count != playerCount)
            throw new InvalidOperationException("Fencing match needs exactly two Players.");

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
        {
            _state = new CompletedState();

            if (!IsDraw() && priorityPlayer is null)
                _winner = _scores.MaxBy(s => s.Value).Key;
            else if (!IsDraw())
                _winner = priorityPlayer;
        }
    }

    public bool IsDraw()
    {
        List<FencingScore> values = [.. _scores.Values];

        return values[0].CompareTo(values[1]) == 0 && priorityPlayer is null;
    }

    private void Apply(FencingEvent matchEvent)
    {
        matchEvent.Apply(_scores);

        if (matchEvent is IWinningEvent<Player> winningEvent)
        {
            if (winningEvent.Winner is not null)
                priorityPlayer = winningEvent.Winner;
        }
    }

    public void AddEvent(FencingEvent matchEvent)
    {
        if (!_state.CanAddEvent(matchEvent))
            throw new InvalidOperationException("Cannot add this event in current state.");

        Apply(matchEvent);

        _events.Add(matchEvent);
    }

    public void CancelEvent(FencingEvent canceledEvent)
    {
        if (!_events.Remove(canceledEvent))
            throw new InvalidOperationException("Cannot cancel an event that has not taken place.");

        RecalculateScores();
    }

    private void RecalculateScores()
    {
        priorityPlayer = null;

        foreach (Player player in _scores.Keys.ToList())
            _scores[player] = new FencingScore();

        foreach (FencingEvent FencingEvent in _events)
            Apply(FencingEvent);
    }

    public void AddParticipant(Player participant)
    {
        if (!_state.CanAddParticipant)
            throw new InvalidOperationException("Cannot add participant in current state.");
        
        if (_participants.Count >= playerCount)
            throw new InvalidOperationException("Fencing match needs at most two players.");

        _participants.Add(participant);

        _scores.Add(participant, new FencingScore());
    }
}