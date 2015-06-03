package com.android.hal.cashbox;

import com.ctrl.gpio.Ioctl;



public class JBInterface {

	/**
	 * Open CashBox 打开
	 * */
	public static void openCashBox() {
		Ioctl.activate(16, 1);
		try {
			Thread.sleep(50);
			Ioctl.activate(16, 0);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	/**
	 * Close CashBox 关闭
	 * */
	public static void  closeCashBox() {
		Ioctl.activate(16, 0);
	}
	
	
}
