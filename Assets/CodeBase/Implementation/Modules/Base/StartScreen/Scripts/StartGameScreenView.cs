using CodeBase.Core.MVVM;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Implementation.Modules.Base.StartScreen.Scripts
{
    public class StartGameScreenView : BaseScreenView
    {
        [Header("UI Interaction Components")]
        [SerializeField] private Button continueButton;

        [Header("Progress UI Components")]
        [SerializeField] private TMP_Text progressValueText;
        [SerializeField] private TMP_Text progressStatusText;
        [SerializeField] private Image progressBarFillImage;

        [Header("Splash Screen Components")] 
        [SerializeField] private Image startBgImage;
        [SerializeField] private TMP_Text splashTooltipsText;
        [SerializeField] private TMP_Text versionText;

        public void SetupEventListeners(UnityAction onStartButtonClicked) => 
            continueButton.onClick.AddListener(onStartButtonClicked);

        public UniTask ReportProgress(float expProgress, string progressStatus)
        {
            if (progressStatusText != null) 
                progressStatusText.text = progressStatus;

            if (progressBarFillImage != null && progressValueText != null)
            {
                return DOTween.To(() => progressBarFillImage.fillAmount, x =>
                {
                    progressBarFillImage.fillAmount = x;
                    progressValueText.text = $"{(int)(x * 100)}";
                }, expProgress, 1f).ToUniTask();
            }

            if (progressBarFillImage != null)
            {
                return DOTween.To(() => progressBarFillImage.fillAmount, x =>
                {
                    progressBarFillImage.fillAmount = x;
                }, expProgress, 1f).ToUniTask();
            }

            if (progressValueText != null)
            {
                return DOTween.To(() => 0f, x =>
                {
                    progressValueText.text = $"{(int)(x * 100)}";
                }, expProgress, 1f).ToUniTask();
            }

            return UniTask.CompletedTask;
        }

        public void ResetProgressIndicators()
        {
            if (progressValueText != null) progressValueText.text = "0";
            if (progressBarFillImage != null) progressBarFillImage.fillAmount = 0f;
        } 
        
        public void SetTooltipText(string text) =>
            splashTooltipsText.text = text;

        public override UniTask Show()
        {
            SetActive(true);
            continueButton.gameObject.SetActive(false);
            ResetProgressIndicators();
            return UniTask.CompletedTask;
        } 
        
        public void ShowContinueButton()
        {
            continueButton.gameObject.SetActive(true);
        }

        public void RemoveStartBg() => startBgImage.DOFade(0f, 0.25f);

        private void RemoveEventListeners() => continueButton.onClick.RemoveAllListeners();

        public override void Dispose()
        {
            RemoveEventListeners();
            base.Dispose();
        }
    }
}