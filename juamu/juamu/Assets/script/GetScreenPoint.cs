using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScreenPoint : MonoBehaviour
{
    public Camera maincamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //画面の左上の位置座標を返す
    public Vector3 GetScreenTopLeft()
    {
        Vector3 TopLeft = maincamera.ScreenToWorldPoint(Vector3.zero);
        TopLeft.Scale(new Vector3(1f, -1f, 1f));
        return TopLeft;
    }

    public Vector3 GetScreenTopRight()
    {
        Vector3 TopRight = maincamera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));                    
        TopRight.Scale(new Vector3(1f, -1f, 1f));                                                               //上下反転させる
        return TopRight;
    }

    //画面の右下の位置座標を返す
    public Vector3 GetScreenBottomRight()
    {
        Vector3 BottomRight = maincamera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0.0f));
        BottomRight.Scale(new Vector3(1f, -1f, 1f));
        return BottomRight;
    }

    //画面の右下の位置座標を返す
    public Vector3 GetScreenBottomLeft()
    {
        Vector3 BottomRight = maincamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        BottomRight.Scale(new Vector3(1f, -1f, 1f));
        return BottomRight;
    }
}
