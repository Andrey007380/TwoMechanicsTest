using PathCreation;
using UnityEngine;

namespace PathCreation.Examples 
{ 
    public class PinsMover : MonoBehaviour
    {
        private Touch _touch;
        private Vector3 _moveToPoint;
        public PathCreator pathCreator;

        public Vector3 MoveToPoint { get => _moveToPoint; set => _moveToPoint = value; }

       private void Start()
        {
            _touch = new Touch();

        }

        private void LateUpdate()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.touches[0];
                if (_touch.phase == TouchPhase.Moved)
                {
                    DragPinToPoint();
                }
                return; 
            }
            DragPinToPoint();
        }

        private void DragPinToPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(_touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {

                if (hit.collider.gameObject == gameObject)
                {
                    Vector3 worldPosition = hit.point;
                    Vector3 nearestWorldPositionOnPath = pathCreator.path.GetPointAtDistance(pathCreator.path.GetClosestDistanceAlongPath(worldPosition));
                    transform.position = nearestWorldPositionOnPath;
                }
                
            }
        }
    } 
}
