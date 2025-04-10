using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Deck;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Loader;
using Logic;
using Logic.Animations;
using Logic.GameBoard;
using Logic.GameProgress;
using Logic.Interactive;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, Transform parent, LoadingCurtain curtain,
            AllServices services)  
        {
            
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader, parent, curtain, 
                    services.Single<IAssetProvider>(), services.Single<IPersistentProgressService>(), 
                    services.Single<IGameFactory>(), services.Single<ICardsDeck>(), 
                    services.Single<ICardsListCreator>() ),
                
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), 
                    services.Single<ISaveLoadService>()),
                
                [typeof(PrepareGameState)] = new PrepareGameState( this, services.Single<ICardAnimator>(), 
                     services.Single<IOpenCardService>(),services.Single<IGameBoardLogicService>(), services.Single<IGameTimer>()),
                
                [typeof(PlayerTurnState)] = new PlayerTurnState(this, services.Single<IInputService>(), 
                    services.Single<IDragCardService>(), services.Single<IGameBoardLogicService>()),
                
                [typeof(GameProcessState)] = new GameProcessState(this, services.Single<IOpenCardService>(), 
                    services.Single<IMovesCounter>(), services.Single<ICardsDeck>(), services.Single<IEndGameService>(),
                    services.Single<IGameBoardLogicService>()),
                
                [typeof(EndGameState)] = new EndGameState(this, services.Single<IEndGameService>(), services.Single<IGameTimer>())
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}