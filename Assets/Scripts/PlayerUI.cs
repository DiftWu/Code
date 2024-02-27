using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public CharacterStats playerStats; // 引用角色的 CharacterStats 组件
    public Text healthText;
    public Text levelText;
    public Text experienceText;

    // 新增的六个Text组件，用于显示武器信息
    public Text weaponText1;
    public Text weaponText2;
    public Text weaponText3;
    public Text weaponText4;
    public Text weaponText5;
    public Text weaponText6;

    void Start()
    {
        // 获取角色的 CharacterStats 组件
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();

        // 确保你在 UI 上分配了相应的 Text 组件
        if (healthText == null || levelText == null || experienceText == null ||
            weaponText1 == null || weaponText2 == null || weaponText3 == null ||
            weaponText4 == null || weaponText5 == null || weaponText6 == null)
        {
            Debug.LogError("UI Text components are not assigned in the inspector!");
        }
    }

    void Update()
    {
        // 更新显示的信息
        UpdateUI();
    }

    void UpdateUI()
    {
        // 显示角色血量、等级和经验
        healthText.text = "Health: " + playerStats.currentHealth + " / " + playerStats.maxHealth;
        levelText.text = "Level: " + playerStats.level;
        experienceText.text = "Experience: " + playerStats.experience + " / " + playerStats.maxExperience;

        // 显示角色装备的武器
        DisplayEquippedWeapons();
    }

    void DisplayEquippedWeapons()
    {
        // 获取角色当前装备的武器列表
        List<string> equippedWeapons = playerStats.GetEquippedWeapons();

        // 将武器信息显示在相应的Text上
        DisplayWeaponText(weaponText1, equippedWeapons, 0);
        DisplayWeaponText(weaponText2, equippedWeapons, 1);
        DisplayWeaponText(weaponText3, equippedWeapons, 2);
        DisplayWeaponText(weaponText4, equippedWeapons, 3);
        DisplayWeaponText(weaponText5, equippedWeapons, 4);
        DisplayWeaponText(weaponText6, equippedWeapons, 5);
    }

    void DisplayWeaponText(Text weaponText, List<string> equippedWeapons, int index)
    {
        // 如果武器列表中存在对应位置的武器，则显示在相应的Text上，否则显示为空
        if (index < equippedWeapons.Count)
        {
            weaponText.text = "Weapon " + (index + 1) + ": " + equippedWeapons[index];
        }
        else
        {
            weaponText.text = "Weapon " + (index + 1) + ": Empty";
        }
    }
}
