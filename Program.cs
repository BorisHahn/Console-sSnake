using Console_sSnake.shared;
using Console_sSnake.snake;

namespace Console_sSnake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SnakeGameLogic gameLogic = new SnakeGameLogic();
            ConsoleInput input = new ConsoleInput();
            gameLogic.InitializeInput(input);
            var lastFrameTime = DateTime.Now;
            gameLogic.GotoGameplay();
            while (true)
            {
                input.Update();
                var frameStartTime = DateTime.Now;
                var deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
                gameLogic.Update(deltaTime);
                lastFrameTime = frameStartTime;
            }
        }
    }
}
