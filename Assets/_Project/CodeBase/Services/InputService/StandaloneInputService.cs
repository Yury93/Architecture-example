using UnityEngine;

namespace CodeBase.Services.InputService
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = GetSimpleInputAxis();
                if (axis == Vector2.zero)
                {
                    axis = GetUnityAxis();
                }
                return axis;
            }
        }  
        private static Vector2 GetUnityAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        
    }
}