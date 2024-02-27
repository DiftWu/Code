using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Transform playerTransform; // 角色的Transform
    public float fastMoveSpeed = 20f; // 快速移动速度
    public float normalMoveSpeed = 5f; // 地图上随机移动速度
    public float fastMoveDuration = 3f; // 快速移动持续时间
    public float normalMoveDuration = 10f; // 地图上随机移动持续时间

    private float nextMoveTime;
    private bool isFastMoving;

    void Start()
    {
        // 获取角色的Transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // 初始化下一次移动的时间
        nextMoveTime = Time.time + normalMoveDuration;
    }

    void Update()
    {
        if (Time.time >= nextMoveTime)
        {
            if (isFastMoving)
            {
                // 快速移动结束，切换到地图上随机移动
                isFastMoving = false;
                nextMoveTime = Time.time + normalMoveDuration;
            }
            else
            {
                // 地图上随机移动结束，切换到快速移动
                isFastMoving = true;
                nextMoveTime = Time.time + fastMoveDuration;
            }
        }

        // 根据当前移动状态选择移动方式
        if (isFastMoving)
        {
            FastMoveToTarget();
        }
        else
        {
            RandomMove();
        }
    }

    // 快速移动到角色所在位置
    void FastMoveToTarget()
    {
        // 获取朝向角色的方向向量
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // 移动到角色位置
        transform.Translate(direction * fastMoveSpeed * Time.deltaTime);
    }

    // 地图上随机移动
    void RandomMove()
    {
        // 随机生成一个新的目标位置
        Vector3 randomTarget = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), -1f);

        // 获取朝向目标位置的方向向量
        Vector3 direction = (randomTarget - transform.position).normalized;

        // 移动到目标位置
        transform.Translate(direction * normalMoveSpeed * Time.deltaTime);
    }
}
