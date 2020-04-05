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

    public GameObject bulletPrefab;
    public LayerMask layermask;
    LineRenderer line;

    bool isControllable;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        isControllable = photonView.IsMine || !PhotonNetwork.InRoom;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0) {
            timer -= Time.deltaTime;
        }
    }

    public void Shoot(Vector3 origin, Vector3 dir) {
        if(timer  <= 0) {
            //bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            timer = cooldown;
            Debug.DrawRay(transform.position, dir);

            RaycastHit hit;

            bool onHit = Physics.Raycast(origin, dir, out hit, 1000f, layermask);
            if (onHit) {
                line.enabled = true;
                Vector3[] points = {
                    transform.position,
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
            }
        }
    }
}
