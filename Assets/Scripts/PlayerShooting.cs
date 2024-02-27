using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public float shootInterval = 5f;
    public float timeBetweenBullets = 5f;

    private float nextShootTime;
    private CharacterStats characterStats;
    public List<GameObject> weaponPrefabs; // 存储不同武器对应的预制体


    void Start()
    {
        nextShootTime = Time.time + shootInterval;
        characterStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            StartCoroutine(ShootBullets());
            nextShootTime = Time.time + shootInterval;
        }
    }

    IEnumerator ShootBullets()
    {
        // 获取当前角色已装备的武器列表
        List<string> equippedWeapons = characterStats.GetEquippedWeapons();

        // 检查是否有装备的武器
        if (equippedWeapons.Count > 0)
        {
            // 循环遍历每一把已装备的武器进行射击
            foreach (string currentWeaponName in equippedWeapons)
            {
                // 获取对应子弹的预制体
                GameObject bulletPrefab = GetBulletPrefabByName(currentWeaponName);

                if (bulletPrefab != null)
                {
                    // 根据武器名称选择不同的实例化逻辑
                    switch (currentWeaponName)
                    {
                        case "光束环刀":
                            InstantiateBeamSwordBullet(bulletPrefab);
                            break;
                        case "冲锋枪":
                            InstantiateSubmachineGunBullet(bulletPrefab);
                            break;
                        case "反射器":
                            InstantiateReflectorBullet(bulletPrefab);
                            break;
                        case "喷气飞镖":
                            InstantiateJetDartBullet(bulletPrefab);
                            break;
                        case "地雷":
                            InstantiateLandmineBullet(bulletPrefab);
                            break;
                        case "战术步枪":
                            InstantiateTacticalRifleBullet(bulletPrefab);
                            break;
                        case "手榴弹":
                            InstantiateGrenadeBullet(bulletPrefab);
                            break;
                        case "飞弹":
                            InstantiateMissileBullet(bulletPrefab);
                            break;
                        default:
                            Debug.LogError("Unhandled weapon: " + currentWeaponName);
                            break;
                    }
                }
                else
                {
                    Debug.LogError("Bullet prefab not found for weapon: " + currentWeaponName);
                }
            }

            // 等待一定时间再进行下一轮射击
            yield return new WaitForSeconds(timeBetweenBullets);
        }
        else
        {
            Debug.LogWarning("No equipped weapons to shoot!");
        }
    }

    void InstantiateBeamSwordBullet(GameObject bulletPrefab)
    {
        // 光束环刀的实例化逻辑
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator InstantiateSubmachineGunBullet(GameObject bulletPrefab)
    {
        // 冲锋枪的实例化逻辑
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 获取子弹脚本
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        if (bulletScript != null)
        {
            bulletScript.moveSpeed = 10f;
            bulletScript.size = 0.1f;
            bulletScript.damage = 10;
        }

        // 等待0.1秒再发射下一发子弹
        yield return new WaitForSeconds(0.1f);
    }

    void InstantiateReflectorBullet(GameObject bulletPrefab)
    {
        // 反射器的实例化逻辑
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void InstantiateJetDartBullet(GameObject bulletPrefab)
    {
        // 喷气飞镖的实例化逻辑
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void InstantiateLandmineBullet(GameObject bulletPrefab)
    {
        // 地雷的实例化逻辑
        StartCoroutine(SpawnLandmineBullet(bulletPrefab));
    }

    IEnumerator SpawnLandmineBullet(GameObject bulletPrefab)
    {
        // 每秒实例化一个地雷
        while (true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    void InstantiateTacticalRifleBullet(GameObject bulletPrefab)
    {
        // 战术步枪的实例化逻辑，每次在角色的上下左右四个位置实例化4发子弹

        // 上方
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 0f));

        // 下方
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 180f));

        // 左方
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 90f));

        // 右方
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, -90f));
    }

    void InstantiateGrenadeBullet(GameObject bulletPrefab)
    {
        // 手榴弹的实例化逻辑，每次在角色的上下左右以及左上右上左下右下八个位置实例化8发子弹

        // 上
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.up);

        // 下
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.down);

        // 左
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.left);

        // 右
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.right);

        // 左上
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.up + Vector3.left);

        // 右上
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.up + Vector3.right);

        // 左下
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.down + Vector3.left);

        // 右下
        InstantiateBulletWithOffset(bulletPrefab, firePoint.position + Vector3.down + Vector3.right);
    }
    
    void InstantiateBulletWithOffset(GameObject bulletPrefab, Vector3 position)
    {
        // 实例化子弹
        Instantiate(bulletPrefab, position, firePoint.rotation);
    }

    void InstantiateMissileBullet(GameObject bulletPrefab)
    {
        // 飞弹的实例化逻辑
        for (int i = 0; i < 5; i++)
        {
            // 请根据实际需求修改子弹的位置和旋转
            Vector3 bulletPosition = firePoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            Quaternion bulletRotation = firePoint.rotation;

            // 实例化子弹
            Instantiate(bulletPrefab, bulletPosition, bulletRotation);
        }
    }

    GameObject GetBulletPrefabByName(string weaponName)
    {
        // 根据武器名称查找对应的预制体
        return weaponPrefabs.Find(prefab => prefab.name == weaponName);
    }
}
