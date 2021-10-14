
using UnityEngine;

public class PinsMover : MonoBehaviour
{
    private Touch _touch;
    private Vector3 _moveToPoint;

    public Vector3 MoveToPoint { get => _moveToPoint; set => _moveToPoint = value; }

    void Start()
    {
        _touch = new Touch();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + _touch.deltaPosition.x * 5f * Time.deltaTime,
                    transform.position.y,
                    transform.position.z + _touch.deltaPosition.y * 5f * Time.deltaTime);
            }
        }
        else if(Physics.Raycast(ray, out hit))
        {
            MoveToPoint = transform.position = new Vector3(
                    hit.point.x,
                    transform.position.y,
                    hit.point.z);
        }
    }
}
