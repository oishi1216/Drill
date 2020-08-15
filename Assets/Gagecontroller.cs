using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gagecontroller : MonoBehaviour {

    //タッチの判定を入れる
    private bool touch = false;

    // ゲームオーバテキスト
    private GameObject gameOverText;
    //ゲームオーバーの判定
    private bool isGameOver = false;

    private RectTransform rect;

    //コンボテキスト
    private GameObject comboText;
    //コンボ数を入れる
    private int combo = 0;

    //スコアテキスト
    private GameObject scoreText;
    //スコアを入れる
    private float score = 0;

    //ゲージの長さを入れる
    int i = 0;
    //ゲージの往復回数を入れる
    int number = 0;

    // Use this for initialization
    void Start () {
        // シーンビューからオブジェクトの実体を検索する
        this.gameOverText = GameObject.Find("GameOver");
        this.comboText = GameObject.Find("Combo");
        this.scoreText = GameObject.Find("Score");
        rect = GetComponent<RectTransform>();
        //ゲージの伸縮を開始
        StartCoroutine("Gauge");
    }

    // Update is called once per frame
    void Update () {

        //タッチしていないかつゲームオーバーじゃない状態の時ゲージを伸縮させる
        if (touch == false && isGameOver == false) {
            rect.sizeDelta = new Vector2(i, rect.sizeDelta.y);
        }


        if(touch == false)
        {
            //ゲージが1往復するまでに
            if (number < 1)
            {
                //タッチをすると
                if (Input.GetMouseButtonDown(0))
                {

                    //ゲージの動きを止める
                    rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);

                    //ゲージが74～90までで止まった場合
                    if (rect.sizeDelta.x >= 74 && rect.sizeDelta.x <= 90)
                    {
                        //コンボに1追加
                        combo += 1;
                        if(combo >= 2)
                        { 
                        //コンボ数を表示
                        this.comboText.GetComponent<Text>().text = combo -1 + "COMBO";
                        //コンボテキストを表示
                        comboText.SetActive(true);
                        }
                        //スコアを計算
                        Score();

                        //1秒後にゲージを初期化
                        StartCoroutine("Initialization");
                    }
                    //ゲージが64以上74未満で止まった場合もしくは90より上100以下で止まった場合
                    else if(rect.sizeDelta.x >= 64 && rect.sizeDelta.x < 74 || rect.sizeDelta.x > 90 && rect.sizeDelta.x <= 100)
                    {
                        //コンボテキストを非表示
                        comboText.SetActive(false);
                        //コンボ数を初期化
                        combo = 0;
                        //スコアを計算
                        Score();

                        //1秒後にゲージを初期化
                        StartCoroutine("Initialization");

                    }
                    //上記条件以外で止まった場合
                    else
                    {
                        //ゲームオーバーを表示
                        GameOver();

                    }

                }
                //ゲージが1往復しても何もしないと
            }
            else
            {
                //ゲームオーバーを表示
                GameOver();

            }
        }
    }

    public void GameOver()
    {
        //コンボテキストを非表示
        comboText.SetActive(false);
        //コンボ数を初期化
        combo = 0;
        //ゲージを初期化
        rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);
        // ゲームオーバになったときに、画面上にゲームオーバを表示する
        this.gameOverText.GetComponent<Text>().text = "GameOver";
        //ゲームオーバーをtrueに変更
        isGameOver = true;

    }

    public void Score()
    {
        //コンボ数に応じてスコアを加算
        if (combo >= 2 && combo < 6)
        {
            score += 15;
        }
        else if (combo >= 6 && combo < 11)
        {
            score += 20;
        }
        else if (combo >= 11 && combo < 21)
        {
            score += 30;
        }
        else if (combo >= 21)
        {
            score += 50;
        }
        else
        {
            score += 10;
        }

        //獲得したスコアを表示
        this.scoreText.GetComponent<Text>().text = "Score " + score + "pt";
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
        //ゲージを初期化
        rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);
        i = 0;
        //往復数を初期化
        number = 0;
        //ゲージの伸縮を開始
        StartCoroutine("Gauge");
    }

}
