using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;

public class Route : MonoBehaviour
{
    public float walkSpeed = 0.6f;
    private GameObject player;
    public int tileSize = 15;
    public Vector2Int _currentNodeId;
    public Vector2Int goalNodeId;
    Tilemap tilemap;
    List<Vector2Int> _routeList = new List<Vector2Int>();
    List<Sprite> spList = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        // Astar初期化
        RouteManager.Instance.Initialize(tileSize);
        player = GameObject.Find("Char");
        tilemap = GetComponent<Tilemap>();
        OutputSpriteType(tilemap, spList);

        InitSetLock(tilemap);
    }

    private void SetLock(Vector2Int nodeId, bool isLock)
    {
        RouteManager.Instance.SetLock(nodeId, isLock);
    }

    private Coroutine _moveCoroutine;
    // Update is called once per frame
    void Update()
    {
        if (RouteManager.Instance.SearchRoute(_currentNodeId, goalNodeId, _routeList, tilemap))
        {
            Debug.Log("yes");
            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(Move());
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
        Debug.Log("Max.X:" + bound.max.x);
        Debug.Log("Min.X:" + bound.min.x);
        Debug.Log("Min.Y:" + bound.min.y);
        Debug.Log("Max.Y:" + bound.max.y);
    }
    IEnumerator Move()
    {
        var wait = new WaitForSeconds(walkSpeed);

        for (int i = 0; i < _routeList.Count; i++)
        {
            var nodeId = _routeList[i];         //←これが進みかたを格納したリスト　（俺のもこの書き方はできる）
            var goal = new Vector3(nodeId.x, nodeId.y, 0);//たぶん3Dやから   xyz座標に置換
            Transform transform = player.gameObject.transform;
            Vector3 pos = transform.position;
            //unityちゃんが動く原理
            pos.x = goal.x;
            pos.y = goal.y;
            pos.z = goal.z;
            transform.localPosition = pos;

            //  Debug.Log(player.gameObject.transform.position);
            yield return wait;
        }
    }

    void InitSetLock(Tilemap tilemap)
    {
        var bound = tilemap.cellBounds;
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
                    Debug.Log("(" + "x:" + (x + 31) + "y:" + (y + 15) + ")" + tilemap.GetTile(position));
                    SetLock(new Vector2Int((x + 31), (y + 15)), true);
                   // var index = spriteList.IndexOf(tile.sprite);
                   // builder.Append(index);
                }
            }
            builder.Append("\n");
        }
    }
}
