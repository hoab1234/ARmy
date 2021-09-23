using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//�ȵ���̵� �������� �����ؼ�
//���� Ȥ�� ������ �����ϸ�
//�ε� ���丮�� ��� �ʹ�.

public class GalleryTest : MonoBehaviour
{
    public GameObject temporaryImageDoodle;
    public GameObject temporaryVideoDoodle;
    public int maxImgDoodleSize = 256;
    private string local_file;
    public void PickImageOrVideo()
    {
        temporaryImageDoodle.SetActive(false);
        temporaryVideoDoodle.SetActive(false);
        if (NativeGallery.CanSelectMultipleMediaTypesFromGallery())
        {
            NativeGallery.Permission permission = NativeGallery.GetMixedMediaFromGallery((path) =>
            {
                Debug.Log("Media path: " + path);
                if (path != null)
                {
                    local_file = path; //��� ����
                    // Determine if user has picked an image, video or neither of these
                    switch (NativeGallery.GetMediaTypeOfFile(path))
                    {
                        case NativeGallery.MediaType.Image:
                            mediaType = NativeGallery.MediaType.Image;
                            Debug.Log("Picked image");
                            Texture texture = NativeGallery.LoadImageAtPath(path, maxImgDoodleSize);
                            if (texture == null)
                            {
                                DebugUI.instance.UpdateDebugForGm("fucktexture");
                                return;
                            }
                            temporaryImageDoodle.SetActive(true);
                            Material material = temporaryImageDoodle.GetComponent<MeshRenderer>().materials[0];
                            material.mainTexture = texture;
                            break;
                        case NativeGallery.MediaType.Video:
                            mediaType = NativeGallery.MediaType.Video;
                            temporaryVideoDoodle.SetActive(true);
                            VideoPlayer vp = temporaryVideoDoodle.GetComponent<VideoPlayer>();
                            vp.url = path;
                            Debug.Log("Picked video");
                            break;
                        default:
                            Debug.Log("Probably picked something else");
                            break;
                    }
                }
            }, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video, "Select an image or video");

            Debug.Log("Permission result: " + permission);
        }
    }
    FirebaseStorage storage;
    StorageReference storageReference;
    NativeGallery.MediaType mediaType;
    
    public void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://airy-907b8.appspot.com/");
    }
    public void UpLoadFile()
    {
        temporaryImageDoodle.SetActive(false);
        temporaryVideoDoodle.SetActive(false);
        if (mediaType == NativeGallery.MediaType.Image)
        {
            //��ǲ �ؽ�Ʈ�� �ִ� �ؽ�Ʈ�� ��������
            var inputField = temporaryImageDoodle.GetComponentInChildren<InputField>();
            var inputText = inputField.text;
            var new_meatadata = new MetadataChange
            {
                CustomMetadata = new Dictionary<string, string>
                {
                    { "ImageDoodleText", inputText },
                }
            };
            DebugUI.instance.UpdateDebugForGm("���ε��Լ�����");
            StorageReference uploadRef = storageReference.Child("ImageDoodle/newFile" + (RealTimeDataBase.instance.ImgNum).ToString() + ".jpg");
            
            // Upload the file to the path "images/rivers.jpg"
            uploadRef.PutFileAsync("file://" + local_file, new_meatadata) //�����؊x�� ��� �־��� + ��Ÿ������ ����
              .ContinueWith((Task<StorageMetadata> task) =>
              {
                  DebugUI.instance.UpdateDebugForGm("putfile�Լ� ����");
                  if (task.IsFaulted || task.IsCanceled)
                  {
                      DebugUI.instance.UpdateDebugForGm((local_file).ToString());
                  }
                  else
                  {
                      // Metadata contains file metadata such as size, content-type, and download URL.
                      StorageMetadata metadata = task.Result;
                      //string download_url = metadata.DownloadUrl.ToString();
                      Debug.Log("Finished uploading...");
                      // Debug.Log("download url = " + download_url);
                      DebugUI.instance.UpdateDebugForGm("���ε强��");
                      print("updateDebugForgmm ---------updateimagenum");
                      RealTimeDataBase.instance.UpdateImageNum();
                      DebugUI.instance.UpdateDebugForGm("inputField = " + inputText);
                  }
              });
        }
        else if (mediaType == NativeGallery.MediaType.Video)
        {
            var inputField = temporaryImageDoodle.GetComponentInChildren<InputField>();
            var inputText = inputField.text;
            var new_meatadata = new MetadataChange
            {
                CustomMetadata = new Dictionary<string, string>
                {
                    { "VideoDoodleText", inputText },
                }
            };

            StorageReference uploadRef = storageReference.Child("VideoDoodle/newFile" + (RealTimeDataBase.instance.VideoNum).ToString() + ".mp4");
            DebugUI.instance.UpdateDebugForGm("���ε��Լ�����");
            uploadRef.PutFileAsync("file://" + local_file, new_meatadata) //�����؊x�� ��� �־���
            .ContinueWith((Task<StorageMetadata> task) =>
            {
                DebugUI.instance.UpdateDebugForGm("putfile�Լ� ����");
                if (task.IsFaulted || task.IsCanceled)
                {
                    DebugUI.instance.UpdateDebugForGm((local_file).ToString());
                }
                else
                {
                      // Metadata contains file metadata such as size, content-type, and download URL.
                      StorageMetadata metadata = task.Result;
                      //string download_url = metadata.DownloadUrl.ToString();
                      Debug.Log("Finished uploading...");
                      // Debug.Log("download url = " + download_url);
                      DebugUI.instance.UpdateDebugForGm("���ε强��");
                      print("debugui updatevideonum");
                      RealTimeDataBase.instance.UpdateVideoNum();
                    DebugUI.instance.UpdateDebugForGm("inputField = " + inputText);
                }
            });
        }
    }
}


