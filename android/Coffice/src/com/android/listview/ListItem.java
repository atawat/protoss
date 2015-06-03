package com.android.listview;

import java.util.HashMap;
import java.util.Map;

/**
 * listview部件类
 * 
 * @author 贾豆
 * 
 */
public class ListItem 
{
	/**类型*/
	public int mType;
	/**键值对应Map*/
	public Map<Object, ?> mMap ;
	
	public ListItem(int type, HashMap<Object, ?> map)
	{
		mType = type;
		mMap = map;
	}
}
