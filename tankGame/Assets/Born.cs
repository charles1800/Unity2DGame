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
        //��ʱ���÷�����
        Invoke("TankBorn",1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TankBorn()
    {                             //����λ�ã�       ��ת������
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
