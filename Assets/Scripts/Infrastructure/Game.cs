using Infrastructure.Services;
using Infrastructure.States;
using Loader;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {

        public GameStateMachine StateMachine;
        public Game(Transform parent, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(), parent, curtain, AllServices.Container);
        }
    }
}