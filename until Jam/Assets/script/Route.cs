﻿using System.Collections;
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

    Animator anim;

    public float walkSpeed;
    public float stopTime;
    public float ClockstopTime;
    private GameObject player;
    private GameObject Tansu;
    private GameObject deathTileMap;

    public int tileSize = 15;
    private Vector2Int _startNodeId;
    public Vector2Int goalNodeId;
    bool IsStopCoroutine = true;

    private int StopTimeCount = 0;

    /*一度だけ実行用変数*/
    bool AtOne = false;
    bool AtOneinClock = false;
    bool AtOne2 = false;

    private Vector2Int TansBlock;
    private Vector2Int TansTateBlock;

    touch Touch;
    Tilemap tilemap;
    Tilemap Death;
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
        /////////////////////////////////////
        /*////////GameObjectFind///////////*/
        /////////////////////////////////////
        player = GameObject.Find("Char");
        Tansu = GameObject.Find("NewWallManager");
        deathTileMap = GameObject.Find("DeathTileMap");
        Touch = Tansu.GetComponent<touch>();
        anim = player.GetComponent<Animator>();



        ///////////////////////////////////////
        /*//////////TileMapFind/////////////*/
        /////////////////////////////////////
        tilemap = GetComponent<Tilemap>();
        Death = deathTileMap.GetComponent<Tilemap>();


        //当たり判定の初期化
        InitSetLock(tilemap);
        DeathTileMap(Death);

        _startNodeId.x = (int)player.transform.position.x;
        _startNodeId.y = (int)player.transform.position.y - 1;

        //最短経路の計算
        RouteManager.Instance.SearchRoute(_startNodeId, goalNodeId, _routeList, tilemap);

    }

    private void SetLock(Vector2Int nodeId, bool isLock)
    {
        RouteManager.Instance.SetLock(nodeId, isLock);
    }

    private void SetDeath(Vector2Int nodeId, bool isDeath)
    {

        RouteManager.Instance.SetDeath(nodeId, isDeath);
    }

    private Coroutine _moveCoroutine;
    // Update is called once per frame
    void Update()
    {
        MousePos = Mouse.MousePosition();
        if((int)player.transform.position.x == goalNodeId.x && (int)player.transform.position.y == goalNodeId.y)
        {
            Debug.Log("LivePlayer!");
        }
        else if (RouteManager.Instance.CheckDeath((int)player.transform.position.x, (int)player.transform.position.y))
        {
            Debug.Log("DeathPlayer!");
        }

        RouteUpdate();

        //キャラクターが止まっていないとき。
        if (IsStopCoroutine == true)
        {
            IsStopCoroutine = false;
            _moveCoroutine = StartCoroutine(Move());
            _startNodeId = goalNodeId;
        }

        if (Time.timeScale == 0)
        {
            Debug.Log("A");
            if(StopTimeCount >= 120)
            {
                Time.timeScale = 1;
            }
            StopTimeCount++;
        }
    }

    IEnumerator Move()
    {
        var wait = new WaitForSeconds(walkSpeed);
        Vector3 pos = transform.position;
        for (int i = 0; i < _routeList.Count; i++)
        {

            var nodeId = _routeList[i];         //←これが進みかたを格納したリスト　（俺のもこの書き方はできる）
            Transform transform = player.gameObject.transform;
           
            if (pos.y > nodeId.y)
            {
                anim.Play("F");

            }
            else if (pos.y < nodeId.y)
            {
                anim.Play("B");

            }
            else if (pos.x > nodeId.x)
            {
                anim.Play("R");

            }
            else if (pos.x < nodeId.x)
            {
                anim.Play("L");

            }
            pos.x = nodeId.x;
            pos.y = nodeId.y;
            pos.z = 0;
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

        for (int y = bound.max.y - 1; y >= bound.min.y; --y)
        {
            for (int x = bound.min.x; x < bound.max.x; ++x)
            {
                var tile = tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));

                if (tile == null)
                {

                }
                else
                {
                    var position = new Vector3Int(x, y, 0);
                    SetLock(new Vector2Int((x + (int)(tilemap.transform.position.x) + 1), (y + (int)(tilemap.transform.position.y) + 1)), true);
                }
            }

        }


    }

    void DeathTileMap(Tilemap Death)
    {
        var bound = tilemap.cellBounds;
        for (int y = bound.max.y - 1; y >= bound.min.y; --y)
        {
            for (int x = bound.min.x; x < bound.max.x; ++x)
            {
                var tile = Death.GetTile<Tile>(new Vector3Int(x, y, 0));

                if (tile == null)
                {

                }
                else
                {
                    var position = new Vector3Int(x, y, 0);
                    SetDeath(new Vector2Int((x + (int)(Death.transform.position.x)), (y + (int)(Death.transform.position.y))), true);

                }
            }

        }

    }

    void RouteUpdate()
    {
        if (Touch.Tansu == true && AtOne == false)
        {
            //一度だけ実行
            AtOne = true;
            //最短経路リストの更新
            RouteManager.Instance.Initialize(tileSize);
            InitSetLock(tilemap);
            DeathTileMap(Death);
            _routeList.Clear();
            _routeList = new List<Vector2Int>();
            _startNodeId = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.y);
            if (Touch.TansuTouch())
            {

                SetLock(new Vector2Int((int)TansTateBlock.x, (int)TansTateBlock.y + 1), true);
                SetLock(new Vector2Int((int)MousePos.x, (int)MousePos.y + 2), true);

            }
            //壁の更新

            SetLock(new Vector2Int((int)MousePos.x + 1, (int)MousePos.y), true);
            SetLock(new Vector2Int((int)MousePos.x + 2, (int)MousePos.y), true);

            TansBlock = new Vector2Int((int)MousePos.x, (int)MousePos.y);
            //キャラクターを一瞬止める
            StopCoroutine(_moveCoroutine);

            Invoke("RestartRoutine", stopTime);
            walkSpeed -= 0.15f;
        }

        if (Touch.TansuTouch() && AtOne2 == false)
        {
            //一度だけ実行
            AtOne2 = true;
            //最短経路リストの更新
            RouteManager.Instance.Initialize(tileSize);
            InitSetLock(tilemap);
            DeathTileMap(Death);
            _routeList.Clear();
            _routeList = new List<Vector2Int>();
            _startNodeId = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.y);

            if (Touch.Tansu == true)
            {

                SetLock(new Vector2Int((int)TansBlock.x + 1, (int)TansBlock.y), true);
                SetLock(new Vector2Int((int)TansBlock.x + 2, (int)TansBlock.y), true);

            }
            //壁の更新

            SetLock(new Vector2Int((int)MousePos.x, (int)MousePos.y + 1), true);
            SetLock(new Vector2Int((int)MousePos.x, (int)MousePos.y), true);
            SetLock(new Vector2Int((int)MousePos.x, (int)MousePos.y + 2), true);

            TansTateBlock = new Vector2Int((int)MousePos.x, (int)MousePos.y);

            //キャラクターを一瞬止める
            StopCoroutine(_moveCoroutine);

            Invoke("RestartRoutine", stopTime);
            
        }

        if (Touch.ClockTouch() && !(AtOneinClock))
        {
            
            AtOneinClock = true;
           
            Time.timeScale = 0;
           
        }

    }
}