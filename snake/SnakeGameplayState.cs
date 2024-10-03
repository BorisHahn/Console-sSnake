using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Console_sSnake.shared;


namespace Console_sSnake.snake
{
    public enum SnakeDir
    {
        Up, Down, Left, Right
    }
    public class SnakeGameplayState : BaseGameState
    {
        const char snakeSymbol = '■';
        const char circleSymbol = '0';
        private Cell _apple = new Cell();
        private Random _random = new Random();
        public bool gameOver;
        private SnakeDir currentDir = SnakeDir.Left;
        private float _timeToMove = 0f;
        private List<Cell> _body = new();
        public int fieldWidth { get; set; }
        public int fieldHeight { get; set; }
        public bool hasWon = false;
        public int level;
        private struct Cell
        {
            public int X;
            public int Y;

            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public void SetDirection(SnakeDir dir)
        {
            currentDir = dir;
        }

        public void GenerateApple()
        {
            Cell cell;
            cell.X = _random.Next(fieldWidth);
            cell.Y = _random.Next(fieldHeight);

            if (_body[0].Equals(cell))
            {
                if (cell.Y > fieldHeight / 2)
                {
                    cell.Y -= 1;
                } else
                {
                    cell.Y += 1;
                }
            } else
            {
                _apple = cell;
            }
        }

        private Cell ShiftTo(Cell curCell, SnakeDir dir)
        {
            switch (dir)
            {
                case SnakeDir.Up:
                    return new Cell(curCell.X, curCell.Y - 1);
                case SnakeDir.Down:
                    return new Cell(curCell.X, curCell.Y + 1);
                case SnakeDir.Left:
                    return new Cell(curCell.X - 1, curCell.Y);
                case SnakeDir.Right:
                    return new Cell(curCell.X + 1, curCell.Y);
            }
            return curCell;
        }
        public override void Reset()
        {
            _body.Clear();
            var middleY = fieldHeight / 2;
            var middleX = fieldWidth / 2;
            currentDir = SnakeDir.Left;
            _body.Add(new(middleX + 2, middleY));
            _timeToMove = 0f;
            _apple = new Cell(middleX + 10, middleY + 10);
            gameOver = false;
            hasWon = false;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f || gameOver)
                return;

            _timeToMove = 1f / (4 + level);
            var head = _body[0];
            var nextCell = ShiftTo(head, currentDir);
            if (nextCell.X == _apple.X && nextCell.Y == _apple.Y)
            {
                _body.Insert(0, _apple);
                hasWon = _body.Count >= level + 3;
                GenerateApple();
                return;
            }
            if (nextCell.X >= fieldWidth || nextCell.X < 0 || nextCell.Y >= fieldHeight || nextCell.Y < 0)
            {
                gameOver = true;
                return;
            }

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            renderer.DrawString($"Level: {level}", 3, 1, ConsoleColor.Green);
            renderer.DrawString($"Score: {_body.Count - 1}", 3, 2, ConsoleColor.Green);

            foreach (var item in _body)
            {
                renderer.SetPixel(item.X, item.Y, snakeSymbol, 2);
            }
            
            renderer.SetPixel(_apple.X, _apple.Y, circleSymbol, 1);
        }

        public override bool IsDone()
        {
            return gameOver || hasWon;
        }
    }
}
