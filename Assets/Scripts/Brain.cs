using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int DNALength = 2;
    public float timeAlive;
    public float timeWalking;
    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    bool seeGround = true;

    // Ethan
    public GameObject ethanPrefab;
    GameObject ethan;

    private void OnDestroy()
    {
        Destroy(this.ethan);
    }
    //

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dead")
        {
            this.alive = false;
            // wiping out stats when they die
            this.timeAlive = 0;
            this.timeWalking = 0;
        }
    }

    public void Init()
    {
        // initialise DNA
        // 0 forward
        // 1 left
        // 2 right
        this.dna = new DNA(this.DNALength, 3);
        this.timeAlive = 0;
        this.alive = true;

        // Ethan
        this.ethan = Instantiate(this.ethanPrefab, this.transform.position, this.transform.rotation);
        this.ethan.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = this.transform;
        //
    }

    private void Update()
    {
        if (!this.alive) return;
                
        Vector3 pos = this.eyes.transform.position;
        Vector3 forward = this.eyes.transform.forward * 10;
        // Debug.DrawRay(pos, forward, Color.red, 10);        

        this.seeGround = false;
        RaycastHit hit;
        if(Physics.Raycast(pos, forward, out hit))
        {
            if(hit.collider.gameObject.tag == "platform")
            {
                this.seeGround = true;
            }
        }
        
        this.timeAlive = PopulationManager.elapsed;

        // read DNA
        float turn = 0;
        float move = 0;
        if(this.seeGround)
        {
            // make v relative to character and always move forward
            if(this.dna.GetGene(0) == 0)
            {
                move = 1;
                this.timeWalking++;
            }
            else if(this.dna.GetGene(0) == 1) { turn = -90; }
            else if (this.dna.GetGene(0) == 2) { turn = 90; }
        }
        else
        {
            if (this.dna.GetGene(1) == 0)
            {
                move = 1;
                this.timeWalking++;
            }
            else if (this.dna.GetGene(1) == 1) { turn = -90; }
            else if (this.dna.GetGene(1) == 2) { turn = 90; }
        }

        this.transform.Translate(0, 0, move * 0.1f);
        this.transform.Rotate(0, turn, 0);
    }
}
