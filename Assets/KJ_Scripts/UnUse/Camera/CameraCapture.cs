//using DeadMosquito.AndroidGoodies;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

public class CameraCapture : MonoBehaviour
{

    bool onCapture = false;

    public void PressBtnCapture(){
        if(onCapture == false)
        {
            StartCoroutine("CRSaveScreenshot");
        }
    }
    
    IEnumerator CRSaveScreenshot()
    {
        onCapture = true;

        yield return new WaitForEndOfFrame();

        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);

            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => Application.isFocused == true);

            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
            {
                //다이얼로그를 위해 별도의 플러그인을 사용했었다. 이 코드는 주석 처리함.
                //AGAlertDialog.ShowMessageDialog("권한 필요", "스크린샷을 저장하기 위해 저장소 권한이 필요합니다.",
                //"Ok", () => OpenAppSetting(),
                //"No!", () => AGUIMisc.ShowToast("저장소 요청 거절됨"));

                // 별도로 확인 팝업을 띄우지 않을꺼면 OpenAppSetting()을 바로 호출함.
//                OpenAppSetting();

                onCapture = false;
                yield break;
            }
        }
    }
}