using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.TileControllers
{
    [CreateAssetMenu(menuName = "SwipeStrategy/Down")]
    public class SwipeDownStrategy : SwipeStrategy
    {
        public override async Task MoveTiles(Tile[,] tiles)
        {
            for (int y = 1; y <= tiles.GetLength(1) - 1; y++)
            {
                for (int x = 0; x <= tiles.GetLength(0) - 1; x++)
                {
                    if (tiles[x, y].Points == null) continue;

                    await tiles[x, y].TryPassPoints(Direction.Down);
                }
            }
        }
    }
}
