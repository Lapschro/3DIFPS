using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponSpawner : MonoBehaviourPun
{

    public GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.InstantiateSceneObject(Path.Combine("Weapons", weapons[1].name), transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void SpawnWeaponOnWorld(int weaponID, Vector3 at) {
        Weapon weapon = PhotonView.Find(weaponID).gameObject.GetComponent<Weapon>();
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("Called");
            PhotonNetwork.InstantiateSceneObject(Path.Combine("Weapons", weapon.nameInPath), at, Quaternion.identity);
        }
    }

}
