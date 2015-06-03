package com.android.coffice.order;

import java.util.ArrayList;
import java.util.HashMap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.annotation.SuppressLint;
import android.app.Fragment;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationSet;
import android.view.animation.ScaleAnimation;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.FrameLayout;
import android.widget.ListView;
import android.widget.Toast;

import com.android.coffice.R;
import com.android.listview.ImageListAdapter;
import com.android.listview.SuperListView;
import com.android.listview.ImageListAdapter.GetViewListener;
import com.android.listview.ImageListAdapter.IItemClickListener;
import com.android.listview.SuperListView.OnLoadingListener;
import com.android.listview.SuperListView.OnRefreshListener;
import com.android.listview.ListItem;
import com.android.web.HttpHelp;
import com.android.web.IpConfig;
import com.android.web.HttpHelp.GetHttpListener;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.display.RoundedBitmapDisplayer;

/**
 * 订单界面；
 * 
 * @author 贾豆
 * 
 */
@SuppressLint("ValidFragment")
public class OrderFragment extends Fragment {
	private View _view;
	private SuperListView _orderListView;
	private ListView _classifyListView;
	private ListView _detailListView;
	private ImageListAdapter _orderAdapter;
	private ImageListAdapter _classifyAdapter;
	private ImageListAdapter _detailAdapter;
	private Context _superContext;
	private DisplayImageOptions _imageLoadingOptions;
	private ImageLoader _imageLoader;
	private ArrayList<ListItem> orderGroupItem = new ArrayList<ListItem>();
	private ArrayList<ListItem> classifyGroupItem = new ArrayList<ListItem>();
	private ArrayList<ListItem> detailGroupItem = new ArrayList<ListItem>();
	private String TAG = "OrderFragment";
	private String ORDER_ID = "order_id";
	private String DETAIL_ID = "detail_id";
	private static int _page = 1;
	private static HttpHelp _httpHelp;
	private static int _isPrintFlag=0;

	public OrderFragment() {
		_httpHelp = new HttpHelp();
	}

	public OrderFragment(Context superContext, ImageLoader imageLoader) {
		_superContext = superContext;
		_imageLoader = imageLoader;
		_httpHelp = new HttpHelp();
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// 使用DisplayImageOptions.Builder()创建DisplayImageOptions
		_view = inflater.inflate(R.layout.fragment_order, container, false);
		_imageLoadingOptions = new DisplayImageOptions.Builder()
				.showStubImage(R.drawable.ic_stub) // 设置图片下载期间显示的图片
				.showImageForEmptyUri(R.drawable.ic_empty) // 设置图片Uri为空或是错误的时候显示的图片
				.showImageOnFail(R.drawable.ic_error) // 设置图片加载或解码过程中发生错误显示的图片
				.cacheInMemory(true) // 设置下载的图片是否缓存在内存中
				.cacheOnDisc(true) // 设置下载的图片是否缓存在SD卡中
				.displayer(new RoundedBitmapDisplayer(40)) // 设置成圆角图片
				.build();
		initClassifyListView();
		initOrderListView();
		initDetailListView();
		return _view;
	}

	/*
	 * 初始化classify列表
	 */
	public void initClassifyListView() {
		Log.d(TAG, "初始化listview");
		_classifyListView = (ListView) _view
				.findViewById(R.id.order_classify_listView);
		_classifyAdapter = new ImageListAdapter(_superContext,
				classifyGroupItem, _imageLoadingOptions, _imageLoader,
				itemClickListener);
		_classifyAdapter.AddType(R.layout.order_classify_item);
		_classifyListView.setAdapter(_classifyAdapter);
		classifyGroupItem.add(new ListItem(0, getClassifyHashMap("全部订单")));
		classifyGroupItem.add(new ListItem(0, getClassifyHashMap("未出票")));
		classifyGroupItem.add(new ListItem(0, getClassifyHashMap("已出票")));
		_classifyAdapter.notifyDataSetChanged();
		_classifyListView.setOnItemClickListener(onClassifyClickListener);
	}

	/*
	 * 初始化Order列表
	 */
	public void initOrderListView() {
		Log.d(TAG, "初始化listview");
		_orderListView = (SuperListView) _view
				.findViewById(R.id.order_listView);
		_orderListView.setIcon(R.drawable.listview_arrow);
		_orderAdapter = new ImageListAdapter(_superContext, orderGroupItem,
				_imageLoadingOptions, _imageLoader, itemClickListener);
		_orderAdapter.AddType(R.layout.order_order_item);
		_orderListView.setAdapter(_orderAdapter);
		_orderListView.setonRefreshListener(new OnRefreshListener() {
			@Override
			public void onRefresh() {
				orderGroupItem.clear();
				_orderAdapter.notifyDataSetChanged();
				GetOrderAsynTask(1, 10, new GetDataFinshListener() {

					@Override
					public void onFinsh(String data) {
						// TODO Auto-generated method stub
						_orderListView.onRefreshComplete();
					}

				});
			}
		});
		_orderListView.setonLoadingListener(new OnLoadingListener() {

			@Override
			public void onLoading() {
				// TODO Auto-generated method stub
				_page++;
				GetOrderAsynTask(_page, 10, new GetDataFinshListener() {

					@Override
					public void onFinsh(String data) {
						// TODO Auto-generated method stub
						_orderListView.onLoadingComplete();
					}

				});
			}
		});

		_orderAdapter.notifyDataSetChanged();
		_orderListView.setOnItemClickListener(onOrderClickListener);
	}

	/*
	 * 初始化OrderDetail列表
	 */
	public void initDetailListView() {
		Log.d(TAG, "初始化listview");
		_detailListView = (ListView) _view
				.findViewById(R.id.order_detail_listView);
		_detailAdapter = new ImageListAdapter(_superContext, detailGroupItem,
				_imageLoadingOptions, _imageLoader, itemClickListener);
		_detailAdapter.AddType(R.layout.order_detail_item);
		_detailListView.setAdapter(_detailAdapter);

		_detailAdapter.notifyDataSetChanged();
	}

	/*
	 * 分类列表点击监听
	 */
	OnItemClickListener onClassifyClickListener = new OnItemClickListener() {

		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position,
				long id) {
			// TODO Auto-generated method stub
			ClickAnim(view);
			_page = 1;
			orderGroupItem.clear();

			_orderAdapter.notifyDataSetChanged();
			addRefreshingView(R.layout.refreshing, _orderListView);
			switch (position) {
			case 0:
				_isPrintFlag = 0;
				GetOrderAsynTask(1, 10, null);
				break;
			case 1:
				_isPrintFlag = 1;
				GetOrderAsynTask(1, 10, null);
				break;
			case 2:
				_isPrintFlag = 2;
				GetOrderAsynTask(1, 10, null);
				break;
			}

			ClassifyOnClickAnim(position, view, parent);
		}

	};

	/*
	 * 订单列表点击监听
	 */
	OnItemClickListener onOrderClickListener = new OnItemClickListener() {

		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position,
				long id) {
			// TODO Auto-generated method stub
			ClickAnim(view);
			OrderOnClickAnim(position, view, parent);
			GetOrderDetailAsynTask((Integer) orderGroupItem.get(position - 1).mMap
					.get(ORDER_ID));
		}

	};

	/*
	 * 获取资源哈希列表；
	 */
	public HashMap<Object, Object> getClassifyHashMap(String msg) {
		HashMap<Object, Object> map = new HashMap<Object, Object>();
		map.put(R.id.order_classify_name, msg);
		return map;
	}

	/*
	 * 获取资源哈希列表；
	 */
	public HashMap<Object, Object> getOrderHashMap(String name,
			boolean isPrint, double price, String time, int orderId) {
		HashMap<Object, Object> map = new HashMap<Object, Object>();
		map.put(R.id.order_name_id, name);
		String printfMsg = isPrint ? "已出票" : "未出票";
		map.put(R.id.order_isprint_id, printfMsg);
		map.put(R.id.order_price_id, price);
		map.put(R.id.order_time_id, time);
		map.put(ORDER_ID, orderId);
		return map;
	}

	/*
	 * 获取详情哈希列表
	 */
	private HashMap<Object, Object> getDetailHashMap(String name, int number,
			double price, String remark, int detailId) {
		HashMap<Object, Object> map = new HashMap<Object, Object>();
		map.put(R.id.order_detail_name_id, name);
		map.put(R.id.order_detail_number_id, number);
		map.put(R.id.order_detail_price_id, price);
		map.put(R.id.order_detail_property_id, remark);
		map.put(DETAIL_ID, detailId);
		return map;
	}

	/*
	 * 异步任务获取订单列表数据源
	 */
	private void GetOrderAsynTask(int page, int pageCount,
			final GetDataFinshListener getDataFinshListener) {
		String urlString = "";
		switch (_isPrintFlag) {
		case 0:
			urlString = IpConfig.hostIpString + IpConfig.getOrderByCondition
					+ "?page=" + page + "&pageCount=" + pageCount;
			break;
		case 1:
			urlString = IpConfig.hostIpString + IpConfig.getOrderByCondition
					+ "?isPrint=" + false + "&page=" + page + "&pageCount="
					+ pageCount;
			break;
		case 2:
			urlString = IpConfig.hostIpString + IpConfig.getOrderByCondition
					+ "?isPrint=" + true + "&page=" + page + "&pageCount="
					+ pageCount;
			break;
		}
		_httpHelp.startGetAsnyTask(urlString, new GetHttpListener() {
			@SuppressWarnings("unchecked")
			@Override
			public void getHttpResult(String res) {
				try {
					JSONArray jsonArray = new JSONArray(res);
					for (int i = 0; i < jsonArray.length(); i++) {
						JSONObject jsonObject = jsonArray.getJSONObject(i);
						int id = jsonObject.getInt("Id");
						String OrderNum = jsonObject.getString("OrderNum");
						double TotalPrice = jsonObject.getDouble("TotalPrice");
						String Addtime = jsonObject.getString("Addtime");
						Boolean IsPrint = jsonObject.getBoolean("IsPrint");
						orderGroupItem.add(new ListItem(0, getOrderHashMap(
								OrderNum, IsPrint, TotalPrice, Addtime, id)));
					}
					_orderAdapter.notifyDataSetChanged();
					removeRefreshingView(R.layout.refreshing, _orderListView);
					if (getDataFinshListener != null) {
						getDataFinshListener.onFinsh(res);
					}
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				// Toast.makeText(_super, res,
				// Toast.LENGTH_LONG).show();
			}

		});

	}

	/*
	 * 异步任务获取商品列表数据源
	 */
	private void GetOrderDetailAsynTask(final int orderId) {
		detailGroupItem.clear();
		_detailAdapter.notifyDataSetChanged();
		addRefreshingView(R.layout.refreshing, _detailListView);
		_httpHelp.startGetAsnyTask(IpConfig.hostIpString
				+ IpConfig.GetOrderDetailByOrder + "?orderid=" + orderId,
				new GetHttpListener() {
					@SuppressWarnings("unchecked")
					@Override
					public void getHttpResult(String res) {
						try {
							Log.d(TAG, res);
							JSONArray jsonArray = new JSONArray(res);
							for (int i = 0; i < jsonArray.length(); i++) {
								JSONObject jsonObject = jsonArray
										.getJSONObject(i);
								int id = jsonObject.getInt("Id");
								String ProductName = jsonObject
										.getString("ProductName");
								int ProductId = jsonObject.getInt("ProductId");
								double TotalPrice = jsonObject
										.getDouble("TotalPrice");
								String Remark = jsonObject.getString("Remark");
								int count = (int) jsonObject.getDouble("Count");
								detailGroupItem.add(new ListItem(0,
										getDetailHashMap(ProductName, count,
												TotalPrice, Remark, id)));
							}
							_detailAdapter.notifyDataSetChanged();
							removeRefreshingView(R.layout.refreshing,
									_detailListView);
						} catch (Exception e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
					}
				});
	}

	/*
	 * 点击order标记
	 */
	private boolean OrderOnClickAnim(final int positionNow, final View view,
			AdapterView<?> parent)// Icon移动；
	{
		for (int i = 0; i < parent.getChildCount(); i++) {
			View v = parent.getChildAt(i);
			v.setBackgroundColor(getResources().getColor(
					R.color.coffice_product_pink));
		}
		view.setBackgroundColor(getResources().getColor(
				R.color.coffice_product_lightgray));
		// 此处消除listview其他view点击印记，及保持点击处印记；
		_orderAdapter.setGetViewCallback(new GetViewListener() {
			@Override
			public void getView(int position, View convertView, ViewGroup parent) {
				// TODO Auto-generated method stub
				if (position + 1 != positionNow) {
					convertView.setBackgroundColor(getResources().getColor(
							R.color.coffice_product_pink));
				} else {
					convertView.setBackgroundColor(getResources().getColor(
							R.color.coffice_product_lightgray));
				}
			}
		});

		return false;
	}

	/*
	 * 点击状态标记
	 */
	private boolean ClassifyOnClickAnim(final int positionNow, final View view,
			AdapterView<?> parent)// Icon移动；
	{
		for (int i = 0; i < parent.getChildCount(); i++) {
			View v = parent.getChildAt(i);
			v.setBackgroundColor(getResources().getColor(
					R.color.coffice_product_gray));
		}
		view.setBackgroundColor(getResources().getColor(
				R.color.coffice_product_pink));
		// 此处消除listview其他view点击印记，及保持点击处印记；
		_classifyAdapter.setGetViewCallback(new GetViewListener() {
			@Override
			public void getView(int position, View convertView, ViewGroup parent) {
				// TODO Auto-generated method stub
				if (position + 1 != positionNow) {
					convertView.setBackgroundColor(getResources().getColor(
							R.color.coffice_product_gray));
				} else {
					convertView.setBackgroundColor(getResources().getColor(
							R.color.coffice_product_pink));
				}
			}
		});

		return false;
	}

	/*
	 * 点击动画
	 */
	private boolean ClickAnim(View view) {
		/** 设置缩放动画 */
		ScaleAnimation animation = new ScaleAnimation(0.0f, 1.0f, 0.0f, 1.0f,
				Animation.RELATIVE_TO_SELF, 0.5f, Animation.RELATIVE_TO_SELF,
				0.5f);
		animation.setDuration(200);// 设置动画持续时间
		animation.startNow();
		AnimationSet animationSet = new AnimationSet(true);
		animationSet.addAnimation(animation);
		view.startAnimation(animationSet);
		animationSet.start();
		return false;
	}

	/*
	 * 点击子控件处理
	 */
	IItemClickListener itemClickListener = new IItemClickListener() {

		@Override
		public void ItemOnClick(View v, View parentView, int postion) {
			// TODO Auto-generated method stub
			Toast.makeText(_superContext, "测试", Toast.LENGTH_SHORT).show();
			ClickAnim(v);
		}
	};

	/*
	 * 添加正在刷新视图
	 */
	private void addRefreshingView(int newLayoutId, View oldView) {
		LayoutInflater inflater = (LayoutInflater) _superContext
				.getSystemService(_superContext.LAYOUT_INFLATER_SERVICE);
		FrameLayout lpLayout = ((FrameLayout) oldView.getParent());
		try {
			lpLayout.removeViewAt(lpLayout.indexOfChild(oldView) + 1);// 去除覆盖页面；
			lpLayout.addView(inflater.inflate(newLayoutId, null),
					lpLayout.indexOfChild(oldView) + 1);// 添加覆盖页面;
		} catch (Exception error) {
			try {
				lpLayout.addView(inflater.inflate(newLayoutId, null),
						lpLayout.indexOfChild(oldView) + 1);// 添加覆盖页面；
			} catch (Exception e) {

			}
		}

	}

	/*
	 * 删除正在刷新视图
	 */
	private void removeRefreshingView(int newLayoutId, View oldView) {
		try {
			FrameLayout lpLayout = ((FrameLayout) oldView.getParent());
			lpLayout.removeViewAt(lpLayout.indexOfChild(oldView) + 1);// 去除覆盖页面；
		} catch (Exception e) {

		}
	}

	/*
	 * 获取完数据回调；
	 */
	public interface GetDataFinshListener {
		public void onFinsh(String data);
	}
}