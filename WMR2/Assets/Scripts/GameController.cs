using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreLogic;
using System;

public class GameController : MonoBehaviour
{
    private ShellGameLogic coreLogic;

    [SerializeField]
    private int numberOfStrkies;

    [SerializeField]

    private GameObject[] itemContainers;

    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                GameObject go = GameObject.FindGameObjectWithTag("GameController");
                instance = go.GetComponent<GameController>();
                return instance;
            }
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        coreLogic = new ShellGameLogic(itemContainers.Length, numberOfStrkies);
    }
    void Start()
    {
        HookupCoreLogicEvents();

        StartTurn();
    }

    private void StartTurn()
    {
        //Prepare our items for the new turn
        //Loop Through the containersand make sure the peas are covered up
        //actually hide the pea from the container so the plater can't cheat

        // Animate items

        // Play animation SOunds

        //Reset Items
        coreLogic.ResetItem();

        coreLogic.CheckForItem(1);
    }

    private void HookupCoreLogicEvents()
    {
        coreLogic.ItemReset += CoreLogic_ItemReset;
        coreLogic.GameOver += CoreLogic_GameOver;
        coreLogic.ResetComplete += CoreLogic_ResetComplete;
        coreLogic.SelectedItem += CoreLogic_SelectedItem;
        coreLogic.CheckingItem += CoreLogic_CheckingItem;
        coreLogic.MatchNotMade += CoreLogic_MatchNotMade;
        coreLogic.MatchMade += CoreLogic_MatchMade;
        coreLogic.StartTurn += CoreLogic_StartTurn;

     
    }

    

    private void CoreLogic_MatchMade(object sender, MatchEventArgs e)
    {
        Debug.Log($"Match Made: {e.Id} Score: {e.Score}");
    }

    private void CoreLogic_MatchNotMade(object sender, NoMatchEventArgs e)
    {
        Debug.Log($"Match Not Made: {e.IsStrike}");
    }

    private void CoreLogic_CheckingItem(object sender, ItemEventArgs e)
    {
        Debug.Log($"Checking Item: {e.Id}");
    }

    private void CoreLogic_SelectedItem(object sender, ItemEventArgs e)
    {
        Debug.Log($"Selected Item: {e.Id}");
    }

    private void CoreLogic_ResetComplete(object sender, EventArgs e)
    {
        Debug.Log("Reset Complete");
    }

    private void CoreLogic_GameOver(object sender, EventArgs e)
    {
        Debug.Log("Game Over");
    }

    private void CoreLogic_ItemReset(object sender, EventArgs e)
    {
        Debug.Log("Item Reset");
    }

    private void CoreLogic_StartTurn(object sender, EventArgs e)
    {
        Debug.Log("Start Turn");
        Invoke(nameof(StartTurn), 1.2f); //delay for animaitons
    }

    public void CheckForItem(int itemId)
    {
        Debug.Log($"Check for Pea: {itemId}");
        coreLogic.CheckForItem(itemId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        coreLogic.ItemReset -= CoreLogic_ItemReset;
        coreLogic.GameOver -= CoreLogic_GameOver;
        coreLogic.ResetComplete -= CoreLogic_ResetComplete;
        coreLogic.SelectedItem -= CoreLogic_SelectedItem;
        coreLogic.CheckingItem -= CoreLogic_CheckingItem;
        coreLogic.MatchNotMade -= CoreLogic_MatchNotMade;
        coreLogic.MatchMade -= CoreLogic_MatchMade;
        coreLogic.StartTurn -= CoreLogic_StartTurn;
    }
    
}
