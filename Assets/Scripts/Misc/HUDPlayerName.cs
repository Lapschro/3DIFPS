using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class HUDPlayerName : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text textConnection;
    [SerializeField]
    private UnityEngine.UI.InputField inputField;

    [SerializeField]
    private GameObject nameUI;

    public int maxNameLength;

    bool playerSpawned = false;
    bool nameDefined = false;

    public GameObject playerPrefab;
    public GameObject HUDPrefab;

    // Start is called before the first frame update
    void Start()
    {
        inputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            textConnection.text = "Disconnected";
        }
        else
        {
            if (!PhotonNetwork.IsConnectedAndReady)
            {
                textConnection.text = "Connected";
            }
            else
            {
                string str = PhotonNetwork.CloudRegion is null ? "" : PhotonNetwork.CloudRegion.ToUpper();
                textConnection.text = "Ready (" + str + ")";
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ConfirmName();
        }
    }

    public void ConfirmName()
    {
        if (String.IsNullOrEmpty(inputField.text))
            PhotonNetwork.NickName = "Player";
        else if (inputField.text.Length > maxNameLength)
            PhotonNetwork.NickName = inputField.text.Substring(0, maxNameLength);
        else
            PhotonNetwork.NickName = inputField.text;

        if (!playerSpawned)
            SpawnPlayer();

        nameDefined = true;
        nameUI.SetActive(false);
    }

    private void SpawnPlayer()
    {
        playerSpawned = true;
        GameObject playerInstance = Instantiate(playerPrefab, new Vector3(0.0f, 2.0f, -10.0f), Quaternion.identity);
        GameObject hudInstance = Instantiate(HUDPrefab, Vector3.zero, Quaternion.identity);
        hudInstance.GetComponent<HUDManager>().player = playerInstance;
    }


    private void OnEnable()
    {
        inputField.text = PhotonNetwork.NickName;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

}
