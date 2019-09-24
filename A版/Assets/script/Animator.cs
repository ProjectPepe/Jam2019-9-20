using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    Animation anim;

    GameObject CubeManager;

    Touch moushon;

    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();

        CubeManager = GameObject.Find("CubeManager");

        moushon = CubeManager.GetComponent<Touch>();

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (moushon.Moushon())
    //    {
    //        anim.Play();
    //    }
    //}
}
