using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUIInDistance : MonoBehaviour
{

    public Transform position;
    public Text txt;
    public float maxDistance;
    public Transform player;
    public Image image;

    Color initial;
    Color final;

    Color imageInitial;
    Color imageFinal;

    // Start is called before the first frame update
    void Start()
    {
        initial = txt.color;
        final = initial;
        final.a = 0;

        if (image) {
            imageInitial = image.color;
            imageFinal = imageInitial;
            imageFinal.a = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color color = txt.color;

        float dist = (player.position - position.position).magnitude + 0.001f;

        txt.color = Color.Lerp(initial, final, Mathf.Clamp01((dist / maxDistance)));

        if (image) {
            image.color = Color.Lerp(imageInitial, imageFinal, Mathf.Clamp01((dist / maxDistance)));
        }
    }
}
