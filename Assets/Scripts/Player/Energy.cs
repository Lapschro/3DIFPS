using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Energy : MonoBehaviourPun, IPunObservable
{
    public float energy = 100;
    public Renderer objectRenderer;
    public GameObject weapon;
    public PlayerControl player;

    public float minimunEnergy;

    public bool insideSafetyZone = true;

    public bool isControllable;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(energy);
        }else if (stream.IsReading) {
            energy = (float)stream.ReceiveNext();
        }
    }

    private void Awake() {
        isControllable = photonView.IsMine || !PhotonNetwork.InRoom;
    }

    // Update is called once per frame
    void Update()
    {
        if(energy > minimunEnergy) {
            objectRenderer.enabled = false;
            if(!isControllable) //Should make weapon disappear only on other clients
                foreach (MeshRenderer renderer in weapon.GetComponentsInChildren<MeshRenderer>()) {
                    renderer.enabled = false;
                }
        }
        else {
            objectRenderer.enabled = true;
            if (!isControllable) //Should make weapon disappear only on other clients
                foreach (MeshRenderer renderer in weapon.GetComponentsInChildren<MeshRenderer>()) {
                    renderer.enabled = true;
                }
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
