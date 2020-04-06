using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{

    //enemy placeholder
    public GameObject enemy;

    private float distance;
    private bool started = false;
    private int eventIndex = -1;
    protected CustomEventEmitter eventEmitter;

    // Start is called before the first frame update
    void Start()
    {
        //Placeholder stuff
        enemy = GameObject.Find("Inimigo");
        eventEmitter = CustomEventEmitter.instance;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, enemy.transform.position);
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
