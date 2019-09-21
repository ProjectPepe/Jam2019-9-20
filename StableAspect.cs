using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableAspect : MonoBehaviour
{
    private Camera cam;
    public float width = 28f;
    public float height = 15f;

    private float pixelPerUnit = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        float aspect = (float)Screen.height / (float)Screen.width;
        float bgAspect = height / width;

        //カメラのコンポーネントを取得
        cam = GetComponent<Camera>();

        //カメラのorthographicSizeを設定
        cam.orthographicSize = height / 2f / pixelPerUnit;

        if (bgAspect > aspect)
        {
            //倍率
            float bgScale = height / Screen.height;

            // viewport rectの幅
            float camWidth = width / (Screen.width * bgScale);

            // viewportRectを設定
            cam.rect = new Rect((1f - camWidth) / 2f, 0f, camWidth, 1f);
        }
        else
        {

            float bgScale = width / Screen.width;

            float camHeight = height / (Screen.height * bgScale);

            cam.rect = new Rect(0f, (1f - camHeight) / 2f, 1f, camHeight);
        }
    }
}
