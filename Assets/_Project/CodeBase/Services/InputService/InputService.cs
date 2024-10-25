using UnityEngine;

namespace CodeBase.Services.InputService
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Button = "Fire";
        public abstract Vector2 Axis { get; }

        protected static Vector2 GetSimpleInputAxis() =>
          new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        public bool IsAttackButtonUp() => 
            SimpleInput.GetButtonUp(Button);
        
    }
}