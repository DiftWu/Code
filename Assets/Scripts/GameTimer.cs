using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private static GameTimer instance;

    public static GameTimer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameTimer>();
            }
            return instance;
        }
    }

    private float startTime;
    public Text timerText;

    void Start()
    {
        // 记录游戏开始的时间
        startTime = Time.time;
    }

    void Update()
    {
        // 计算经过的时间
        float elapsedTime = Time.time - startTime;

        // 将时间格式化为分钟:秒的形式
        string formattedTime = string.Format("{0:00}:{1:00}", Mathf.Floor(elapsedTime / 60), elapsedTime % 60);

        // 更新UI文本显示
        timerText.text = formattedTime;
    }
}
