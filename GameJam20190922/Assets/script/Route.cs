using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Route : MonoBehaviour
{
 
    public int tileSize = 15;
    public Vector2Int _currentNodeId;
    public Vector2Int goalNodeId;
    List<Vector2Int> _routeList = new List<Vector2Int>();
    List<Sprite> spList = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        // Astar初期化
        RouteManager.Instance.Initialize(tileSize);

        Tilemap tilemap = GetComponent<Tilemap>();
        OutputSpriteType(tilemap, spList);
    }

    // Update is called once per frame
    void Update()
    {
        if (RouteManager.Instance.SearchRoute(_currentNodeId, goalNodeId, _routeList,spList))
        {
           Debug.Log("yes");
        }

        _currentNodeId = goalNodeId;
    }

    public static void OutputSpriteType(Tilemap tilemap, List<Sprite> spriteList)
    {
        // 使われているSpriteをリストアップ
        var bound = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bound);
        for (int y = bound.max.y - 1; y >= bound.min.y; --y)
        {
            for (int x = bound.min.x; x < bound.max.x; ++x)
            {
                var tile = tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if (tile != null && !spriteList.Contains(tile.sprite))
                {
                    spriteList.Add(tile.sprite);
                }
            }
        }
    }
    }
