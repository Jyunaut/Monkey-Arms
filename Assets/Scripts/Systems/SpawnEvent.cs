using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvent : MonoBehaviour
{
    public enum TripTarget { Arm, Body, Camera }
    public float spawnDelay;
    public TripTarget tripTarget;
    public List<GameObject> EnemyCollection;

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (tripTarget)
        {
            case TripTarget.Body:
                if (col.tag == "Body")
                {
                    Spawn();
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
                break;
            case TripTarget.Arm:
                if (col.tag == "Arm")
                {
                    Debug.Log("Spawn");
                    Spawn();
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
                break;
            case TripTarget.Camera:
                if (LayerMask.GetMask(GlobalStrings.kCamera) == (LayerMask.GetMask(GlobalStrings.kCamera) | (1 << col.gameObject.layer)))
                {
                    Spawn();
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
                break;
                default:
                goto case TripTarget.Body;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < EnemyCollection.Count; i++)
        {
            if (EnemyCollection[i] == null)
                Debug.LogWarning("Missing Enemy Prefab.", this);
            else
                StartCoroutine(SpawnAfterDelay(EnemyCollection[i]));
        }
    }

    IEnumerator SpawnAfterDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(spawnDelay);
        enemy.SetActive(true);
        enemy.transform.SetParent(null);
    }
}
