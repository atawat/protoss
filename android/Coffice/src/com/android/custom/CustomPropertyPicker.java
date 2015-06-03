package com.android.custom;

import java.util.ArrayList;
import java.util.HashMap;

import android.content.Context;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.AnimationSet;
import android.view.animation.ScaleAnimation;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.SimpleAdapter;

import com.android.coffice.R;
/**
 * 自定义属性选择器；
 * 
 * @author 贾豆
 * 
 */
public class CustomPropertyPicker extends LinearLayout {
	private Context _context;
	private OnSelectedClickListener _onItemClickListener;
	private ArrayList<HashMap<String, Object>> _itemList;

	public CustomPropertyPicker(Context context) {
		super(context);
		this._context = context;
		initLayout();
	}

	public CustomPropertyPicker(Context context, AttributeSet attrs,
			int defStyle) {
		super(context, attrs, defStyle);
		this._context = context;
		initLayout();
	}

	public CustomPropertyPicker(Context context, AttributeSet attrs) {
		super(context, attrs);
		this._context = context;
		initLayout();
	}

	private void initLayout() {
		LayoutInflater inflater = (LayoutInflater) _context
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		inflater.inflate(R.layout.custom_property_picker, this);
		initView();
	}

	private void initView() {
		Init_GridView();
	}

	/*
	 * 点击动画
	 */
	private boolean ClickAnim(View view)// Icon移动；
	{
		/** 设置缩放动画 */
		ScaleAnimation animation = new ScaleAnimation(0.0f, 1.0f, 0.0f, 1.0f,
				Animation.RELATIVE_TO_SELF, 0.5f, Animation.RELATIVE_TO_SELF,
				0.5f);
		animation.setDuration(200);// 设置动画持续时间
		animation.startNow();
		AnimationSet animationSet = new AnimationSet(true);
		animationSet.addAnimation(animation);
		view.startAnimation(animationSet);
		animationSet.start();
		return false;
	}

	/*
	 * 实例化回调接口
	 */
	public void setSelectedClickCallback(
			OnSelectedClickListener onSelectedClickListener) {
		this._onItemClickListener = onSelectedClickListener;
	}

	/*
	 * 回调接口
	 */
	public interface OnSelectedClickListener {
		public void onSelectedClick(String name, int id,View view);
	}

	/* 初始化网格界面 */
	private void Init_GridView() {
		GridView gridview = (GridView) findViewById(R.id.property_gridview);
		_itemList = new ArrayList<HashMap<String, Object>>();
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("itemText", "冷饮");
		_itemList.add(map);
		HashMap<String, Object> map1 = new HashMap<String, Object>();
		map1.put("itemText", "热饮");
		_itemList.add(map1);
		HashMap<String, Object> map2 = new HashMap<String, Object>();
		map2.put("itemText", "温饮");
		_itemList.add(map2);
		SimpleAdapter saImageItems = new SimpleAdapter(_context,
				_itemList,// 数据源
				R.layout.custom_property_picker_item,// 显示布局
				new String[] { "itemText" },
				new int[] { R.id.property_picker_item_text });
		gridview.setAdapter(saImageItems);
		gridview.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				// TODO Auto-generated method stub
//				ResetViewStyle(parent, R.color.coffice_product_lightgray);
//				view.setBackgroundColor(getResources().getColor(
//						R.color.coffice_product_gray));
				ClickAnim(view);
				_onItemClickListener.onSelectedClick(_itemList.get(position).get("itemText").toString(), 0,view);
			}

		});
	}

	/*
	 * 重置样式；
	 */
	private void ResetViewStyle(AdapterView<?> parent, int color) {
		// TODO Auto-generated method stub
		for (int i = 0; i < parent.getChildCount(); i++) {
			View v = parent.getChildAt(i);
			v.setBackgroundColor(getResources().getColor(color));
		}
	}
}
