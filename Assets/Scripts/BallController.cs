using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour {

    public Rigidbody rb;
    public float impulseForce = 5f;

    private Vector3 startPos = new Vector3(0,4.03499985f,-1.33000004f);
    public int perfectPass = 0;
    private bool ignoreNextCollision;
    public bool isSuperSpeedActive;
    [SerializeField]GameObject splaterEffect;
    [SerializeField]AudioSource sfxPlayer;
    [SerializeField]AudioClip bounceClip, speedClip, splashClip,breakingClip;

    private void Update()
    {
        // activate super speed
        if (perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
            sfxPlayer.PlayOneShot(speedClip);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (ignoreNextCollision)
            return;
        
        
        if (isSuperSpeedActive)
        {
            if (!other.transform.GetComponent<Goal>())
            {
                GameObject parent = other.transform.parent.gameObject;
                var allHelixs = parent.GetComponentsInChildren<Transform>();

                foreach(Transform obj in allHelixs)
                {
                    if(!obj.GetComponent<Rigidbody>())
                    {
                        obj.AddComponent<Rigidbody>().AddForce(-Vector3.forward, ForceMode.Force);
                        obj.AddComponent<AudioSource>().PlayOneShot(breakingClip);
                    }
                }

                Destroy(other.transform.parent.gameObject, 0.5f);
            }
        }
        // If super speed is not active and a death part git hit -> restart game
        else
        {
            DeathPart deathPart = other.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                sfxPlayer.PlayOneShot(splashClip);
                rb.isKinematic = true;
                //deathPart.HittedDeathPart();
                GameManager.singleton.EndGame();
                return;
            }
        }

        //create a splater effect
        sfxPlayer.PlayOneShot(bounceClip);
         CreateSplat(other.transform ,new Vector3(other.GetContact(0).point.x, other.transform.position.y + 0.362f, other.GetContact(0).point.z));

        rb.velocity = Vector3.zero; // Remove velocity to not make the ball jump higher after falling done a greater distance
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
        // Safety check
        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);
        // Handlig super speed
        perfectPass = 0;
        isSuperSpeedActive = false;
    }
    void CreateSplat(Transform parent, Vector3 point)
    {
        GameObject splat = Instantiate(splaterEffect, parent);
        splat.transform.position = point;
    }
    public void ResetBall()
    {
        transform.position = startPos;
        rb.isKinematic = false;
    }
    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }
}
