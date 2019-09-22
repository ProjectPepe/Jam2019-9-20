using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public int tileSize = 15;
    public Vector2Int _currentNodeId;
    public Vector2Int goalNodeId;
    public Astar astar;
    List<Vector2Int> _routeList = new List<Vector2Int>();

    // Start is called before the first frame update
    void Start()
    {
        RouteManager.Instance.Initialize(tileSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (RouteManager.Instance.SearchRoute(_currentNodeId, goalNodeId, _routeList))
        {
           // Debug.Log("yes");
        }

        _currentNodeId = goalNodeId;
    }
}
