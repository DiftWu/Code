using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public int defense = 10;
    public int attack = 30;

    // 每次出场属性翻倍的次数
    private int appearanceCount = 0;

    // 属性翻倍的倍数
    private int attributeMultiplier = 2;

    void Start()
    {
        // 初始化当前血量
        currentHealth = maxHealth;

        // 根据出场次数翻倍属性
        IncreaseAttributes();
    }

    // Boss受伤方法
    public void TakeDamage(int damage)
    {
        // 根据防御力计算实际受到的伤害
        int actualDamage = Mathf.Max(0, damage - defense);

        // 减少血量
        currentHealth -= actualDamage;

        // 检查是否死亡
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Boss死亡方法
    void Die()
    {
        // 处理Boss死亡逻辑，比如播放死亡动画、掉落物品等
        Destroy(gameObject); // 销毁Boss对象
        IncreasePlayerExperience();
    }

    // 每次出场翻倍Boss属性
    void IncreaseAttributes()
    {
        // 翻倍属性
        attack *= attributeMultiplier;
        defense *= attributeMultiplier;
        maxHealth *= attributeMultiplier;
        currentHealth = maxHealth;
    }

    public void IncreaseAppearanceCount()
    {
        appearanceCount++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞的物体的标签
        if (other.gameObject.CompareTag("Player"))
        {
            // 触发角色碰撞逻辑
            HandlePlayerCollision(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            // 触发子弹碰撞逻辑
            HandleBulletCollision(other.gameObject);
        }
    }

    // 处理与子弹碰撞的逻辑
    void HandleBulletCollision(GameObject bullet)
    {
        // 获取子弹的伤害值
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        int bulletDamage = bulletScript.damage;

        // 减少Boss生命值
        TakeDamage(bulletDamage);

        // 在这里可以添加其他逻辑，比如销毁子弹对象等
        Destroy(bullet);
    }


    //处理与角色碰撞的逻辑
    void HandlePlayerCollision(GameObject player)
    {
        // 获取角色的攻击和防御值
        CharacterStats characterStats = player.GetComponent<CharacterStats>();
        int playerAttack = characterStats.attack;
        int playerDefense = characterStats.defense;

        // 计算减少的生命值
        int damage = Mathf.Max(0, (playerAttack - defense) / 10);

        // 减少Boss生命值
        SubtractHealth(damage);

        // 在这里可以添加其他逻辑，比如播放音效等
    }

    void SubtractHealth(int amount)
    {
        // 减少生命值
        currentHealth = Mathf.Max(0, currentHealth - amount);

        // 在这里可以添加其他逻辑，比如更新UI等

        // 检查boss是否死亡
        if (currentHealth <= 0)
        {
            Die(); // 如果生命值小于等于0，执行死亡逻辑
        }
    }

    void Update()
    {        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void IncreasePlayerExperience()
    {
        // 查找角色对象
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // 获取角色的 CharacterStats 组件
        CharacterStats characterStats = player.GetComponent<CharacterStats>();

        // 增加角色经验值
        characterStats.AddExperience(200);
    }
}
