namespace SportsCompetition.Core.Parties;
using System.Threading;

public sealed class IDGenerator
{
    private static readonly IDGenerator _instance = new();

    private int _nextId = 0;

    private IDGenerator() { }

    public static IDGenerator Instance => _instance;

    public int GetNextId()
    {
        return Interlocked.Increment(ref _nextId);
    }
}