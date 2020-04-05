using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HUDScoreboard : MonoBehaviour
{
    // We are assigned to the HUD game object;
    [SerializeField]
    private UnityEngine.UI.Text textPlayerList;
    [SerializeField]
    private UnityEngine.UI.Text textScoreList;
    [SerializeField]
    private GameObject scoreboardPanel;

    //Score score;

    // Start is called before the first frame update
    void Start()
    {
        //score = GetComponent<HUDManager>().player.GetComponent<Score>();
        scoreboardPanel.SetActive(false);
        //Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        //text = GetComponent<UnityEngine.UI.Text>();
        //playerHP = hudManager.player.GetComponent<HP>();
        //playerEnergy = hudManager.player.GetComponent<Energy>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
//          BuildScoreboard();
            scoreboardPanel.SetActive(true);
            BuildScoreboard();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboardPanel.SetActive(false);
        }
        /*
        if (Input.GetKey(KeyCode.Tab))
        {
            scoreboardPanel.SetActive(true);
        }
        else
        {
            scoreboardPanel.SetActive(false);
        }*/
    }

    void BuildScoreboard()
    {
        textPlayerList.text = "";
        textScoreList.text = "";
        

        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log("playername: " + player.NickName);
            textPlayerList.text += player.NickName + "\n";
            Debug.Log("property: " + player.CustomProperties);
            textScoreList.text += player.CustomProperties["Score"].ToString() + "\n";
        }
    }
}
