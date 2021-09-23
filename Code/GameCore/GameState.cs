namespace GameCore;

public class GameState
{
    private ulong[,] _board;
    private ulong _score;

    public ulong[,] Board
    {
        get => _board;
        set
        {
            if (value.GetLength(0) < 2 || value.GetLength(1) < 2)
            {
                throw new ArgumentOutOfRangeException(
                    $"Number of {(value.GetLength(0) < 2 ? "rows" : "columns")} must be greater than 1");
            }

            _board = value;
        }
    }

    public ulong Score
    {
        get => _score;
        set
        {
            if (value % 2 != 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"The value of the scored points must be an even number. {value} is a wrong value.");
            }

            _score = value;
        }
    }

    public bool IsGameOver { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((GameState)obj);
    }

    protected bool Equals(GameState other)
    {
        return Utility.CompareMultidimensionalArrays(Board, other.Board) &&
               Score == other.Score &&
               IsGameOver == other.IsGameOver;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Board, Score, IsGameOver);
    }

    // Only for debug information in case of tests error
    public override string ToString()
    {
        return $"Board: {Utility.TwoDimensionalArrayToString(Board)}," +
               $"Score: {Score}," +
               $"IsGameOver: {IsGameOver}";
    }
}
