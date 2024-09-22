using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_sSnake.shared;

public enum SnakeDir
{
    Up, Down, Left, Right
}


namespace Console_sSnake.snake
{
    public class SnakeGameplayState : BaseGameState
    {
        const char snakeSymbol = '■';
        private SnakeDir currentDir = SnakeDir.Right;
        private float _timeToMove = 0f;
        private List<Cell> _body = new();
        public int fieldWidth;
        public int fieldHeight;
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

        private Cell ShiftTo(Cell curCell, SnakeDir dir)
        {
            switch (dir)
            {
                case SnakeDir.Up:
                    return new Cell(curCell.X, curCell.Y - 1);
                    break;
                case SnakeDir.Down:
                    return new Cell(curCell.X, curCell.Y + 1);
                    break;
                case SnakeDir.Left:
                    return new Cell(curCell.X - 1, curCell.Y);
                    break;
                case SnakeDir.Right:
                    return new Cell(curCell.X + 1, curCell.Y);
                    break;
            }
            return curCell;
        }
        public override void Reset()
        {
            _body.Clear();
            var middleY = fieldHeight / 2;
            var middleX = fieldWidth / 2;
            currentDir = SnakeDir.Right;
            _body.Add(new Cell(middleX + 3, middleY));
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if (_timeToMove > 0f)
                return;
            
            _timeToMove = 1f / 5;
            var head = _body[0];
            var nextCell = ShiftTo(head, currentDir);

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            foreach (var item in _body)
            {
                renderer.SetPixel(item.X, item.Y, snakeSymbol, 1);
            }
        }
    }
}
