package com.android.custom;

import android.R.string;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.SharedPreferences;
import android.util.AttributeSet;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.TextView.OnEditorActionListener;

import com.android.coffice.R;
import com.android.web.IpConfig;

public class CustomSetting extends LinearLayout {
	private Context _context;
	private static valueChangeListener _valueChangeListener;

	public CustomSetting(Context context) {
		super(context);
		this._context = context;
		initLayout();
	}

	@SuppressLint("NewApi") public CustomSetting(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		this._context = context;
		initLayout();
	}

	public CustomSetting(Context context, AttributeSet attrs) {
		super(context, attrs);
		this._context = context;
		initLayout();
	}

	private void initLayout() {
		LayoutInflater inflater = (LayoutInflater) _context
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		inflater.inflate(R.layout.custom_setting, this);
		final CustomEditText customEditText = (CustomEditText) findViewById(R.id.index_domain_edittext_id);
		customEditText.setText(ReadDomain());
		customEditText.setOnEditorActionListener(new OnEditorActionListener() {

			@Override
			public boolean onEditorAction(TextView v, int actionId,
					KeyEvent event) {
				// TODO Auto-generated method stub
				_valueChangeListener.onChange(customEditText.getText().toString().trim());
				return false;
			}



		});
	}

	public void setValueChangeListener(valueChangeListener valueChangeListener) {
		this._valueChangeListener = valueChangeListener;
	}

	public interface valueChangeListener {
		public void onChange(String msg);
	}
	
    /* ∂¡»°≈‰÷√Œƒº˛ */
    private String ReadDomain()
    {
        SharedPreferences sharedata = _context.getSharedPreferences("Setting", 0);
        IpConfig.hostIpString=sharedata.getString("domain", IpConfig.hostIpString);
        return IpConfig.hostIpString;
    }
}
