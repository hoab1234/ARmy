using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mapbox.Examples
{
	public class CharacterMovement : MonoBehaviour
	{
		public Material[] Materials;
		public Transform Target;
		public Animator CharacterAnimator;
		public float Speed;
		AstronautMouseController _controller;
		void Start()
		{
			_controller = GetComponent<AstronautMouseController>();
		}

		void Update()
		{
			
			if (_controller.enabled)// Because the mouse control script interferes with this script
			{
				return;
			}

			foreach (var item in Materials)
			{
				item.SetVector("_CharacterPosition", transform.position);
			}

			// CamControl();

			var distance = Vector3.Distance(transform.position, Target.position);
			if (distance > 0.1f)
			{
				transform.LookAt(Target.position);
				transform.Translate(Vector3.forward * Speed);
				CharacterAnimator.SetBool("IsWalking", true);
			}
			else
			{
				CharacterAnimator.SetBool("IsWalking", false);
			}
		}

		#region CameraControl
		[Header("CameraSettings")]
		[SerializeField]
		Camera cam;
		Vector3 previousPos = Vector3.zero;
		Vector3 deltaPos = Vector3.zero;

		void CamControl()
		{
			deltaPos = transform.position - previousPos;
			deltaPos.y = 0;
			cam.transform.position = Vector3.Lerp(cam.transform.position, cam.transform.position + deltaPos, Time.time);
			previousPos = transform.position;
		}
		#endregion
	}
}