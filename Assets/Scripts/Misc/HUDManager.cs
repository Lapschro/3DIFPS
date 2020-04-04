using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HUDManager : MonoBehaviourPun
{
    public GameObject player;

    private void Awake()
    {
        if (PhotonNetwork.InRoom && !photonView.IsMine)
        {
            enabled = true;
        }
    }
}

