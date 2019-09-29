﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RouteManager : MonoBehaviour
{
    // 簡易的なシングルトン
    public static RouteManager Instance
    {
        get { return _instance; }
    }
    private static RouteManager _instance;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private Astar _astar;

    public void Initialize(int tileSize)
    {
        _astar = new Astar();
        _astar.InitNode(tileSize);
    }

    public void SetLock(Vector2Int lockNodeId, bool isLock)
    {
        _astar.SetLock(lockNodeId, isLock);
    }

    public void SetDeath(Vector2Int lockNodeId, bool isDeath)
    {
        _astar.SetDeath(lockNodeId, isDeath);
    }

    public bool CheckDeath(int positionx, int positiony)
    {
        return _astar.CheckDeath(positionx, positiony);
    }


    public bool SearchRoute(Vector2Int startNodeId, Vector2Int goalNodeId, List<Vector2Int> result, Tilemap tilemap)
    {
        return _astar.SearchRoute(startNodeId, goalNodeId, result, tilemap);
    }
}