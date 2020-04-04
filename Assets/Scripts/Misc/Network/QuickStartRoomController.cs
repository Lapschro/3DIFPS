using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex;

    public override void OnEnable() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() {
        Debug.Log("Joined Room");
        StartGame();
    }

    // Start is called before the first frame update
    void StartGame()
    {
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("StartingGame");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
            
        }
    }
}
