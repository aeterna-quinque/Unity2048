using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameField _gameField;

        [SerializeField]
        private Vector2Int _fieldSize;

        public static event Action TurnEnded;

        private void Start()
        {
            _gameField.Initialize(_fieldSize);
            _gameField.SpawnPoints();
            _gameField.SpawnPoints();
        }

        private void OnEnable()
        {
            InputDetector.InputDetected += HandleInput;
        }

        private async void HandleInput(Vector2Int inputVector)
        {
            await _gameField.MoveTiles(inputVector);
            TurnEnded?.Invoke();
            _gameField.SpawnPoints();
        }
    }
}