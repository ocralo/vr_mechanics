//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float k_MaxDistance = 10;
    private GameObject m_GazedAtObject = null;

    private WaitForSeconds doubleClickTreashHold = new WaitForSeconds(1f);
    private int clickCount;


    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;

#if UNITY_EDITOR

        // Bit shift the index of the layer (8) to get a bit mask
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "interactive")
            {
                // GameObject detected in front of the camera.
                if (m_GazedAtObject != hit.transform.gameObject)
                {
                    // New GameObject.
                    //m_GazedAtObject?.SendMessage("OnPointerExit");
                    m_GazedAtObject = hit.transform.gameObject;
                    //m_GazedAtObject?.SendMessage("OnPointerEnter");
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerClick();
        }
#endif


#if UNITY_ANDROID

        if (Physics.Raycast(transform.position, transform.forward, out hit, k_MaxDistance))
        {
            if (hit.transform.tag == "interactive")
            {
                // GameObject detected in front of the camera.
                if (m_GazedAtObject != hit.transform.gameObject)
                {
                    // New GameObject.
                    m_GazedAtObject?.SendMessage("OnPointerExit");
                    m_GazedAtObject = hit.transform.gameObject;
                    m_GazedAtObject?.SendMessage("OnPointerEnter");
                }
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            m_GazedAtObject?.SendMessage("OnPointerExit");
            m_GazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            /* m_GazedAtObject?.SendMessage("OnPointerClick");
            m_GazedAtObject?.SendMessage("OnPointerClickTeleport"); */
            OnPointerClick();

        }
#endif
    }

    //metodo para detectar el doble click o doble toque
    private void OnPointerClick()
    {
        clickCount++;
        if (clickCount == 2)
        {
            print("double click!");
            m_GazedAtObject?.SendMessage("OnPointerClickTeleport");
            clickCount = 0;
        }
        else
        {
            StartCoroutine(TickDown());
        }
    }

    private IEnumerator TickDown()
    {
        yield return doubleClickTreashHold;
        if (clickCount > 0)
        {
            clickCount--;
        }
    }

}
