using Menu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private MenuView _menuView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_menuView);
        }
    }
}