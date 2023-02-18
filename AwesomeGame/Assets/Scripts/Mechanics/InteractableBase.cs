using UnityEngine;

namespace Mechanics
{
    public abstract class InteractableBase : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
                Interact();
        }

        protected abstract void Interact();
    }
}