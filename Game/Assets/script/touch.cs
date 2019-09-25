using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    GameObject GameManeger;
    Mouse_Operation Mouse;

    private Vector2 MousePos;
    public Vector3 Pos;                 //入力は左上の座標にしてね！！
    public Vector3 ClockPos;
    public GameObject Cube;
    public int TansuRange;
    public int ClockRangeX;
    public int ClockRangeY;
    public bool Tansu = false;
    public bool Clock = false;

    GameObject playertouch;

    // Start is called before the first frame update
    void Start()
    {
        GameManeger = GameObject.Find("GameManeger");
        Mouse = GameManeger.GetComponent<Mouse_Operation>();
        playertouch = GameObject.Find("Char");

       
    }

    // Update is called once per frame
    void Update()
    {
        //タンスをタッチしたとき
        if (Mouse.MouseLeft() && Tansu == false)
        {
            MousePos = Mouse.MousePosition();
            if (MousePos.x >= Pos.x && MousePos.x <= Pos.x + 1 && MousePos.y >= Pos.y - 1 && MousePos.y <= Pos.y)
            {
                if (MousePos.y - playertouch.gameObject.transform.position.y < TansuRange)
                {
                    Cube.SetActive(true);
                    Tansu = true;
                }
            }
        }
        
        //時計をタッチしたとき
        if (Mouse.MouseLeft() && Tansu == false)
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
    }
    public bool Moushon()
    {
        if (Mouse.MouseLeft() && Tansu == false)
        {
            MousePos = Mouse.MousePosition();
            if (MousePos.x >= Pos.x && MousePos.x <= Pos.x + 1 && MousePos.y >= Pos.y - 1 && MousePos.y <= Pos.y)
            {
                
                    Cube.SetActive(true);
                    Tansu = true;
                
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
}

