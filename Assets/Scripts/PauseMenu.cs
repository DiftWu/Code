using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // ������ͣ�˵� UI ���

    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // �������ESC��
        {
            if (pauseMenuUI.activeSelf) // �����ͣ�˵��Ѿ����������Ϸ
            {
                ResumeGame();
            }
            else // ������ͣ��Ϸ
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // ��ͣ��Ϸ
        pauseMenuUI.SetActive(true); // ��ʾ��ͣ�˵�
    }

    public void ResumeGame() // ����ť���õļ�����Ϸ����
    {
        Time.timeScale = 1f; // �ָ���Ϸ�����ٶ�
        pauseMenuUI.SetActive(false); // ������ͣ�˵�
    }
}
