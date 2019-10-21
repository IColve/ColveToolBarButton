using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolBarButtonManager : MonoBehaviour
{
	private static List<ToolBarButtonClass> toolBarButtonList;

	public static List<ToolBarButtonClass> ToolBarButtonList
	{
		get
		{
			UpdateButtonList();
			return toolBarButtonList;
		}
	}

	private static void UpdateButtonList()
	{
		if (toolBarButtonList == null)
		{
			toolBarButtonList = new List<ToolBarButtonClass>();
		}
		
		toolBarButtonList.Clear();

		UpdateAddButton();

		toolBarButtonList.RemoveAll(x => string.IsNullOrEmpty(x.name));
	}

	private static void UpdateAddButton()
	{
		toolBarButtonList.Add(new ToolBarButtonClass(button1Name, Button1Action));
		toolBarButtonList.Add(new ToolBarButtonClass(button2Name, Button2Action));
		toolBarButtonList.Add(new ToolBarButtonClass(button3Name, Button3Action));
		toolBarButtonList.Add(new ToolBarButtonClass(button4Name, Button4Action));
		toolBarButtonList.Add(new ToolBarButtonClass(button5Name, Button5Action));
		toolBarButtonList.Add(new ToolBarButtonClass(button6Name, Button6Action));
		toolBarButtonList.Add(new ToolBarButtonClass(button7Name, Button7Action));
		toolBarButtonList.Add(new ToolBarButtonClass(button8Name, Button8Action));
	}

	//Edit toolbar here
	//if name is empty, never show on toolbar;
	#region ButtonAction

	private static string button1Name = "1";
	private static void Button1Action()
	{
		Debug.Log(1);
	}
	
	private static string button2Name = "";
	private static void Button2Action()
	{
		
	}
	
	private static string button3Name = "";
	private static void Button3Action()
	{
		
	}
	
	private static string button4Name = "";
	private static void Button4Action()
	{
		
	}
	
	private static string button5Name = "";
	private static void Button5Action()
	{
		
	}
	
	private static string button6Name = "6";
	private static void Button6Action()
	{
		Debug.Log(6);
	}
	
	private static string button7Name = "";
	private static void Button7Action()
	{
		
	}
	
	private static string button8Name = "8";
	private static void Button8Action()
	{
		Debug.Log(8);
	}

	#endregion
	

}

public class ToolBarButtonClass
{
	public string name;
	public Action action;

	public ToolBarButtonClass(string name, Action action)
	{
		this.name = name;
		this.action = action;
	}
}