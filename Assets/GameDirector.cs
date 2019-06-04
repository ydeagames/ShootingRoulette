using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public int totalCount;
    public int hitCount;

    int[] lucks = new int[6];
    float accuracy = 1;
    float luckaccuracy = 1;

    Text detailText;

    public void Luck(int rank)
    {
        lucks[rank]++;
    }

    // Use this for initialization
    void Start()
    {
        detailText = GameObject.Find("DetailText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // 命中率
        float acc = totalCount <= 0 ? 1 : ((float)hitCount / totalCount);
        // 大吉率
        //float excacc = totalCount <= 0 ? 1 : ((float)lucks[5] / totalCount);
        // 運
        float luckacc;
        {
            float luckScore = 0;
            int luckAll = 0;
            float[] luckTable = new float[6] { 0, .5f, 2.5f, 3.5f, 5.5f, 6f };
            for (int i = 0; i < 6; i++)
            {
                luckScore += luckTable[i] * lucks[i];
                luckAll += lucks[i];
            }
            luckacc = luckAll <= 0 ? 1 : luckScore / 6f / luckAll;
        }

        string rank = "D";
        if (acc > .99f && luckacc > .90f)
            rank = "SSS";
        else if (acc > .95f && luckacc > .6f)
            rank = "SS";
        else if (acc > .90f && luckacc > 1f / 6f)
            rank = "S";
        else if (acc > .80f && luckacc > 1f / 8f)
            rank = "A";
        else if (acc > .40f && luckacc > 1f / 12f)
            rank = "B";
        else if (acc > .20f && luckacc > 1f / 16f)
            rank = "C";
        if (totalCount <= 0)
            rank = "???";
        if (totalCount < 4)
            rank = string.Format("あと{0}回投げてください\n  (推定: {1})", 4 - totalCount, rank);

        accuracy = Mathf.Lerp(accuracy, acc, .1f);
        luckaccuracy = Mathf.Lerp(luckaccuracy, luckacc, .1f);
        detailText.text = string.Format("投げた回数: {0}\nヒット回数: {1}\n命中率: {2:f1}%\n運: {3:f1}%\n総合評価: {4}", totalCount, hitCount, accuracy * 100, luckacc * 100, rank);
    }

    public static GameDirector Get()
    {
        return GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }
}
