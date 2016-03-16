using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public WebCamTexture mCamera = null;
    public RawImage cameraRawImage;

    WebCamDevice[] devices;

    // Use this for initialization
    void Start()
    {
        devices = WebCamTexture.devices;
        foreach (WebCamDevice cam in devices)
        {
            if (cam.isFrontFacing)
            {
                hasFrontFacingCamera = true;
                break;
            }
        }

        foreach (WebCamDevice cam in devices)
        {
            if (!cam.isFrontFacing)
            {
                hasBackFacingCamera = true;
                break;
            }
        }
        StartCamera();
    }

    public bool hasFrontFacingCamera;
    public bool hasBackFacingCamera;
    public bool useFrontFacingCamera = true;
    WebCamTexture wct;
    public bool cameraOn = false;
    int cameraRotation = 0;
    public void StartCamera()
    {
        if (!cameraOn)
            cameraOn = true;
        if(wct != null)
            wct.Stop();
        devices = WebCamTexture.devices;
        if (useFrontFacingCamera)
        {
            if(hasFrontFacingCamera)
            {
                foreach (WebCamDevice cam in devices)
                {
                    if (cam.isFrontFacing)
                    {
                        wct = new WebCamTexture(cam.name, Screen.width, Screen.width);
                        wct.Play();

                        // Texture2D texture = new Texture2D(Screen.width, Screen.height);
                        // cameraRotation = (int)(wct.videoRotationAngle - 90f - 90f);
                        // texture.SetPixels32(RotateMatrix(wct.GetPixels32(), (int)(wct.videoRotationAngle - 90f - 90f)));
                        // cameraRawImage.texture = texture;

                        cameraRawImage.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, wct.videoRotationAngle - 270f + 90f));
                        // cameraRawImage.transform.localScale = new Vector3(-1, -1, -1);
                        cameraRawImage.texture = wct;
                        break;
                    }
                }
            }
        }

        if(!useFrontFacingCamera)
        {
            foreach (WebCamDevice cam in devices)
            {
                if (!cam.isFrontFacing)
                {
                    wct = new WebCamTexture(cam.name, Screen.width, Screen.width);
                    wct.Play();
                    
                    // Texture2D texture = new Texture2D(Screen.width, Screen.height);
                    // cameraRotation = (int)(wct.videoRotationAngle - 90f - 90f);
                    // texture.SetPixels32(RotateMatrix(wct.GetPixels32(), cameraRotation));
                    // cameraRawImage.texture = texture; 

                    cameraRawImage.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, wct.videoRotationAngle - 90f - 90f));
                    cameraRawImage.texture = wct;

                    // cameraRawImage.transform.localScale = new Vector3(-1, -1, -1);
                    cameraRawImage.texture = wct;

                    break;
                }
            }
        }

        if(!hasFrontFacingCamera && !hasBackFacingCamera)
        {
            // MenuController.instance.profilePhotoPanel.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            // MenuController.instance.profilePhotoPanel.GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void ToggleCamera()
    {
        useFrontFacingCamera = !useFrontFacingCamera;
        StartCamera();
    }

    public void StopCamera()
    {
        if (wct != null)
            wct.Stop();
    }

    PhotoHelper currentPhotoHelper;
    public void SetPhotoShot(PhotoHelper photoHelper)
    {
        currentPhotoHelper = photoHelper;
        StartCamera();
    }

    public void TakePicture()
    {
        if (wct != null)
            wct.Pause();
        else
            return;

        Texture2D temp = new Texture2D(wct.width, wct.height, TextureFormat.RGBA32, false);
        temp.SetPixels(wct.GetPixels(0, 0, wct.width, wct.height));
        temp.Apply();

        currentPhotoHelper.photo.texture = temp;

        currentPhotoHelper.photo.rectTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));

        currentPhotoHelper.ShowPlaceHolder(false);
        currentPhotoHelper.ShowPhoto(true);

        wct.Stop();
    }
}