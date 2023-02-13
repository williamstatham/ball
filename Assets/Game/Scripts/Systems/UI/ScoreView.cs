using Game.Scripts.Systems.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Systems.UI
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private string textFormat;

        private GameUIViewModel _uiViewModel;
        
        [Inject]
        public void Construct(GameUIViewModel uiViewModel)
        {
            _uiViewModel = uiViewModel;
        }
        
        private void Start()
        {
            _uiViewModel.CurrentScore
                .Subscribe(UpdateCurrentScoreText)
                .AddTo(this);
        }

        private void UpdateCurrentScoreText(int score)
        {
            scoreText.text = string.Format(textFormat, score.ToString());
        }
    }
}