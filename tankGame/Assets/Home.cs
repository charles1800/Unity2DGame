using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite brokenHome;
    public GameObject explosionPrefab;
    // Start is called before the first frame update1
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void HomeBroken()
    {
        sr.sprite = brokenHome;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
