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
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(points);
        } else if (stream.IsReading) {
            points = (int)stream.ReceiveNext();
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
