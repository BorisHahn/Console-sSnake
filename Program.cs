using Console_sSnake.shared;
using Console_sSnake.snake;

namespace Console_sSnake
{
    internal class Program
    {
        const float targetFrameTime = 1f / 60f;
        static void Main()
        {
            SnakeGameLogic gameLogic = new SnakeGameLogic();
            
            var pallete = gameLogic.CreatePallet();

            var renderer0 = new ConsoleRenderer(pallete);
            var renderer1 = new ConsoleRenderer(pallete);

            var input = new ConsoleInput();
            gameLogic.InitializeInput(input);

            var prevRenderer = renderer0;
            var currRenderer = renderer1;
            var lastFrameTime = DateTime.Now;
            while (true)
            {
                var frameStartTime = DateTime.Now;
                var deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;

                input.Update();
                
                gameLogic.DrawNewState(deltaTime, currRenderer);
                lastFrameTime = frameStartTime;

                if (!currRenderer.Equals(prevRenderer)) currRenderer.Render();

                var tmp = prevRenderer;
                prevRenderer = currRenderer;
                currRenderer = tmp;
                currRenderer.Clear();

                var nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
                var endFrameTime = DateTime.Now;
                if (nextFrameTime > endFrameTime)
                {
                    Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
                }
            }
        }
    }
}
