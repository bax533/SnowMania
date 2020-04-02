using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModelScript : MonoBehaviour
{

    public Animator anim;
    float timer = 0f;
    int timeSpace = 15;
    int currentTrick = 0; //0 - tail; 1 - mute; 2 - cross;
    public float rotSpeed = 1f;

    string[] trick = { "tail", "mute", "cross", "japan", "truck", "bow" };

    void Animate()
    {
        timer += Time.deltaTime;
        if (timer > timeSpace)
        {
            timer = 0;
            int x = Random.Range(0, 6);
            while (x == currentTrick)
                x = Random.Range(0, 6);

            currentTrick = x;

            anim.SetTrigger(trick[x]);
        }
    }

    void Rotate()
    {
        transform.Rotate(0f, -rotSpeed, 0f);
    }

    void Update()
    {
        Animate();
        Rotate();
    }
}
