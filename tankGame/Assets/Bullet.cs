using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletMoveSpeed = 10;
    public bool isPlayerBullet;
    Home home;
    // Start is called before the first frame update1
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * bulletMoveSpeed * Time.deltaTime,Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("·¢ÉúÅö×²¡£");
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    
                    collision.SendMessage("Die");
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                Destroy(gameObject);
                break;
            case "Home":
                collision.SendMessage("HomeBroken");
                Destroy(gameObject);
                break;
            case "Enemy":
                break;
            case "AirWall":
                Destroy(gameObject);
                break;
        }
    }
}
