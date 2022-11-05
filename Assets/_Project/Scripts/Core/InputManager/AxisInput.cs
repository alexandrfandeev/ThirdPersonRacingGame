using UnityEngine;

namespace _Project.Scripts.Core.InputManager
{
    public class AxisInput : SourceInput
    {
        public float Horizontal;
        public float Vertical;
        public Vector2 Direction => new Vector2(Horizontal, Vertical);
        public Vector2 NormalizedDirection => Direction.normalized;
        public float Magnitude => Direction.sqrMagnitude;

        public AxisInput(string inputID)
        {
            InputID = inputID;
        }
    }
}