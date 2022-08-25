using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Selector playerSelector;
    bool placed;
    Vector3 startPos; // where character was instantiated off the grid
    LevelManager manager;
    CameraControl camControl;

    public int health = 1;
    public float shootForce = 1f;
    public GameObject shootPoint;
    public Animator anim;
    public Projectile bulletPrefab;

    void Start()
    {
        placed = false;
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        camControl = Camera.main.GetComponent<CameraControl>();
        startPos = transform.position;
    }

    // called by selector when character is put on tile
    public void Placed()
    {
        anim.Play("Unhover");
        camControl.TriggerLightScreenShake();
        if (!placed)
        {
            manager.CharacterPlaced(gameObject); // passes in self so the level manager can keep a record of all placed characters
            placed = true;
        }
    }

    // called by selector when character is dropped anywhere not on the grid
    public void ResetPos()
    {
        transform.position = startPos;
        anim.Play("Unhover");
        if (placed)
        {
            manager.CharacterRemoved(gameObject);
            placed = false;
        }
    }

    private void OnMouseOver()
    {
        anim.Play("HoveredAnim");
        playerSelector.SetSelectedCharacter(gameObject);

    }

    private void OnMouseExit()
    {
        if(GetComponent<BoxCollider2D>().enabled == true)
        {
            anim.Play("Unhover");
        }
        
        playerSelector.SetSelectedCharacter(null);
    }

    public void Shoot()
    {
        Projectile bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, shootPoint.transform.position, transform.rotation);
        bulletInstance.rb.AddForce(shootPoint.transform.right * shootForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Projectile>() != null)
        {
            health -= other.GetComponent<Projectile>().damage;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                camControl.TriggerHeavyScreenShake();
                manager.CharacterKilled();
                Destroy(gameObject);
            }
        }
    }
}
