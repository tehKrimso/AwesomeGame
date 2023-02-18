using System;
using Infrastructure;
using UnityEngine;

namespace Mechanics
{
    public class Door : InteractableBase
    {
        public SubLevelController SublevelTo;
        
        public SubLevelSwitcher Switcher;

        protected override void Interact()
        {
            Switcher.SwitchTo(SublevelTo);
        }
    }
}