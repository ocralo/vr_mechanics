using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.XR.Management;
using TMPro;

public class Login : changeScene
{
    public Button submit;
    public TMP_InputField user;
    public TMP_InputField password;
    public VrManagment vrManagment;

    // Start is called before the first frame update
    void Start()
    {
        //Google.XR.Cardboard.Widget.CloseButtonRect = false;
        //Screen.orientation = ScreenOrientation.Portrait;
#if !UNITY_EDITOR
        //StopXR();
#endif


    }

    public void StopXR()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator NoVR()
    {
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        XRGeneralSettings.Instance.Manager.activeLoader.Stop();
        XRGeneralSettings.Instance.Manager.activeLoader.Deinitialize();
    }

    /*  */
    public void Submit()
    {
        /* submit.onClick.AddListener(() =>
        {
            ViewLoadScene(1);
        }); */

        StartCoroutine(LoginRequest(user.text, password.text, json =>
        {
            ResponseQuery rs = JsonUtility.FromJson<ResponseQuery>(json);
            Debug.Log(json);
            Debug.Log(rs.auth);
        }));
    }


    /*  */
    public IEnumerator LoginRequest(string email, string password, Action<string> result)
    {
        string url = "http://localhost:7001/login";

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        //string form = "{\"password\":\"" + password + "\", \"email\":\"" + email + "\"}";

        //Debug.Log(form);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        //www.SetRequestHeader("Content-Type", "multipart/form-data");
        //www.SetRequestHeader("Content-Type", "application/json");
        //www.SetRequestHeader("Authorization", "Bearer " + token);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            result(www.downloadHandler.text);
        }
    }

    public IEnumerator getDataDB(string user, string password, Action<bool> result)
    {
        string url = "http://localhost:7001";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {

            //webRequest.SetRequestHeader("Authorization", "Bearer " + token);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                //ResponseBasket responseBasket = JsonUtility.FromJson<ResponseBasket>(json);
                Debug.Log(json);
                //result(responseBasket);
            }
        }
    }
}

[Serializable]
public class ResponseQuery
{
    public string message;
    public bool auth;
    public string token;
}
