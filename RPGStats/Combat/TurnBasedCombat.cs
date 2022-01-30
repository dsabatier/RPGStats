using System;
using System.Collections.Generic;
using System.Linq;
using RPGStats.Characters;

namespace RPGStats.Combat
{
    public class TurnBasedCombat
    {
        public event Action OnPlayerAdded;
        public event Action OnEnemyAdded;
        public event Action OnBegin;
        public event Action OnComplete;
        
        private Character _player;
        private Character _enemy;

        private ICombatState _currentState;
        private List<ICombatState> _stateQueue = new List<ICombatState>();
        
        public void AddPlayer(Character player)
        {
            _player = player;
            OnPlayerAdded?.Invoke();
        }
        
        public void AddEnemy(Character enemy)
        {
            _enemy = enemy;
            OnEnemyAdded?.Invoke();
        }

        public Character GetPlayer()
        {
            return _player;
        }

        public Character GetEnemy()
        {
            return _enemy;
        }

        public void EnqueueState(ICombatState state)
        {
            if (_stateQueue.Contains(state))
                throw new DuplicateStateAddedException();
            
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            
            _stateQueue.Add(state);
        }

        public void Begin()
        {
            if (_stateQueue.Count == 0)
                throw new NoStatesAddedToCombatException();
            
            GoToNextState();
            
            OnBegin?.Invoke();
        }

        private void HandleStateComplete(ICombatState state)
        {
            if (_stateQueue.Count > 0)
            {
                GoToNextState();
            }
            else
            {
                OnComplete?.Invoke();
            }
        }

        private void GoToNextState()
        {
            if(_currentState != null)
                _currentState.OnComplete -= HandleStateComplete;
            
            _currentState = _stateQueue.First();
            _stateQueue.RemoveAt(0);
            _currentState.OnComplete += HandleStateComplete;
            
            _currentState.Begin();
        }
    }

    public class DuplicateStateAddedException : Exception
    {
    }

    public class NoStatesAddedToCombatException : Exception
    {
    }
}