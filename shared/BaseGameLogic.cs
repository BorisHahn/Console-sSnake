using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_sSnake.shared
{
    public abstract class BaseGameLogic : IArrowListener
    {
        protected BaseGameState? currentState { get; private set; }
        protected float time { get; private set; }
        protected int screenWidth { get; private set; }
        protected int screenHeight { get; private set; }
        public abstract void OnArrowDown();

        public abstract void OnArrowLeft();

        public abstract void OnArrowRight();

        public abstract void OnArrowUp();
        public abstract ConsoleColor[] CreatePallet();
        public abstract void Update(float deltaTime);


        public void InitializeInput(ConsoleInput consoleInput)
        {
            consoleInput.Subscribe(this);
        }

        public void ChangeState(BaseGameState? baseGameState)
        {
            currentState?.Reset();      
            currentState = baseGameState;
        }

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            time += deltaTime;
            screenHeight = renderer.height;
            screenWidth = renderer.width;
            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);
            Update(deltaTime);
        }
    }
}
