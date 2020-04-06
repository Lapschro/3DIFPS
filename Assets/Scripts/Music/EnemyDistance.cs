using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class EnemyDistance : MonoBehaviourPun
{

    //enemy placeholder

    private float distance;
    private bool started = false;
    private int eventIndex = -1;
    protected CustomEventEmitter eventEmitter;

    // Start is called before the first frame update
    void Start()
    {
        
        //PhotonMapper list of player 
        //Placeholder stuff
        eventEmitter = CustomEventEmitter.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) {
            return;
        }
        List<GameObject> enemy;
        enemy = new List<GameObject>();
        foreach (Player p in PhotonNetwork.PlayerListOthers) {
            enemy.Add((GameObject)p.TagObject);
        }

        GameObject nearestEnemy = null;
        float distance = Mathf.Infinity;

        foreach(GameObject go in enemy) {
            if (go) {
                float goDist = Vector3.Distance(transform.position, go.transform.position);
                if(goDist < distance) {
                    distance = goDist;
                    nearestEnemy = go;
                }
            }
            else {
                continue;
            }
        }

        if (!nearestEnemy)
            return;

        distance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
        //Parâmetro vai de 0 a 10, dividir valor por 3
        distance = distance / 3;

        if(distance <= 10){
            if(!started){
                started = true;
                // print(FMODEvents.events[(int)FMODEvents.General.ENEMY_CLOSE]);
                // eventIndex = eventEmitter.StartEventInstance(FMODEvents.events[(int)FMODEvents.General.ITEM_IDLE]);
                eventIndex = eventEmitter.StartEventThatFollows(FMODEvents.events[(int)FMODEvents.General.ENEMY_CLOSE], gameObject, GetComponentInChildren<Rigidbody>());
                eventEmitter.PlayEventInstance(eventIndex);
            }
            else{
                eventEmitter.SetLocalParameter(FMODEvents.LocalParameters.Distance.ToString(), distance, eventIndex);
            }
        }
        else{
            if(started){
                eventEmitter.StopInstance(eventIndex, true);
                started = false;
            }
        }

        // print(distance);
    }
}
