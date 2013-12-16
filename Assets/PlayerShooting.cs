﻿using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
		public GameObject POV;
		public GameObject ShotFrom;
		public LineRenderer ShotLine;
		public int ClipSize;
		public int TimeBetweenShots;
		public int TimeShotStaying;

		protected int pr_bulletsLeft;
		protected Vector3 pr_lookDirection;
		protected int pr_currentTime;

		// Use this for initialization
		void Start ()
		{
				pr_bulletsLeft = ClipSize;
				ShotLine.enabled = false;
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				pr_currentTime++;
				pr_lookDirection = POV.transform.forward;

				if (pr_currentTime == TimeShotStaying) {
						ShotLine.enabled = false;
				}
				if (Input.GetAxis (AxesManager.Fire) > 0) {
						shoot ();
				}
				if (Input.GetAxis (AxesManager.Reload) > 0) {
						reload ();
				}
		}

		void shoot ()
		{
				if (pr_bulletsLeft > 0 && pr_currentTime >= TimeBetweenShots) {
						pr_bulletsLeft--;
						pr_currentTime = 0;

						ShotLine.enabled = true;
						Ray t_ray = new Ray (ShotFrom.transform.position, pr_lookDirection);
						RaycastHit t_rayHit;

						if (Physics.Raycast (t_ray, out t_rayHit, 50)) {
								ShotLine.SetPosition (1, t_rayHit.point);
						} else {
								ShotLine.SetPosition (1, ShotFrom.transform.position + pr_lookDirection * 10);
						}
						ShotLine.SetPosition (0, ShotFrom.transform.position);
				}
		}

		void reload ()
		{
				pr_bulletsLeft = ClipSize;
		}
}
