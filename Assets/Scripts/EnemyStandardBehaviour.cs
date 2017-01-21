﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandardBehaviour : MonoBehaviour
{
    public float timetoWaitToEnjoy = 1f;
    private float startLenght;
    Vector2 center;
    public bool walking;
    public float angle, length, velocity;
    Vector2 position;
    Animator animator;
    EnemyType type;
    public float velScaleDown;

    public List<GameObject> prefabType;
    // Use this for initialization
    virtual protected void Start()
    {
        position = new Vector2();
    }

    public void setType(EnemyType type)
    {
        this.type = type;
        int i = (int)type;
        if (prefabType[i] != null)
        {
            GameObject g = GameObject.Instantiate<GameObject>(prefabType[i], transform, false);
            animator = g.GetComponent<Animator>();
        }
    }
    public void Initialize(float a, float l, float v)
    {
        startLenght = l;
        angle = a;
        length = l;
        velocity = v;
        walking = true;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (walking)
        {
            length -= velocity * Time.deltaTime;
            position.x = center.x + length * Mathf.Cos(angle * Mathf.Deg2Rad);
            position.y = center.y + length * Mathf.Sin(angle * Mathf.Deg2Rad);

            transform.position = position;
        }

        if (Vector2.Distance(transform.position, center) < 0.5)
        {
            //GetComponent<SpriteRenderer>().color = Color.red;
            Destroy(gameObject);
        }
    }

    IEnumerator _scaleDown()
    {
        Vector3 v = transform.localScale;
        while(v.x > 0)
        {
            v.x -= velScaleDown * Time.deltaTime;
            v.y -= velScaleDown * Time.deltaTime;
            transform.localScale = v;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }


    IEnumerator _waitToEnjoy()
    {
        yield return new WaitForSeconds( Vector2.Distance(transform.position, center) / startLenght * timetoWaitToEnjoy);
        animator.SetTrigger("Enjoy");
        walking = false;
        StartCoroutine("_scaleDown");
    }

    IEnumerator _waitToStop()
    {
        yield return new WaitForSeconds(Vector2.Distance(transform.position, center) / startLenght * timetoWaitToEnjoy);
        StopCoroutine("_scaleDown");
        animator.SetTrigger("StopEnjoying");
        walking = true;
    }

    virtual protected void Enjoy()
    {
        StartCoroutine("_waitToEnjoy");

    }

    virtual protected void StopEnjoying()
    {
        StartCoroutine("_waitToStop");
       
    }
}
