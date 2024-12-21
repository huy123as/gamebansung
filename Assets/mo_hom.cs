using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mo_hom : MonoBehaviour
{
    public GameObject itemToFly; // Vật phẩm sẽ bay ra
    public Transform flyToTarget; // Vị trí vật phẩm sẽ bay đến
    public float flySpeed = 5f; // Tốc độ bay của vật phẩm

    private bool isLidOpen = false; // Trạng thái nắp đã mở chưa
    private bool isItemFlying = false; // Trạng thái vật phẩm đang bay
    void Start()
{
    // Đặt lại vị trí ban đầu cho itemToFly nếu cần
    if (itemToFly != null)
    {
        itemToFly.transform.position = new Vector3(1.84f, 0, 0.05f);
    }
}
    void Update()
    {
        // Kiểm tra phím B để mở nắp
        if (Input.GetKeyDown(KeyCode.B) && !isLidOpen)
        {
            OpenLid();
        }

        // Xử lý vật phẩm bay
        if (isItemFlying)
        {
            Debug.Log("Vật phẩm đang bay...");
            Debug.Log($"Vị trí hiện tại: {itemToFly.transform.position}, Đích: {flyToTarget.position}");

            itemToFly.transform.position = Vector3.MoveTowards(itemToFly.transform.position, flyToTarget.position, flySpeed * Time.deltaTime);

            if (Vector3.Distance(itemToFly.transform.position, flyToTarget.position) < 0.1f)
            {
                Debug.Log("Vật phẩm đã đến đích!");
                isItemFlying = false; // Dừng bay khi đến nơi
            }
        }
    }

    void OpenLid()
    {
        Debug.Log("Đang mở nắp hòm...");
        GetComponent<Animation>().Play();
        isLidOpen = true;

        if (itemToFly != null && flyToTarget != null)
        {
            Debug.Log("Kích hoạt vật phẩm bay...");
            isItemFlying = true;

            if (!itemToFly.activeSelf)
            {
                itemToFly.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("itemToFly hoặc flyToTarget chưa được gán!");
        }
    }
}
