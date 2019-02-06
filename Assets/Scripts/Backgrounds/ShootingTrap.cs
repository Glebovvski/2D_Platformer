using Assets.Scripts.Enemies;
using System.Collections;
using UnityEngine;

public class ShootingTrap : MonoBehaviour, IEnemy
{
    [SerializeField]
    private GameObject itemToShootPrefab;

    [HideInInspector]
    public bool isStopped = false;

    [SerializeField]
    private float shootStartDelay = 0.1f;

    [SerializeField]
    private float shootInterval = 2f;

    public float destroyAfter;

    private GameObject spike;
    // Use this for initialization
    void Start()
    {
        spike = Instantiate(itemToShootPrefab);
        spike.SetActive(false);
        InvokeRepeating("ShootObject", shootStartDelay, shootInterval);
        //StartCoroutine(ShootObject(shootInterval));
    }

    private void ShootObject()
    {
        if (!itemToShootPrefab.activeInHierarchy)
        {
            spike.transform.position = transform.position;
            spike.SetActive(true);

            //yield return new WaitForSeconds(delay + shootStartDelay);
            //shootStartDelay = 0f;
            //
            //var item = (GameObject)Instantiate(itemToShootPrefab, transform.position, transform.rotation);
            //Destroy(item.gameObject, destroyAfter);
            //StartCoroutine(ShootObject(shootInterval));
        }
        //else itemToShootPrefab = null;
    }

    public void Stop()
    {
        isStopped = !isStopped;
    }

    public void Damage(float damageAmount)
    {
        
    }
}
