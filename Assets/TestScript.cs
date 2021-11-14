using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wolf3D.ReadyPlayerMe.AvatarSDK;

public class TestScript : MonoBehaviour
{
    [SerializeField]private WebView _webView;

    private GameObject _avatar;
    private string _avatarUrl;

    void Start()
    {
        var url = LoadUrl();
        if (url == null)
        {
            _webView.CreateWebView();
            _webView.OnAvatarCreated = OnAvatarCreated;
            _webView.SetVisible(true);

        }
        else
        {
            OnAvatarImport(url);
        }
    }

    /// <summary>
    /// Load avatar from PlayerPrefs
    /// </summary>
    /// <param name="url"></param>
    private void OnAvatarImport(string url)
    {
        LoadPrepare(url);
    }

    /// <summary>
    /// Callback
    /// </summary>
    /// <param name="obj"></param>
    private void OnAvatarImported(GameObject obj)
    {
        Debug.Log("Imported");
    }

    /// <summary>
    /// Create Avatar
    /// </summary>
    /// <param name="url"></param>
    private void OnAvatarCreated(string url)
    {
        _avatarUrl = url;
        PlayerPrefs.SetString("url", url);

        LoadPrepare(url);
    }

    private void LoadPrepare(string url)
    {
        AvatarLoader loader = new AvatarLoader();
        loader.LoadAvatar(url, OnAvatarImported, OnAvatarLoaded);
    }

    /// <summary>
    /// Avatar is loaded
    /// </summary>
    /// <param name="avatar"></param>
    /// <param name="metaData"></param>
    private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
    {
        _avatar = avatar;
        _avatar.transform.position = new Vector3(0, 0, -5);
        _avatar.transform.LookAt(Camera.main.transform.position);
    }
    
    /// <summary>
    /// PlayerPrefs check
    /// </summary>
    /// <returns></returns>
    private string LoadUrl()
    {
        if(PlayerPrefs.HasKey("url"))
            return PlayerPrefs.GetString("url");

        return null;
    }

}
