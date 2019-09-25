using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    GameObject GameManeger;
    Mouse_Operation Mouse;

    private Vector2 MousePos;
   
    private Vector3 TansuTate;                 //入力は左上の座標にしてね！！
    public GameObject Cube;
    private Vector3 TansuPos;
    private Vector3 TansuTatePos;
    private Vector3 ClockPos;
    public int TansuRange;
    public int ClockRangeX;
    public int ClockRangeY;
    public bool Tansu = false;
    public bool Tansu2 = false;
    public bool Clock = false;

    GameObject playertouch;

    // Start is called before the first frame update
    void Start()
    {
        GameManeger = GameObject.Find("GameManeger");
        Mouse = GameManeger.GetComponent<Mouse_Operation>();
        playertouch = GameObject.Find("Char");
        TansuPos = GameObject.Find("TansuSet").transform.position;
        TansuTatePos = GameObject.Find("TansuTateSet").transform.position;
        ClockPos = GameObject.Find("ClockPos").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //タンス横用をタッチしたとき
        if (Mouse.MouseLeft() && Tansu == false)
        {
            MousePos = Mouse.MousePosition();
            if (MousePos.x >= TansuPos.x && MousePos.x <= TansuPos.x + 1 && MousePos.y >= TansuPos.y - 1 && MousePos.y <=  TansuPos.y)
            {
                if (MousePos.y - playertouch.gameObject.transform.position.y < TansuRange && MousePos.y - playertouch.gameObject.transform.position.y > -(TansuRange) &&
                    MousePos.x - playertouch.gameObject.transform.position.x > -(TansuRange) && MousePos.x - playertouch.gameObject.transform.position.x < TansuRange)
                {
                    Cube.SetActive(true);
                    Tansu = true;
                }
            }
        }
        
        //タンス縦用をタッチしたとき
        if (Mouse.MouseLeft() && Tansu2 == false)
        {
            MousePos = Mouse.MousePosition();
            if (MousePos.x >= TansuTatePos.x && MousePos.x <= TansuTatePos.x + 1 && MousePos.y >= TansuTatePos.y - 1 && MousePos.y <= TansuTatePos.y)
            {
                if (MousePos.y - playertouch.gameObject.transform.position.y < TansuRange && MousePos.y - playertouch.gameObject.transform.position.y > -(TansuRange) &&
                    MousePos.x - playertouch.gameObject.transform.position.x > -(TansuRange) && MousePos.x - playertouch.gameObject.transform.position.x < TansuRange)
                {
                    Cube.SetActive(true);
                    Tansu2 = true;
                }
            }
        }

        //時計をタッチしたとき
        if (Mouse.MouseLeft() && Clock == false)
        {
            MousePos = Mouse.MousePosition();
            if (MousePos.x >= ClockPos.x && MousePos.x <= ClockPos.x + 1 && 
                MousePos.y >= ClockPos.y - 1 && MousePos.y <= ClockPos.y)
            {
                if (MousePos.y - playertouch.gameObject.transform.position.y < ClockRangeY && MousePos.y - playertouch.gameObject.transform.position.y > -(ClockRangeY) && 
                    MousePos.x - playertouch.gameObject.transform.position.x > -(ClockRangeX) && MousePos.x - playertouch.gameObject.transform.position.x < ClockRangeX)
                {
                    Clock = true;
                }
                
            }
        }

        ClockTouch();
        TansuTouch();
    }
    public bool Moushon()
    {
        if (Mouse.MouseLeft() && Tansu == false)
        {
            MousePos = Mouse.MousePosition();
            if (MousePos.x >= TansuTatePos.x && MousePos.x <= TansuTatePos.x + 1 && MousePos.y >= TansuTatePos.y - 1 && MousePos.y <= TansuTatePos.y)
            {
                if (MousePos.y - playertouch.gameObject.transform.position.y < TansuRange && MousePos.y - playertouch.gameObject.transform.position.y > -(TansuRange) &&
                    MousePos.x - playertouch.gameObject.transform.position.x > -(TansuRange) && MousePos.x - playertouch.gameObject.transform.position.x < TansuRange)
                {
                    Cube.SetActive(true);
                    Tansu = true;
                }
            }
        }
        return Tansu;
    }

    public bool ClockTouch()
    {
        if(Clock)
        {
            return true;
        }
        return false;
    }

    public bool TansuTouch()
    {
        if (Tansu2)
        {
            return true;
        }
        return false;
    }
}

