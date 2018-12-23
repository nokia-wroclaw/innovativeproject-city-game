using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighlightPlus {

	[RequireComponent (typeof(HighlightEffect)), ExecuteInEditMode]
	[HelpURL("http://kronnect.com/taptapgo")]
	public class HighlightManager : MonoBehaviour {
		public LayerMask layerMask = -1;
		public Camera raycastCamera;

		HighlightEffect baseEffect, currentEffect;
		Collider currentCollider;

		void OnEnable () {
			currentCollider = null;
			currentEffect = null;
			if (baseEffect == null) {
				baseEffect = GetComponent<HighlightEffect> ();
				if (baseEffect == null) {
					baseEffect = gameObject.AddComponent<HighlightEffect> ();
				}
			}
			raycastCamera = GetComponent<Camera> ();
			if (raycastCamera == null) {
				raycastCamera = Camera.main;
				if (raycastCamera == null) {
					raycastCamera = FindObjectOfType<Camera> ();
					if (raycastCamera == null) {
						Debug.LogError ("Highlight Manager: no camera found!");
					}
				}
			}
		}


		void OnDisable () {
			SwitchesCollider (null);
		}

		void Update () {
			if (raycastCamera == null)
				return;
			Ray ray = raycastCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast (ray, out hitInfo, raycastCamera.farClipPlane, layerMask)) {
				// Check if the object has a Highlight Effect
				if (hitInfo.collider != currentCollider) {
					SwitchesCollider (hitInfo.collider);
				}
			} else {
				SwitchesCollider (null);
			}
		}

		void SwitchesCollider (Collider newCollider) {
			if (currentEffect != null) {
				currentEffect.SetHighlighted (false);
			}
			currentCollider = newCollider;
			if (newCollider == null || newCollider.GetComponent<HighlightTrigger> () != null)
				return;
			
			HighlightEffect otherEffect = newCollider.GetComponent<HighlightEffect> ();
			currentEffect = otherEffect != null ? otherEffect : baseEffect;
			currentEffect.SetTarget (currentCollider.transform);
			currentEffect.SetHighlighted (true);
		}


	}

}