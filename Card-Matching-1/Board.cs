using System;

class Board
{
    private int _rows { get; }
    private int _columns { get; }
    public int[,] Values { get; }
    public bool[,] Revealed { get; }

    public Board(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
        Values = new int[rows, columns];
        Revealed = new bool[rows, columns];
        ShuffleBoard();
    }

    public void ShuffleBoard()
    {
        int pairs = (_rows * _columns) / 2;
        int[] pool = new int[_rows * _columns];
        int index = 0;
        for (int v = 1; v <= pairs; v++)
        {
            pool[index++] = v;
            pool[index++] = v;
        }

        var rand = new Random();
        for (int i = pool.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            int temp = pool[i];
            pool[i] = pool[j];
            pool[j] = temp;
        }

        index = 0;
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                Values[r, c] = pool[index++];
                Revealed[r, c] = false;
            }
        }
    }
}