using UnityEngine;
public class MathUtil {

	public static float LinearToDecibel(float linear){
		float decibel = -80f;
		if(!Mathf.Approximately(0f, linear)){
			decibel = 20f * Mathf.Log10 (linear);
		}
		return decibel;
	}

	public static float DecibelToLinear(float decibel){
		return Mathf.Pow (10.0f, decibel / 20.0f);
	}
}
