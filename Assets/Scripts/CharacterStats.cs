using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int maxExperience = 5;
    public int maxExperienceCap = 2000;
    public int attack = 1;
    public int maxHealth = 10;
    public int currentHealth = 10;
    public int defense = 5;
    public float moveSpeed = 5f;

    // 武器相关
    public List<string> weaponNames = new List<string>  // 存储武器名称
    {
        //"变形战术步枪",
        "冲锋枪",
        "地雷",
        "反射器",
        "飞弹",
        "光束环刀",
        //"狙击步枪",
        //"榴弹发射器",
        //"能量场",
        //"能量行利刃",
        "喷气飞镖",
        "手榴弹",
        //"霰弹枪",
        "战术步枪"
    };
    private List<string> equippedWeapons = new List<string>(); // 当前装备的武器

    void Start()
    {
        currentHealth = maxHealth;
        string newWeaponName = GetRandomWeaponName();
    }

    void Update()
    {
        if (experience >= maxExperience && level < 30)
        {
            LevelUp();
        }
    }

    // 升级逻辑，包括从武器库中随机选择武器
    void LevelUp()
    {
        level++;
        maxExperience = Mathf.Min(maxExperience * 2, maxExperienceCap);
        attack += 1;
        maxHealth += 5;
        currentHealth = maxHealth;
        defense += 1;

        // 检查武器数量是否已达到上限
        if (equippedWeapons.Count < 6)
        {
            // 从武器库中随机选择一把武器
            string newWeaponName = GetRandomWeaponName();
        } 
    }

    public void AddExperience(int amount)
    {
        experience += amount;
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Character died!");
        GameOver();
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HandleEnemyCollision(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            HandleBossCollision(other.gameObject);
        }
    }

    void HandleEnemyCollision(GameObject enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        int enemyAttack = enemyStats.attack;
        int enemyDefense = enemyStats.defense;
        int damage = Mathf.Max(1, (enemyAttack - defense) / 10);
        SubtractHealth(damage);
    }

    void HandleBossCollision(GameObject boss)
    {
        BossStats bossStats = boss.GetComponent<BossStats>();
        int bossAttack = bossStats.attack;
        int bossDefense = bossStats.defense;
        int damage = Mathf.Max(1, (bossAttack - defense) / 10);
        SubtractHealth(damage);
    }

    void SubtractHealth(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 从武器库中随机选择一把武器的名称
    string GetRandomWeaponName()
    {
        // 检查武器库中是否还有未装备的武器
        if (weaponNames.Count > equippedWeapons.Count)
        {
            List<string> availableWeapons = new List<string>(weaponNames);
            availableWeapons.RemoveAll(weapon => equippedWeapons.Contains(weapon));

            // 检查是否还有可选的武器
            if (availableWeapons.Count > 0)
            {
                int randomIndex = Random.Range(0, availableWeapons.Count);
                equippedWeapons.Add(availableWeapons[randomIndex]);
                return availableWeapons[randomIndex];
            }
        }

        // 如果所有武器都已经装备，返回 null 或者其他适当的值
        return null;
    }


    public List<string> GetEquippedWeapons()
    {
        return equippedWeapons;
    }
}
