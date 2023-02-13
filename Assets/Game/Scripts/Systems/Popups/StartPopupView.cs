using Doozy.Runtime.UIManager.Containers;
using Game.Scripts.Systems.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Scripts.Systems.Popups
{
    [RequireComponent(typeof(UIPopup))]
    public class StartPopupView : MonoBehaviour
    {
        [SerializeField] private UIPopup popup;
        [SerializeField] private Button startButton;

        private GamePopupViewModel _popupViewModel;
        
        [Inject]
        public void Construct(GamePopupViewModel popupViewModel)
        {
            _popupViewModel = popupViewModel;
        }

        private void OnEnable()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        
        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            popup.Hide();
            _popupViewModel.InvokeStartGameButtonClicked();
        }

        protected virtual void Reset()
        {
            popup = GetComponent<UIPopup>();
        }
    }
}