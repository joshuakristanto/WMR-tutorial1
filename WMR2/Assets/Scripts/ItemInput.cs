using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
public class ItemInput : MonoBehaviour, IInputClickHandler
{
    [SerializeField]
    private int itemId;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log(itemId);
        GameController.Instance.CheckForItem(itemId);
    }
    /*
    void IInputHandler.OnInputUp(InputEventData eventData)
    {
        Debug.LogFormat("OnInputUp\r\nSource: {0}  SourceId: {1}  InteractionPressKind: {2}",
            eventData.InputSource,
            eventData.SourceId,
            eventData.PressType);
        // Mark the event as used, so it doesn't fall through to other handlers.
        eventData.Use();
    }
    void IInputHandler.OnInputDown(InputEventData eventData)
    {
        Debug.LogFormat("OnInputDown\r\nSource: {0}  SourceId: {1}  InteractionPressKind: {2}",
            eventData.InputSource,
            eventData.SourceId,
            eventData.PressType);
        // Mark the event as used, so it doesn't fall through to other handlers.
        eventData.Use();
    }
    void IInputClickHandler.OnInputClicked(InputClickedEventData eventData)
    {
        Debug.LogFormat("OnInputClicked\r\nSource: {0}  SourceId: {1}  InteractionPressKind: {2}  TapCount: {3}",
            eventData.InputSource,
            eventData.SourceId,
            eventData.PressType,
            eventData.TapCount);
        // Mark the event as used, so it doesn't fall through to other handlers.
        eventData.Use();
    }
    */
}
