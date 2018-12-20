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

    [SerializeField]
    private float destroyItemDelay = 0.5f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(ShootObject(shootInterval));
    }

    private IEnumerator ShootObject(float delay)
    {
        if (!isStopped)
        {
            yield return new WaitForSeconds(delay + shootStartDelay);
            shootStartDelay = 0f;

            var item = (GameObject)Instantiate(itemToShootPrefab, transform.position, transform.rotation);
            Destroy(item.gameObject, 1f);
            StartCoroutine(ShootObject(shootInterval));
        }
        else itemToShootPrefab = null;
    }

    public void Stop()
    {
        isStopped = !isStopped;
    }
}
