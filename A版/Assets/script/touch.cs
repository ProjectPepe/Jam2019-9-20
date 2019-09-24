using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    GameObject GameManeger;
    Mouse_Operation Mouse;

    private Vector3 MousePos;
    public Vector3 Pos;                 //入力は左上の座標にしてね！！

    public GameObject Cube;
    public bool Tansu = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManeger = GameObject.Find("GameManeger");

        Mouse = GameManeger.GetComponent<Mouse_Operation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.MouseLeft()&&Tansu==false)
        {
            MousePos = Mouse.MousePosition();
            if (MousePos.x >= Pos.x  && MousePos.x <= Pos.x + 1 && MousePos.y >= Pos.y - 1 && MousePos.y <= Pos.y )
            {
                Cube.SetActive(true);
                Tansu = true;
            }
        }
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
}

