package com.android.hal.cashbox;

import android.content.Context;
import android.os.AsyncTask;
import android.widget.Toast;

/**
 * 钱盒操作帮助类；
 * 
 * @author 贾豆
 * 
 */
public class CashboxHelper {
	private Context _context;
	private OperatFinshListener _operatFinshListener;
	public static boolean _isOpen=false;

	public CashboxHelper(Context _super, OperatFinshListener operatFinshListener) {
		this._context = _super;
		this._operatFinshListener = operatFinshListener;
	}

	public void operateCashbox(final boolean isOpen) {
		AsyncTask<Void, Void, Boolean> aTask = new AsyncTask<Void, Void, Boolean>() {
			@Override
			protected Boolean doInBackground(Void... params) {
				try {
					Thread.sleep(1000);
					if (isOpen) {
						 JBInterface.openCashBox();
					} else {
						 JBInterface.closeCashBox();
					}

				} catch (Exception error) {
					return false;
				}
				return true;
			}

			@Override
			protected void onPostExecute(Boolean result) {
				super.onPostExecute(result);
				_isOpen=isOpen;
				_operatFinshListener.operateFinshed();
			}
		};
		aTask.execute();
	}

	/*
	 * 打印完回调
	 */
	public interface OperatFinshListener {
		public void operateFinshed();
	}
}
