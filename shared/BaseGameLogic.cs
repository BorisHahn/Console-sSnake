using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_sSnake.shared
{
    public abstract class BaseGameLogic : IArrowListener
    {
        protected BaseGameState currentState;
        private float _time;
        protected int _screenWidth;
        protected int _screenHeight;
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

        public void ChangeState(BaseGameState baseGameState)
        {
            currentState?.Reset();      
            currentState = baseGameState;
        }

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            _time += deltaTime;
            _screenHeight = renderer.height;
            _screenWidth = renderer.width;
            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);
            this.Update(deltaTime);
        }
    }
}
