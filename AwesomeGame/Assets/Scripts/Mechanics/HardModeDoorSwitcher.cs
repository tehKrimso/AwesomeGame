using UnityEngine;

namespace Mechanics
{
    public class HardModeDoorSwitcher : MonoBehaviour
    {
        public GameObject ZeroLevelDoor;
        public GameObject FirstLevelDoor;
        public GameObject SecondLevelDoor;

        public void OnHardModeOn()
        {
            ZeroLevelDoor.SetActive(false);
            FirstLevelDoor.SetActive(false);
            SecondLevelDoor.SetActive(false);
        }
    }
}
