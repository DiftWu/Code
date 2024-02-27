using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    // Start is called before the first frame update
    public float targetAspectRatio = 16f / 9f;

    void Start()
    {
        Camera mainCamera = GetComponent<Camera>();

        // 计算目标宽高比
        float targetWidth = Screen.width;
        float targetHeight = targetWidth / targetAspectRatio;

        // 设置相机的视图矩形，确保目标宽高比
        mainCamera.rect = new Rect(0f, (1f - targetHeight / Screen.height) / 2f, 1f, targetHeight / Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
