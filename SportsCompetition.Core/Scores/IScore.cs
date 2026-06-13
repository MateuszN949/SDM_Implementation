namespace SportsCompetition.Core.Scores;

public interface IScore<T> : IComparable<T>
{
    public T Aggregate(T score);
}