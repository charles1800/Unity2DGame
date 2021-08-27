using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isProtected= true;
    private float protectTime = 3f;
    public GameObject protectedPrefab;

    private float bulletTime = 0.4f;
    private Vector3 bulletEulerAngles;
    public float moveSpeed = 3;
    private SpriteRenderer sr;
    public GameObject bullet;
    public Sprite[] tankMovaScript;//˳ʱ��
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //bullet = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //�̶�����֡0.02sִ��һ��
    private void FixedUpdate()
    {

        Move();
        //����CD
        if (bulletTime >= 0.4f)
        {
            Attack();
        }
        else
        {
            bulletTime += Time.deltaTime;
        }
        if (isProtected)
        {
            //����
            protectedPrefab.SetActive(true);
            protectTime -= Time.deltaTime;
            if(protectTime <= 0)
            {
                isProtected = false;
                protectedPrefab.SetActive(false);
            }
        }       
    }

    /// <summary>
    /// 
    /// ̹�˵��ƶ�������
    /// </summary>
    private void Move()
    {
        //��ȡ���xy������
        //hֵ-1,0,1ֵ��
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //lock ��
        int hlock = 0;
        int vlock = 0;
        //����ˮƽ������ 
        //Vector3.right�൱��Vector3(1,0,0)����Ч����ͬ
        //space.World ��ʾ����ϵ�ǹ̶��ģ��������Ŷ���ĸı���ı�
        //space.Self ���ı�����ϵ

        //�˰취�޷����˫��ͬ�����ȼ�������
        {
            /*if (Input.GetKey(KeyCode.LeftArrow))
            {
                sr.sprite = tankMovaScript[3];
                h = -1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                h = 1;
                sr.sprite = tankMovaScript[1];
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                v = 1;
                sr.sprite = tankMovaScript[0];
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                v = -1;
                sr.sprite = tankMovaScript[2];
            }*/
        }

        if (h == 0 && v == 0)//AKF�����
        {
            hlock = vlock = 0;
        }

        if (h != 0 && v == 0)//����
        {
            hlock = 2;
            vlock = 1;
            if (h > 0)
            {
                sr.sprite = tankMovaScript[1];
                bulletEulerAngles = new Vector3(0, 0, -90);
            }
            else
            {
                sr.sprite = tankMovaScript[3];
                bulletEulerAngles = new Vector3(0, 0, 90);
            }

        }
        else if (h == 0 && v != 0)//����
        {
            hlock = 1;
            vlock = 2;
            if (v > 0)
            {
                sr.sprite = tankMovaScript[0];
                bulletEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                sr.sprite = tankMovaScript[2];
                bulletEulerAngles = new Vector3(0, 0, -180);
            }
        }
        else if (h != 0 && v != 0)//˫��
        {
            if (vlock * hlock == 0)//������Է�ֹAFK�����˫���밴����б��
            {
                return;
            }
            else if (hlock < vlock)
            {
                h = 0;
            }
            else if (hlock > vlock)
            {
                v = 0;
            }
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //                ��ǰ�����λ�á�        ��ת�Ƕȡ�Quaternion.EulerAngles(transform.eulerAngles) ��ǰ�ĽǶȡ�
            Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            bulletTime = 0;
        }
    }
    private void Die()
    {
        if (isProtected)
        {
         
            return;
        }
            //Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);       
    }
}
