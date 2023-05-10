using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuAdHandler : MonoBehaviour {

    #region "Getter"
    private static MenuAdHandler _instance;
    public static MenuAdHandler Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MenuAdHandler>();
            }
            return _instance;
        }
    }
    #endregion

    [Header("Menu Ad Components")]
    public Image menuAdBg;

    void Awake()
    {
        _instance = this;
        Disable();
    }

    public void Enable(Texture2D tex,bool isFullScreen)
    {
        if(!isFullScreen)
        {
            transform.localScale = Vector3.one * 0.7f;
        }else
        {
            transform.localScale = Vector3.one;
        }
        menuAdBg.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        Disable();
        AdsManager.Instance.StartCoroutine(AdsManager.Instance.LoadNextLevel(AdsManager.Instance.nextSceneLoadingDelay));
    }
    public void ClickOnAd()
    {
        Application.OpenURL(AdsManager.Instance.menuAdMarketUrl);
        Close();
    }

}
