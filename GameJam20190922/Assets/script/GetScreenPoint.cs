using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScreenPoint : MonoBehaviour
{
    public Camera maincamera;
    Vector3 TopLeft;
    Vector3 TopRight;
    Vector3 BottomLeft;
    Vector3 BottomRight;
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetScreenTopLeft();
        GetScreenTopRight();
        GetScreenBottomRight();
        GetScreenBottomLeft();
        Debug.Log("TopLeft");
        Debug.Log(TopLeft);
        Debug.Log("TopRight");
        Debug.Log(TopRight);
        Debug.Log("BottomRight");
        Debug.Log(BottomRight);
        Debug.Log("BottomLeft");
        Debug.Log(BottomLeft);
    }


    //画面の左上の位置座標を返す
    public Vector3 GetScreenTopLeft()
    {
        TopLeft = maincamera.ScreenToWorldPoint(Vector3.zero);
        return TopLeft;
    }

    public Vector3 GetScreenTopRight()
    {
        TopRight = maincamera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        return TopRight;
    }

    //画面の右下の位置座標を返す
    public Vector3 GetScreenBottomRight()
    {
        BottomRight = maincamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        return BottomRight;
    }

    //画面の右下の位置座標を返す
    public Vector3 GetScreenBottomLeft()
    {
        BottomLeft = maincamera.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f));
        return BottomRight;
    }
}
