using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.TileControllers
{
    [CreateAssetMenu(menuName = "SwipeStrategy/Right")]
    public class SwipeRightStrategy : SwipeStrategy
    {
        public override async Task MoveTiles(Tile[,] tiles)
        {
            for (int x = tiles.GetLength(1) - 2; x >= 0; x--)
            {
                for (int y = 0; y <= tiles.GetLength(0) - 1; y++)
                {
                    if (tiles[x, y].Points == null) continue;

                    await tiles[x, y].TryPassPoints(Direction.Right);
                }
            }
        }
    }
}