
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        public Image ImageCurrent;

        public void SetValue(float value, float max)
        {
            ImageCurrent.fillAmount = value / max;
        }

    }
     
}