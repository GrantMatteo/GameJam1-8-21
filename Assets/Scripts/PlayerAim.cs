using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    Rotator headRotator;
    Rotator bodyRotator;
    Transform effector;
    float pointDistance = 1f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        bodyRotator = transform.GetComponent<Rotator>();
        headRotator = transform.FindDeepChild("Head").GetComponent<Rotator>();
        effector = transform.parent.FindDeepChild("target");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Shoot");
        }

        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(transform.position, target) <= pointDistance)
            return;
        effector.position = target;
        headRotator.transform.up = (target - (Vector2)headRotator.transform.position).normalized; //.Face(target, lockZ: false);
        bodyRotator.transform.up = (target - (Vector2)bodyRotator.transform.position).normalized; //.Face(target, lockZ: false);
    }
}
