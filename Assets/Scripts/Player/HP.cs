using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int hp;

    public void Damage(int damage) {
        hp -= damage;
    }

    bool IsDead() {
        return hp <= 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead()) {
            Destroy(gameObject);
        }
    }
}
