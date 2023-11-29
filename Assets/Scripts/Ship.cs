using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IMove, IShoot
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] float _thrustSpeed;
    [SerializeField] float _torqueSpeed;

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
            _rb.AddTorque(_turnDirection * _torqueSpeed);
        }
    }

    public void Shoot(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageInputs();
    }

    void FixedUpdate()
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
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }
}
