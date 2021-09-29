using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Game
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        private int? _points;

        private Dictionary<Direction, Tile> _neighbours = new Dictionary<Direction, Tile>();

        public static event Action<Tile> TileCleared;

        public static event Action<Tile> PointsPassed;

        public Vector2Int Position { get; set; }

        public int? Points
        {
            get => _points;
            private set
            {
                _points = value;
                _text.text = value.ToString();
            }
        }

        public bool Merged { get; private set; } = false;

        public void SpawnPoints()
        {
            float spawnProbability = Random.value;
            if (spawnProbability >= 0.8)
            {
                Points = 4;
            }
            else
            {
                Points = 2;
            }
        }

        public async Task TryPassPoints(Direction direction)
        {
            if (!_neighbours.ContainsKey(direction)) return;

            Tile neighbour = _neighbours[direction];

            if (neighbour.Points == null)
            {
                neighbour.Points = Points;
                PointsPassed?.Invoke(neighbour);
                Reset();
                await neighbour.TryPassPoints(direction);
            }
            else if (neighbour.Points == Points && !neighbour.Merged && !Merged)
            {
                neighbour.Points += Points;
                neighbour.Merged = true;
                Reset();
            }
        }

        private void Reset()
        {
            Points = null;
            TileCleared?.Invoke(this);
        }

        private void OnEnable()
        {
            Game.TurnEnded += () => Merged = false;
        }

        public static void ConnectUpperLowerTiles(Tile upper, Tile lower)
        {
            upper._neighbours[Direction.Down] = lower;
            lower._neighbours[Direction.Up] = upper;
        }

        public static void ConnectLeftRightTiles(Tile right, Tile left)
        {
            right._neighbours[Direction.Left] = left;
            left._neighbours[Direction.Right] = right;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
