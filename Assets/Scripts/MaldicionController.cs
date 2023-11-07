using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class MaldicionController : MonoBehaviour
{
    public List<PlayerContoller> players;

    PlayerContoller player1;
    PlayerContoller player2;

    public void AddPlayer(PlayerInput player)
    {
        PlayerContoller aux = player.gameObject.GetComponent<PlayerContoller>();
        if (players.Count ==1)
        {
            aux.SetMaldicion(true);
        }
        players.Add(aux);
    }
    private void Awake()
    {
       
    }

    private void Update()
    {
        if(players.Count == 2) {

           // Debug.Log("Jugadores conectados!");
            player1 = players[0];
            player2 = players[1];


            if (player1.GetMaldicion() == true)
                Debug.Log("Player1 maldito");
            if (player2.GetMaldicion() == true)
                Debug.Log("Player2 maldito");
        }

        
    }

    public void PassMaldicion()
    {
        
        if (player1.GetMaldicion() == true)
        {
            player1.SetMaldicion(false);
            player2.SetMaldicion(true);

        }
        if (player2.GetMaldicion() == true)
        {
            player2.SetMaldicion(false);
            player1.SetMaldicion(true);
        }
        
    }

    public bool PlayersReady()
    {
        if(players.Count == 2)
            return true;
        return false;
    }
}
