using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float size = 0.5f;
    public int damage = 10;
    public Vector2 playerDirection;

    // 添加子弹预制体的名字
    public string bulletName;

    private Transform playerTransform;


    void Start()
    {
        // 初始化子弹的尺寸
        transform.localScale = new Vector3(size, size, 1);

        // 获取角色移动脚本
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        // 获取角色的移动方向
        playerDirection = playerMovement.GetMoveDirection();

        // 获取角色的 Transform
        playerTransform = playerMovement.transform;
    }

    void Update()
    {
        // 根据子弹的名字决定不同的射出方式
        switch (bulletName)
        {
            case "光束环刀":
                // 使用光束环刀的射出方式
                BeamSwordMove();
                break;

            case "冲锋枪":
                // 使用冲锋枪的射出方式
                SubmachineGunMove();
                break;

            case "反射器":
                // 使用反射器的射出方式
                ReflectiveMove();
                break;

            case "喷气飞镖":
                // 使用喷气飞镖的射出方式
                JetDartMove();
                break;

            case "地雷":
                // 使用地雷的射出方式
                LandmineMove();
                break;

            case "战术步枪":
                // 使用战术步枪的射出方式
                TacticalRifleMove();
                break;

            case "手榴弹":
                // 使用手榴弹的射出方式
                GrenadeMove();
                break;

            case "飞弹":
                // 使用飞弹的射出方式
                MissileMove();
                break;

            // 添加其他子弹的射出方式

            default:
                // 默认使用向前移动
                MoveForward();
                break;
        }
    }

    // 光束环刀的射出方式
    private float angle; // 用于记录子弹的旋转角度
    private float radius = 2f; // 圆环的半径
    private float rotations = 2f; // 旋转的圈数
    private float angularSpeed = 180f; // 角速度

    void BeamSwordMove()
    {
        // 更新子弹的角度
        angle += angularSpeed * Time.deltaTime;

        // 将极坐标转换为直角坐标
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // 设置子弹的位置
        transform.position = new Vector3(x, y, -1f);

        // 如果子弹旋转了足够的圈数，销毁子弹
        if (angle >= 360f * rotations)
        {
            Destroy(gameObject);
        }
    }

    // 冲锋枪的射出方式
    void SubmachineGunMove()
    {
        MoveForward();
    }

    // 反射器的射出方式
    private float existenceTimer = 0f;
    private float maxExistenceTime1 = 8f;
    void ReflectiveMove()
    {
        // 更新存在时间
        existenceTimer += Time.deltaTime;

        // 如果存在时间超过最大存在时间，销毁子弹
        if (existenceTimer >= maxExistenceTime1)
        {
            Destroy(gameObject);
            return;
        }

        // 检查是否触碰到屏幕边缘
        CheckScreenEdge();

        // 子弹向前移动
        MoveForward();
    }

     // 检查是否触碰到屏幕边缘，并反弹
    void CheckScreenEdge()
    {
        // 获取屏幕边缘的位置
        Vector3 screenEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -1));

        // 如果子弹超过屏幕右边缘，反弹
        if (transform.position.x > screenEdge.x)
        {
            Reflect(Vector2.left);
        }

        // 如果子弹超过屏幕左边缘，反弹
        if (transform.position.x < -screenEdge.x)
        {
            Reflect(Vector2.right);
        }

        // 如果子弹超过屏幕上边缘，反弹
        if (transform.position.y > screenEdge.y)
        {
            Reflect(Vector2.down);
        }

        // 如果子弹超过屏幕下边缘，反弹
        if (transform.position.y < -screenEdge.y)
        {
            Reflect(Vector2.up);
        }
    }

    // 反射子弹的移动方向
    void Reflect(Vector2 reflectionDirection)
    {
        // 计算反射后的移动方向
        Vector2 newDirection = Vector2.Reflect(playerDirection, reflectionDirection);

        // 设置新的移动方向
        playerDirection = newDirection.normalized;
    }

    // 喷气飞镖的射出方式
    IEnumerator JetDartMove()
    {
        float elapsedTime = 0f;
        float duration = 3f; // 螺旋运动持续时间

        while (elapsedTime < duration)
        {
            // 实现螺旋运动逻辑，可以根据需要修改
            float angle = elapsedTime * Mathf.PI * 2f; // 每秒转一圈
            float radius = 2f; // 螺旋半径

            // 计算螺旋运动的位置
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // 将子弹位置设置为螺旋运动的位置
            transform.position = new Vector3(x, y, -1f) + playerTransform.position;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // 地雷的射出方式
    void LandmineMove()
    {
        moveSpeed = 0f;
        transform.Translate(-playerDirection * moveSpeed * Time.deltaTime);
    }

    // 战术步枪的射出方式
    private float maxExistenceTime2 = 1f;
    void TacticalRifleMove()
    {
        existenceTimer += Time.deltaTime;

        // 如果存在时间超过最大存在时间，销毁子弹
        if (existenceTimer >= maxExistenceTime2)
        {
            Destroy(gameObject);
            return;
        }
    }

    // 手榴弹的射出方式
    void GrenadeMove()
    {
        StartCoroutine(MoveAndExplode());
    }

    // 协程：手榴弹移动和爆炸逻辑
    IEnumerator MoveAndExplode()
    {
        // 子弹移动的持续时间
        float moveDuration = 2f;
        // 子弹停留在原地的持续时间
        float stayDuration = 2f;

        // 获取子弹的初始位置
        Vector3 initialPosition = transform.position;

        // 子弹在移动时间内不断向前移动
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            transform.Translate(-playerDirection * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 停留在原地
        yield return new WaitForSeconds(stayDuration);

        // 子弹停留在原地两秒后，销毁子弹对象
        Destroy(gameObject);
    }


    // 飞弹的射出方式
    void MissileMove()
    {
        // 在角色半径3之内生成随机的极坐标
        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(0f, 10f);

        // 将极坐标转换为世界坐标
        float x = randomRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float y = randomRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        // 设置子弹的位置
        transform.position = new Vector3(x, y, -1f);
    }


    // 向前移动
    void MoveForward()
    {
        transform.Translate(-playerDirection * moveSpeed * Time.deltaTime);
    }
}

