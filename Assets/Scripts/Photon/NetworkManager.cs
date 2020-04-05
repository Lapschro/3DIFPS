using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


// Must inherit from MonoBehaviourPunCallbacks
public class NetworkManager : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        // On start, try to connect to Photon's master server
        if(!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting to Photon's master server...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // OnConnectedToMaster is called after the connection is established
    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);
        
        // All clients must use the same scene as the master client
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // OnConnectedToMaster is called when the client disconnects from the master server
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected: " + cause);
        Debug.Log("retrying...");
        //base.OnDisconnected(cause);
        PhotonNetwork.ConnectUsingSettings();
    }

}
