using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CDTextSync : MonoBehaviour, IPunObservable
{
    public UnityEngine.UI.Text countdownText;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.countdownText.text);
        }
        else
        {
            this.countdownText.text = (string)stream.ReceiveNext();
        }
    }
}
