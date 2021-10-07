using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Capture : MonoBehaviour
{

	public void TakeScreenshotAndSave()
	{
		UIManager.instance.UIScreenShotBtnOff();
		UIManager.instance.UIBackIconBtnOff();

		StartCoroutine(CaptureAndSave());


	}

	IEnumerator CaptureAndSave()
    {
		yield return new WaitForSeconds(0.2f);
		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		//Save the screenshot to Gallery/Photos
		NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "ARMY Gallery", "Image.jpg", (success, path) => UIManager.instance.UITempImageDoodleOn(path));

		// To avoid memory leaks
		Destroy(ss);
	}

}
