package com.android.hal.printer;

import java.util.Calendar;
import java.util.List;

import com.android.bean.Order;
import com.android.bean.OrderDetail;
import com.android.bean.PrintModel;

import android.content.Context;
import android.os.AsyncTask;
import android.text.format.Time;
import android.util.Log;
import android.widget.Toast;

/**
 * ��ӡ�����ࣻ
 * 
 * @author �ֶ�
 * 
 */
public class PrinterHelper {
	private PrinterFinshListener _printerFinshListener;
	private String TAG="PrinterHelper";

	public PrinterHelper(PrinterFinshListener printerFinshListener) {
		// TODO Auto-generated constructor stub
		try {
			JBInterface.initPrinter();
		} catch (Exception error) {
		}

		this._printerFinshListener = printerFinshListener;
	}

	public void printString(final String msg) {
		AsyncTask<Void, Void, Boolean> aTask = new AsyncTask<Void, Void, Boolean>() {
			@Override
			protected Boolean doInBackground(Void... params) {
				try {
					Thread.sleep(1000);
					JBInterface.setBold();
					JBInterface.setRight();
					JBInterface.printText(msg);
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				return true;
			}

			@Override
			protected void onPostExecute(Boolean result) {
				super.onPostExecute(result);
				// JBInterface.closePrinter();
				_printerFinshListener.printFinshed(-1);
			}
		};
		aTask.execute();
	}

	public void printReceipt(final Order printModel) {
		AsyncTask<Void, Void, Boolean> aTask = new AsyncTask<Void, Void, Boolean>() {
			@Override
			protected Boolean doInBackground(Void... params) {
				try {
					Thread.sleep(500);
					Calendar c = Calendar.getInstance();//���Զ�ÿ��ʱ���򵥶��޸�
					int year = c.get(Calendar.YEAR); 
					int month = c.get(Calendar.MONTH); 
					int date = c.get(Calendar.DATE); 
					int hour = c.get(Calendar.HOUR_OF_DAY); 
					int minute = c.get(Calendar.MINUTE); 
					int second = c.get(Calendar.SECOND); 
					JBInterface.setBold();
					JBInterface.setRight();
					String psString = "������СƱ" + "\r\n\r\n" + "������:"
							+ printModel.OrderNum + "\r\n" + "����:"
							+ printModel.Type + "\r\n" + "�绰:"
							+ printModel.PhoneNumber + "\r\n"
							+ "==============================";
					String ps1String = "\r\n";
					for (OrderDetail od : printModel.Details) {
						ps1String = ps1String + od.Name + "  " + od.Count
								+ "  " + od.Price +"  "+od.Remark+ "\r\n";
					}
					psString = psString + ps1String;
					psString = psString + "=============================="+ "\r\n" 
							+ "��ַ:" + printModel.DeliveryAddress + "\r\n"
							+ "�ܽ�" + printModel.TitalPrice + "\r\n" + "��ӡʱ�䣺"
							+ year + "/" + month + "/" + date + " " +hour + ":" +minute + ":" + second;
					JBInterface.printText(psString);
					Log.v(TAG, psString);
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				return true;
			}

			@Override
			protected void onPostExecute(Boolean result) {
				super.onPostExecute(result);
				_printerFinshListener.printFinshed(printModel.Id);
			}
		};
		aTask.execute();
	}

	/*
	 * ��ӡ��ص�
	 */
	public interface PrinterFinshListener {
		public void printFinshed(int orderId);
	}

	/*
	 * �رմ�ӡ��
	 */
	public void closePrinter() {
		// JBInterface.closePrinter();
	}
}
