using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1;
    public int nOfBullets = 1;
    public float cooldown = 1;

    float timer;

    public GameObject bulletPrefab;
    public LayerMask layermask;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
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
            Debug.Log("Pew");
            Debug.DrawRay(transform.position, dir);

            RaycastHit hit;

            bool onHit = Physics.Raycast(origin, dir, out hit, 10f, layermask);
            if (onHit) {
                line.enabled = true;
                Vector3[] points = {
                    transform.position,
                    hit.point
                };
                line.SetPositions(points);
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.GetComponent<HP>()?.Damage(damage);
            }
        }
        else {
        }
    }
}
