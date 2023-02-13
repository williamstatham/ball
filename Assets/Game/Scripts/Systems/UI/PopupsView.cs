using Game.Scripts.Systems.ViewModel;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Systems.UI
{
    public sealed class PopupsView : MonoBehaviour
    {
        private GameUIViewModel _gameUIViewModel;
        
        [Inject]
        public void Construct(GameUIViewModel gameUIViewModel)
        {
            
        }
    }
}