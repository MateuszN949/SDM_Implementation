using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;

namespace SportsCompetition.Core.Matches;

public class FootballMatch : IMatch<FootballEvent, FootballScore, Team>
{
    private readonly List<Team> _participants = [];
    public IReadOnlyList<Team> Participants => _participants;

    private readonly List<FootballEvent> _events = [];
    public IReadOnlyList<FootballEvent> Events => _events;

    private IMatchState _state = new CreatingState();
    public IMatchState State => _state;

    private Dictionary<Team, FootballScore> _scores = [];
    public IReadOnlyDictionary<Team, FootballScore> Scores => _scores;

    public void Start()
    {
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
        _state = new CompletedState();
    }

    public void AddEvent(FootballEvent matchEvent)
    {
        if (!_state.CanAddEvent(matchEvent))
            throw new InvalidOperationException("Cannot add this event in current state.");

        matchEvent.Apply(_scores);

        _events.Add(matchEvent);
    }

    public void AddParticipant(Team participant)
    {
        if (!_state.CanAddParticipant)
            throw new InvalidOperationException("Cannot add participant in current state.");

        _participants.Add(participant);

        _scores.Add(participant, new FootballScore());
    }
}