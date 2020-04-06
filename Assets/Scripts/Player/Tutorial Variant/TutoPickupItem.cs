using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;

public class TutoPickupItem : MonoBehaviour{
    public LayerMask layermask;
    InputMaster input;

    bool getWeapon;

    public GameObject currentWeapon;

    public Text tutorialText;
    public GameObject showTutTextPos;

    private void OnDisable() {
        input.Disable();
    }

    private void OnEnable() {
        input.Enable();
    }

    private void Awake() {
        input = new InputMaster();

    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, layermask)) {
            Debug.Log(hit.collider.gameObject.name);
            Vector3 pos = Camera.main.WorldToScreenPoint(showTutTextPos.transform.position);
            tutorialText.enabled = true;
            tutorialText.rectTransform.position = pos;
            Debug.Log(pos);

            if (input.Player.Interact.triggered) {
                ChangeWeapon(hit.collider.gameObject);
            }
        }
        else {
            tutorialText.enabled = false;
        }

        if (currentWeapon) {

        }
    }
    
    public void ChangeWeapon(GameObject photonID) {
        GameObject go = photonID;

        go.GetComponent<Rigidbody>().isKinematic = true;
        go.GetComponent<Collider>().enabled = false;
        go.GetPhotonView().RequestOwnership();
        go.transform.parent = this.transform;
        go.transform.rotation = this.transform.rotation;
        go.transform.localPosition = go.GetComponent<Weapon>().offset;
        

        TutoPlayer player = GetComponentInParent<TutoPlayer>();
        player.playerWeapon = go.GetComponent<Weapon>();

        currentWeapon = go;
    }

}
