using UnityEngine;

namespace Infrastructure
{
    public class SubLevelController : MonoBehaviour
    {
        public GameObject CommonObjects;
        public GameObject NormalMode;
        public GameObject HardMode;
        public void TurnLevelOff()
        {
            CommonObjects.SetActive(false);
            NormalMode.SetActive(false);
            HardMode.SetActive(false);
        }

        public void NormalModeOn()
        {
            TurnOnCommonObjects();
            NormalMode.SetActive(true);
        }

        public void HardModeOn()
        {
            TurnOnCommonObjects();
            HardMode.SetActive(true);
        }

        private void TurnOnCommonObjects() => CommonObjects.SetActive(true);
    }
}