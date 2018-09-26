using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float LaserDMG = 100f;
	
	public float GetDamage()
	{
			return LaserDMG;
	}
	
	public void Hit()
	{
		Destroy(gameObject);		
	}
}
