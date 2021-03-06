using System.Collections.Generic;
using Components.BaseComponents;
using Extensions;
using Managers;
using UnityEngine;

namespace Components.GameStateComponents
{
    public class GameStateActivationComponent : BaseButtonComponent
    {
        [SerializeField]
        private List<GameState> _targetGameStates = new List<GameState>();
        [SerializeField]
        private bool _isReverted;

        public override void Initialize()
        {
            base.Initialize();

            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    GameStateManager.Instance.GameStateChanged += GameStateManagerOnGameStateChanged;
                    GameStateManagerOnGameStateChanged(GameStateManager.Instance.GameState);
                },
                () => GameStateManager.Instance != null &&
                      GameStateManager.Instance.IsInitialized);
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.GameStateChanged -= GameStateManagerOnGameStateChanged;
            }
        }

        private void Redraw(bool isActive)
        {
            _button.interactable = isActive;
        }

        private void GameStateManagerOnGameStateChanged(GameState gameState)
        {
            var isActive = _targetGameStates.Contains(gameState);
            if (_isReverted)
            {
                isActive = !isActive;
            }
            Redraw(isActive);
        }
    }
}
