using CodeBase.Core.UI.Popup;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Implementation.Popup.Popups
{
    public class AddonVersionsPopup : BasePopup
    {
        [SerializeField] private TMP_Text versionsText;
        [SerializeField] private Button closeButton;
        
        protected virtual void Awake() =>
            closeButton.onClick.AddListener(() => Close().Forget());

        public override UniTask Open<T>(T param)
        {
            if (!(param is string versions)) return base.Open(param);

            var formattedVersions = versions.Replace(" ", "\n");
            versionsText.text = formattedVersions;

            return base.Open(param);
        }
    }
}