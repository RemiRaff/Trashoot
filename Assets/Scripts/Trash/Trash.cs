using UnityEngine;
using NaughtyAttributes;
using TNRD;

[RequireComponent(typeof(Rigidbody2D))]
public class Trash : MonoBehaviour, IExplose
{
    [SerializeField, Label("Score Effect")] private SerializableInterface<IExecute> _scoreEffectSerialized;

    // Properties for the interfaces
    private IExecute _scoreEffect => _scoreEffectSerialized.Value;
    private Rigidbody2D _rb;

    private Pool _pool;

    [SerializeField] public float _size = 1.0f;
    [SerializeField] private float _minSize = 0.35f;
    [SerializeField] private float _maxSize = 1.65f;
    [SerializeField] float _movementSpeed = 50f;
    [SerializeField] float _maxLifetime = 30f;

    private float _time;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Assign random properties to make each trash feel unique
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // Set the scale and mass of the trash based on the assigned size so
        // the physics is more realistic
        transform.localScale = Vector3.one * _size;
        _rb.mass = _size;
    }

    public void Init(Quaternion rotation, Pool pool)
    {
        transform.rotation = rotation;
        _pool = pool;
        _size = Random.Range(_minSize, _maxSize);
        _time = Time.fixedTime;
    }

    public void SetTrajectory(Vector2 direction)
    {
        // The trash only needs a force to be added once since they have no
        // drag to make them stop moving
        _rb.AddForce(direction * _movementSpeed);
    }

    private void FixedUpdate()
    {
        if (Time.fixedTime > (_time + _maxLifetime))
        {
            _pool.Release(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Explose();
        }
    }

    private void CreateSplit()
    {
        // Set the new asteroid poistion to be the same as the current asteroid
        // but with a slight offset so they do not spawn inside each other
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Create the new asteroid at half the size of the current
        GameObject go = _pool.Get(position);
        Trash t = go.GetComponent<Trash>();
        t.Init(transform.rotation, _pool);
        t._size *= 0.5f;

        // Set a random trajectory
        t.SetTrajectory(Random.insideUnitCircle.normalized);
    }

    public void Explose()
    {
        // Destroy the current trash since it is either replaced by two
        // new trashes or small enough to be destroyed by the bullet
        _pool.Release(this.gameObject);

        // Check if the trash is large enough to split in half
        // (both parts must be greater than the minimum size)
        if (_minSize <= (_size * 0.5))
        {
            CreateSplit();
            CreateSplit();
        }

        // Score it
        _scoreEffect.Execute();
    }
}
