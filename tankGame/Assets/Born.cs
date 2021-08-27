using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public 
    // Start is called before the first frame update
    void Start()
    {
        //延时调用方法。
        Invoke("TankBorn",1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TankBorn()
    {                             //产生位置，       旋转度数。
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
