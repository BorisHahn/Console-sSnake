using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_sSnake.shared;

namespace Console_sSnake.snake
{
    public class SnakeGameLogic : BaseGameLogic
    {
        SnakeGameplayState gameplayState = new SnakeGameplayState();
        public override void Update(float deltaTime)
        {
            gameplayState.Update(deltaTime);
        }

        public override void OnArrowUp()
        {
            if (currentState != gameplayState)
            {
                return;
            }
            gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            if (currentState != gameplayState)
            {
                return;
            }
            gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            if (currentState != gameplayState)
            {
                return;
            }
            gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            if (currentState != gameplayState)
            {
                return;
            }   
            gameplayState.SetDirection(SnakeDir.Right);
        }

        public void GotoGameplay()
        {
            gameplayState.fieldWidth = _screenWidth;
            gameplayState.fieldHeight = _screenHeight;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return [ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow];
        }
    }
}
