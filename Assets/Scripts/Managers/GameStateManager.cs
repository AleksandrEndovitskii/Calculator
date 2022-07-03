using System;
using Helpers;
using UnityEngine;

namespace Managers
{
    public class GameStateManager : BaseManager<GameStateManager>
    {
        public event Action<GameState> GameStateChanged = delegate { };
        public GameState GameState
        {
            get => _gameState;
            private set
            {
                if (_gameState == value)
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_gameState}->{value}");
                }
                _gameState = value;

                GameStateChanged.Invoke(_gameState);
            }
        }
        private GameState _gameState;

        public override void Initialize()
        {
            GameState = GameState.None;

            IsInitialized = true;
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
        }
    }
}
