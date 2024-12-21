using System.Collections;
using UnityEngine;

public class animation : MonoBehaviour
{
      Animation anim; // Sử dụng Animation thay vì Animator
      bool _reload = false;
    public GameObject muzzleFlashPrefab; // Tham chiếu tới Prefab của hiệu ứng tóe lửa
    public Transform muzzleFlashPoint;   // Điểm nòng súng nơi hiệu ứng xuất hiện (tùy chỉnh nếu cần)

    void Start()
    {
        anim = GetComponent<Animation>();  
        anim.CrossFade("Take_In");  
    }

    void LateUpdate()
    {
        // Kiểm tra nếu nhấn chuột trái
        if (Input.GetButton("Fire1"))
        {
            anim.CrossFade("Fire"); 
           GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
              Destroy(muzzleFlash, 0.1f); 
            transform.parent.GetComponentInChildren<bansungmp40>().Firegun();
            
         
        }
        
        else if (_reload || transform.parent.GetComponentInChildren<bansungmp40>().danconlai==0)
        {
            StartCoroutine(thaydan(2.1f));  
          
        }
        else
        {
            anim.CrossFade("Idle");  
        }

         
        if (Input.GetKey(KeyCode.M))
        {
            _reload = true;  
        }
    }

    IEnumerator thaydan(float t)
    {
        anim.CrossFade("Reload");  
        yield return new WaitForSeconds(t);  
        _reload = false;
    }
}
