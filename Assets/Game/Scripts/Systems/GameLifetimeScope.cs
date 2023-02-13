using Game.Scripts.Systems.Camera;
using Game.Scripts.Systems.Field;
using Game.Scripts.Systems.Field.Entities;
using Game.Scripts.Systems.Lives;
using Game.Scripts.Systems.Movement;
using Game.Scripts.Systems.Player;
using Game.Scripts.Systems.Popups;
using Game.Scripts.Systems.Score;
using Game.Scripts.Systems.ViewModel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ApplicationSettings applicationSettings;
        
        [Space]
        [SerializeField] private GameSettings gameSettings;
        
        [Space]
        [SerializeField] private UnityEngine.Camera inputCamera;
        [SerializeField] private InputPanel inputPanel;
        
        [Space]
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private PlayerView playerView;

        [Space]
        [SerializeField] private FieldWallsView wallsView;
        [SerializeField] private FieldEntityPoolManager fieldEntityPoolManager;

        [Space] 
        [SerializeField] private GamePopupManager gamePopupManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<FieldManager, FieldManager>(Lifetime.Scoped)
                .AsImplementedInterfaces();
            
            builder.Register<IPlayerMovementManager, CursorPlayerMovementManager>(Lifetime.Scoped)
                .AsImplementedInterfaces();
            
            builder.Register<LivesManager>(Lifetime.Scoped);
            builder.Register<ScoreManager>(Lifetime.Scoped);
            
            builder.Register<CameraMovementManager, CameraMovementManager>(Lifetime.Scoped)
                .AsImplementedInterfaces();
            
            builder.Register<GameUIViewModel>(Lifetime.Scoped);
            builder.Register<GamePopupViewModel>(Lifetime.Scoped);

            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(movementSettings);
                componentsBuilder.AddInstance(gameSettings);
                
                componentsBuilder.AddInstance(inputCamera);
                componentsBuilder.AddInstance(playerView);
                componentsBuilder.AddInstance(inputPanel);

                componentsBuilder.AddInstance(fieldEntityPoolManager)
                    .AsImplementedInterfaces();

                componentsBuilder.AddInstance(wallsView);

                componentsBuilder.AddInstance(gamePopupManager);

                componentsBuilder.AddInstance(applicationSettings);
            });
            
            builder.UseEntryPoints(entryPointsBuilder =>
            {
                entryPointsBuilder.Add<GamePresenter>();
                entryPointsBuilder.Add<ApplicationPresenter>();
            });
        }
    }
}