using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl_HolyBook : MonoBehaviour
{
    public float damage; //�����
    public int pen; //����

    public float rotationSpeed = 150f; //���� �ӵ� //WeaponManager�� �Ѿ 240622 16:45

    ///**/private float weaponActiveTime = 5f;
    ///**/private float weaponActiveTimer;
    ///**/private float weaponCooldown = 3f;
    ///**/private float spawnTimer;
    //private void OnEnable()
    //{
    //    weaponActiveTimer = weaponActiveTime;
    //}

    
    void Update()
    {        
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); //�ݴ� �������� ���� //WeaponManager�� �Ѿ 240622 16:45

        //weaponActiveTimer -= Time.deltaTime;
        //if(weaponActiveTimer <= 0)
        //{
        //    gameObject.SetActive(false);               
        //}
        //else
        //{
        //    gameObject.SetActive(true);
        //}
    }
    

    //private IEnumerator ReEnableWeapon()
    //{
    //    while(spawnTimer > 0)
    //    {
    //        spawnTimer -= Time.deltaTime;
    //        yield return null;
    //    }
    //    GameManager.Instance.poolManager.Get(gameObject);
    //}

    
    private void Start()
    {
        transform.localRotation = Quaternion.identity; //WeaponManager�� �Ѿ 240622 16:45
    } 
    

    public void Init(ItemData data /*float damage, int pen*/)
    {
        //this.damage = damage;
        damage = data.baseDamage;
        //this.pen = pen;
        this.pen = -1;
    }
}
