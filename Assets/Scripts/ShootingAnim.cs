﻿//Code written by Luis Fletcher
using UnityEngine;
using System.Collections;

public class ShootingAnim : MonoBehaviour {
	
	public GameObject Cannons;
	public int SpeedRotation; //ALWAYS set to 1.
	public int AccRotation; //ALWAYS set to 10.
	public bool ActivateShooting;
	public AudioSource TurretSound;
	public AudioClip ShootingSound;
	public AudioClip ShootingStart;
	public AudioClip ShootingFinish;

	void Start(){
		SpeedRotation = 0;
		AccRotation = 10;
		}

	void Update () 
	{
		if (ActivateShooting) {

			if (SpeedRotation < 900) {
				SpeedRotation += AccRotation;
				Shooting ();
				TurretSound.clip=ShootingStart;
				if(TurretSound.isPlaying==false){
					TurretSound.pitch=0.4f;
					TurretSound.Play(25000);
				}
			} else {
				Shooting ();
				TurretSound.clip=ShootingSound;
				TurretSound.pitch=1;
				TurretSound.loop=false;
				if(TurretSound.isPlaying==false){
					TurretSound.Play();
				}
			}
		} else {
			if(SpeedRotation>0){
				SpeedRotation-=AccRotation;
				Shooting();
				TurretSound.clip=ShootingFinish;
				if(SpeedRotation>600 && TurretSound.isPlaying==false){
					TurretSound.pitch=0.4f;
					TurretSound.Play();
				}
			}else{
				SpeedRotation=0;
			}
		}
				
	}

	void Shooting ()
	{
		transform.Rotate (0, 0, Time.deltaTime * SpeedRotation);

	}
}

