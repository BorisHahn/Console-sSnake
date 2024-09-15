using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_sSnake.shared
{
    public abstract class BaseGameLogic : IArrowListener
    {
        public abstract void OnArrowDown();


        public abstract void OnArrowLeft();


        public abstract void OnArrowRight();


        public abstract void OnArrowUp();


        public void InitializeInput(ConsoleInput consoleInput)
        {
            consoleInput.Subscribe(this);
        }

        public abstract void Update(float deltaTime);
    }
}
