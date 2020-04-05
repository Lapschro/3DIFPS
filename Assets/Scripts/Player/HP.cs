using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;

public class HP : MonoBehaviourPun {
    public int hp;

    bool insideSafetyZone;

    public float zoneDamageTickTime = 0.5f;
    float timer;

    [PunRPC]
    public void Damage(int damage) {
        hp -= damage;
    }

    public bool IsDead() {
        return hp <= 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = zoneDamageTickTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!insideSafetyZone) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                photonView.RPC("Damage", RpcTarget.All, 1);
                timer = zoneDamageTickTime;
            }
        }

        if (IsDead()) {
            Destroy(gameObject);
            if (photonView.IsMine) {
                PhotonNetwork.LeaveRoom();
                SceneManager.LoadScene(FindObjectOfType<SetupManager>().lobbyScene);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Zona") {
            Debug.Log("Enter ring");
            insideSafetyZone = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Zona") {
            Debug.Log("Exit ring");
            insideSafetyZone = false;
            timer = zoneDamageTickTime;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(this.hp);
        }
        else {
            hp = (int)stream.ReceiveNext();
        }
    }
}
