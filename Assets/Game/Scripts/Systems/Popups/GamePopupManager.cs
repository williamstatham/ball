using Doozy.Runtime.UIManager.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Systems.Popups
{
    public sealed class GamePopupManager : PopupManagerBase
    {
        [SerializeField] private UIPopupLink startGamePopup;
        [SerializeField] private UIPopupLink restartGamePopup;

        public void ShowStartGamePopup()
        {
            ShowPopup(startGamePopup);
        }

        public void ShowRestartGamePopup()
        {
            ShowPopup(restartGamePopup);
        }
    }
}