using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Score : MonoBehaviourPun, IPunObservable
{
    int points;
    public int Points {
        get { return points; }
        set { points = value; }
    }

    [PunRPC]
    public void AddPoint() {
        points++;
        if (photonView.IsMine)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Score", points } });
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(points);
        } else if (stream.IsReading) {
            points = (int)stream.ReceiveNext();
        }
    }

    void Awake()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Score", points } });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
