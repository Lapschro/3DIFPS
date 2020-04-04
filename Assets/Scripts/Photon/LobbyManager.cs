using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


// Must inherit from MonoBehaviourPunCallbacks
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // [SerializeField]
    // private GameObject buttonJoin;
    // [SerializeField]
    // private GameObject buttonStop;
    
    [SerializeField]
    private byte roomSize;

    // // OnConnectedToMaster is called after the connection is established (see NetworkManager)
    // public override void OnConnectedToMaster()
    // {
    //     // All clients must use the same scene as the master client
    //     PhotonNetwork.AutomaticallySyncScene = true;
    // }

    // Attempts to join an already existing room
    public void JoinRoom()
    {
        Debug.Log("finding game...");
        PhotonNetwork.JoinRandomRoom();
    }

    // OnJoinRandomFailed is called if it is not possible to join a random room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("failed to join random game: " + returnCode + " / " + message);

        // Then try to create a room
        CreateRoom();
    }

    // Attempts to create a new game room
    void CreateRoom()
    {
        Debug.Log("creating room");

        // Room's unique identifier
        string roomID = "R" + 0; //Random.Range(0, 10);

        // Room's options
        // IsVisible = public game
        // IsOpen = accepts new players
        // MaxPlayers = maximum number of players
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = roomSize };

        PhotonNetwork.CreateRoom(roomID, roomOptions);
    }

    // OnCreateRoomFailed is called if it is not possible to create a room with the given parameters
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to crate room: " + returnCode + " / " + message);
        //base.OnCreateRoomFailed(returnCode, message);

        // Try again
        CreateRoom();
    }

    public void StopSearching()
    {
        //buttonJoin.SetActive(true);
        //buttonStop.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

}