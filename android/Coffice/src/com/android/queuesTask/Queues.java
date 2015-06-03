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
 * 任务队列；
 * 
 * @author 贾豆
 * 
 */
public class Queues {
	public static List<Task> queue = new LinkedList<Task>();


	/*	队列添加任务*/
	public static void add(Task t) {
		synchronized (Queues.queue) {
			Queues.queue.add(t); // 添加任务
			Queues.queue.notifyAll();// 激活该队列对应的执行线程，全部Run起来
		}
	}

	public static class Task {
		private Order _printModel;
		private int OptionSType;
		
		/* 构造函数重载1：发送String*/
		public Task(Order printModel) {
			this._printModel = printModel;
		}

		/* 此处执行对应任务*/
		public void RunTask() {
			startPrintTask();
		}
		
		/* 执行打印任务*/
		public void startPrintTask(){
			MainActivity.mainPrinterHelper.printReceipt(_printModel);
		}
	}
}
