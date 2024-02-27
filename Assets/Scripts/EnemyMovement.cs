using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float maxmoveSpeed = 5.5f;
    private Transform playerTransform;
    private float startTime;

    void Start()
    {
        // 获取角色的Transform组件
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 记录游戏开始的时间
        startTime = Time.time;

        // 检查是否找到了角色的Transform组件
        if (playerTransform == null)
        {
            Debug.LogError("Player not found!");
        }

        // 每隔半分钟调用一次 Speedup
        InvokeRepeating("Speedup", 0f, 30f);
    }

    void Update()
    {
        // 检查是否找到了角色的Transform组件
        if (playerTransform != null)
        {
            // 计算朝向角色的方向
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            // 移动Enemy
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void Speedup()
    {
        // 获取经过的时间
        float elapsedTime = Time.time - startTime;

        if (Mathf.FloorToInt(elapsedTime) % 30 == 0 && moveSpeed < maxmoveSpeed)
        {
            moveSpeed++;
        }
    }
}
