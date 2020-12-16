using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Zoom : MonoBehaviour
{
	public Material sniperMaterial;
	public float ZoomSpeed = 0.01f;
	public float ZoomValue = 1f;
	private void OnEnable()
	{
		if(!sniperMaterial)
		{
			enabled = false;
			return;
		}
	}

	private void Update()
	{
		if(Input.GetAxis("Mouse ScrollWheel") != 0f)
		{
			ZoomValue += ZoomSpeed * Input.mouseScrollDelta.y * Time.deltaTime;
			if (ZoomValue < 0f)
			{
				ZoomValue = 0f;
			}
			if (ZoomValue > 1f)
			{
				ZoomValue = 1f;
			}
			sniperMaterial.SetFloat("_Zoom", ZoomValue);
		}
	}
}