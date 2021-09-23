namespace GameCore;

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}

public class Game
{
    private static readonly Random random = new();

    // Generate the initial game board
    public GameState Init(int rows, int cols, int tilesOnNewBoard)
    {
        if (tilesOnNewBoard > rows * cols)
        {
            throw new ArgumentOutOfRangeException(nameof(tilesOnNewBoard),
                "Number of non-zero tiles is greater than total number of tiles," +
                $" i.e. {nameof(tilesOnNewBoard)} > {nameof(rows)} * {nameof(cols)}.");
        }

        if (rows < 2 || cols < 2)
        {
            throw new ArgumentOutOfRangeException($"Number of {(rows < 2 ? nameof(rows) : nameof(cols))} must be greater than 1");
        }

        return new GameState
        {
            Board = AddNewRandomTiles(new ulong[rows, cols], tilesOnNewBoard),
            Score = 0,
            IsGameOver = false
        };
    }

    // Calculation of next configuration of the game board after the gamer's turn
    public GameState NextState(GameState inputState, Direction? direction, int newTiles)
    {
        if (newTiles > inputState.Board.GetLength(0) * inputState.Board.GetLength(1))
        {
            throw new ArgumentOutOfRangeException("Number of non-zero tiles is greater than total number of tiles," +
             $" i.e. {newTiles} (new tiles) > {inputState.Board.GetLength(0)} (columns) * {inputState.Board.GetLength(1)} (rows).");
        }

        var nextState = Update(inputState, direction);

        // If no movement leads to change, then the game is over
        if (!TryToMove(nextState.Board))
        {
            nextState.IsGameOver = true;
            return nextState;
        }

        // It's possible that the gamer's move doesn't sum and/or move tiles
        // In this case it isn't necessary to add new tiles
        if (Utility.CompareMultidimensionalArrays(inputState.Board, nextState.Board))
        {
            return nextState;
        }

        // It's possible that the number of empty tiles is less than the number of new tiles that we wish to add
        var freeTiles = nextState.Board.Length - Utility.Calculate2DArrayNonZeroValues(nextState.Board);
        var addedTiles = Math.Min(newTiles, freeTiles);
        AddNewRandomTiles(nextState.Board, addedTiles);

        return nextState;
    }

    private GameState Update(GameState inputState, Direction? direction)
    {
        var updateState = new GameState
        {
            Board = (ulong[,])inputState.Board.Clone(),
            Score = inputState.Score,
            IsGameOver = false
        };

        var isHorizontalMove = direction == Direction.Left || direction == Direction.Right;
        var isIncreasing = direction == Direction.Left || direction == Direction.Up;

        var maxRow = inputState.Board.GetLength(0);
        var maxCol = inputState.Board.GetLength(1);

        // Depending on the direction of movement, we only need maxRow or maxCol
        var lines = isHorizontalMove ? maxRow : maxCol;
        var maxNumInLine = isHorizontalMove ? maxCol : maxRow;

        // Extract one row or one column from a two-dimensional array
        var extractLine = isHorizontalMove
            ? new Func<ulong[,], int, ulong[]>((board, num) => Enumerable.Range(0, maxCol)
                .Select(x => board[num, x]).ToArray())
            : (board, num) => Enumerable.Range(0, maxRow)
                .Select(x => board[x, num]).ToArray();

        var condition = isIncreasing ? new Func<int, int, bool>((a, b) => a < b - 1) : (a, b) => a >= 0;
        var change = isIncreasing ? new Func<int, int>(j => j + 1) : j => j - 1;

        // Shift all non-zero elements
        var shift = isIncreasing
            ? new Func<ulong[], ulong[]>(a => a.OrderBy(x => x == 0).ToArray())
            : a => a.OrderByDescending(x => x == 0).ToArray();

        // To insert a row, use the fast Buffer.BlockCopy method, and use a "for" to insert a column
        var insertLine = isHorizontalMove
            ? new Action<ulong[,], ulong[], int>((board, row, i) =>
                Buffer.BlockCopy(row, 0, board, i * maxCol * sizeof(ulong), maxNumInLine * sizeof(ulong)))
            : (board, col, i) =>
            {
                for (var j = 0; j < maxNumInLine; j++)
                {
                    board[j, i] = col[j];
                }
            };

        var line = new ulong[maxNumInLine];

        // Iteration of rows or columns (depending on the gamer's move)
        for (var i = 0; i < lines; i++)
        {
            // Extract 1D array from 2D array
            line = extractLine(updateState.Board, i);

            // Shift all non-zero elements
            line = shift(line);

            // Sum equal tiles and calculate score
            for (var j = isIncreasing ? 0 : maxNumInLine - 2; condition(j, maxNumInLine); j = change(j))
            {
                if (line[j] != 0 && line[j] == line[j + 1])
                {
                    line[j] *= 2;
                    line[j + 1] = 0;

                    updateState.Score += line[j];

                    if (!isIncreasing)
                    {
                        j--;
                    }
                }
            }

            // Shift all non-zero elements, if there are appeared
            line = shift(line);

            // Insert 1D array to 2D array
            insertLine(updateState.Board, line, i);
        }

        return updateState;
    }

    // Add new tiles to game board
    private ulong[,] AddNewRandomTiles(ulong[,] board, int newDigits)
    {
        // List of all empty tiles
        var freeTiles = new List<(int x, int y)>();

        for (var rows = 0; rows < board.GetLength(0); rows++)
        {
            for (var cols = 0; cols < board.GetLength(1); cols++)
            {
                if (board[rows, cols] == 0)
                {
                    freeTiles.Add((rows, cols));
                }
            }
        }

        for (var i = 0; i < newDigits; i++)
        {
            var r = random.Next(freeTiles.Count);

            // The new tile must be 2 with 90% probability or 4 with 10% probability
            board[freeTiles[r].x, freeTiles[r].y] = random.NextDouble() < 90.0 / 100.0 ? 2ul : 4ul;

            freeTiles.RemoveAt(r);
        }

        return board;
    }

    private bool TryToMove(ulong[,] board)
    {
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            var cloneIn = new GameState { Board = (ulong[,])board.Clone() };

            var cloneOut = Update(cloneIn, dir);

            if (!Utility.CompareMultidimensionalArrays(cloneIn.Board, cloneOut.Board))
            {
                return true;
            }
        }

        return false;
    }
}
