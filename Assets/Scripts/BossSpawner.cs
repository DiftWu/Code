using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // Boss的预制体
    public Transform playerTransform; // 角色的 Transform
    public float spawnRadius = 8f; // 调整这个值以确定生成 Boss 的距离
    private float startTime;

    void Start()
    {
        // 获取角色的 Transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 记录游戏开始的时间
        startTime = Time.time;

        // 每隔一分钟调用一次 SpawnBoss
        InvokeRepeating("SpawnBoss", 0f, 60f);
    }

    void SpawnBoss()
    {
        // 获取经过的时间
        float elapsedTime = Time.time - startTime;

        // 检查 elapsedTime 是否为 60 的倍数
        if (Mathf.FloorToInt(elapsedTime) % 60 == 0)
        {
            // 在角色周围创建一个环
            Vector3 spawnOffset = Random.onUnitSphere * spawnRadius;
            spawnOffset.y = 0f; // 保持在同一平面上
            Vector3 spawnPosition = playerTransform.position + spawnOffset;

            // 实例化 Boss 预制体
            GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

            // 获取 BossStats 脚本并更新出场次数
            BossStats bossStats = boss.GetComponent<BossStats>();
            if (bossStats != null)
            {
                bossStats.IncreaseAppearanceCount();
            }
            else
            {
                Debug.LogError("BossStats script not found on the spawned boss!");
            }
        }
    }
}
