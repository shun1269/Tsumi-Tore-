using Unity.VisualScripting;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private SRSKickData _srsKickData;
    [SerializeField] private TetriminoData[] _tetriminoDatas;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GameContext>(Lifetime.Singleton);

        builder.RegisterInstance(_gameSettings);
        builder.RegisterInstance(_srsKickData);
        builder.RegisterInstance(_tetriminoDatas);
    }
}