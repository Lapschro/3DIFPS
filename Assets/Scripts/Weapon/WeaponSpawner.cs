using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{

    public GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.InstantiateSceneObject(Path.Combine("Weapons", weapons[0].name), transform.position, Quaternion.identity);
    }

}
