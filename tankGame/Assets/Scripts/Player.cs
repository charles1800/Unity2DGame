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
    public Sprite[] tankMovaScript;//顺时针
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
    //固定物理帧0.02s执行一次
    private void FixedUpdate()
    {

        Move();
        //攻击CD
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
            //控制
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
    /// 坦克的移动方法。
    /// </summary>
    private void Move()
    {
        //获取玩家xy轴输入
        //h值-1,0,1值。
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //lock 锁
        int hlock = 0;
        int vlock = 0;
        //定义水平正方向 
        //Vector3.right相当于Vector3(1,0,0)两者效果相同
        //space.World 表示坐标系是固定的，不会随着对象的改变而改变
        //space.Self 则会改变坐标系

        //此办法无法解决双键同按优先级的问题
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

        if (h == 0 && v == 0)//AKF的情况
        {
            hlock = vlock = 0;
        }

        if (h != 0 && v == 0)//竖向
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
        else if (h == 0 && v != 0)//横向
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
        else if (h != 0 && v != 0)//双键
        {
            if (vlock * hlock == 0)//这个可以防止AFK后快速双键齐按导致斜走
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
            //                当前对象的位置。        旋转角度。Quaternion.EulerAngles(transform.eulerAngles) 当前的角度。
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
