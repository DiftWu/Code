using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetSceneName;

    void Start()
    {
        // 获取按钮组件
        Button button = GetComponent<Button>();

        // 添加按钮点击事件监听器
        button.onClick.AddListener(SwitchScene);
    }

    // 切换场景的方法
    void SwitchScene()
    {
        // 使用场景名称切换场景
        SceneManager.LoadScene(targetSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
