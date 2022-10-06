using Photon.Pun;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PhotonControl : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    [SerializeField] Animator animator; // 에니메이터 가져오기9-29

    public int score;
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
            if (photonView.IsMine) // 자기 자신일때만 10-5
            {
                score++;
                //UIManager.instance.score++; // 스코어 점수 ++ 해라 /10-5
            }

            PlayFabDataSave(); // 세이브 데이터 함수 호출 10-5

            PhotonView view = other.gameObject.GetComponent<PhotonView>();

            // 자기 자신
            if (view.IsMine) // 충돌한 물체가 자기 자신이라면
            {               
                // 충돌당한 네트워크 객체를 파괴합니다.
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 로컬 오브젝트라면 쓰기 부분이 실행됩니다.
        if (stream.IsWriting)
        {
            // 네트워크를 통해 score 값을 보냅니다.
            stream.SendNext(score);
        }
        else //원격 오브젝트라면 읽기 부분이 실행됩니다.
        {
            score = (int)stream.ReceiveNext();
        }
    }

    public void PlayFabDataSave() // 플레이 데이터 세이브 함수
    {
        PlayFabClientAPI.UpdatePlayerStatistics
        (
            new UpdatePlayerStatisticsRequest // 데이터 저장 함수
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "Score", Value = score
                    },
                }
            },

            // 무명 함수
            (result) => { UIManager.instance.scoreText.text = "Current Crystal : " + score.ToString(); },
            (error) => { UIManager.instance.scoreText.text = "No value Saverd "; }
            // 값이 저장되지않았다
        );
    }
}
