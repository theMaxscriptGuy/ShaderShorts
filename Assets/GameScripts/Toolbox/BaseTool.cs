using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTool : MonoBehaviour
{
	public delegate void onExecuteTool();
	public event EventHandler<int> OnExecuteTool;
	internal virtual void OnExecute()
	{
		OnExecuteTool.Invoke(null, 0);
	}
}
