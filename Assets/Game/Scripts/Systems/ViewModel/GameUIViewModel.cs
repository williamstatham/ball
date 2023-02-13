using UniRx;

namespace Game.Scripts.Systems.ViewModel
{
    public readonly struct LivesData
    {
        private readonly int _maxLives;
        private readonly int _currentLives;

        public int MaxLives => _maxLives;
        public int CurrentLives => _currentLives;

        public LivesData(int maxLives, int currentLives)
        {
            _maxLives = maxLives;
            _currentLives = currentLives;
        }
    }
    
    public sealed class GameUIViewModel
    {
        public readonly ReactiveProperty<LivesData> LivesData = new();
        public readonly ReactiveProperty<int> CurrentScore = new();
    }
}