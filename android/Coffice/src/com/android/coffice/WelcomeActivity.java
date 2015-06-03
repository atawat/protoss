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
 * 欢迎界面；
 * 
 * @author 贾豆
 * 
 */
public class WelcomeActivity extends Activity{
	Handler handler = new Handler();
	@Override
	public void onCreate(Bundle saveInstanceStateBundle){
		requestWindowFeature(Window.FEATURE_NO_TITLE); // 设置无标题
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN); // 设置全屏
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE); // 设置横屏
		super.onCreate(saveInstanceStateBundle);
		setContentView(R.layout.welcome);
		handler.postDelayed(runnable, 3000);// 每两秒执行一次runnable.
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
