public class Tetromino
{
    private static readonly bool[,,,] Shapes = {
        // I形
        {{{false, false, false, false},
          {true, true, true, true},
          {false, false, false, false},
          {false, false, false, false}}},
        
        // O形
        {{{false, true, true, false},
          {false, true, true, false},
          {false, false, false, false},
          {false, false, false, false}}},
        
        // T形
        {{{false, true, false, false},
          {true, true, true, false},
          {false, false, false, false},
          {false, false, false, false}}},
        
        // S形
        {{{false, true, true, false},
          {true, true, false, false},
          {false, false, false, false},
          {false, false, false, false}}},
        
        // Z形
        {{{true, true, false, false},
          {false, true, true, false},
          {false, false, false, false},
          {false, false, false, false}}},
        
        // J形
        {{{true, false, false, false},
          {true, true, true, false},
          {false, false, false, false},
          {false, false, false, false}}},
        
        // L形
        {{{false, false, true, false},
          {true, true, true, false},
          {false, false, false, false},
          {false, false, false, false}}}
    };
    
    public bool[,] Shape { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public Tetromino()
    {
        Shape = new bool[4, 4];
        Random rand = new Random();
        int shapeIndex = rand.Next(Shapes.GetLength(0));
        
        // 复制选中的形状
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                Shape[i, j] = Shapes[shapeIndex, 0, i, j];
        
        X = 3; // 起始X位置
        Y = -4; // 起始Y位置
    }
    
    public void Rotate()
    {
        bool[,] newShape = new bool[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                newShape[i, j] = Shape[3 - j, i];
        Shape = newShape;
    }
    
    public void RotateBack()
    {
        bool[,] newShape = new bool[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                newShape[i, j] = Shape[j, 3 - i];
        Shape = newShape;
    }
} 