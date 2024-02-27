using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseSceneController : MonoBehaviour
{
    private string selectedCharacter; // 用于保存选择的角色名称

    void Start()
    {
        // 获取按钮组件
        Button button = GetComponent<Button>();

        // 添加按钮点击事件监听器
        button.onClick.AddListener(() => SelectCharacter(button));
    }

    // 当点击按钮时调用，每个按钮对应一个角色
    public void SelectCharacter(Button button)
    {
        // 获取按钮的名称，即角色名称
        selectedCharacter = button.name;

        // 保存选择的角色到 PlayerPrefs
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter);
        SceneManager.LoadScene("Game");
    }

    void Update()
    {
        
    }
}
