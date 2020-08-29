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
    public TMP_InputField user;
    public TMP_InputField password;
    public VrManagment vrManagment;

    // Start is called before the first frame update
    void Start()
    {
        //Google.XR.Cardboard.Widget.CloseButtonRect = false;
        //Screen.orientation = ScreenOrientation.Portrait;
#if !UNITY_EDITOR
        StopXR();
        
#endif


    }

    /* public void StopXR()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
        }
    } */

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
    ///////////
    IEnumerator StartXR()
    {
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            yield return null;
        }
    }


    void StopXR()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Camera.main.ResetAspect();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }
    /////////////

    /*  */
    public void Submit()
    {
        StartCoroutine(LoginRequest(user.text, password.text, json =>
        {
            ResponseQuery rs = JsonUtility.FromJson<ResponseQuery>(json);
            Debug.Log(json);
            if (rs.auth)
            {
                StartXR();
                CreateGlobalToken(rs.token);
                ViewLoadScene(1);
            }
            else
            {

            }
            Debug.Log(rs.auth);
        }));
    }

    public void CreateGlobalToken(string token)
    {
        GameObject globalData = GameObject.Find("globalData");
        if (GameObject.Find("globalData") != null)
        {
            globalData.GetComponent<GlobalData>().Token = token;
        }
        else
        {
            GameObject globalDataNew = new GameObject();
            globalDataNew.name = "globalData";
            globalDataNew.AddComponent<GlobalData>();
            globalDataNew.GetComponent<GlobalData>().Token = token;
            DontDestroyOnLoad(globalDataNew);
        }
    }

    /*  */

    /* public void StartXR()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    } */


    /*  */
    public IEnumerator LoginRequest(string email, string password, Action<string> result)
    {
        string url = "http://localhost:7001/login";

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(url, form);

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
