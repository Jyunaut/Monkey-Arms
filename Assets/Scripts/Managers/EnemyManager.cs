using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; set; }

    [field: SerializeField] public Rect Top { get; set; }
    [field: SerializeField] public Rect OffBottom { get; set; }
    [field: SerializeField] public float Offset { get; set; } = 0.5f;
    private Camera _mainCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Vector2 TL, TR, BL, BR;
        BL = new Vector2(Camera.main.rect.x, Camera.main.rect.y);
        Instantiate(new GameObject("lmao"), BL, Quaternion.identity);
    }
}
