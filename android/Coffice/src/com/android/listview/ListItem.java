package com.android.listview;

import java.util.HashMap;
import java.util.Map;

/**
 * listview������
 * 
 * @author �ֶ�
 * 
 */
public class ListItem 
{
	/**����*/
	public int mType;
	/**��ֵ��ӦMap*/
	public Map<Object, ?> mMap ;
	
	public ListItem(int type, HashMap<Object, ?> map)
	{
		mType = type;
		mMap = map;
	}
}
