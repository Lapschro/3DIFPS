using Photon.Pun;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private void Start() {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs","Player","Player"), Vector3.zero, Quaternion.identity);
    }
}
