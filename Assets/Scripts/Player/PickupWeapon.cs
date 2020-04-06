using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PickupWeapon : MonoBehaviourPun
{
    public LayerMask layermask;
    InputMaster input;

    bool getWeapon;

    public GameObject currentWeapon;

    private void OnDisable() {
        input.Disable();
    }

    private void OnEnable() {
        input.Enable();
    }

    private void Awake() {
        input = new InputMaster();

        currentWeapon = GetComponentInChildren<Weapon>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 2f, layermask)) {
            Debug.Log(hit.collider.gameObject.name);
            if (input.Player.Interact.triggered) {
                photonView.RPC("ChangeWeapon", RpcTarget.All, hit.collider.gameObject.GetPhotonView().ViewID);
                //ChangeWeapon(hit.collider.gameObject);
            }
        }
    }

    [PunRPC]
    public void ChangeWeapon(int photonID) {
        GameObject go = PhotonView.Find(photonID).gameObject;
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.GetComponent<Collider>().enabled = true;
        currentWeapon.GetPhotonView().TransferOwnership(5);

        go.GetComponent<Rigidbody>().isKinematic = true;
        go.GetComponent<Collider>().enabled = false;
        go.GetPhotonView().RequestOwnership();
        go.transform.parent = this.transform;
        go.transform.localPosition = go.GetComponent<Weapon>().offset;
        go.transform.rotation = this.transform.rotation;// currentWeapon.transform.rotation;

        PlayerControl player = GetComponentInParent<PlayerControl>();
        player.playerWeapon = go.GetComponent<Weapon>();
        player.weaponTransform = go.transform;

        //Debug.Log(currentWeapon.GetPhotonView().ViewID);
        //Debug.Log(currentWeapon.transform.position);
        //Debug.Log(FindObjectOfType<WeaponSpawner>());
        if (photonView.IsMine) {
            FindObjectOfType<WeaponSpawner>()?.photonView.RPC("SpawnWeaponOnWorld", RpcTarget.All, currentWeapon.GetPhotonView().ViewID, currentWeapon.transform.position);
        }

        Destroy(currentWeapon.gameObject);
        //foreach (MeshRenderer renderer in currentWeapon.GetComponentsInChildren<MeshRenderer>()) {
        //    renderer.enabled = true;
        //}

        currentWeapon = go;
    }

}
