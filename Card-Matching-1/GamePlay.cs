using System;
using System.Diagnostics;
using System.Threading;

class GamePlay : IGame
{
    protected int _row;
    protected int _column;
    protected int _count;
    protected int _foundPairs;
    protected int _maxPairs;
    protected bool _isGameOver;

    public GamePlay(int row, int column)
    {
        _row = row;
        _column = column;
        _isGameOver = false;
        _maxPairs = (row * column) / 2;
        _foundPairs = 0;
        _count = 0;
    }

    public void StartGame()
    {
        Console.WriteLine("카드를 섞는 중...");
        Thread.Sleep(1500);

        var board = new Board(_row, _column);

        while (!_isGameOver)
        {
            Console.WriteLine();
            PrintBoard(board);
            Console.WriteLine();
            Console.WriteLine($"시도 횟수: {_count} | 찾은 쌍: {_foundPairs}/{_maxPairs}");
            Console.WriteLine();
                        
            int r1 = -1, c1 = -1;
            while (true)
            {
                Console.Write("첫 번째 카드를 선택하세요 (행 열): ");
                var line = Console.ReadLine();
                var parts = line?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                if (string.IsNullOrWhiteSpace(line)
                    || parts.Length < 2
                    || !int.TryParse(parts[0], out int rrParsed)
                    || !int.TryParse(parts[1], out int ccParsed)
                    || rrParsed - 1 < 0
                    || rrParsed - 1 >= _row
                    || ccParsed - 1 < 0
                    || ccParsed - 1 >= _column)
                {
                    Console.WriteLine("제대로 된 값을 입력해주세요.");
                    continue;
                }

                int rr = rrParsed - 1;
                int cc = ccParsed - 1;

                if (board.Revealed[rr, cc])
                {
                    Console.WriteLine("이미 공개된 카드입니다. 다른 카드를 선택하세요.");
                    continue;
                }

                r1 = rr; c1 = cc;
                break;
            }
            board.Revealed[r1, c1] = true;
            Console.WriteLine();
            PrintBoard(board);

            int r2 = -1, c2 = -1;
            while (true)
            {
                Console.Write("두 번째 카드를 선택하세요 (행 열): ");
                var line = Console.ReadLine();
                var parts = line?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                if (string.IsNullOrWhiteSpace(line)
                    || parts.Length < 2
                    || !int.TryParse(parts[0], out int rrParsed)
                    || !int.TryParse(parts[1], out int ccParsed)
                    || rrParsed - 1 < 0
                    || rrParsed - 1 >= _row
                    || ccParsed - 1 < 0
                    || ccParsed - 1 >= _column)
                {
                    Console.WriteLine("제대로 된 값을 입력해주세요.");
                    continue;
                }

                int rr = rrParsed - 1;
                int cc = ccParsed - 1;

                if (rr == r1 && cc == c1)
                {
                    Console.WriteLine("같은 카드를 선택할 수 없습니다. 다른 카드를 선택하세요.");
                    continue;
                }

                if (board.Revealed[rr, cc])
                {
                    Console.WriteLine("이미 공개된 카드입니다. 다른 카드를 선택하세요.");
                    continue;
                }

                r2 = rr; c2 = cc;
                break;
            }
            board.Revealed[r2, c2] = true;
            Console.WriteLine();
            PrintBoard(board);

            _count++;

            if (board.Values[r1, c1] == board.Values[r2, c2])
            {
                _foundPairs++;
                Console.WriteLine();
                Console.WriteLine("짝을 찾았습니다!");
                Thread.Sleep(1000);
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("짝이 맞지 않습니다!");
                Thread.Sleep(1000);
                board.Revealed[r1, c1] = false;
                board.Revealed[r2, c2] = false;
            }

            if (_foundPairs >= _maxPairs)
            {
                _isGameOver = true;
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== 게임 클리어! ===");
        Console.WriteLine($"총 시도 횟수: {_count}");
        Console.WriteLine();
        Console.Write("새 게임을 하시겠습니까? (Y/N): ");
        string input = Console.ReadLine().Trim().ToUpper();

        while (true)
        {
            if (input == "Y")
            {
                Console.Clear();
                Process.Start(Environment.ProcessPath);
                Environment.Exit(0);
            }

            else if (input == "N")
            {
                Console.WriteLine("게임을 종료합니다.");
                break;
            }

            else
            {
                Console.WriteLine("잘못된 입력입니다. Y 또는 N을 입력해주세요.");
                input = Console.ReadLine()?.Trim().ToUpper();
            }
        }
    }

    private void PrintBoard(Board board)
    {
        Console.Write("    ");
        for (int i = 0; i < _column; i++)
        {
            Console.Write($"{i + 1}열  ");
        }
        Console.WriteLine();

        for (int i = 0; i < _row; i++)
        {
            Console.Write($"{i + 1}행 ");
            for (int j = 0; j < _column; j++)
            {
                if (board.Revealed[i, j])
                {
                    Console.Write($" {board.Values[i, j],-3}");
                }

                else
                {
                    Console.Write(" **  ");
                }                    
            }
            Console.WriteLine();
        }
    }
}