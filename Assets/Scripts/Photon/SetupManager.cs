using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class SetupManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject HUDPrefab;

    public int lobbyScene;

    public bool isWaitingRoom = false;
    GameObject countdownObject;
    public int minPlayers;

    public List<GameObject> playerObjects;
    public GameObject safetyZonePrefab;
    public GameObject countdownPrefab;
    //private GameObject safetyZoneActive;

    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);

        Debug.Log("instantiate player");
        GameObject playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-40.0f, 40.0f), 5.0f, Random.Range(-40.0f, 40.0f)), Quaternion.identity);
        GameObject hudInstance = PhotonNetwork.Instantiate(HUDPrefab.name, Vector3.zero, Quaternion.identity);
        hudInstance.GetComponent<HUDManager>().player = playerInstance;

        SpawnSafetyZone();
    }

    private void Update()
    {
        //Debug.Log(
        //    "MC=" + PhotonNetwork.IsMasterClient
        //    + " isWaitingRoom=" + isWaitingRoom
        //    + " null=" + (countdownObject is null)
        //    + " pcount=" + PhotonNetwork.CurrentRoom.PlayerCount
        //    + " min=" + minPlayers
        //);

        if (PhotonNetwork.IsMasterClient && isWaitingRoom && (countdownObject is null) && (PhotonNetwork.CurrentRoom.PlayerCount >= minPlayers))
        {
            InitializeCountdown();
        }        
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene(lobbyScene);
    }

    void SpawnSafetyZone()
    {
        float x = Random.Range(-50.0f, 50.0f);
        float z = Random.Range(-50.0f, 50.0f);
        PhotonNetwork.InstantiateSceneObject(safetyZonePrefab.name, new Vector3(x, 0.0f, z), Quaternion.identity);
    }

    void InitializeCountdown()
    {
        countdownObject = PhotonNetwork.InstantiateSceneObject(countdownPrefab.name, Vector3.zero, Quaternion.identity);
        //countdownObject = PhotonNetwork.InstantiateSceneObject(countdownPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
