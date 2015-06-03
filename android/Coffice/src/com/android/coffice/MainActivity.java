package com.android.coffice;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.os.Handler;
import android.os.PowerManager;
import android.os.PowerManager.WakeLock;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;
import android.widget.Toast;

import com.android.bean.Order;
import com.android.bean.OrderDetail;
import com.android.bean.PrintModel;
import com.android.coffice.index.IndexFragment;
import com.android.coffice.order.OrderFragment;
import com.android.coffice.product.ProductFragment;
import com.android.hal.printer.PrinterHelper;
import com.android.hal.printer.PrinterHelper.PrinterFinshListener;
import com.android.listview.ImageListAdapter;
import com.android.listview.ListItem;
import com.android.queuesTask.Queues;
import com.android.queuesTask.StartTaskThread;
import com.android.web.HttpHelp;
import com.android.web.IpConfig;
import com.android.web.HttpHelp.GetHttpListener;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.display.RoundedBitmapDisplayer;

/**
 * ��������ڣ��ɶ��Fragement���ɣ�
 * 
 * @author �ֶ�
 * 
 */
public class MainActivity extends Activity {

	public String TAG = "MainActivity";
	public Context _context;
	private DisplayImageOptions _imageLoadingOptions;
	private ImageLoader _imageLoader;
	public static PrinterHelper mainPrinterHelper;
	public static int _durTime = 600000;// ˢ�¼����ms����
	public Handler handler = new Handler();
	PowerManager powerManager = null; 
    WakeLock wakeLock = null; 
	/*
	 * �������
	 */
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		requestWindowFeature(Window.FEATURE_NO_TITLE); // �����ޱ���
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN); // ����ȫ��
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE); // ���ú���
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);
		this._context = this;
		_imageLoadingOptions = new DisplayImageOptions.Builder()
				.showStubImage(R.drawable.ic_stub) // ����ͼƬ�����ڼ���ʾ��ͼƬ
				.showImageForEmptyUri(R.drawable.ic_empty) // ����ͼƬUriΪ�ջ��Ǵ����ʱ����ʾ��ͼƬ
				.showImageOnFail(R.drawable.ic_error) // ����ͼƬ���ػ��������з���������ʾ��ͼƬ
				.cacheInMemory(true) // �������ص�ͼƬ�Ƿ񻺴����ڴ���
				.cacheOnDisc(true) // �������ص�ͼƬ�Ƿ񻺴���SD����
				.displayer(new RoundedBitmapDisplayer(0)) // ���ó�Բ��ͼƬ
				.build();
		_imageLoader = ImageLoader.getInstance();

		initParameter();
		initMenuList();
		initFragment();

		initPrinter();// ��ʼ����ӡ����
		initPrintQueues();// ������ӡ���߳���ռ���У�
		initBackThread();// ������̨�̣߳���ʱ����;
		initPower();//������Ļ����
	}

	private void initPower() {
		// TODO Auto-generated method stub
		 this.powerManager = (PowerManager)this.getSystemService(Context.POWER_SERVICE); 
	      this.wakeLock = this.powerManager.newWakeLock(PowerManager.FULL_WAKE_LOCK, "My Lock"); 
	}

	private void initParameter() {
		// TODO Auto-generated method stub
		ReadDomain();
	}

	/*
	 * ��ʼ��fragmentģ�飻
	 */
	public void initFragment() {
		IndexFragment indexFragment = new IndexFragment(_context, _imageLoader);
		getFragmentManager().beginTransaction()
				.replace(R.id.fragment_content, indexFragment).commit();
	}

	/*
	 * ��ʼ���˵�
	 */
	public void initMenuList() {
		Log.d(TAG, "��ʼ���˵��б�");
		ListView listView = (ListView) findViewById(R.id.menu_listView);
		ArrayList<ListItem> list_GroupItem = new ArrayList<ListItem>();
		final ImageListAdapter listGroup = new ImageListAdapter(this,
				list_GroupItem, _imageLoadingOptions, _imageLoader, null);
		listGroup.AddType(R.layout.menu_item);
		listView.setAdapter(listGroup);
		list_GroupItem.add(new ListItem(0, getHashMap(R.drawable.menu_index,
				this.getString(R.string.menu_str0))));
		list_GroupItem.add(new ListItem(0, getHashMap(R.drawable.menu_product,
				this.getString(R.string.menu_str1))));
		list_GroupItem.add(new ListItem(0, getHashMap(R.drawable.menu_order,
				this.getString(R.string.menu_str2))));
		listGroup.notifyDataSetChanged();
		listView.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				ResetMenuStyle(parent);
				view.setBackgroundColor(getResources().getColor(
						R.color.coffice_product_yellow));
				switch (position) {
				case 0:
					IndexFragment indexFragment = new IndexFragment(_context,
							_imageLoader);
					getFragmentManager().beginTransaction()
							.replace(R.id.fragment_content, indexFragment)
							.commit();
					break;
				case 1:
					ProductFragment productFragment = new ProductFragment(
							_context, _imageLoader);
					getFragmentManager().beginTransaction()
							.replace(R.id.fragment_content, productFragment)
							.commit();
					break;
				case 2:
					OrderFragment orderFragment = new OrderFragment(_context,
							_imageLoader);
					getFragmentManager().beginTransaction()
							.replace(R.id.fragment_content, orderFragment)
							.commit();
					break;
				default:
					break;
				}
			}

		});

	}

	/*
	 * �������а�����ʽ��
	 */
	private void ResetMenuStyle(AdapterView<?> parent) {
		// TODO Auto-generated method stub
		for (int i = 0; i < parent.getChildCount(); i++) {
			View v = parent.getChildAt(i);
			v.setBackgroundColor(getResources().getColor(
					R.color.coffice_product_dark));
		}
	}

	/*
	 * ��ȡ��Դ��ϣ�б�
	 */
	public HashMap<Object, Object> getHashMap(int imgUrlId, String menuStr) {
		HashMap<Object, Object> map1 = new HashMap<Object, Object>();
		map1.put(R.id.menu_img_bg, imgUrlId);
		map1.put(R.id.menu_tital_bg, menuStr);
		return map1;
	}

	/* ��ȡ�����ļ� */
	private String ReadDomain() {
		SharedPreferences sharedata = _context.getSharedPreferences("Setting",
				0);
		IpConfig.hostIpString = sharedata.getString("domain", IpConfig.hostIpString);
		return IpConfig.hostIpString;
	}

	/*
	 * �˳�
	 */
	@Override
	public void onDestroy() {
		super.onDestroy();
		_imageLoader.clearMemoryCache();// ����ڴ滺�棻
		mainPrinterHelper.closePrinter();//�رմ�ӡ����
	}

	/*
	 * ��д���ذ�������
	 */
	@Override
	public void onBackPressed() {
	}

	/*
	 * ��ʼ����ӡ��
	 */
	public void initPrinter() {
		mainPrinterHelper = new PrinterHelper(new PrinterFinshListener() {
			@Override
			public void printFinshed( final int orderId) {
				String url=IpConfig.hostIpString + IpConfig.GetUpdataOrderIsPrintStatusByOrderId+"?orderId="+orderId+"&isPrint="+true;
				new HttpHelp().startGetAsnyTask(url, new GetHttpListener(){
					@Override
					public void getHttpResult(String res) {
						// TODO Auto-generated method stub
						Toast.makeText(_context, "��ӡ "+orderId+"="+res, Toast.LENGTH_SHORT).show();
					}
				});
			}
		});
	}

	/*
	 * �������߳���ռ��ӡ����
	 */
	public void initPrintQueues() {
		StartTaskThread.RunAllThread(2); // ���������߳����ڴ�ӡ��
	}

	/*
	 * ������̨ˢ���̣߳�
	 */
	public void initBackThread() {
		handler.postDelayed(runnable, _durTime);
	}

	/*
	 * ˢ���̣߳�
	 */
	Runnable runnable = new Runnable() {
		@Override
		public void run() {
			// TODO Auto-generated method stub
			new HttpHelp().startGetAsnyTask(IpConfig.hostIpString + IpConfig.getOrderByCondition
					+ "?isPrint=" + false + "&page=" + 1 + "&pageCount="
					+ 10, new GetHttpListener() {
				@SuppressWarnings("unchecked")
				@Override
				public void getHttpResult(String res) {
					Log.d(TAG, res);
					try {
						JSONArray jsonArray = new JSONArray(res);
						for (int i = 0; i < jsonArray.length(); i++) {
							JSONObject jsonObject = jsonArray.getJSONObject(i);
							try{
								addToPrintQueues(jsonObject);
							}catch(Exception e){
								
							}
						}
					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}

			});
			handler.postDelayed(this, _durTime);
		}
	};
	
	/*
	 * �����ӡ����
	 */
	private void addToPrintQueues(JSONObject jsonObject) throws JSONException {
		// TODO Auto-generated method stub
		Order order = new Order();
		
		order.setId(jsonObject.getInt("Id"));
		order.setCounponNum(jsonObject.getString("Coupon"));
		order.setDeliveryAddress(jsonObject.getString("DeliveryAddress"));
		order.setDiscount(jsonObject.getDouble("Discount"));
		order.setLocationX(jsonObject.getDouble("LocationX"));
		order.setLocationY(jsonObject.getDouble("LocationY"));
		order.setPayType(jsonObject.getInt("PayType"));
		order.setPhoneNumber(jsonObject.getString("PhoneNumber"));
		order.setType(jsonObject.getInt("Type"));
		order.setTitalPrice(jsonObject.getDouble("TotalPrice"));
		order.setOrderNum(jsonObject.getString("OrderNum"));
		JSONArray jsonArray=jsonObject.getJSONArray("Details");
		
		List<OrderDetail> odlist = new ArrayList<OrderDetail>();
		for (int i = 0; i < jsonArray.length(); i++) {
			JSONObject jo = jsonArray
					.getJSONObject(i);
			OrderDetail oDetail = new OrderDetail();
			oDetail.setCount((int)jo.getDouble("Count"));
			oDetail.setProductId(jo.getInt("ProductId"));
			oDetail.setRemark(jo.getString("Remark"));
			oDetail.setName(jo.getString("ProductName"));
			oDetail.setPrice(jo.getDouble("TotalPrice"));
			odlist.add(oDetail);
		}
		order.setDetails(odlist);
		Log.d(TAG, order.toString());
		Queues.Task task = new Queues.Task(order);
		Queues.add(task);
	}
}
