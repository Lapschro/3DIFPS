using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject startButton;
    [SerializeField]
    GameObject cancelButton;
    [SerializeField]
    int roomSize;


    public override void OnConnectedToMaster() {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
    }


    public void QuickStart() {
        startButton.SetActive(false);
        cancelButton.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("Failed to join room! Creating a room!");
        CreateRoom();
    }

    void CreateRoom() {
        int randomNumber = Random.Range(0, 123456);
        RoomOptions roomOptions = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)roomSize
        };

        PhotonNetwork.CreateRoom("room"+randomNumber, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        CreateRoom();
    }

    public void QuickCancel() {
        cancelButton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
