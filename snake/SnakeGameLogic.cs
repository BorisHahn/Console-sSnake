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
        private SnakeGameplayState gameplayState = new SnakeGameplayState();
        private bool newGamePending = false;
        private int currentLevel = 1;
        private ShowTextState showTextState = new(2f);
        
        private void GotoNextLevel()
        {
            currentLevel++;
            newGamePending = false;
            showTextState.text = $"Level {currentLevel}";
            ChangeState(showTextState);
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
            gameplayState.level = currentLevel;
            gameplayState.fieldWidth = screenWidth;
            gameplayState.fieldHeight = screenHeight;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return [
                ConsoleColor.Red, 
                ConsoleColor.Green, 
                ConsoleColor.Blue, 
                ConsoleColor.Yellow
                ];
        }

        public void GoToGameOver()
        {
            currentLevel = 0;
            newGamePending = true;
            showTextState.text = $"Потрачено!";
            ChangeState(showTextState);
        }

        public override void Update(float deltaTime)
        {
            if (currentState != null && !currentState.IsDone())
            {
                return;
            }
            if (currentState == null || currentState == gameplayState && !gameplayState.gameOver)
            {
                GotoNextLevel();
            }
            else if (currentState == gameplayState && gameplayState.gameOver)
            {
                GoToGameOver();
            }
            else if (currentState != gameplayState && newGamePending)
            {
                GotoNextLevel();
            }
            else if (currentState != gameplayState && !newGamePending)
            {
                GotoGameplay();
            }
            
        }
    }
}
