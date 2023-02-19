using UnityEngine;

namespace Mechanics
{
    public class GlareColorChanger : MonoBehaviour
    {
        public MeshRenderer GlareRenderer;
        public Material Green;
        public Material Yellow;

        public void SetYellowColor() => GlareRenderer.sharedMaterial = Yellow;

        public void SetGreenColor() => GlareRenderer.sharedMaterial = Green;
    }
}
