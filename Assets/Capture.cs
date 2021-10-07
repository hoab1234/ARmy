using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Capture : MonoBehaviour
{

	public void TakeScreenshotAndSave()
	{
		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();



		//Save the screenshot to Gallery/Photos
		NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result:" + path));

		// Debug.Log("Media save result: " + success + " " + path))

		Debug.Log("Permission result: " + permission);
		
		// To avoid memory leaks
		Destroy(ss);
	}
}
