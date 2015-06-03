package com.android.coffice;

import com.android.coffice.R;
import com.android.web.IpConfig;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.os.Handler;
import android.text.format.Time;
import android.view.Window;
import android.view.WindowManager;
/**
 * ��ӭ���棻
 * 
 * @author �ֶ�
 * 
 */
public class WelcomeActivity extends Activity{
	Handler handler = new Handler();
	@Override
	public void onCreate(Bundle saveInstanceStateBundle){
		requestWindowFeature(Window.FEATURE_NO_TITLE); // �����ޱ���
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN); // ����ȫ��
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE); // ���ú���
		super.onCreate(saveInstanceStateBundle);
		setContentView(R.layout.welcome);
		handler.postDelayed(runnable, 3000);// ÿ����ִ��һ��runnable.
	}
	Runnable runnable = new Runnable() {
		@Override
		public void run() {
			// TODO Auto-generated method stub
			Intent intent=new Intent();
			intent.setClass(WelcomeActivity.this,MainActivity.class);
			startActivity(intent);
		}
	};
}
