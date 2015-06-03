package com.android.custom;

import android.content.Context;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.AnimationSet;
import android.view.animation.ScaleAnimation;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.android.coffice.R;
/**
 * 自定义数字选择器；
 * 
 * @author 贾豆
 * 
 */
public class CustomNumberPicker extends LinearLayout {
	private Context _context;
	private ImageView _numberPlusButtonImageView;
	private ImageView _numberMinusButtonImageView;
	private CustomEditText _numberEditText;
	private ValueChangeListener _valueChangeListener;
	private int _initValue = 1;
	private int _minValue=0;
	private int _maxValue=100;

	public CustomNumberPicker(Context context) {
		super(context);
		this._context = context;
		initLayout();
	}

	public CustomNumberPicker(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		this._context = context;
		initLayout();
	}

	public CustomNumberPicker(Context context, AttributeSet attrs) {
		super(context, attrs);
		this._context = context;
		initLayout();
	}

	private void initLayout() {
		LayoutInflater inflater = (LayoutInflater) _context
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		inflater.inflate(R.layout.custom_number_picker, this);
		initView();
	}

	public void setInitValue(int value,int minValue,int maxValue) {
		this._initValue = value;
		this._minValue=minValue;
		this._maxValue=maxValue;
	}

	private void initView() {
		_numberPlusButtonImageView = (ImageView) this
				.findViewById(R.id.number_plus_id);
		_numberMinusButtonImageView = (ImageView) this
				.findViewById(R.id.number_minus_id);
		_numberEditText = (CustomEditText) this
				.findViewById(R.id.number_select_id);
		_numberEditText.setText(String.valueOf(_initValue));
		_numberPlusButtonImageView.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				ClickAnim(v);
				int count = Integer.parseInt(_numberEditText.getText()
						.toString().trim());
				if (count < _maxValue) {
					_numberEditText.setText(String.valueOf(count + 1));
				} else {
					Toast.makeText(_context, "您输入值过大！", Toast.LENGTH_SHORT)
							.show();
				}
				_valueChangeListener.onChange(Integer.parseInt(_numberEditText
						.getText().toString().trim()));
			}
		});
		_numberMinusButtonImageView.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				ClickAnim(v);
				// TODO Auto-generated method stub
				int count = Integer.parseInt(_numberEditText.getText()
						.toString().trim());
				if (count > _minValue) {
					_numberEditText.setText(String.valueOf(count - 1));
				} else {
					Toast.makeText(_context, "不能再减少！", Toast.LENGTH_SHORT)
							.show();
				}
				_valueChangeListener.onChange(Integer.parseInt(_numberEditText
						.getText().toString().trim()));
			}
		});
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
	public void SetOnchangeCallback(ValueChangeListener valueChangeListener) {
		this._valueChangeListener = valueChangeListener;
	}

	/*
	 * 回调接口
	 */
	public interface ValueChangeListener {
		public void onChange(int value);
	}
}
