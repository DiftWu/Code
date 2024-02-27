using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public GameObject[] characterPrefabs; // 存放不同角色的预制体

    void Start()
    {
        // 从PlayerPrefs中读取选择的角色
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");

        // 实例化选择的角色预制体
        InstantiateCharacter(selectedCharacter, new Vector3(0, 0, -1));
    }

    void InstantiateCharacter(string characterName, Vector3 position)
    {
        // 根据选择的角色名称查找对应的预制体
        GameObject characterPrefab = GetCharacterPrefab(characterName);

        if (characterPrefab != null)
        {
            // 实例化角色，并设置位置
            Instantiate(characterPrefab, position, transform.rotation);

            // 这里你可以进一步处理实例化后的角色，比如设置旋转等
        }
    }

    GameObject GetCharacterPrefab(string characterName)
    {
        // 根据名称查找对应的预制体
        foreach (var prefab in characterPrefabs)
        {
            if (prefab.name == characterName)
            {
                return prefab;
            }
        }

        return null;
    }
}

