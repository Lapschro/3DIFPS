using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Energy : MonoBehaviourPun, IPunObservable
{
    public float energy = 100;
    public Renderer objectRenderer;
    public PlayerControl player;

    public float minimunEnergy;

    public bool insideSafetyZone = true;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(energy);
        }else if (stream.IsReading) {
            energy = (float)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(energy > minimunEnergy) {
            objectRenderer.enabled = false;
        }
        else {
            objectRenderer.enabled = true;
        }

        if(!insideSafetyZone)
        {
            energy -= 80f * Time.deltaTime;
        }
        else
        {
            if (player.moving)
            {
                energy += 2f * Time.deltaTime;
            }
            else
            {
                energy -= 4f * Time.deltaTime;
            }
        }
        
        energy = Mathf.Clamp(energy, 0, 100);
    }


}
