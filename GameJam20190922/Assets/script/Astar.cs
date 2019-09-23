using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//juamu no hou
public struct Node
{
    public Vector2Int From;    //親のノード
    public Vector2Int NodeID;
    public bool isLock { get; private set; }    
    public bool isActive { get; private set; }  

    public double MoveCost { get; private set; }    //移動量
    public double HeuristickCost;                    //ゴールまでの予測量

    internal static Node CreateBlankNode(Vector2Int position)
    {
        return new Node(position, new Vector2Int(-1, -1));
    }

    internal static Node CreateNode(Vector2Int position, Vector2Int goalPosition)
    {
        return new Node(position, goalPosition);
    }

    internal Node(Vector2Int nodeId, Vector2Int goalNodeId) : this()

    {

        NodeID = nodeId;
        isLock = false;
        Remove();
        MoveCost = 0;
        UpdateGoalNodeID(goalNodeId);

    }
    public void UpdateGoalNodeID(Vector2Int goal)  //ゴールノードをアップデート
    {
        HeuristickCost = Mathf.Sqrt(
            Mathf.Pow(goal.x - NodeID.x, 2) +
            Mathf.Pow(goal.y - NodeID.y, 2));
       // Debug.Log("HeuristicCost:"+HeuristickCost);
    }

    public double GetScore()            //このスコアをもとに移動する。
    {
        return (MoveCost + HeuristickCost);
    }

    public double UpdateMoveCost(double cost)       //移動量の更新
    {
        MoveCost = cost;
        return MoveCost;
    }

    public void Remove()
    {
        isActive = false;
    }

    public void Add()
    {
        isActive = true;
    }

    public void Clear()       //リセット。
    {
        Remove();
        MoveCost = 0;
        UpdateGoalNodeID(new Vector2Int(-1, -1));
    }
}

public class Astar : MonoBehaviour
{
    public int fieldSize;               //ぐっちの作ってくれたタイルマップに相当する。
    public Node[,] Nodes;
    public Node[,] OpenNodes;
    public Node[,] CloseNodes;

    //初期化
    public void InitNode(int size)
    {
        fieldSize = size;
        Nodes = new Node[fieldSize, fieldSize];
        OpenNodes = new Node[fieldSize, fieldSize];
        CloseNodes = new Node[fieldSize, fieldSize];

        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                Nodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                OpenNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
                CloseNodes[x, y] = Node.CreateBlankNode(new Vector2Int(x, y));
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //ルート探索。
    public bool SearchRoute(Vector2Int StartNodeID, Vector2Int GoalNodeID,List<Vector2Int> RouteList,List<Sprite> spList)
    {
       // ResetNode();
        if (StartNodeID == GoalNodeID)
        {
            return false;
        }

        //ノードの更新
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                Nodes[x, y].UpdateGoalNodeID(GoalNodeID);
                OpenNodes[x, y].UpdateGoalNodeID(GoalNodeID);
                CloseNodes[x, y].UpdateGoalNodeID(GoalNodeID);
            }
        }

        // スタート地点の初期化
        OpenNodes[StartNodeID.x, StartNodeID.y] = Node.CreateNode(StartNodeID, GoalNodeID);
        OpenNodes[StartNodeID.x, StartNodeID.y].From = StartNodeID;
        Debug.Log("StartNodeID:" + StartNodeID);
        OpenNodes[StartNodeID.x, StartNodeID.y].Add();

        while (true)
        {
            //Vector2int 型
            var bestScoreNodeId = GetBestScoreNodeID();
            OpenNode(bestScoreNodeId, GoalNodeID,spList);
            // ゴールに辿り着いたら終了
            if (bestScoreNodeId == GoalNodeID)
            {
                break;
            }
            
        }
        ResolveRoute(StartNodeID, GoalNodeID, RouteList);
        return true;
    }

    void ResetNode()
    {
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                Nodes[x, y].Clear();
                OpenNodes[x, y].Clear();
                CloseNodes[x, y].Clear();
            }
        }
    }

    //周り、四方向のノードをOpenし、探索
    void OpenNode(Vector2Int BestNodeID, Vector2Int GoalNodeID,List<Sprite> spList)
    {
        for (int dx = -1; dx < 2; dx++)
        {
            for (int dy = -1; dy < 2; dy++)
            {
                int cx = BestNodeID.x + dx;
                int cy = BestNodeID.y + dy;

                if (!(CheckWalk(dx, dy, BestNodeID.x, BestNodeID.y, spList)))
                {
                    continue;
                }
                if (Nodes[cx, cy].isLock)
                {
                    continue;
                }
                var AddCost = dx * dy == 0 ? 1 : 1;
                Nodes[cx, cy].UpdateMoveCost(OpenNodes[BestNodeID.x, BestNodeID.y].MoveCost + AddCost);
                Nodes[cx, cy].From = BestNodeID;
                //Debug.Log(Nodes[cx, cy].MoveCost +"/" + Nodes[cx, cy].HeuristickCost);
                UpdateNodeList(cx, cy, GoalNodeID);
            }
        }

        // 展開が終わったノードは closed に追加する
        CloseNodes[BestNodeID.x, BestNodeID.y] = OpenNodes[BestNodeID.x, BestNodeID.y];
       // Debug.Log(OpenNodes[BestNodeID.x,BestNodeID.y].From);
        // closedNodesに追加
        CloseNodes[BestNodeID.x, BestNodeID.y].Add();
        // openNodesから削除
        OpenNodes[BestNodeID.x, BestNodeID.y].Remove();
    }

    bool CheckWalk(int dx, int dy, int x, int y,List<Sprite> spList)
    {
        if (dx == 0 && dy == 0) //その場にいる
        {
            return false;
        }

        int cx = x + dx;
        int cy = y + dy;

        if (cx < 0 || cx == fieldSize || cy < 0 || cy == fieldSize)
        {
            return false;
        }
        return true;
    }

    //新規ノードリストを更新
    void UpdateNodeList(int x, int y, Vector2Int GoalNodeID)
    {
        if (OpenNodes[x, y].isActive)
        {
           // Debug.Log("Cost:" + OpenNodes[x, y].GetScore() + " / " + Nodes[x, y].GetScore());
            if (OpenNodes[x, y].GetScore() > Nodes[x, y].GetScore())
            {
                //ノードを更新
                OpenNodes[x, y].UpdateMoveCost(Nodes[x, y].MoveCost);
                OpenNodes[x, y].From = Nodes[x, y].From;
                Debug.Log("0.From:" + Nodes[x, y].From);


            }
        }
        else if (CloseNodes[x, y].isActive)
        {
            if (CloseNodes[x, y].GetScore() > Nodes[x, y].GetScore())
            {
                CloseNodes[x, y].Remove();
                OpenNodes[x, y].Add();
                OpenNodes[x, y].UpdateMoveCost(Nodes[x, y].MoveCost);
                OpenNodes[x, y].From = Nodes[x, y].From;
                Debug.Log("1.From:" + Nodes[x, y].From);
            } 
        }
        else        //open にも　close でもない場合。
        {
            OpenNodes[x, y] = new Node(new Vector2Int(x, y), GoalNodeID);
            OpenNodes[x, y].From = Nodes[x, y].From;
            OpenNodes[x, y].UpdateMoveCost(Nodes[x, y].MoveCost);
            OpenNodes[x, y].Add();
            Debug.Log("2.From:" + Nodes[x, y].From);
        }
    }

    void ResolveRoute(Vector2Int StartNodeID, Vector2Int GoalNodeID, List<Vector2Int> Result)
    {
        if (Result == null)
        {
            Result = new List<Vector2Int>();
        }
        else
        {
            Result.Clear();
        }
        var node = CloseNodes[GoalNodeID.x, GoalNodeID.y];
        Result.Add(GoalNodeID);
      
        int TryCount = 1000;
        bool isSuccess = false;
        for(int Count = 0; Count < TryCount; Count++)
        {
            var BeforeNode = Result[0];
            if (BeforeNode == node.From)
            {
                Debug.LogError("同じポジションなので終了失敗" + BeforeNode + " / " + node.From + " / " + GoalNodeID);
                break;
            }

            if (node.From == StartNodeID)
            {
                Debug.Log("true");
                isSuccess = true;
                break;
            }
            else
            {
                Result.Insert(0,node.From);
            }

            node = CloseNodes[node.From.x, node.From.y];
            
        }

        if (isSuccess == false)

        {

            Debug.LogError("失敗" + StartNodeID + " / " + node.From);

        }
        return;
    }

    public Vector2Int GetBestScoreNodeID()
    {
        var result = new Vector2Int(0, 0);
        double min = double.MaxValue;

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                if (OpenNodes[x, y].isActive == false)
                {
                  //  Debug.Log("isActive:false");
                    continue;
                }
                if (min > OpenNodes[x, y].GetScore())
                {
                    //Debug.Log("isActive:true");
                    // 優秀なコストの更新(値が低いほど優秀)
                    min = OpenNodes[x, y].GetScore();
                    result = OpenNodes[x, y].NodeID;
                  //  Debug.Log("result"+result);
                }
            }
        }

        return result;
    }

    public void SetLock(Vector2Int lockNodeId, bool isLock)

    {

       // Nodes[lockNodeId.x, lockNodeId.y].SetIsLock(isLock);

    }

}
