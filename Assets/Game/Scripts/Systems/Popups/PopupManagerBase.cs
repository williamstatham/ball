using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems.Popups
{
    public abstract class PopupManagerBase : MonoBehaviour
    {
        private IObjectResolver _objectResolver;
        
        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        protected void ShowPopup(UIPopupLink popupLink)
        {
            UIPopup popup = UIPopup.Get(popupLink.prefabName);
            _objectResolver.InjectGameObject(popup.gameObject);
            popup.Show();
        }
    }
}