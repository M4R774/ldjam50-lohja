using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class World : ScriptableObject
{
    public int groundWidth;
    public int groundHeight;

    public Tilemap groundMap;
    public Tilemap terrainMap;
    public Tilemap objectMap;
    public Tilemap fireMap;

    public TileBase fireTile;

    public Fire[,] fires;
    public List<Fire> activeFires;

    public void InitMaps(Tilemap groundMap, Tilemap terrainMap, Tilemap objectMap, Tilemap fireMap)
    {
        this.groundMap = groundMap;
        this.terrainMap = terrainMap;
        this.objectMap = objectMap;
        this.fireMap = fireMap;

        groundWidth = groundMap.size.x;
        groundHeight = groundMap.size.y;
    }

    public void InitFires()
    {
        fires = new Fire[groundHeight, groundWidth];
        for (int y = 0; y < groundHeight; y++)
        {
            for (int x = 0; x < groundWidth; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (groundMap.HasTile(position))
                {
                    fires[y, x] = new Fire(fires, position);
                }
            }
        }
        for (int y = 0; y < fires.GetLength(0); y++)
        {
            for (int x = 0; x < fires.GetLength(1); x++)
            {
                Fire fire = fires[y, x];
                if (fire != null)
                {
                    fires[y, x].FindNeighbours();
                }
            }
        }
    }

    public void TickFire()
    {
        for (int y = 0; y < fires.GetLength(0); y++)
        {
            for (int x = 0; x < fires.GetLength(1); x++)
            {
                Fire fire = fires[y, x];
                if (fire != null)
                {
                    fires[y, x].PrintNeighbours();
                }
            }
        }
    }
}
