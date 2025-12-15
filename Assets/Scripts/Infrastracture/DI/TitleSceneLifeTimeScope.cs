using VContainer;
using VContainer.Unity;

public class TitleSceneLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // TitleScene専用の依存関係を登録します
    }
}
