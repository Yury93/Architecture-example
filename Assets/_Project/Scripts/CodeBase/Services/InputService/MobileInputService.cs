﻿ using UnityEngine;

namespace CodeBase.Services.InputService
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => GetSimpleInputAxis();
    }
}