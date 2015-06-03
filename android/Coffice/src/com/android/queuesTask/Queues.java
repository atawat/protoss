package com.android.queuesTask;

import java.util.LinkedList;
import java.util.List;

import android.widget.Toast;

import com.android.bean.Order;
import com.android.bean.PrintModel;
import com.android.coffice.MainActivity;
import com.android.coffice.R;
import com.android.hal.printer.PrinterHelper;
import com.android.hal.printer.PrinterHelper.PrinterFinshListener;
/**
 * ������У�
 * 
 * @author �ֶ�
 * 
 */
public class Queues {
	public static List<Task> queue = new LinkedList<Task>();


	/*	�����������*/
	public static void add(Task t) {
		synchronized (Queues.queue) {
			Queues.queue.add(t); // �������
			Queues.queue.notifyAll();// ����ö��ж�Ӧ��ִ���̣߳�ȫ��Run����
		}
	}

	public static class Task {
		private Order _printModel;
		private int OptionSType;
		
		/* ���캯������1������String*/
		public Task(Order printModel) {
			this._printModel = printModel;
		}

		/* �˴�ִ�ж�Ӧ����*/
		public void RunTask() {
			startPrintTask();
		}
		
		/* ִ�д�ӡ����*/
		public void startPrintTask(){
			MainActivity.mainPrinterHelper.printReceipt(_printModel);
		}
	}
}
