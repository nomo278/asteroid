using UnityEngine;
using UnityEngine.UI;

public class PhotoHelper : MonoBehaviour {

    public Image placeHolderImage;
    public Text photoText;
    public RectTransform photoParent;
    public RawImage photo;

    public void ShowPlaceHolder(bool show)
    {
        placeHolderImage.gameObject.SetActive(show);
    }

    public void ShowPhoto(bool show)
    {
        photoParent.gameObject.SetActive(show);
    }
}
