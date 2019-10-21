using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using System;

public class ToolBarButtonEditor : MonoBehaviour 
{
	private static Rect leftButtonRect = new Rect(380, 5, 110, 22);
	private static Rect rightButtonRect = new Rect(1050, 5, 110, 22);

	private static void OnGUI()
	{
		List<ToolBarButtonClass> buttonList = ToolBarButtonManager.ToolBarButtonList;

		for (int i = 0; i < buttonList.Count; i++)
		{
			Rect _buttonRect = GetRect(i + 1);
			if (GUI.Button(_buttonRect, buttonList[i].name, new GUIStyle("button")))
			{
				buttonList[i].action();
			}

			_buttonRect.x += _buttonRect.width + 1;
		}
	}

	private static Rect GetRect(int index)
	{
		if (index <= 4)
		{
			float x = (index - 1) * (leftButtonRect.width + 1) + leftButtonRect.x;
			return new Rect(x, leftButtonRect.y, leftButtonRect.width, leftButtonRect.height);
		}
		else
		{
			float x = (index - 5) * (rightButtonRect.width + 1) + rightButtonRect.x;
			return new Rect(x, rightButtonRect.y, rightButtonRect.width, rightButtonRect.height);
		}
	}
	
	[InitializeOnLoadMethod]
	private static void InitializeOnLoad()
	{
		EditorApplication.delayCall += WaitForUnityEditorToolbar;
	}

	private static Type Toolbar =
		typeof(EditorGUI)
			.Assembly
			.GetType("UnityEditor.Toolbar");

	private static FieldInfo Toolbar_get =
		Toolbar
			.GetField("get");

	private static Type GUIView =
		typeof(EditorGUI)
			.Assembly
			.GetType("UnityEditor.GUIView");

	private static PropertyInfo GUIView_imguiContainer =
		GUIView
			.GetProperty(
				"imguiContainer",
				BindingFlags.Instance |
				BindingFlags.Public |
				BindingFlags.NonPublic);

	private static FieldInfo IMGUIContainer_m_OnGUIHandler =
		typeof(IMGUIContainer)
			.GetField(
				"m_OnGUIHandler",
				BindingFlags.Instance |
				BindingFlags.Public |
				BindingFlags.NonPublic);

	private static void WaitForUnityEditorToolbar()
	{
		var toolbar = Toolbar_get.GetValue(null);
		if (toolbar != null)
		{
			AttachToUnityEditorToolbar(toolbar);
			return;
		}

		EditorApplication.delayCall += WaitForUnityEditorToolbar;
	}

	private static void AttachToUnityEditorToolbar(object toolbar)
	{
		var toolbarGUIContainer = (IMGUIContainer) GUIView_imguiContainer.GetValue(toolbar, null);

		var toolbarGUIHandler =
			(Action)
			IMGUIContainer_m_OnGUIHandler
				.GetValue(toolbarGUIContainer);

		toolbarGUIHandler += OnGUI;

		IMGUIContainer_m_OnGUIHandler
			.SetValue(toolbarGUIContainer, toolbarGUIHandler);
	}
}
