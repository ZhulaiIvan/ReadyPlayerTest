using System;
using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;

public class AvatarCreator : MonoBehaviour
{
    [SerializeField]private WebView _webView;

    private GameObject _avatar;
    private string _url;

    public GameObject Avatar
    {
        get => _avatar;
    }

    public string Url
    {
        get => _url;
    }
    
    public static event Func<string> OnUrlLoaded;
    public static event Action<string> OnAvatarImported;
    public static event Action OnAvatarCreated;
    
    void Awake()
    {
        OnUrlLoaded += LoadUrl;
        OnAvatarImported += ImportAvatar;
        OnAvatarCreated += CreateAvatar;
        
        /*var url = OnUrlLoaded?.Invoke();
        if (url == null)
        {
            OnAvatarCreated?.Invoke();
        }
        else
        {
            OnAvatarImported?.Invoke(url);
        }*/
    }

    private void CreateAvatar()
    {
        _webView.CreateWebView();
        _webView.SetScreenPadding(0, 0, 0, 0);
        _webView.OnAvatarCreated = AvatarCreating;
        _webView.SetVisible(true);
        
    }

    /// <summary>
    /// Load avatar from PlayerPrefs
    /// </summary>
    /// <param name="url"></param>
    private void ImportAvatar(string url)
    {
        LoadPrepare(url);
    }

    /// <summary>
    /// Callback
    /// </summary>
    /// <param name="obj"></param>
    private void OnAvatarImportCallback(GameObject obj)
    {
        Debug.Log("Imported");
    }

    /// <summary>
    /// Create Avatar
    /// </summary>
    /// <param name="url"></param>
    private void AvatarCreating(string url)
    {
        _url = url;
        PlayerPrefs.SetString("url", url);

        LoadPrepare(url);
    }

    private void LoadPrepare(string url)
    {
        AvatarLoader loader = new AvatarLoader();
        loader.LoadAvatar(url, OnAvatarImportCallback, OnAvatarLoaded);
    }

    /// <summary>
    /// Avatar is loaded
    /// </summary>
    /// <param name="avatar"></param>
    /// <param name="metaData"></param>
    private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
    {
        _avatar = avatar;
    }

    /// <summary>
    /// PlayerPrefs check
    /// </summary>
    /// <returns></returns>
    private string LoadUrl()
    {
        if (PlayerPrefs.HasKey("url"))
            return PlayerPrefs.GetString("url");

        return null;
    }

}
