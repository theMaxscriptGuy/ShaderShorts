using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BoxBlur : ImageEffectsShaderBase
{
	public int iterations = 10;
	public Vector2 offset;
	public float blurOffset;
	public float maxExp;
	public float Exp;

	private Vignette m_vignette;

	private void Start()
	{
		m_vignette = GetComponent<Vignette>();
	}
	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			Exp = 0;
			m_vignette.enabled = false;
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			Exp = maxExp;
			m_vignette.enabled = true;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		m_effectMaterial.SetInt("iterations", iterations);
		m_effectMaterial.SetFloat("_XOffset", offset.x);
		m_effectMaterial.SetFloat("_YOffset", offset.y);
		m_effectMaterial.SetFloat("_BlurOffset", blurOffset);
		m_effectMaterial.SetFloat("_Exp", Exp);
		Graphics.Blit(source, destination, m_effectMaterial);
	}
}
