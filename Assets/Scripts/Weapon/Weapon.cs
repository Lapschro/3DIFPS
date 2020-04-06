using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Weapon : MonoBehaviourPun
{
    public int damage = 1;
    public int nOfBullets = 1;
    public float cooldown = 1;

    float timer;

    float lineTimer = 0.3f;

    public GameObject startOfLine;
    public LayerMask layermask;
    LineRenderer line;

    bool isControllable;

    public ParticleSystem particles;

    public string nameInPath;

    protected CustomEventEmitter eventEmitter;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        eventEmitter = CustomEventEmitter.instance;
        isControllable = photonView.IsMine || !PhotonNetwork.InRoom;

    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0) {
            timer -= Time.deltaTime;
            lineTimer -= Time.deltaTime;

            line.widthMultiplier = lineTimer;
        }

        if(lineTimer <= 0) {
            line.enabled = false;
        }

    }

    public void Shoot(Vector3 origin, Vector3 dir) {
        if(timer  <= 0) {
            //bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            timer = cooldown;
            Debug.DrawRay(transform.position, dir);

            // eventEmitter.PlaySFXOneShot(FMODEvents.events[(int)FMODEvents.Guns.LASER_SHOT], transform.position);
             int eventIndex;
            if(gameObject.name.Contains("glock 1")){
                eventIndex = eventEmitter.StartEventThatFollows(FMODEvents.events[(int)FMODEvents.Guns.GLOCK_SHOT], gameObject, gameObject.GetComponent<Rigidbody>());
            }else{
                eventIndex = eventEmitter.StartEventThatFollows(FMODEvents.events[(int)FMODEvents.Guns.LASER_SHOT], gameObject, gameObject.GetComponent<Rigidbody>());
            }
            
            eventEmitter.PlayEventInstance(eventIndex);

            particles.Play();

            RaycastHit hit;

            bool onHit = Physics.Raycast(origin, dir, out hit, 1000f, layermask);
            if (onHit) {
                line.enabled = true;
                Vector3[] points = {
                    startOfLine.transform.position,
                    hit.point
                };
                line.SetPositions(points);
                if (isControllable) {
                    Debug.Log(hit.collider.gameObject.name);
                    hit.collider.GetComponent<HP>()?.photonView.RPC("Damage", Photon.Pun.RpcTarget.All, damage);
                    if (hit.collider.GetComponent<HP>()?.IsDead() ?? false) {
                        GetComponentInParent<Score>()?.photonView.RPC("AddPoint", RpcTarget.All);
                    }
                }
                lineTimer = 0.3f;
            }

        }
    }
}
