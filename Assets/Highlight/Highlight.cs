using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Highlight : MonoBehaviour
{
	public float pingpongvalue = 1f;
	public float pingpongspeed = 1f;
	public Color highlightColor;
	public Shader highlightShader;
	
	private Material m_material;
	private float pingpong;

	private void OnEnable()
	{
		if (highlightShader == null)
		{
			enabled = false;
			return;
		}
		m_material = new Material(highlightShader);
		m_material.SetColor("_HighlightColor", highlightColor);
	}
	private void Start()
	{
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		List<Material> mats = renderer.sharedMaterials.ToList();
		mats.Add(m_material);
		renderer.sharedMaterials = mats.ToArray();
	}

	private void Update()
	{
		pingpong = Mathf.PingPong(Time.time, 1) * pingpongspeed;
		m_material.SetFloat("_Highlight", pingpong);
	}
}
