using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bansungmp40 : MonoBehaviour
{
    int khoangcachban = 100;
    float lucban = 5f;
    private int soluongbangdan = 10;  // Tổng số băng đạn có
    private int dantrongbang = 40;     // Số đạn trong mỗi băng
    private float shottime = 0.5f;     // Thời gian giữa các phát bắn
    public int danconlai = 0;           // Số đạn còn lại hiện tại
    private float timeNextshort = 0f;   // Thời gian chờ bắn phát tiếp theo

    public GameObject particlePrefab;    // Tham chiếu tới Prefab của hiệu ứng tóe lửa
    public Transform muzzleFlashPoint;
    public AudioClip gunSound;          
    public AudioClip gunreloadSound;  // Âm thanh nạp đạn      
    private AudioSource audioSource;  // Nguồn âm thanh
    public float impactForce = 150;
    public int damageAmount = 20;
    void Start()
    {
        danconlai = dantrongbang; // Khởi tạo số đạn còn lại
        audioSource = GetComponent<AudioSource>(); // Lấy thành phần AudioSource
    }

    public void Firegun()
    {
        if (danconlai == 0)
            return;

        while (timeNextshort < Time.time && danconlai != 0)
        {
            FireOneShot();
            timeNextshort += shottime;
        }
    }

    RaycastHit hit;
    void FireOneShot()
    {
        Vector3 huongban = transform.TransformDirection(Vector3.forward);
        Vector3 rayOrigin = muzzleFlashPoint ? muzzleFlashPoint.position : transform.position;

        if (Physics.Raycast(rayOrigin, huongban, out hit, khoangcachban))
        {
            // Tạo hiệu ứng tóe lửa khi bắn trúng
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForceAtPosition(huongban * lucban, hit.point);
            }
            hit.collider.SendMessage("Voidsatthuong", 5f, SendMessageOptions.DontRequireReceiver);
            
            // Tạo hiệu ứng tóe lửa
            if (particlePrefab)
            {
                GameObject particleEffect = Instantiate(particlePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Destroy(particleEffect, 2f); // Huỷ hiệu ứng sau 2 giây
            }

            // Phát âm thanh bắn súng
            if (gunSound && audioSource)
            {
                audioSource.PlayOneShot(gunSound); // Chơi âm thanh một lần khi bắn
            }
            enemi e= hit.transform.GetComponentInParent<enemi>();

             if(e != null)
            {
                e.TakeDamage(damageAmount);
                return;
            }
        }

        danconlai--; // Giảm số đạn còn lại
        if (danconlai == 0)
        {
            napdan(); // Nạp đạn nếu không còn đạn
        }
    }

    void napdan()
    {
        // Phát âm thanh nạp đạn
        if (gunreloadSound && audioSource)
        {
            audioSource.PlayOneShot(gunreloadSound);
        }

        StartCoroutine(reloaddan(2.1f));
    }

    IEnumerator reloaddan(float t)
    {
        yield return new WaitForSeconds(t);
        if (soluongbangdan > 0)
        {
            soluongbangdan--;
            danconlai = dantrongbang; // Nạp lại số đạn trong băng
        }
    }
}
