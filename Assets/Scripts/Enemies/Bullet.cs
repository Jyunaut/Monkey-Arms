using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [field: SerializeField] public float ActiveTime { get; set; } = 5f;

    private void Awake()
    {
        StartCoroutine(ActiveTimer());
    }
    
    private IEnumerator ActiveTimer()
    {
        yield return new WaitForSeconds(ActiveTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (LayerMask.GetMask(GlobalStrings.kMonkeyHand) == (LayerMask.GetMask(GlobalStrings.kMonkeyHand) | (1 << col.gameObject.layer)))
        {
            if(col.tag == "Monkey Left Hand")
                Debug.Log("Shot Left hand");
            if(col.tag == "Monkey Right Hand")
                Debug.Log("Shot Right hand");
            StopCoroutine(ActiveTimer());
            // Destroy(this.gameObject);
        }
        if (LayerMask.GetMask(GlobalStrings.kMonkeyBody) == (LayerMask.GetMask(GlobalStrings.kMonkeyBody) | (1 << col.gameObject.layer)))
        {
            Debug.Log("Shot Body - insta deth");
            StopCoroutine(ActiveTimer());
            // Destroy(this.gameObject);
        }
    }
}
