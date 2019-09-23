using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;
public class TileMap : MonoBehaviour
{
    public List<Sprite> spriteList = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {

        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        OutputSpriteType(tilemap, spriteList);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void OutputSpriteType(Tilemap tilemap, List<Sprite> spriteList)
    {
        // 使われているSpriteをリストアップ
        var bound = tilemap.cellBounds;
        //TileBase[] allTiles = tilemap.GetTilesBlock(bound);
        //for (int x = 0; x < bound.size.x; x++)
        //{
        //    for (int y = 0; y < bound.size.y; y++)
        //    {
        //        TileBase tile = allTiles[x + y * bound.size.x];
               
        //        var position = new Vector3Int(x, y, 0);
        //        Debug.Log("(" + "x:" + x + "y:" + y + ")" + tilemap.GetTile(position));
        //        //else
        //        //{
        //        //    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
        //        //}
        //    }
        //}
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
        // どの場所でそのSpriteが使われているかを出力
        var builder = new StringBuilder();
        for (int y = bound.max.y - 1; y >= bound.min.y; --y)
        {
            for (int x = bound.min.x; x < bound.max.x; ++x)
            {
                var tile = tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                
                if (tile == null)
                {
                    builder.Append("_");
                }
                else
                {
                    var position = new Vector3Int(x, y, 0);
                    Debug.Log("(" + "x:" + x + "y:" + y + ")" + tilemap.GetTile(position));

                    var index = spriteList.IndexOf(tile.sprite);
                    builder.Append(index);
                }
            }
            builder.Append("\n");
        }
        Debug.Log(builder.ToString());
    }
}
