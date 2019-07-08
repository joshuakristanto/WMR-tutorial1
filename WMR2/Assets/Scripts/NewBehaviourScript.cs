using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using HoloToolkit.Unity.InputModule;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.XR.WSA.Input;
using System.Collections;
public class NewBehaviourScript : MonoBehaviour, IFocusable, IMixedRealityInputHandler
{
    public bool Rotating;
    public Vector3 ScaleChange;
    public float RotationSpeed;
   // public void OnInputClicked(EventSystem eventSystem) { Debug.Log("HelloWOrld"); }
    public void OnFocusEnter() { Rotating = true; }
    public void OnFocusExit() { Rotating = false; }
    void Update() { if (Rotating) transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
        if (Input.GetButtonDown("press"))
        {
            Debug.Log("Hello");
            Debug.Log(Input.mousePosition);
        }
    }

    public void OnInputUp(Microsoft.MixedReality.Toolkit.Input.InputEventData eventData)
    {
        Debug.Log("HelloWOrld1 ");
    }

    public void OnInputDown(Microsoft.MixedReality.Toolkit.Input.InputEventData eventData)
    {
        Debug.Log("HelloWOrld2 ");
    }
    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        Debug.Log("HelloWOrld");
    }

   
}
