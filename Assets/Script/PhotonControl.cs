using Photon.Pun;
using UnityEngine;

public class PhotonControl : MonoBehaviourPun
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    [SerializeField] Animator animator; // 에니메이터 가져오기9-29
    void Start()
    {
        animator = GetComponent<Animator>(); // 에니메이터

        // 현재 플레이어가 나 자신이라면
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
        // 현제 플레이어가 나 자신이 아니라면
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1")) // 9-29
        {
            //animator.Play("Attack");
            //animator.SetBool("Attack", true); // 에니메이션 줄2개 추가후 9-29
            animator.SetTrigger("Attack"); // 9-30 수정
        }

        // 내용
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

            // 자기 자신
            if (view.IsMine) // 충돌한 물체가 자기 자신이라면
            {               
                // 충돌당한 네트워크 객체를 파괴합니다.
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }
}
