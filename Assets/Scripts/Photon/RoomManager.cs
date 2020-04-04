using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int sceneIndex;
    [SerializeField]
    private int lobbySceneIndex;

    /// Called when the local player left the room. We need to load the launcher scene.
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(lobbySceneIndex);
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        //base.OnDisable();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined");
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("staring game");
            PhotonNetwork.LoadLevel(sceneIndex);
        }

    }
}
