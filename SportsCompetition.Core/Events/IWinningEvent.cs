using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Events;

public interface IWinningEvent <TParty> where TParty : IParty
{
    TParty? Winner { get; }
};