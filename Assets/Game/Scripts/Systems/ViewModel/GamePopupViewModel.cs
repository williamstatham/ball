using System;

namespace Game.Scripts.Systems.ViewModel
{
    public sealed class GamePopupViewModel
    {
        public event Action StartGameButtonClicked = () => { };
        

        public void InvokeStartGameButtonClicked()
        {
            StartGameButtonClicked.Invoke();
        }
    }
}