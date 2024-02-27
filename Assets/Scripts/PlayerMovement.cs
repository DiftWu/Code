using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterStats characterStats;

    void Start()
    {
        // 获取角色的CharacterStats组件
        characterStats = GetComponent<CharacterStats>();
        
        // 检查是否找到CharacterStats组件
        if (characterStats == null)
        {
            Debug.LogError("CharacterStats component not found!");
        }
    }

    private Vector2 moveDirection;

    void Update()
    {
        // 获取用户输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 计算移动方向
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // 移动角色
        transform.Translate(moveDirection * characterStats.moveSpeed * Time.deltaTime);
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}



