using System.Collections.Generic;
using System.Linq;

namespace Sokoban.Implementation
{
    internal class GameImplementation
    {
        private readonly Level _level;
        private readonly Stack<GameState> _states;
        private readonly Stack<GameState> _undoStack;

        public GameImplementation(Level level)
        {
            _level = level;
            _states = new Stack<GameState>(new[] { new GameState(level.PlayerStartingPosition, level.BoxStartingPositions) });
            _undoStack = new Stack<GameState>();
        }

        public void MovePlayer(Direction direction)
        {
            var currentState = GetCurrentState();
            var nextState = MovePlayerInternal(_level, currentState, direction);
            if (nextState != currentState)
            {
                _states.Push(nextState);
            }

            _undoStack.Clear();
        }

        private static GameState MovePlayerInternal(Level level, GameState currentState, Direction direction)
        {
            var nextPosition = currentState.PlayerPosition + direction;
            if (level.Walls.Contains(nextPosition))
            {
                return currentState;
            }

            if (!currentState.BoxPositions.Contains(nextPosition))
            {
                return new GameState(nextPosition, currentState.BoxPositions);
            }

            var behindNextPosition = nextPosition + direction;
            if (level.Walls.Contains(behindNextPosition) || currentState.BoxPositions.Contains(behindNextPosition))
            {
                return currentState;
            }

            var nextBoxPositions = currentState.BoxPositions.Add(behindNextPosition).Remove(nextPosition);
            return new GameState(nextPosition, nextBoxPositions);
        }

        public bool HasWon()
        {
            return GetCurrentState().BoxPositions.All(p => _level.TargetPositions.Contains(p));
        }

        public GameState GetCurrentState()
        {
            return _states.Peek();
        }

        public void Reset()
        {
            _undoStack.Clear();
            while (_states.Count > 1)
            {
                _states.Pop();
            }
        }

        public void Undo()
        {
            if (_states.Count > 1)
            {
                _undoStack.Push(_states.Pop());
            }
        }

        public void Redo()
        {
            if (_undoStack.Count > 0)
            {
                _states.Push(_undoStack.Pop());
            }
        }
    }
}
