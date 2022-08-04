using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private float _moveSpeed;
    private float _rotateSpeed;
    
    private Rigidbody2D _rb;
    private SpaceshipController _shipController;
    private Spaceship _ship;

    // Smooth moves to a speed based on damping
    private float MoveToSpeed(float toSpeed)
    {
        var damping = _ship.moveSpeed / _ship.moveDamp * Time.deltaTime;
        return Mathf.Lerp(_moveSpeed, toSpeed, damping);
    }
    
    // Smooth rotates to a speed based on damping
    private float RotateToSpeed(float toSpeed)
    {
        var damping = _ship.rotationSpeed / _ship.rotationDamp * Time.deltaTime;
        return Mathf.Lerp(_rotateSpeed, toSpeed, damping);
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _shipController = GetComponent<SpaceshipController>();

        _ship = _shipController.ship;
        
        _shipController.onMove = Accelerate;
        _shipController.onEndMove = Decelerate;
        
        _shipController.onRotate = Rotate;
        _shipController.onEndRotate = EndRotation;
    }

    private void OnDestroy()
    {
        _shipController.onMove -= Accelerate;
        _shipController.onEndMove -= Decelerate;
        
        _shipController.onRotate -= Rotate;
        _shipController.onEndRotate -= EndRotation;
    }

    private void Accelerate()
    {
        _moveSpeed = MoveToSpeed(_ship.moveSpeed);
    }

    private void Decelerate()
    {
        if (_moveSpeed > _ship.moveDamp || _moveSpeed < -_ship.moveDamp)
            _moveSpeed = MoveToSpeed(0);
        else _moveSpeed = 0;
    }

    private void Rotate(int direction)
    {
        _rotateSpeed = RotateToSpeed(_ship.rotationSpeed * direction);
    }

    private void EndRotation()
    {
        if (_rotateSpeed > _ship.rotationDamp || _rotateSpeed < -_ship.rotationDamp) 
            _rotateSpeed = RotateToSpeed(0);
        else _rotateSpeed = 0;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = (Vector2) transform.up * _moveSpeed;

        var angle = Quaternion.AngleAxis(_rotateSpeed, transform.forward);
        _rb.MoveRotation(_rb.transform.rotation * angle);
    }
}
