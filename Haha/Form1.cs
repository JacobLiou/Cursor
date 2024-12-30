namespace TetrisGame
{
    public partial class Form1 : Form
    {
        private Game game;
        private System.Windows.Forms.Timer gameTimer;
        
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            game = new Game();
            
            // 设置游戏区域大小
            this.ClientSize = new Size(400, 600);
            
            // 初始化游戏计时器
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 500; // 500毫秒移动一次
            gameTimer.Tick += GameTimer_Tick;
            
            // 添加键盘事件
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            
            // 开始游戏
            gameTimer.Start();
            
            // 添加绘制事件
            this.Paint += Form1_Paint;
            this.DoubleBuffered = true;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            game.MoveDown();
            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.MoveLeft();
                    break;
                case Keys.Right:
                    game.MoveRight();
                    break;
                case Keys.Down:
                    game.MoveDown();
                    break;
                case Keys.Up:
                    game.Rotate();
                    break;
            }
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }
    }
} 