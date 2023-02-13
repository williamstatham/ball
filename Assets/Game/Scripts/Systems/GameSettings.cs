using Game.Scripts.Core.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Systems
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Descriptors/Game/" + nameof(GameSettings))]
    public sealed class GameSettings : ScriptableObject
    {
        [SerializeField] private int maxLives;
        [SerializeField] private float defaultGameSpeed;
        [SerializeField] private IntFloatSerializableDictionary speedSettings; // key - score, value - speed multiplier
        [SerializeField] private float gameSpeedChangeTimeSeconds;
        [SerializeField] private AnimationCurve gameSpeedChangeFunction;
        [SerializeField] private float defaultEntitySpawnTime;
        
        [Space]
        [SerializeField] private float tryTriggerWallsEventEachSeconds;
        [SerializeField, Range(0, 100)] private int wallsEventChancePercent;
        [SerializeField] private float wallsEventDurationSeconds;
        [SerializeField] private float wallsEventAppearanceTimeSeconds;
        [SerializeField] private Vector2 minMaxWallsSize;


        public float DefaultGameSpeed => defaultGameSpeed;
        public int MaxLives => maxLives;
        public float GameSpeedChangeTimeSeconds => gameSpeedChangeTimeSeconds;
        public AnimationCurve GameSpeedChangeFunction => gameSpeedChangeFunction;
        public float DefaultEntitySpawnTime => defaultEntitySpawnTime;
        public float TryTriggerWallsEventEachSeconds => tryTriggerWallsEventEachSeconds;
        public float WallsEventDurationSeconds => wallsEventDurationSeconds;
        public float WallsEventAppearanceTimeSeconds => wallsEventAppearanceTimeSeconds;
        public Vector2 MinMaxWallsSize => minMaxWallsSize;
        
        public bool TryGetNewGameSpeed(int score, out float newGameSpeed)
        {
            return speedSettings.TryGetValue(score, out newGameSpeed);
        }

        public bool TryTriggerWalls()
        {
            return Random.value <= wallsEventChancePercent / 100f;
        }
    }
}