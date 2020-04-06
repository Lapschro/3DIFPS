using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CDTextSync : MonoBehaviour, IPunObservable
{
    public UnityEngine.UI.Text countdownText;

    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(this.countdownText.text);
    //    }
    //    else
    //    {
    //        this.remainingTime = (float) stream.ReceiveNext();
    //        this.countdownText.text = ((int)remainingTime).ToString("D2");
    //    }
    //}
}
