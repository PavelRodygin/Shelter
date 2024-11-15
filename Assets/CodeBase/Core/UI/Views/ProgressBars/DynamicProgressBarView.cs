using DG.Tweening;
using UnityEngine;

namespace CodeBase.Core.UI.Views.ProgressBars
{
    public class DynamicProgressBarView : ProgressBarView
    {
        public void IncreaseAnimate(float duration, float value)
        {
            if (value == 0) return;
            var targetValue = value < 0 ? Mathf.Max(image.fillAmount + value, 0f) : //TODO 
                Mathf.Max(image.fillAmount + value, 1f);
            image.DOFillAmount(targetValue, duration);
        }
    }   
}