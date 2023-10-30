using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterEffect : MonoBehaviour
{
   public GameObject SurfacePostProcessing;
   public GameObject WaterPostProcessing;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag ("MainCamera"))
		{
			SurfacePostProcessing.gameObject.SetActive(false);
			WaterPostProcessing.gameObject.SetActive(true);
			RenderSettings.fog = true;
	
		}
}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag ("MainCamera"))
		{
			SurfacePostProcessing.gameObject.SetActive(true);
			WaterPostProcessing.gameObject.SetActive(false);
			RenderSettings.fog = false;
		}
	}

}
