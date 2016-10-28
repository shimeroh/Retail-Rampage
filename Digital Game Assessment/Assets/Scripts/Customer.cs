﻿using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particles;
    public Transform[] path;
    [SerializeField]
    private int currentPathPosition = 0;
    [SerializeField]
    private float customerSpeed;
    public Store store;

    Transform target;
    bool canMove = true;
	
    void Start()
    {
        particles.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (currentPathPosition < this.path.Length)
        {
            if(target == null)
            {
                target = path[currentPathPosition];
                
            }

            MoveToPosition();
        }
	}

    void MoveToPosition()
    {
        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, customerSpeed * Time.deltaTime);
            if (transform.position == target.position)
            {
                ++currentPathPosition;
                if (currentPathPosition < path.Length)
                {
                    target = path[currentPathPosition];
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Interact" && col.gameObject.GetInstanceID() == store.storeOwner.interactObject.GetInstanceID())
        {
            particles.gameObject.SetActive(true);

            particles.Emit(5);

            particles.transform.parent = null;

            store.AddSale();
            Destroy(this.gameObject);
        }
    }
}
