using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlinch : MonoBehaviour
{
	float rotateAmount = 0.0f;
	float maxRotate = 0.0f;
	public enum FlinchAmount
	{
		Small, Medium, Large
	}

	private float flinchSmall = 5.0f;
    private float flinchMedium = 10.0f;
    private float flinchLarge = 15.0f;

    public void flinch(FlinchAmount amount)
	{
		if (rotateAmount > 0.0f) return;

		switch (amount)
		{
			case FlinchAmount.Small:
				maxRotate = flinchSmall;
                break;
			case FlinchAmount.Medium:
                maxRotate = flinchMedium;
                break;
			case FlinchAmount.Large:
                maxRotate = flinchLarge;
                break;
			default:
                maxRotate = flinchSmall;
                break;
		}
		StartCoroutine(increaseFlinch());
	}

    IEnumerator increaseFlinch()
	{
		while (rotateAmount < maxRotate)
		{
            rotateAmount += maxRotate * Time.fixedDeltaTime * 6;
            transform.Rotate(rotateAmount, 0.0f, 0.0f);
            yield return new WaitForFixedUpdate();
		}
		rotateAmount = maxRotate;
        transform.Rotate(rotateAmount, 0.0f, 0.0f);
        yield return StartCoroutine(unflinch());
	}

	IEnumerator unflinch()
	{
        while (rotateAmount >= 0.0f)
        {
            rotateAmount -= maxRotate * Time.fixedDeltaTime * 4;
			transform.Rotate(rotateAmount, 0.0f,0.0f);
            yield return new WaitForFixedUpdate();
        }
        transform.Rotate(0.0f, 0.0f, 0.0f);
        yield return null;
    }
}
