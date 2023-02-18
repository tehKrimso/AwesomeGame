using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField] string tagName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tagName)
        Destroy(gameObject, 0.05f);
    }
}
