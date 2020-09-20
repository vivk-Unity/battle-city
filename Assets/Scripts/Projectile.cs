using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public int speed = 1;

    [SerializeField]
    public bool destroySteel = false;

    [SerializeField]
    private bool _toBeDestroyed = false;

    GameObject brickGameObject, steelGameObject;

    Tilemap tilemap;

    Rigidbody2D rb2d;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
        brickGameObject = GameObject.FindGameObjectWithTag("Brick");
        steelGameObject = GameObject.FindGameObjectWithTag("Steel");

    }

    private void OnEnable()
    {
        if (rb2d != null)
        {
            rb2d.velocity = transform.up * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector2.zero;
        tilemap = collision.gameObject.GetComponent<Tilemap>();

        if (collision.gameObject.GetComponent<HealthController>() != null)
        {
            collision.gameObject.GetComponent<HealthController>().TakeDamage();
        }
        if ((collision.gameObject == brickGameObject) || (destroySteel && collision.gameObject == steelGameObject))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts )
            {
                hitPosition.x = hit.point.x - 0.1f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.1f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (_toBeDestroyed)
        {
            Destroy(this.gameObject);
        }
    }

    public void DestroyProjectile()
    {
        if (gameObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
        _toBeDestroyed = true;
    }

}
