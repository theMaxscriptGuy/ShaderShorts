using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Monochrome : MonoBehaviour
{
	public Shader monochromeShader;
	public float monochromeValue;

	private Material m_monochromeMaterial;

	private void OnEnable()
	{
		if(monochromeShader == null)
		{
			enabled = false;
			return;
		}
		m_monochromeMaterial = new Material(monochromeShader);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		m_monochromeMaterial.SetFloat("_monochromeValue", monochromeValue);
		Graphics.Blit(source, destination, m_monochromeMaterial);
	}
}
