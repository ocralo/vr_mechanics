using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPiece_diseable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        //TeleportRandomly();
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnterDoubleClick()
    {
        //Destroy(this);
        this.transform.gameObject.SetActive(false);
        //SetMaterial(true);
    }
}
