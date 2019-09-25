using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Operation : MonoBehaviour
{
    private bool Mouse_DL=false;
    private bool Mouse_DR=false;
    private Vector3 Mouse_Position;

    // 位置座標
    private Vector3 position;

    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public bool MouseLeft()
    {
        Mouse_DL = false;
        if (Input.GetMouseButtonDown(0))
        {
            Mouse_DL = true;
        }
        return Mouse_DL;
    }

    public bool MouseRight()
    {
        Mouse_DR = false;
        if (Input.GetMouseButtonDown(1))
        {
            Mouse_DR = true;
        }
        return Mouse_DR;
    }

    public Vector2 MousePosition()
    {
        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;

        // Z軸修正
        position.z = 10f;

        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

        return screenToWorldPointPosition;
    }
}
