using VContainer;
using VContainer.Unity;
using UnityEngine;

public class ProjectLifetimeScope : LifetimeScope
{
    private const string DefaultSrsKickDataResourcePath = "SRSKickData/DefaultSRSKickData";

    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private SRSKickData _srsKickData;
    [SerializeField] private TetriminoData[] _tetriminoDatas;

    protected override void Awake()
    {
        ResolveDefaultReferences();
        base.Awake();
    }

    private void OnValidate()
    {
        ResolveDefaultReferences();
    }

    protected override void Configure(IContainerBuilder builder)
    {
        ResolveDefaultReferences();

        builder.Register<GameContext>(Lifetime.Singleton);

        builder.RegisterInstance(_gameSettings);
        builder.RegisterInstance(_srsKickData);
        builder.RegisterInstance(_tetriminoDatas);
    }

    private void ResolveDefaultReferences()
    {
        if (_srsKickData == null)
        {
            _srsKickData = Resources.Load<SRSKickData>(DefaultSrsKickDataResourcePath);
        }
    }
}