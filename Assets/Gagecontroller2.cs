using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gagecontroller2 : MonoBehaviour
{

    bool touch = false;

    //ゲームオーバーの判定
    private bool isGameOver = false;

    private RectTransform rect;

    //ゲージの長さを入れる
    int i = 0;
    //ゲージの往復回数を入れる
    int number = 0;

    // Use this for initialization
    void Start()
    {
        rect = GetComponent<RectTransform>();
        //ゲージの伸縮を開始
        StartCoroutine("Gauge");
    }

    // Update is called once per frame
    void Update()
    {

        //タッチしていないかつゲームオーバーじゃない状態の時矢印を移動させる
        if (touch == false && isGameOver == false)
        {
            rect.transform.localPosition = new Vector2(i, rect.transform.localPosition.y);
        }

        //タッチがされていると
        if (touch == false)
        {
            //ゲージが1往復するまでに
            if (number < 1)
            {
                //タッチをすると
                if (Input.GetMouseButtonDown(0))
                {

                //矢印の動きを止める
                rect.transform.localPosition = new Vector2(rect.transform.localPosition.x, rect.transform.localPosition.y);
                //1秒後にゲージを初期化
                StartCoroutine("Initialization");

                    //ゲージの64以下で止めた場合はゲームオーバー処理をする
                    if (rect.transform.localPosition.x < 64)
                    {
                        //ゲームオーバーを処理
                        GameOver();
                    }

                }
            }
            else
            {
                //ゲームオーバーを処理
                GameOver();
            }
        }

    }

    public void GameOver()
    {
        //ゲージを初期化
        rect.transform.localPosition = new Vector2(0, rect.transform.localPosition.y);
        i = 0;
        //ゲームオーバーをtrueに変更
        isGameOver = true;

    }

    IEnumerator Gauge()
    {

        //ゲージをMAXまで増やす
        for (i = 0; i < 100; i = i + 5)
        {

            yield return null;

        }

        //ゲージをMINまで減らす
        for (i = 100; i > 0; i = i - 5)
        {

            yield return null;

        }

        //ゲージが一往復したら数を増やす
        number += 1;

        //タッチされた場合は処理を破棄
        if (touch == true)
        {
            yield break;
        }

    }

    IEnumerator Initialization()
    {

        //タッチ判定をtrueへ変更
        touch = true;

        //1秒停止
        yield return new WaitForSeconds(1);

        //タッチ判定をfalseに変更
        touch = false;
        //矢印の位置を初期化
        rect.transform.localPosition = new Vector2(0, rect.transform.localPosition.y);
        i = 0;
        //往復数を初期化
        number = 0;
        //ゲージの伸縮を開始
        StartCoroutine("Gauge");
    }

}