using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class SetupManager : MonoBehaviour
{

    public GameObject playerPrefab;

    public int lobbyScene;

    public List<GameObject> playerObjects;

    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
        Debug.Log("instantiate player");
        //PhotonNetwork.Instantiate("CylinderPlayer", new Vector3(Random.Range(-40.0f,40.0f), 0, Random.Range(-40.0f, 40.0f)), Quaternion.identity);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-40.0f, 40.0f), 5.0f, Random.Range(-40.0f, 40.0f)), Quaternion.identity);
    }
}
