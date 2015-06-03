package com.android.hal.beep;


import com.android.coffice.R;
import com.ctrl.gpio.Ioctl;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;

public class MainActivity extends Activity {
	private Button button, laiser, shanguan;
	private int statc = 0, statc1 = 0, statc3 = 3;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.hal_beep);
		button = (Button) findViewById(R.id.button);
		laiser = (Button) findViewById(R.id.laiser);
		shanguan = (Button) findViewById(R.id.shanguan);

		shanguan.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				if (statc3 == 0) {
					// for(int i=11;i<=23;i++){
					// Ioctl.activate(i, 1);
					// }
					Ioctl.activate(23, 0);
					shanguan.setText("OPEN FLASH");
					statc3 = 1;
				} else {
					// for(int i=11;i<=23;i++){
					// Ioctl.activate(i, 0);
					// }
					Ioctl.activate(23, 1);
					shanguan.setText("CLOSE FLASH");
					statc3 = 0;
				}

			}
		});
		laiser.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				if (statc1 == 0) {
					int a1 = Ioctl.activate(22, 1);
					System.out.println("a1 ==== " + a1);
					laiser.setText("CLOSE LAISER");
					statc1 = 1;
				} else {
					int a2 = Ioctl.activate(22, 0);
					System.out.println("a2 ==== " + a2);
					laiser.setText("OPEN LAISER");
					statc1 = 0;
				}

			}
		});

		button.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				if (statc == 0) {
					Ioctl.activate(21, 1);
					Ioctl.convertPSAM();
					button.setText("CLOSE BEEP");
					statc = 1;
				} else {
					Ioctl.activate(21, 0);
					button.setText("OPEN BEEP");
					statc = 0;
				}

			}
		});
	}

	@Override
	protected void onDestroy() {
		Ioctl.activate(21, 0);
		Ioctl.activate(22, 0);
		Ioctl.activate(23, 0);
		button.setText("OPEN BEEP");
		laiser.setText("OPEN LAISER");
		shanguan.setText("OPEN FLASH");
		statc = 0;
		statc1 = 0;
		statc3 = 0;
		super.onDestroy();
	}

	@Override
	protected void onStop() {
		// TODO Auto-generated method stub
		super.onStop();
		Ioctl.activate(21, 0);
		Ioctl.activate(22, 0);
		Ioctl.activate(23, 0);
		button.setText(" OPEN BEEP");
		laiser.setText(" CLOSE LAISER");
		shanguan.setText(" CLOSE FLASH");
		statc = 0;
		statc1 = 0;
		statc3 = 0;
	}

	@Override
	protected void onPause() {
		Ioctl.activate(21, 0);
		Ioctl.activate(22, 0);
		Ioctl.activate(23, 0);
		button.setText(" OPEN BEEP");
		laiser.setText(" OPEN LAISER");
		shanguan.setText(" OPEN FLASH");
		statc = 0;
		statc1 = 0;
		statc3 = 0;
		super.onPause();
	}

}
