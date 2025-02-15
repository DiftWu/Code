using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // 引用暂停菜单 UI 面板

    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // 如果按下ESC键
        {
            if (pauseMenuUI.activeSelf) // 如果暂停菜单已经激活，继续游戏
            {
                ResumeGame();
            }
            else // 否则暂停游戏
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // 暂停游戏
        pauseMenuUI.SetActive(true); // 显示暂停菜单
    }

    public void ResumeGame() // 供按钮调用的继续游戏函数
    {
        Time.timeScale = 1f; // 恢复游戏正常速度
        pauseMenuUI.SetActive(false); // 隐藏暂停菜单
    }
}
