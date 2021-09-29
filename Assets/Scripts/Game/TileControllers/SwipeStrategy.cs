using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.TileControllers
{
    public abstract class SwipeStrategy : ScriptableObject
    {
        [SerializeField]
        private Vector2Int _swipeVector;

        public Vector2 Vector => _swipeVector;

        public abstract Task MoveTiles(Tile[,] tiles);
    }
}
