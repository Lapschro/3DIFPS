using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class HP : MonoBehaviourPun {
    public int hp;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead()) {
            Destroy(gameObject);
        }
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(this.hp);
        }
        else {
            hp = (int)stream.ReceiveNext();
        }
    }*/
}
