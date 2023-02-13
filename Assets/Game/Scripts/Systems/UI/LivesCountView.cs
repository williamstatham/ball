using Game.Scripts.Systems.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Scripts.Systems.UI
{
    public sealed class LivesCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI livesCountText;
        [SerializeField] private Image livesCountProgressor;

        private GameUIViewModel _uiViewModel;
        
        [Inject]
        public void Construct(GameUIViewModel uiViewModel)
        {
            _uiViewModel = uiViewModel;
        }

        private void Start()
        {
            _uiViewModel.LivesData
                .Subscribe(OnLivesDataUpdated)
                .AddTo(this);
            
            OnLivesDataUpdated(_uiViewModel.LivesData.Value);
        }

        private void OnLivesDataUpdated(LivesData livesData)
        {
            livesCountText.text = livesData.CurrentLives.ToString();
            livesCountProgressor.fillAmount = livesData.CurrentLives / (float) livesData.MaxLives;
        }
    }
}