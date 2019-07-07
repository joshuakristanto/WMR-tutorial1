using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreLogic;

public class TestPlugin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShellGameLogic logic = new ShellGameLogic(3, 3);
        Debug.Log($"Checked...{logic.CheckForItem(2)})");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
