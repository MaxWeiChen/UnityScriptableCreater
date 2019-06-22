using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MWUtil
{
	/// <summary>
	/// Need Put at /Assets because need EditorHelper to Find Assembly
	/// </summary>
	public class ScriptableCreater : EditorWindow
	{
		private string rootPath = "Assets";
		private string fileName = "NewScriptable";
		private string fileExtenstion = ".asset";
		private int scriptableClassIndex = 0;
		private Type[] scriptableClassTypes = null;
		private string[] scriptableClassNames = null;
		private string filterNamespace = String.Empty;

		[MenuItem("MWUtil/ScriptableCreater", false, 0)]
		static void ShowWindow()
		{
			EditorWindow.GetWindow<ScriptableCreater>("ScriptableCreater");
		}

		void OnEnable()
		{
			ResetScriptableClass();
		}

		private void ResetScriptableClass()
		{
			scriptableClassTypes = EditorHelper.FindDerivedTypes<ScriptableObject>(filterNamespace);
			scriptableClassNames = scriptableClassTypes.Select(t => t.ToString()).ToArray();
		}

		void OnGUI()
		{
			GUILayout.Label("Filter namespace");
			filterNamespace = GUILayout.TextField(filterNamespace);
			if(GUILayout.Button("Filter"))
			{
				ResetScriptableClass();
			}

			GUILayout.Label("Type of ScriptableObject:");
			scriptableClassIndex = EditorGUILayout.Popup(scriptableClassIndex, scriptableClassNames);

			if(GUILayout.Button("Create ScriptableObject"))
			{
				var className = scriptableClassNames[scriptableClassIndex];
				var nameArray = className.Split(new char[] { '.' });
				if(nameArray.Length > 0)
				{
					className = nameArray[nameArray.Length - 1];
				}
				else
				{
					className = this.fileName;
				}
				ScriptableCreateUtility.CreateScriptableObject(scriptableClassTypes[scriptableClassIndex], rootPath, className, fileExtenstion);
			}
		}
	}
}