using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems
{
    public class ApplicationPresenter : IStartable
    {
        private ApplicationSettings _applicationSettings;

        [Inject]
        public void Construct(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }
        
        void IStartable.Start()
        {
            Application.targetFrameRate = _applicationSettings.TargetFramerate;
        }
    }
}