using UnityEngine;

public class Bullet : MonoBehaviour, IProject
{
    [SerializeField] private float _bulletSpeed = 500.0f;
    [SerializeField] private float _maxLifeTime = 10.0f;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rb.AddForce(direction * _bulletSpeed);
        // Pool to implement
        Destroy(gameObject, _maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Pool to implement
        Destroy(gameObject);
    }
}
