using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPiece : MonoBehaviour
{

    private int yRotation = 0;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {

    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter(Transform parent)
    {
        Debug.Log(parent);
        this.transform.SetParent(parent);
        this.transform.localPosition = new Vector3(0, 0, 10);
        this.GetComponent<Rigidbody>().useGravity = false;
        //SetMaterial(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnterClik()
    {
        Debug.Log("a rotar");
        yRotation += 90;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        if (yRotation > 360)
            yRotation = 0;

        //SetMaterial(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnterDoubleClick()
    {
        this.transform.SetParent(null);
        this.GetComponent<Rigidbody>().useGravity = true;
        //SetMaterial(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        //this.GetComponent<Rigidbody>().useGravity = true;
        //SetMaterial(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        //TeleportRandomly();
    }

}
