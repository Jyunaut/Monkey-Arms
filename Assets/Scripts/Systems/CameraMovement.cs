
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public Transform target;

	public float smoothSpeed = 0.2f;
	public Vector3 offsetPos;
	private float offsetX, offsetY, offsetZ;
	
    // Start is called before the first frame update
    void Start() {
	    offsetX = 0;
	    offsetY = 1.5f;
	    offsetZ = -10.0f;
	    
	    offsetPos = new Vector3(offsetX, offsetY, offsetZ);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
	    Vector3 targetOffset = target.position + offsetPos;
	    Vector3 smoothPos = Vector3.Lerp(transform.position, targetOffset, smoothSpeed);
	    transform.position = smoothPos;
	    transform.LookAt(target);
    }
    
    
    
}
