using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMove : MonoBehaviour
{
    Vector3 AimPos;                                                 //マウス位置の格納先
    Vector3[] ObjSize = new Vector3[10];                            //オブジェクトサイズの配列サイズを決定
    public GameObject NULL { get; private set; }                    //NULL定義
    public GameObject[] obj = new GameObject[10];                   //オブジェクトの配列サイズを決定
    bool[] MoveFlag = new bool[10];                                 //移動フラグ
    public float TheWorld;                                          //一旦停止する時間

    // Start is called before the first frame update
    void Start()
    {
        int cnt = 0;
        //フラグの初期化
        for (int count = 0; count < 10; count++)
        {
            MoveFlag[count] = false;
        }
        MoveFlag[0] = true;
        
        //オブジェクトがある間
        while (obj[cnt] != NULL)
        {
            //配列のオブジェクトの大きさを格納
            ObjSize[cnt] = obj[cnt].transform.localScale;

            //配列をずらす
            cnt++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //左クリック入力時
        if (Input.GetMouseButton(0))
        {
            //クリック時の位置情報を格納
            Vector3 MousePotion = Input.mousePosition;

            //Z軸修正
            MousePotion.z = 10f;

            // マウス位置座標をスクリーン座標からワールド座標に変換する
            Vector3 WorldMousePosition = Camera.main.ScreenToWorldPoint(MousePotion);
            int cnt1 = 0;

            //オブジェクトがある間繰り返す
            while (obj[cnt1] != NULL)
            {
                //X軸の当たり判定
                if (WorldMousePosition.x >= (obj[cnt1].transform.position.x - (ObjSize[cnt1].x / 2) * 4) && WorldMousePosition.x <= (obj[cnt1].transform.position.x + (ObjSize[cnt1].x / 2) * 4))
                {
                    //Y軸の当たり判定
                    if (WorldMousePosition.y >= (obj[cnt1].transform.position.y - (ObjSize[cnt1].y / 2) * 4) && WorldMousePosition.y <= (obj[cnt1].transform.position.y + (ObjSize[cnt1].y / 2) * 4))
                    {
                        if (MoveFlag[cnt1] == true)
                        {
                            //マウスの位置情報を代入
                            AimPos = WorldMousePosition;
                            MoveFlag[cnt1 + 1] = true;

                            //指定した秒数後に関数を呼び出す
                            Invoke("Move", TheWorld);
                        }
                    }
                }
                //配列をずらす
                cnt1++;
            }
        }
    }

    //移動
    void Move()
    {
        //ゆっくり移動する
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, AimPos, 3.0f);
    }
}

