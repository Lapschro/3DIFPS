using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

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
        currentWeapon.GetPhotonView().TransferOwnership(0);

        go.GetComponent<Rigidbody>().isKinematic = true;
        go.GetComponent<Collider>().enabled = false;
        go.GetPhotonView().RequestOwnership();
        go.transform.position = currentWeapon.transform.position;
        go.transform.rotation = currentWeapon.transform.rotation;
        go.transform.parent = this.transform;

        PlayerControl player = GetComponentInParent<PlayerControl>();
        player.playerWeapon = go.GetComponent<Weapon>();
        player.weaponTransform = go.transform;
        currentWeapon = go;
    }
}
