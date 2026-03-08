using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PracticeSceneLifeTimeScope : LifetimeScope
{
    private const string DefaultSrsKickDataResourcePath = "SRSKickData/DefaultSRSKickData";

    [SerializeField] private SRSKickData _srsKickData;
    [SerializeField] private TetriminoData[] _tetriminoDatas;

    private void OnValidate()
    {
        ResolveDefaultReferences();
    }

    protected override void Configure(IContainerBuilder builder)
    {
        ResolveDefaultReferences();

        builder.RegisterInstance(_srsKickData);
        builder.RegisterInstance(_tetriminoDatas);
        builder.Register<Field>(Lifetime.Scoped);
        builder.Register<MoveUseCase>(Lifetime.Scoped);
        builder.Register<RotateUseCase>(Lifetime.Scoped);
        builder.Register<MinoGenerator>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<PracticeSceneDebugController>();
    }

    private void ResolveDefaultReferences()
    {
        if (_srsKickData == null)
        {
            _srsKickData = Resources.Load<SRSKickData>(DefaultSrsKickDataResourcePath);
        }
    }
}
