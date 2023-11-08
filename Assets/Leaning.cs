using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaning : MonoBehaviour
{
    private bool LeaningLeft = false;
	private bool LeaningRight = false;
    public Transform thing;
	public Transform self;
	public float leanAngle = 30.0f; // The angle at which the player should lean.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 currentRotation = self.transform.rotation.eulerAngles;
		Quaternion newRotation = Quaternion.Euler(0, currentRotation.y, currentRotation.z);
		self.transform.rotation = newRotation;
		if (LeaningLeft == true) {
			Vector3 currentRotationLeft = self.transform.rotation.eulerAngles;
			Quaternion newRotationLeft = Quaternion.Euler(0, currentRotationLeft.y, 15);
			self.transform.rotation = newRotationLeft;
		}
		if (LeaningRight == true) {
			Vector3 currentRotationRight = self.transform.rotation.eulerAngles;
			Quaternion newRotationRight = Quaternion.Euler(0, currentRotationRight.y, -15);
			self.transform.rotation = newRotationRight;
		}

        if (Input.GetButtonDown("LeanLeft"))
        	{
            	LeaningLeft = !LeaningLeft;
            	//animator.SetBool("ADS", ADS);
            	if (LeaningLeft) {
					LeaningRight = false;
                	StartCoroutine(LeanLeft());
            	} else {
					CancelLean();
				}
        	}
			if (Input.GetButtonDown("LeanRight"))
        	{
            	LeaningRight = !LeaningRight;
            	//animator.SetBool("ADS", ADS);
            	if (LeaningRight) {
					LeaningLeft = false;
                	StartCoroutine(LeanRight());
            	} else {
					CancelLean();
				}	
        	}
    }
    IEnumerator LeanLeft() 
		{
			yield return new WaitForSeconds(0f);
			Debug.Log("Leaned Left");
			Vector3 currentRotation1 = self.transform.rotation.eulerAngles;
			Quaternion newRotation1 = Quaternion.Euler(0, currentRotation1.y, leanAngle);
			self.transform.rotation = newRotation1;
			//self.transform.rotation = Quaternion.Euler(0, self.transform.rotation.y, leanAngle);
		}

		IEnumerator LeanRight() 
		{
			yield return new WaitForSeconds(0f);
			Debug.Log("Leaned Right");
			Vector3 currentRotation2 = self.transform.rotation.eulerAngles;
			Quaternion newRotation2 = Quaternion.Euler(0, currentRotation2.y, -leanAngle);
			self.transform.rotation = newRotation2;
			//self.transform.rotation = Quaternion.Euler(0, self.transform.rotation.y, -leanAngle);
		}

		

		void CancelLean() {
			Vector3 currentRotation3 = self.transform.rotation.eulerAngles;
			Quaternion newRotation3 = Quaternion.Euler(0, currentRotation3.y, 0);
			self.transform.rotation = newRotation3;
            //self.transform.rotation = Quaternion.Euler(0, self.transform.rotation.y, 0);
		}
}
