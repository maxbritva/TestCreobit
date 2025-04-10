using Infrastructure.States;
using Loader;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
      
        private Game _game;
        
        private void Awake()
        {
            _game = new Game(transform, _loadingCurtain);
            _game.StateMachine.Enter<BootstrapState>();
        }

    
    }
}