﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataManager : Singleton<UIDataManager> 
{

	public int TimeIndex = 0;
	public List<string> MonthKeys;

	protected UIDataManager() 
	{
		MonthKeys = createMonthKeyList();
	}

	private List<string> createMonthKeyList() 
	{
		List<string> monthKeyList = new List<string>();
		for (int year = 1997; year <= 2017; year++) 
		{
			for (int month = 1; month <= 12; month++)
			{
				monthKeyList.Add(createMonthKey(year, month));
			}
		}
		return monthKeyList;
	}

	private string createMonthKey(int yearNum, int monthNum) 
	{
		string key = "";
		key += yearNum.ToString();
		key += "-";
		if (monthNum < 10) 
		{
			key += "0";
		}
		key += monthNum.ToString();
		return key;
	}
}