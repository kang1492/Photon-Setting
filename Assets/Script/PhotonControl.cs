using Photon.Pun;
using UnityEngine;

public class PhotonControl : MonoBehaviourPun
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    [SerializeField] Animator animator; // ���ϸ����� ��������9-29
    void Start()
    {
        animator = GetComponent<Animator>(); // ���ϸ�����

        // ���� �÷��̾ �� �ڽ��̶��
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }


    }


    void Update()
    {
        // ���� �÷��̾ �� �ڽ��� �ƴ϶��
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1")) // 9-29
        {
            //animator.Play("Attack");
            //animator.SetBool("Attack", true); // ���ϸ��̼� ��2�� �߰��� 9-29
            animator.SetTrigger("Attack"); // 9-30 ����
        }

        // ����
        Vector3 direction = new Vector3
        (
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        transform.Translate(direction.normalized * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3
        (
            0,
            Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime,
            0
        );
    }

    private void OnTriggerEnter(Collider other) // 10-4
    {
        if(other.gameObject.name == "Crystal(Clone)")
        {
            PhotonView view = other.gameObject.GetComponent<PhotonView>();

            // �ڱ� �ڽ�
            if (view.IsMine) // �浹�� ��ü�� �ڱ� �ڽ��̶��
            {               
                // �浹���� ��Ʈ��ũ ��ü�� �ı��մϴ�.
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }
}
