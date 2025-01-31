using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneContext : LifetimeScope
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private WallSpawner _wallSpawner;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_player);
        builder.RegisterComponent(_scoreManager);
        builder.RegisterComponent(_wallSpawner);
    }
}
