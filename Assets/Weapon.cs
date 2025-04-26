using System.Collections;
using UnityEngine;
using UnityEngine.UI; // 加入 UI 命名空間

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;
    public Camera playerCamera;
    
    public int maxMagazineSize = 30;   
    private int currentAmmo;            
    public float reloadTime = 2f;       
    private bool isReloading = false;   
    public float fireRate = 0.1f; 
    private float nextFireTime = 0f;

    // 新增：UI 元件（Text）來顯示子彈數
    public Text ammoUIText;

    void Start()
    {
        // 初始化子彈數
        currentAmmo = maxMagazineSize;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (isReloading)
            return;

        // 單發射：點擊左鍵
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0 && Time.time >= nextFireTime)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate;
        }

        // 連射：按住左鍵
        if (Input.GetKey(KeyCode.Mouse0) && currentAmmo > 0 && Time.time >= nextFireTime)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate;
        }

        // 按 R 重新裝填
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxMagazineSize && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void FireWeapon()
    {
        // 建立子彈
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // 取得射擊方向
        Vector3 shootDirection = playerCamera.transform.forward.normalized;
        bullet.GetComponent<Rigidbody>().AddForce(shootDirection * bulletVelocity, ForceMode.Impulse);
        
        currentAmmo--;
        Debug.Log("剩餘子彈: " + currentAmmo);

        // 更新 UI
        UpdateAmmoUI();

        // 設定銷毀子彈的協程
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("正在裝填...");

        yield return new WaitForSeconds(reloadTime);

        // 裝填後恢復子彈數
        currentAmmo = maxMagazineSize;
        isReloading = false;
        Debug.Log("裝填完成！剩餘子彈: " + currentAmmo);

        // 更新 UI
        UpdateAmmoUI();
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    // 新增：更新 UI 顯示子彈數的方法
    private void UpdateAmmoUI()
    {
        if (ammoUIText != null)
        {
            ammoUIText.text = "子彈: " + currentAmmo.ToString();
        }
    }
}
