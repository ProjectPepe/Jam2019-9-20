using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    GameObject GameManeger;

    Mouse_Operation MousePos;
    // Start is called before the first frame update
    void Start()
    {
        GameManeger = GameObject.Find("GameManeger");

        MousePos = GameManeger.GetComponent<Mouse_Operation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MousePos.MouseLeft())
        {
            Debug.Log(MousePos.MousePosition());
        }
    }
}
