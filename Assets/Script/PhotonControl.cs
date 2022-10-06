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

    [SerializeField] Animator animator; // ���ϸ����� ��������9-29

    public int score;
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
            if (photonView.IsMine) // �ڱ� �ڽ��϶��� 10-5
            {
                score++;
                //UIManager.instance.score++; // ���ھ� ���� ++ �ض� /10-5
            }

            PlayFabDataSave(); // ���̺� ������ �Լ� ȣ�� 10-5

            PhotonView view = other.gameObject.GetComponent<PhotonView>();

            // �ڱ� �ڽ�
            if (view.IsMine) // �浹�� ��ü�� �ڱ� �ڽ��̶��
            {               
                // �浹���� ��Ʈ��ũ ��ü�� �ı��մϴ�.
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ���� ������Ʈ��� ���� �κ��� ����˴ϴ�.
        if (stream.IsWriting)
        {
            // ��Ʈ��ũ�� ���� score ���� �����ϴ�.
            stream.SendNext(score);
        }
        else //���� ������Ʈ��� �б� �κ��� ����˴ϴ�.
        {
            score = (int)stream.ReceiveNext();
        }
    }

    public void PlayFabDataSave() // �÷��� ������ ���̺� �Լ�
    {
        PlayFabClientAPI.UpdatePlayerStatistics
        (
            new UpdatePlayerStatisticsRequest // ������ ���� �Լ�
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "Score", Value = score
                    },
                }
            },

            // ���� �Լ�
            (result) => { UIManager.instance.scoreText.text = "Current Crystal : " + score.ToString(); },
            (error) => { UIManager.instance.scoreText.text = "No value Saverd "; }
            // ���� ��������ʾҴ�
        );
    }
}
