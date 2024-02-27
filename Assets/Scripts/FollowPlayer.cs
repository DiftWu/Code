using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public Transform playerTransform; // 指向角色对象的Transform组件
    public Vector3 offset = new Vector3(0f, 2f, -5f); // 摄像机与角色的相对偏移

    void Update()
    {
        // 检查角色对象是否存在
        if (playerTransform != null)
        {
            // 设置摄像机的位置，使其始终保持相对偏移
            transform.position = playerTransform.position + offset;
        }
    }
}
