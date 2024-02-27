using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneToLoad = "Begin2"; // 默认场景名称

    // Update is called once per frame
    void Update()
    {
        // 检测空格键是否被按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 进入下一个场景，使用公共变量中的场景名称
            SceneManager.LoadScene(sceneToLoad);
        }

        // 检测Esc键是否被按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 退出游戏
            Application.Quit();
        }
    }
}
