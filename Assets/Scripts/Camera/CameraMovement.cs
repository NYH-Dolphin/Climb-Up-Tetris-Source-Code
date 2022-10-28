using UnityEngine;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject objectToFollow;
        public float speed = 2.0f;
        private GameObject recordObjToFollow; // 记录的Object
        
        private void Start()
        {
            transform.position = objectToFollow.transform.position;
            recordObjToFollow = objectToFollow;
        }
        
        private void Update()
        {
            float interpolation = speed * Time.deltaTime;
            Vector3 position = transform.position;
            position.y = Mathf.Lerp(transform.position.y, objectToFollow.transform.position.y, interpolation);
            position.z = Mathf.Lerp(transform.position.z, objectToFollow.transform.position.z, interpolation);
            position.x = Mathf.Lerp(transform.position.x, objectToFollow.transform.position.x, interpolation);
            transform.position = position;
            transform.rotation = Quaternion.Lerp(transform.rotation, objectToFollow.transform.rotation,
                Time.deltaTime * speed * 0.5f);
        }
    }
}