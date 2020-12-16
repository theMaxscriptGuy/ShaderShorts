using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
	public delegate void UpdatePlayerHealth(float val);
	public static event UpdatePlayerHealth OnUpdatePlayerHealth;

	public float speed = 0.1f;


	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Fire")
		{
			OnUpdatePlayerHealth.Invoke(Time.deltaTime * speed);
		}
	}

}
