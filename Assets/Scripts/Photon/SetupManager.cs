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
    public GameObject SFXPrefab;
    //private GameObject safetyZoneActive;

    public Transform[] spawns;

    public GameObject WinCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);

        Debug.Log("instantiate player");
        GameObject playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity);
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
        if(PhotonNetwork.PlayerListOthers.Length <= 0) {
            WinCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
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
