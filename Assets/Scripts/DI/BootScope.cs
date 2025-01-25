using Loader;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class BootScope : LifetimeScope
    {
        [SerializeField] private SceneLoader _sceneLoader;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sceneLoader);
        }
    }
}