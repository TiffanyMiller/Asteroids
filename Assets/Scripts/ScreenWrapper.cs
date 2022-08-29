using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private Camera _cam;
    private Renderer _rend;
    private Vector2 _viewportPos;
    private Vector2 _newPos;

    private bool _outOfBoundsX, _outOfBoundsY;

    private void Awake()
    {
        _cam = Camera.main;
        _rend = GetComponent<Renderer>();
    }

    private bool HitYBound() => _viewportPos.y > 1f || _viewportPos.y < 0f;
    private bool HitXBound() => _viewportPos.x > 1f || _viewportPos.x < 0f;

    private void Update()
    {
        if (_rend.isVisible)
        {
            _outOfBoundsX = false;
            _outOfBoundsY = false;
            return;
        }

        if (_outOfBoundsX && _outOfBoundsY) return;

        if(GameManager.inst.GameHasStarted)
            ScreenWrap();
    }

    private void ScreenWrap()
    {
        var pos = transform.position;
        _viewportPos = _cam.WorldToViewportPoint(pos);
        
        _newPos = pos;

        if (!_outOfBoundsX && HitXBound())
        {
            _newPos.x = -_newPos.x;
            _outOfBoundsX = true;
        }

        if (!_outOfBoundsY && HitYBound())
        {
            _newPos.y = -_newPos.y;
            _outOfBoundsY = true;
        }
        
        transform.position = _newPos;
    }
}
