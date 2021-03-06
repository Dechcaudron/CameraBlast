﻿//Coded by Luis Fletcher
using UnityEngine;
using System.Collections;

public class ShootingAnim : MonoBehaviour
{
	
		public GameObject Cannons;
		public bool ActivateShooting;
		public AudioSource TurretSound;
		public AudioClip ShootingSound;
		public AudioClip ShootingStart;
		public AudioClip ShootingFinish;
		public GameObject ShootingLine;		
		public int rangeTurret;
		public int SpeedRotation; //Should be set to 0 in the beggining.
	
		protected int AccRotation; //ALWAYS set to 12.
		protected int FrameCounter;

		private GameObject player;

		void Start ()
		{
				SpeedRotation = 0;
				AccRotation = 12;
				ShootingLine.GetComponent<LineRenderer> ().enabled = false;
				FrameCounter = 0;
				player = GameObject.FindGameObjectWithTag ("Player");
		}
	

		void FixedUpdate ()
		{
				if (ActivateShooting && SpeedRotation > 899) {
						FrameCounter++;
						if (FrameCounter < 5) {
								ShootingLine.GetComponent<LineRenderer> ().enabled = true;
								ShootingLine.GetComponent<LengthBehaviour> ().recalculate ();
						}
						if (FrameCounter > 5) {
								ShootingLine.GetComponent<LineRenderer> ().enabled = false;
						}
						if (FrameCounter == 10) {
								FrameCounter = 0;
						}
				}

				float distance = Vector3.Distance (transform.position, player.transform.position);
				if (CameraRotation.isInRange && distance < rangeTurret) {
						ActivateShooting = true;
				} else {
						ActivateShooting = false;
				}
				if (ActivateShooting) {
			
						if (SpeedRotation < 900) {
								SpeedRotation += AccRotation;
								Shooting ();
								TurretSound.clip = ShootingStart;
								if (TurretSound.isPlaying == false) {
										TurretSound.pitch = 0.4f;
										TurretSound.PlayDelayed (0.5f);
								}
						} else {
								Shooting ();
								TurretSound.clip = ShootingSound;
								TurretSound.pitch = 1;
								TurretSound.loop = false;
								if (TurretSound.isPlaying == false) {
										TurretSound.Play ();
								}
						}
				} else {
						if (SpeedRotation > 0) {
								ShootingLine.GetComponent<LineRenderer> ().enabled = false;
								SpeedRotation -= AccRotation;
								Shooting ();
								TurretSound.clip = ShootingFinish;
								if (SpeedRotation > 600 && TurretSound.isPlaying == false) {
										TurretSound.pitch = 0.4f;
										TurretSound.Play ();
								}
						} else {
								SpeedRotation = 0;
						}
				}

		}


		void Shooting ()
		{
				transform.Rotate (0, 0, Time.deltaTime * SpeedRotation);
		}
}

