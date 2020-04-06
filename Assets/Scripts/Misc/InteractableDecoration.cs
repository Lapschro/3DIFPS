using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class InteractableDecoration : MonoBehaviourPun
{
    //Plays audio wheniteracts with player

    [PunRPC]
    public void PlayAudio() {
        Debug.Log("Rustle Rustle");
    }


    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            photonView.RPC("PlayAudio", RpcTarget.All);
        }
    }
}
