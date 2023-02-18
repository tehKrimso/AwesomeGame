using System;
using Infrastructure;
using UnityEngine;

namespace Mechanics
{
    public class Door : MonoBehaviour
    {
        public GameObject SublevelTo;
        
        public SubLevelSwitcher Switcher;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E");
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
                Switcher.SwitchTo(SublevelTo);
        }
    }
}