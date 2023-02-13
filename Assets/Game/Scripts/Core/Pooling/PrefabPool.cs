using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Game.Scripts.Core.Pooling
{
    [Serializable]
    public class PrefabPool<T> where T : MonoBehaviour, IPoolable
    {
        [SerializeField] private T prefab;
        [SerializeField] private int count;

        public T Prefab => prefab;

        public IObjectPool<T> ObjectPool { get; private set; }

        private GameObject _root;
        private bool _autoActivate;

        public void Initialize(GameObject root, bool autoActivate = false)
        {
            _root = root;
            _autoActivate = autoActivate;

            ObjectPool = new ObjectPool<T>(OnCreate, OnGet, OnRelease, OnDestroy, defaultCapacity: count);
        }

        private T OnCreate()
        {
            return Object.Instantiate(prefab, _root.transform);
        }

        private void OnDestroy(T instance)
        {
            Object.Destroy(instance.gameObject);
        }

        private void OnGet(T instance)
        {
            instance.gameObject.SetActive(_autoActivate);
        }

        private void OnRelease(T instance)
        {
            instance.gameObject.SetActive(false);
            instance.transform.parent = _root.transform;
        }
    }
}