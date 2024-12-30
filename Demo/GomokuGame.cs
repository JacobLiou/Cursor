using System;

class GomokuGame
{
    private const int BOARD_SIZE = 15;
    private char[,] board;
    private bool isBlackTurn;

    public GomokuGame()
    {
        board = new char[BOARD_SIZE, BOARD_SIZE];
        InitializeBoard();
        isBlackTurn = true; // 黑子先手
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                board[i, j] = '+';
            }
        }
    }

    public void Start()
    {
        while (true)
        {
            DrawBoard();
            if (MakeMove())
            {
                DrawBoard();
                Console.WriteLine($"{(isBlackTurn ? "白" : "黑")}子赢了！");
                break;
            }
            isBlackTurn = !isBlackTurn;
        }
    }

    private void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4");
        
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            Console.Write(i % 10 + " ");
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine($"\n当前回合: {(isBlackTurn ? "黑" : "白")}子");
    }

    private bool MakeMove()
    {
        while (true)
        {
            Console.Write("请输入落子位置 (行 列): ");
            string[] input = Console.ReadLine().Split(' ');
            
            if (input.Length != 2)
            {
                Console.WriteLine("输入格式错误，请重试！");
                continue;
            }

            if (int.TryParse(input[0], out int row) && int.TryParse(input[1], out int col))
            {
                if (row >= 0 && row < BOARD_SIZE && col >= 0 && col < BOARD_SIZE)
                {
                    if (board[row, col] == '+')
                    {
                        board[row, col] = isBlackTurn ? '●' : '○';
                        return CheckWin(row, col);
                    }
                    else
                    {
                        Console.WriteLine("该位置已有棋子，请重试！");
                    }
                }
                else
                {
                    Console.WriteLine("位置超出范围，请重试！");
                }
            }
            else
            {
                Console.WriteLine("输入必须是数字，请重试！");
            }
        }
    }

    private bool CheckWin(int row, int col)
    {
        char piece = board[row, col];
        
        // 检查横向
        if (CountDirection(row, col, 0, 1) + CountDirection(row, col, 0, -1) >= 4) return true;
        
        // 检查纵向
        if (CountDirection(row, col, 1, 0) + CountDirection(row, col, -1, 0) >= 4) return true;
        
        // 检查左上到右下
        if (CountDirection(row, col, 1, 1) + CountDirection(row, col, -1, -1) >= 4) return true;
        
        // 检查右上到左下
        if (CountDirection(row, col, -1, 1) + CountDirection(row, col, 1, -1) >= 4) return true;

        return false;
    }

    private int CountDirection(int row, int col, int rowDelta, int colDelta)
    {
        char piece = board[row, col];
        int count = 0;
        int currentRow = row + rowDelta;
        int currentCol = col + colDelta;

        while (currentRow >= 0 && currentRow < BOARD_SIZE && 
               currentCol >= 0 && currentCol < BOARD_SIZE && 
               board[currentRow, currentCol] == piece)
        {
            count++;
            currentRow += rowDelta;
            currentCol += colDelta;
        }

        return count;
    }
} 