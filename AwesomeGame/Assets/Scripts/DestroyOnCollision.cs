using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    //[SerializeField] string tagName;
    [SerializeField] List<string> tagNames;
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagNames.Count; i++)
        {
            if (other.gameObject.tag == tagNames[i])
            {
                Destroy(gameObject,0.0001f);
            }
        }
    }
}
