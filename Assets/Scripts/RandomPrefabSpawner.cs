
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] prefabs; // 存储四个预制体
    public float spawnRadius = 5f; // 实例化的半径范围
    public int numberOfPrefabsToSpawn = 10; // 每次实例化的预制体数量
    public float spawnInterval = 5f; // 实例化的时间间隔

    void Start()
    {
        // 检查是否有可用的预制体
        if (prefabs.Length == 0)
        {
            Debug.LogError("No prefabs assigned to the array!");
            return;
        }

        // 启动重复的定时器
        InvokeRepeating("SpawnPrefabsAroundPlayer", 0f, spawnInterval);
    }

    void SpawnPrefabsAroundPlayer()
    {
        // 获取角色位置
        Vector3 playerPosition = transform.position;

        // 在每次调用时实例化指定数量的预制体
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            // 随机选择一个角度
            float randomAngle = Random.Range(0f, 360f);

            // 计算在圆上的位置
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0f) * spawnRadius;

            // 随机选择一个预制体
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject selectedPrefab = prefabs[randomIndex];

            // 在场景中实例化选择的预制体
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

