using Clicker;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class ClickerScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ClickCounter>(Lifetime.Singleton);
            builder.Register<ClickProgressSaver>(Lifetime.Singleton);
        }
    }
}