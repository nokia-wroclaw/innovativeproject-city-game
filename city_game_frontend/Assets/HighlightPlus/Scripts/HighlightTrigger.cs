using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighlightPlus {
	
	[RequireComponent (typeof(HighlightEffect))]
	[HelpURL("http://kronnect.com/taptapgo")]
	public class HighlightTrigger : MonoBehaviour {

		void OnEnable () {
			Collider collider = GetComponent<Collider> ();
			if (collider == null) {
				if (GetComponent<MeshFilter> () != null) {
					gameObject.AddComponent<MeshCollider> ();
				} else if (GetComponent<SpriteRenderer> () != null) {
					gameObject.AddComponent<BoxCollider2D> ();
				}
			}
		}

		void OnMouseDown () {
			Highlight (true);
		}

		void OnMouseEnter () {
			Highlight (true);

		}

		void OnMouseExit () {
			Highlight (false);
		}

		void Highlight (bool state) {
			HighlightEffect hb = transform.GetComponent<HighlightEffect> ();
			if (hb == null && state) {
				hb = gameObject.AddComponent<HighlightEffect> ();
			}
			if (hb != null) {
				if (state && hb.highlighted)
					return;
				if (!state && !hb.highlighted)
					return;
				hb.SetHighlighted (state);
			}
		}

	}

}