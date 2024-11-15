using System;
using System.IO;
using UnityEngine;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityAndroidNativeFileOpener;
#endif

namespace Scripts.AddonData.AddonOpen
{
    public class AddonOpener
    {
        public static OpenAddonResult OpenAddon(string pathToAddon)
        {
            try
            {
                if(!File.Exists(pathToAddon))
                {
                    return OpenAddonResult.Deleted;
                }

#if UNITY_ANDROID && !UNITY_EDITOR
				AndroidContentOpenerWrapper.OpenContent(pathToAddon);
#else
                Debug.Log($"Open {pathToAddon}");
#endif
            }
            catch(AndroidJavaException ex)
            {
                if(ex.Message.Contains("ActivityNotFoundException"))
                {
                    return OpenAddonResult.NotInstalled;
                }

                // AppMetrica.Instance.ReportUnhandledException(ex);
                Debug.LogException(ex);
                return OpenAddonResult.Unknown;
            }
            catch(NotSupportedException)
            {
                return OpenAddonResult.NotSupported;
            }
            catch(Exception ex)
            {
                // AppMetrica.Instance.ReportUnhandledException(ex);
                Debug.LogException(ex);
                return OpenAddonResult.Unknown;
            }

            return OpenAddonResult.Success;
        }
    }
}