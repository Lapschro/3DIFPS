using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Countdown : MonoBehaviourPun, IPunObservable
{
    public UnityEngine.UI.Text countdownText;
    public float countdownTime;
    public int nextScene;

    public int minPlayers;
    private bool isCounting = false;

    float startTime;
    float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        countdownText.text = "Waiting for players";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCounting)
        {
            countdownText.text = "Waiting for players";

            if (PhotonNetwork.CurrentRoom.PlayerCount >= minPlayers)
            {
                startTime = (float) PhotonNetwork.Time;
                isCounting = true;
            }
        }
        else
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount >= minPlayers)
            {
                remainingTime = Mathf.Clamp(countdownTime - ((float)PhotonNetwork.Time - startTime), 0.0f, countdownTime);
                countdownText.text = ((int)remainingTime).ToString("D2");

                if (remainingTime <= 0.0f)
                {
                    // Let the revels begin
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    PhotonNetwork.CurrentRoom.IsVisible = false;
                    PhotonNetwork.LoadLevel(nextScene);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                isCounting = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.remainingTime);
            stream.SendNext(this.startTime);
            //stream.SendNext(this.countdownText.text);
        }
        else
        {
            this.remainingTime = (float) stream.ReceiveNext();
            this.startTime = (float) stream.ReceiveNext();
            //this.countdownText.text = (string) stream.ReceiveNext(); 
        }
    }
}
