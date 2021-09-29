using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.TileControllers
{
    [CreateAssetMenu(menuName = "SwipeStrategy/Left")]
    public class SwipeLeftStrategy : SwipeStrategy
    {
        public override async Task MoveTiles(Tile[,] tiles)
        {
            for (int x = 1; x <= tiles.GetLength(0) - 1; x++)
            {
                for (int y = 0; y <= tiles.GetLength(1) - 1; y++)
                {
                    if (tiles[x, y].Points == null) continue;

                    await tiles[x, y].TryPassPoints(Direction.Left);
                }
            }
        }
    }
}
