using Assets.Scripts.Game.TileControllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    public class GameField : MonoBehaviour
    {
        [SerializeField]
        private Tile _tilePrefab;

        [SerializeField]
        private GridLayoutGroup _tilesContainer;

        [SerializeField]
        private List<SwipeStrategy> _strategiesList;

        private Tile[,] _tiles;
        private List<Tile> _emptyTiles;

        public void Initialize(Vector2Int size)
        {
            _tiles = new Tile[size.x, size.y];
            _emptyTiles = new List<Tile>();
            _tilesContainer.constraintCount = size.x;

            for (int y = 0, i = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++, i++)
                {
                    Tile tile = Instantiate(_tilePrefab, _tilesContainer.transform, false);
                    tile.Position = new Vector2Int(x, y);
                    _tiles[x, y] = tile;
                    if (y > 0)
                    {
                        Tile.ConnectUpperLowerTiles(tile, _tiles[x, y - 1]);
                    }
                    if (x > 0)
                    {
                        Tile.ConnectLeftRightTiles(tile, _tiles[x - 1, y]);
                    }
                    _emptyTiles.Add(tile);
                }
            }
        }

        public void SpawnPoints()
        {
            int randomIndex = Random.Range(0, _emptyTiles.Count - 1);
            Tile tile = _emptyTiles[randomIndex];
            tile.SpawnPoints();
            _emptyTiles.Remove(tile);
        }

        public async Task MoveTiles(Vector2Int inputVector)
        {
            SwipeStrategy strategy = _strategiesList.Find(s => s.Vector == inputVector);
            if (strategy == null) return;
            await strategy.MoveTiles(_tiles);
        }

        private void OnEnable()
        {
            Tile.TileCleared += (tile) => _emptyTiles.Add(tile);
            Tile.PointsPassed += (tile) => _emptyTiles.Remove(tile);
        }
    }
}