public class Game
{
    private const int GRID_SIZE = 30; // 每个方块的大小
    private const int ROWS = 20;
    private const int COLS = 10;
    
    private bool[,] gameBoard;
    private Tetromino currentPiece;
    
    public Game()
    {
        gameBoard = new bool[ROWS, COLS];
        currentPiece = new Tetromino();
    }
    
    public void MoveLeft()
    {
        if (CanMove(-1, 0))
            currentPiece.X--;
    }
    
    public void MoveRight()
    {
        if (CanMove(1, 0))
            currentPiece.X++;
    }
    
    public void MoveDown()
    {
        if (CanMove(0, 1))
            currentPiece.Y++;
        else
        {
            PlacePiece();
            ClearLines();
            currentPiece = new Tetromino();
            if (!CanMove(0, 0)) // 游戏结束检查
            {
                MessageBox.Show("游戏结束！");
                Application.Exit();
            }
        }
    }
    
    public void Rotate()
    {
        currentPiece.Rotate();
        if (!CanMove(0, 0))
            currentPiece.RotateBack();
    }
    
    private bool CanMove(int deltaX, int deltaY)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (currentPiece.Shape[i, j])
                {
                    int newX = currentPiece.X + j + deltaX;
                    int newY = currentPiece.Y + i + deltaY;
                    
                    if (newX < 0 || newX >= COLS || newY >= ROWS)
                        return false;
                    
                    if (newY >= 0 && gameBoard[newY, newX])
                        return false;
                }
            }
        }
        return true;
    }
    
    private void PlacePiece()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (currentPiece.Shape[i, j])
                {
                    int boardY = currentPiece.Y + i;
                    int boardX = currentPiece.X + j;
                    if (boardY >= 0)
                        gameBoard[boardY, boardX] = true;
                }
            }
        }
    }
    
    private void ClearLines()
    {
        for (int row = ROWS - 1; row >= 0; row--)
        {
            bool isLineFull = true;
            for (int col = 0; col < COLS; col++)
            {
                if (!gameBoard[row, col])
                {
                    isLineFull = false;
                    break;
                }
            }
            
            if (isLineFull)
            {
                for (int r = row; r > 0; r--)
                {
                    for (int col = 0; col < COLS; col++)
                    {
                        gameBoard[r, col] = gameBoard[r - 1, col];
                    }
                }
                row++; // 重新检查当前行
            }
        }
    }
    
    public void Draw(Graphics g)
    {
        // 绘制已放置的方块
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                if (gameBoard[i, j])
                {
                    g.FillRectangle(Brushes.Gray, j * GRID_SIZE, i * GRID_SIZE, GRID_SIZE - 1, GRID_SIZE - 1);
                }
            }
        }
        
        // 绘制当前方块
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (currentPiece.Shape[i, j])
                {
                    g.FillRectangle(Brushes.Blue, 
                        (currentPiece.X + j) * GRID_SIZE, 
                        (currentPiece.Y + i) * GRID_SIZE, 
                        GRID_SIZE - 1, GRID_SIZE - 1);
                }
            }
        }
        
        // 绘制网格线
        for (int i = 0; i <= ROWS; i++)
            g.DrawLine(Pens.LightGray, 0, i * GRID_SIZE, COLS * GRID_SIZE, i * GRID_SIZE);
        
        for (int j = 0; j <= COLS; j++)
            g.DrawLine(Pens.LightGray, j * GRID_SIZE, 0, j * GRID_SIZE, ROWS * GRID_SIZE);
    }
} 