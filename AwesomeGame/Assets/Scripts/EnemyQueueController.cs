using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQueueController : MonoBehaviour
{
    [SerializeField] List<GameObject> myEnemiesPacks = new List<GameObject>();
    [SerializeField] float timeBetweenSpawns = 5f;
    private bool _isAlreadyStarted = false;

    private void Start()
    {
        StartCoroutine("QueueSpawner");
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyLaser") && !_isAlreadyStarted)
        {
            _isAlreadyStarted = true;
            StartCoroutine("QueueSpawner");
        }
    }*/

    IEnumerator QueueSpawner()
    {
        for (int i = 0; i < myEnemiesPacks.Count; i++)
        {
            myEnemiesPacks[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
       
    }
}

