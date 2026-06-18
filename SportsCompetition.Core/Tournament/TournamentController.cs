using System;
using System.Collections.Generic;
using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Stages;

namespace SportsCompetition.Core.Tournament;

public class TournamentController<TParty> where TParty : IParty
{
    private readonly List<IStage<TParty>> _stages = new();
    public IReadOnlyList<IStage<TParty>> Stages => _stages;

    private int _currentStageIndex = -1;
    public int CurrentStageIndex => _currentStageIndex;

    public IStage<TParty> CurrentStage
    {
        get
        {
            if (_stages.Count == 0 || _currentStageIndex < 0)
                throw new InvalidOperationException("Tournament contains no stages.");

            return _stages[_currentStageIndex];
        }
    }

    public event Action<int, IStage<TParty>>? StageChanged;

    public void AddStage(IStage<TParty> stage)
    {
        ArgumentNullException.ThrowIfNull(stage);

        _stages.Add(stage);

        if (_currentStageIndex == -1)
        {
            _currentStageIndex = 0;
            OnStageChanged();
        }
    }

    public bool Advance()
    {
        if (_stages.Count == 0) return false;
        if (_currentStageIndex >= _stages.Count - 1) return false;

        _currentStageIndex++;
        OnStageChanged();
        return true;
    }

    public bool Rollback()
    {
        if (_stages.Count == 0) return false;
        if (_currentStageIndex <= 0) return false;

        _currentStageIndex--;
        OnStageChanged();
        return true;
    }

    public IReadOnlyList<TParty> GetRanking() => CurrentStage.GetRanking();

    protected virtual void OnStageChanged()
    {
        StageChanged?.Invoke(_currentStageIndex, CurrentStage);
    }
}
