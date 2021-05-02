using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HpState
{
    heal,
    damage,
    invincible
}

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public int id;
    public Client3 client;
    private int sendSeconds = 10;
    private short jumpCnt = 0;
    public short maxJumpCnt = 1;
    public float jumpPower = 10;
    public float rotValue = 90.0f;
    public int hp = 10;

    [SerializeField] Rigidbody rigid;
    Vector3 mPos;

    private void Start()
    {
        StartCoroutine(MoveSync());
    }

    IEnumerator MoveSync()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (client.id == id)
            {
                float t = 1f / (float)sendSeconds;
                yield return new WaitForSeconds(t);
                client.PlayerPositionSend(transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    private void Update()
    {
        if (client != null)
        {
            if (client.id == id)
            {
                Move();
                InputKey();
                Jump();
                Attack();
                //Rotate();
            }
        }
    }

    void ShowUI() => client.fText.text = sendSeconds.ToString();

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCnt < maxJumpCnt)
            {
                jumpCnt++;
                rigid.velocity = Vector3.up * jumpPower;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 0;
        }
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            sendSeconds++;
            ShowUI();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            sendSeconds--;
            ShowUI();
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveVec = new Vector3(x, 0, z).normalized;
        transform.position += moveVec * speed * Time.deltaTime;
    }

    private void Rotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float y = transform.rotation.y;
            transform.Rotate(new Vector3(0, y + rotValue, 0));

            client.PlayerRotationSend(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, LayerMask.GetMask("Player")))
            {
                PlayerMove p = hit.transform.GetComponent<PlayerMove>();
                if (p.gameObject != this.gameObject)
                {
                    p.Damage(2);
                    client.PlayerHpSend(2, p.id);
                }
            }
        }
    }

    public void Damage(int dam)
    {
        hp -= dam;

        if (client != null)
        {
            if (hp <= 0 && client.id == id)
            {
                client.uiManager.DiePanel.SetActive(true);
                client.chatInput.transform.parent = client.uiManager.DiePanel.transform;
                client.chatTxt.transform.parent = client.uiManager.DiePanel.transform;
            }
        }
    }
}
