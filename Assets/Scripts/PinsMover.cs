using PathCreation;
using UnityEngine;

namespace PathCreation.Examples 
{ 
    public class PinsMover : MonoBehaviour
    {
        private Touch _touch;
        public PathCreator pathCreator;
        private Camera _camera;
        

        private void Start()
       {
           _camera = Camera.main;
           _touch = new Touch();
       }

        private void Update()
        {
            if (Input.touchCount <= 0) return;
            _touch = Input.touches[0];
            if (_touch.phase != TouchPhase.Moved) return;
            Ray ray = _camera.ScreenPointToRay(_touch.position);
            RaycastHit hit;
            
            if (!Physics.Raycast(ray, out hit)) return;
            if (hit.collider.gameObject == gameObject)
            {
                Vector3 worldPosition = hit.point;
                Vector3 nearestWorldPositionOnPath = pathCreator.path.GetPointAtDistance(pathCreator.path.GetClosestDistanceAlongPath(worldPosition));
                transform.position = nearestWorldPositionOnPath;
            }
        }
        
    } 
}
