using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAnima : MonoBehaviour
{
    Animator anim;

    public bool a = true;
    //GameObject NewWallManager;

    //touch moushon;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        //   NewWallManager = GameObject.Find("NewWallManager");

        //   moushon = NewWallManager.GetComponent<touch>();

    }

    //Update is called once per frame
    void Update()
    {
        if (true && a)//範囲に入っていたら
        {
            anim.Play("sumairu");
            Invoke("play", 3);
        }
    }

    void play()
    {
        anim.Play("tuujou");

        a = false;
    }
}
