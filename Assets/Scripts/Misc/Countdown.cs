using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Countdown : MonoBehaviour, IPunObservable
{
    public UnityEngine.UI.Text countdownText;
    public float countdownTime;
    public int nextScene;

    float startTime;
    float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //remainingTime = Mathf.Clamp(countdownTime - (Time.realtimeSinceStartup - startTime), 0.0f, countdownTime);
        Count();
        //countdownText.text = ((int) remainingTime).ToString("D2");

        if (remainingTime <= 0.0f)
        {
            // Let the revels begin
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(nextScene);
            gameObject.SetActive(false);
        }
    }

    [PunRPC]
    void Count()
    {
        remainingTime = Mathf.Clamp(countdownTime - (Time.realtimeSinceStartup - startTime), 0.0f, countdownTime);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.remainingTime);
            stream.SendNext(this.countdownText.text);
        }
        else
        {
            this.remainingTime = (float) stream.ReceiveNext();
            this.countdownText.text = ((int)remainingTime).ToString("D2");
        }
    }
}
