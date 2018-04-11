using System;
using System.Reflection;
using UnityEngine;

namespace WWTK.CrashReporting
{
	/**
	 * Switch CloudId programmatically, used as part of the build process, inside the Editor.
	 */
	public class UnityConnectUtils
	{
		public static void SetCloudProjectId(string id, string name, string company)
		{
			Type type = Type.GetType("UnityEditor.Connect.UnityConnect, UnityEditor, Version = 0.0.0.0, Culture = neutral, PublicKeyToken = null");
			PropertyInfo instanceInfo = type.GetProperty("instance");
			object settingsInstance = instanceInfo.GetValue(null, null);

			MethodInfo unbind = type.GetMethod("UnbindProject");
			MethodInfo bind = type.GetMethod("BindProject");

			Debug.Log("Unbinding current cloud project");
			unbind.Invoke(settingsInstance, new System.Object[] { });

			Debug.LogFormat("Binding cloud project to {0}: {1} ({2})", name, id, company);
			bind.Invoke(settingsInstance, new System.Object[] { id, name, company });
		}
	}
}
