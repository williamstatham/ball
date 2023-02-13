using Game.Scripts.Systems.UI;
using Game.Scripts.Systems.ViewModel;
using VContainer;

namespace Game.Scripts.Systems.Score
{
    public sealed class ScoreManager
    {
        private GameUIViewModel _gameUIViewModel;

        private int _currentScore = 0;

        public int CurrentScore => _currentScore;

        [Inject]
        public void Construct(GameUIViewModel gameUIViewModel)
        {
            _gameUIViewModel = gameUIViewModel;
        }

        public void Reset()
        {
            _currentScore = 0;
            UpdateViewModel();
        }

        private void UpdateViewModel()
        {
            _gameUIViewModel.CurrentScore.Value = _currentScore;
        }

        public void IncrementScore(int count)
        {
            _currentScore += count;
            UpdateViewModel();
        }
    }
}