using UnityEngine;

public class Ship : MonoBehaviour, IMove, IShoot
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private UnityElementPool _bullets;

    [SerializeField] float _thrustSpeed;
    [SerializeField] float _rotationSpeed;

    private bool _thrusting;
    private float _turnDirection;


    public void Move()
    {
        // apply player inputs on rb
        if (_thrusting)
        {
            _rb.AddForce(transform.up * _thrustSpeed);
        }
        if (_turnDirection != 0)
        {
            _rb.AddTorque(_rotationSpeed * _turnDirection);
        }
    }

    public void Shoot(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        ManageInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ManageInputs()
    {
        // use QWERTY keys even for azerty keyboard
        // go foward
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        // turn
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }
        // shoot, collision matrix and layers player, bullet without interactions
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject go = _bullets.Get(transform.position);
        go.GetComponent<Bullet>().Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0f;
            gameObject.SetActive(false);
        }
    }
}
