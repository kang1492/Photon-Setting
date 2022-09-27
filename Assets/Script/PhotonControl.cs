using Photon.Pun;
using UnityEngine;

public class PhotonControl : MonoBehaviourPun
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;
    void Start()
    {
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
}
