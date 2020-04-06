using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponSpawner : MonoBehaviourPun
{

    public GameObject[] weapons;

    public Transform[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient) {
            foreach (Transform trans in spawnLocations) {
                int i = Random.Range(0, weapons.Length);
                PhotonNetwork.InstantiateSceneObject(Path.Combine("Weapons", weapons[i].name), trans.position, Quaternion.identity);
            }
        }
    }

    [PunRPC]
    public void SpawnWeaponOnWorld(int weaponID, Vector3 at) {
        Weapon weapon = PhotonView.Find(weaponID).gameObject.GetComponent<Weapon>();
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("Called");
            PhotonNetwork.InstantiateSceneObject(Path.Combine("Weapons", weapon.nameInPath), at, Quaternion.identity);
        }
    }


    private void OnDrawGizmos() {
        foreach (Transform trans in spawnLocations) {
            Gizmos.color = new Color(0f,0f,1f);
            Gizmos.DrawWireSphere(trans.position, 2f);
        }
    }
}
