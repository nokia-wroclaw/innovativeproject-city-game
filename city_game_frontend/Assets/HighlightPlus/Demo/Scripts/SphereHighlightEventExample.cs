using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

namespace HighlightPlusDemos {
	
	public class SphereHighlightEventExample : MonoBehaviour {

		void Start() {
			HighlightEffect effect = GetComponent<HighlightEffect> ();
			effect.OnObjectHighlightStart += ValidateHighlightObject;
		}


		void ValidateHighlightObject(GameObject obj, ref bool cancelHighlight) {
			// Used to fine-control if the object can be highlighted
			cancelHighlight = false;
		}

		void HighlightStart () {
			Debug.Log ("Gold sphere highlighted!");
		}

		void HighlightEnd () {
			Debug.Log ("Gold sphere not highlighted!");
		}
	}

}