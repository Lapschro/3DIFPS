using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SafetyZone : MonoBehaviourPun
{
    public float growthRate;
    public float minSize;
    public float maxSize;

    //private Vector3 growthVec;

    // Start is called before the first frame update
    void Start()
    {
        //growthVec = new Vector3(growthRate, growthRate, growthRate);
    }

    // Update is called once per frame
    void Update()
    {
        photonView.RPC("GrowZone", RpcTarget.All);
    }

    [PunRPC]
    void GrowZone()
    {
        float newScale = Mathf.Clamp(gameObject.transform.localScale.x + growthRate, minSize, maxSize);
        gameObject.transform.localScale = new Vector3(newScale, gameObject.transform.localScale.y, newScale);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameObject.transform.localScale);
        }
        else
        {
            Vector3 next = (Vector3) stream.ReceiveNext();
            gameObject.transform.localScale = next;
        }
    }

}
