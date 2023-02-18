using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    [SerializeField] string tagName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagName)
            Destroy(gameObject.transform.parent.gameObject, 0.01f);
    }
}
