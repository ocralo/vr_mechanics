using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using socket.io;

public class ManaggeScene : MonoBehaviour
{
    public GameObject player, player2;
    public Socket socket;
    public string json, serverUrl = "http://localhost:7001";
    public User user;
    public User user2;
    // Start is called before the first frame update
    void Start()
    {
        //create conection socket
        socket = Socket.Connect(serverUrl);
    }

    void OnGUI()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {

        /* if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Horizontal") != 0)
        { */
        sendDataSocket();
        /* } */
    }

    public void sendDataSocket()
    {
        if (socket.IsConnected)
        {
            user.position = player.transform.position;
            user.rotation = player.transform.rotation;
            json = JsonUtility.ToJson(user);
            socket.EmitJson("user" + user.id, json);
            Debug.Log(user2.id);
            socket.On("user" + user2.id, (string data) =>
            {
                Debug.Log("entre");
                Debug.Log(data.Replace("\"", ""));
                //text.GetComponent<Text>().text = data;
                user2 = JsonUtility.FromJson<User>(data.Replace("\"", "").Replace("'", "\""));
                player2.transform.position = user2.position;
            });
        }
    }
}

[Serializable]
public class User
{
    public string playerName;
    public Vector3 position;
    public Quaternion rotation;
    public int life;
    public float id;
}

[Serializable]
public class User2
{
    public string playerName;
    public string position;
    public string life;
    public string id;
}
