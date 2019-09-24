using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSwitch : MonoBehaviour
{
    private GameObject obj;
    GetScreenPoint PointScript;
    Vector3 CameraTopLeft;
    Vector3 CameraTopRight;
    Vector3 CameraBottomLeft;
    Vector3 CameraBottomRight;
    Vector3 ViewSize;
    public float ObjectScale;
    Vector3 TileMax;
    Vector2 ReturnPos;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("GameManeger");
        PointScript = obj.GetComponent<GetScreenPoint>();
        CameraTopLeft = PointScript.GetScreenTopLeft();
        CameraTopRight = PointScript.GetScreenTopRight();
        CameraBottomLeft = PointScript.GetScreenBottomLeft();
        CameraBottomRight = PointScript.GetScreenBottomRight();
        GetViewSize();
    }


    // Update is called once per frame
    void Update()
    {

    }


    //入力された数をfloat型の座標に変換
    Vector2 TileAllocation(int Numx, int Numy)
    {
        Vector2 StartPoint;
        StartPoint.x = -((GetViewSize().x / 2) + (ObjectScale / 2));
        StartPoint.y = -((GetViewSize().y / 2)+ (ObjectScale/2));
        ReturnPos.x = (StartPoint.x * Numx);
        ReturnPos.y = (StartPoint.y * Numy);
        return ReturnPos;
    }

    // タイルの最大数
    Vector2 GetTileMax()
    {
        TileMax.x = (ViewSize.x / ObjectScale); //x個
        TileMax.y = (ViewSize.y / ObjectScale); //y個
        return TileMax;
    }

    //画面の大きさを返す
    Vector2 GetViewSize()
    {
        ViewSize.x = (CameraTopRight.x - CameraTopLeft.x);
        ViewSize.y = (CameraBottomLeft.y - CameraTopLeft.y);
        return ViewSize;

        for (int x = 0; x < TileMax.x; x++)
        {
            for (int y = 0; y < TileMax.y; y++)
            {

            }
        }
    }
     
}
