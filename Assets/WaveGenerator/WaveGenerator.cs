﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {


	public List<GameObject> WavePool =  new List<GameObject>();

	public float speed=1f;
	public float timer=1f;
    public EnemyType waveType;
    public float size = 1f;

    // Use this for initialization
    void Start () {
        waveType = EnemyType.None;
        pullFromPool(1f,2f, Color.white);
	}
	
	// Update is called once per frame
	void Update ()
    {
		timer += Time.deltaTime * speed;
		if (timer > 1f) {
			timer = 0f;
            switch (waveType)
            {
                case EnemyType.Classic:
                    pullFromPool(1f, size, Color.red);
                    break;
                case EnemyType.Punk:
                    pullFromPool(1f, size, Color.green);
                    break;
                case EnemyType.Reggae:
                    pullFromPool(1f, size, Color.yellow);
                    break;
                case EnemyType.Eletronic:
                    pullFromPool(1f, size, Color.blue);
                    break;
                case EnemyType.None:
                    pullFromPool(1f, size, Color.white);
                    break;
            }
        }
	}
	public void pullFromPool(float lifeSpan, float targetSize, Color col){
		if(WavePool.Count>0){
			WavePool [0].SetActive(true);
			WavePool [0].GetComponent<Wave>().life = 0f;
			WavePool [0].GetComponent<Wave>().lifeSpan = lifeSpan;
			WavePool [0].GetComponent<Wave>().targetSize = targetSize;
			WavePool [0].GetComponent<Wave> ().transform.localScale = new Vector3(0.25f,0.075f,0.25f);
			WavePool [0].GetComponent<Wave>().myPool = this;
			WavePool [0].GetComponent<SpriteRenderer> ().color = col;
			WavePool.RemoveAt(0);
		}
	}
	public void addToPool(GameObject obj)
	{
		WavePool.Add (obj);
		obj.SetActive (false);
	}
}

