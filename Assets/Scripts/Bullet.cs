using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IProject
{
    [SerializeField] private float _bulletSpeed = 500.0f;
    
    private Pool bulletPool;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        bulletPool = GameObject.FindGameObjectsWithTag("Bullet pool")[0].GetComponent<Pool>();
    }

    public void Project(Vector2 direction)
    {
        _rb.AddForce(direction * _bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       bulletPool.Release(this.gameObject);
    }
}
