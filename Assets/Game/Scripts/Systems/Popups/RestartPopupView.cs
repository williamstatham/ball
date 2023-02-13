using Game.Scripts.Systems.ViewModel;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Systems.Popups
{
    public sealed class RestartPopupView : StartPopupView
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private string scoreTextFormat;

        private GameUIViewModel _gameUIViewModel;

        [Inject]
        public void Construct(GamePopupViewModel popupViewModel, GameUIViewModel gameUIViewModel)
        {
            Construct(popupViewModel);
            _gameUIViewModel = gameUIViewModel;
        }

        public void Start()
        {
            scoreText.text = string.Format(scoreTextFormat, _gameUIViewModel.CurrentScore.Value.ToString());
        }
    }
}