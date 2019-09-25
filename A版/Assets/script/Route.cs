using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;

public class Route : MonoBehaviour
{
    //マウスポジション取得
    GameObject GameManeger;
    Mouse_Operation Mouse;
    private Vector2 MousePos;

    public float walkSpeed;
    public float stopTime;
    private GameObject player;
    private GameObject Tansu;
    
    public int tileSize = 15;
    public Vector2Int _startNodeId;
    public Vector2Int goalNodeId;
    bool IsStopCoroutine = true;
    bool AtOne = false;
    touch Touch;
    Tilemap tilemap;
    List<Vector2Int> _routeList = new List<Vector2Int>();
    List<Sprite> spList = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        //マウスポジションの取得
        GameManeger = GameObject.Find("GameManeger");
        Mouse = GameManeger.GetComponent<Mouse_Operation>();

        // Astar初期化
        RouteManager.Instance.Initialize(tileSize);
        player = GameObject.Find("Char");
        Tansu = GameObject.Find("NewWallManager");
        Touch = Tansu.GetComponent<touch>();
        tilemap = GetComponent<Tilemap>();

        //当たり判定の初期化
        InitSetLock(tilemap);

        //最短経路の計算
        RouteManager.Instance.SearchRoute(_startNodeId, goalNodeId, _routeList, tilemap);

    }

    private void SetLock(Vector2Int nodeId, bool isLock)
    {
        RouteManager.Instance.SetLock(nodeId, isLock);
    }

    private Coroutine _moveCoroutine;
    // Update is called once per frame
    void Update()
    {
        MousePos = Mouse.MousePosition();

        if (Touch.Tansu == true && AtOne == false)
        {
            //一度だけ実行
            AtOne = true;
            //最短経路リストの更新
            RouteManager.Instance.Initialize(tileSize);
            InitSetLock(tilemap);
            _routeList.Clear();
            _routeList = new List<Vector2Int>();
            _startNodeId = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.y);

            //壁の更新
            SetLock(new Vector2Int((int)MousePos.x, (int)MousePos.y + 1), true);
            SetLock(new Vector2Int((int)MousePos.x + 1, (int)MousePos.y + 1), true);
            SetLock(new Vector2Int(((int)MousePos.x + 2), ((int)MousePos.y + 1)), true);

            //キャラクターを一瞬止める
            Debug.Log((int)MousePos.x);
            Debug.Log((int)MousePos.y);
            StopCoroutine(_moveCoroutine);

           Invoke("RestartRoutine", stopTime);
        }

        //キャラクターが止まっていないとき。
        if (IsStopCoroutine == true )
        {           
            IsStopCoroutine = false;
            _moveCoroutine = StartCoroutine(Move());
            _startNodeId = goalNodeId;
        }
       
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
            pos.x = goal.x;
            pos.y = goal.y;
            pos.z = goal.z;
            transform.localPosition = pos;
            yield return wait;
        }
    }

    void RestartRoutine()
    {
        IsStopCoroutine = true;
        RouteManager.Instance.SearchRoute(_startNodeId, goalNodeId, _routeList, tilemap);
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
                    SetLock(new Vector2Int((x + (int)(tilemap.transform.position.x)+1), (y + (int)(tilemap.transform.position.y)+1)), true);
                }
            }
            builder.Append("\n");
        }

        
    }
}