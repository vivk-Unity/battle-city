using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    [SerializeField]
    GameObject projectile;

    GameObject canonBall, fire;
    Projectile canon;

    [SerializeField]
    private int _speed;

    [SerializeField]
    private bool _destroySteel;

    void Start()
    {
        canonBall = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        canon = canonBall.GetComponent<Projectile>();
        canon.speed = _speed;
        canon.destroySteel = _destroySteel;
    }

    public void Fire()
    {
        if (canonBall.activeSelf == false)
        {
            canonBall.transform.position = transform.position;
            canonBall.transform.rotation = transform.rotation;
            canonBall.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (canonBall != null) canon.DestroyProjectile();
    }
}
