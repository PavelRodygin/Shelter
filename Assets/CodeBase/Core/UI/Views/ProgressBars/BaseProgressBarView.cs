using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Core.UI.Views.ProgressBars
{
    public abstract class BaseProgressBarView : MonoBehaviour, IProgress<float>
    {
        public abstract void Report(float value);
        public abstract void ReportToZero(float value);

        public bool canAnimate;
        public bool canAnimateToZero;
        public bool isComplete;
        private float _currentRatio;
        private float _currentDuration;
        private const float Epsilon = 0.1f;


        public async UniTask Animate(float duration, float value = 1f)
        {
            canAnimate = true;
            var ratio = _currentRatio;
            var multiplier = value / duration;
            while (ratio < value && canAnimate)
            {
                _currentRatio = ratio;
                ratio += Time.deltaTime * multiplier;
                Report(ratio);
                await UniTask.Yield();
            }
            canAnimate = false;
            _currentRatio = 0;
        }

        public async UniTask AnimateToZero(float duration, float currentValue = 1f)
        {
            isComplete = false;
            canAnimateToZero = true;
            _currentRatio = currentValue;
            var currentDuration = duration;
            var ratio = currentValue;
            var multiplier = currentValue / duration;
            while (ratio > 0 && canAnimateToZero)
            {
                _currentRatio = ratio;
                currentDuration -= Time.deltaTime;
                ratio -= Time.deltaTime * multiplier;
                ReportToZero(ratio);
                await UniTask.Yield();
            }
            _currentDuration = currentDuration;
            canAnimateToZero = false;
            if (_currentRatio < Epsilon) isComplete = true;
        }
    
        public void PauseAnimation() => canAnimate = false;

        public void ResumeAnimation()
        {
            canAnimate = true;
            Animate(_currentRatio).Forget();
        }      
        
        public void PauseAnimationToZero() => canAnimateToZero = false;

        public void ResumeAnimationToZero()
        {
            canAnimateToZero = true;
            AnimateToZero(_currentDuration, _currentRatio).Forget();
        }
    }
}
