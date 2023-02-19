using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    //[SerializeField] string tagName;
    [SerializeField] List<string> tagNames;
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagNames.Count; i++)
        {
            if (other.gameObject.tag == tagNames[i])
            {
                Destroy(gameObject.transform.parent.gameObject,0.0001f);
            }
        }
        
        /*if (other.gameObject.tag == tagName)
            Destroy(gameObject.transform.parent.gameObject, 0.01f);*/
    }
}
