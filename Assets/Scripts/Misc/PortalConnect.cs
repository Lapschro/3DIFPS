using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalConnect : MonoBehaviour
{

    [SerializeField]
    LobbyManager lobbyManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(lobbyManager);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        lobbyManager.JoinRoom();
    }

}
