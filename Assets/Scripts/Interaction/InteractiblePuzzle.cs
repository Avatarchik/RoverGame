using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class InteractiblePuzzle : InteractibleObject
    {
        public Object tableLevel;
        public UIEvents uiEvents;

        public override void Interact()
        {
            base.Interact();
            uiEvents.LevelButtonEvent(tableLevel);
        }
    }
}