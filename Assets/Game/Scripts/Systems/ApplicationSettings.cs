using UnityEngine;

namespace Game.Scripts.Systems
{
    [CreateAssetMenu(fileName = nameof(ApplicationSettings), menuName = "Descriptors/" + nameof(ApplicationSettings))]
    public class ApplicationSettings : ScriptableObject
    {
        [SerializeField] private int targetFramerate;

        public int TargetFramerate => targetFramerate;
    }
}