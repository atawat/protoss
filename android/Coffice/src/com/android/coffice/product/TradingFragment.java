package com.android.coffice.product;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.annotation.SuppressLint;
import android.app.Fragment;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
import android.text.format.Time;
import android.util.JsonReader;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationSet;
import android.view.animation.ScaleAnimation;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.bean.Order;
import com.android.bean.OrderDetail;
import com.android.bean.PrintModel;
import com.android.coffice.R;
import com.android.hal.cashbox.CashboxHelper;
import com.android.hal.cashbox.CashboxHelper.OperatFinshListener;
import com.android.hal.printer.PrinterHelper;
import com.android.hal.printer.PrinterHelper.PrinterFinshListener;
import com.android.listview.ListItem;
import com.android.queuesTask.Queues;
import com.android.web.DataPair;
import com.android.web.HttpHelp;
import com.android.web.IpConfig;
import com.android.web.HttpHelp.GetHttpListener;
import com.fasterxml.jackson.core.JsonEncoding;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.nostra13.universalimageloader.core.ImageLoader;

@SuppressLint({ "NewApi", "ValidFragment" })
public class TradingFragment extends Fragment {
	private View _view;
	private Context _super;
	private ImageLoader _imageLoader;
	private ImageView _settingButton;
	private TextView _msg;
	private TextView _openCashButton;
	private TextView _printerButton;
	private TextView _overButton;
	private CashboxHelper _cashboxHelper;
	private ArrayList<ListItem> _selectedsGroupItem;
	private double _titalPrice=0;
	private static HttpHelp _httpHelp;
	private static String TAG = "TradingFragment";
	private String PRODUCT_ID = "productId";
	private static String reString = "false";
	private static boolean isCreate = false;
	Handler handler = new Handler();

	public TradingFragment(Context superContext, ImageLoader imageLoader) {
		this._super = superContext;
		this._imageLoader = imageLoader;
		_cashboxHelper = new CashboxHelper(_super, new OperatFinshListener() {
			@Override
			public void operateFinshed() {
				// TODO Auto-generated method stub
				if (_cashboxHelper._isOpen) {
					_msg.setText("钱箱已打开！");
					_openCashButton.setText("关闭钱箱");
				} else {
					_msg.setText("钱箱已关闭！");
					_openCashButton.setText("打开钱箱");
				}

			}
		});
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		_view = inflater.inflate(R.layout.fragment_trading, container, false);
		isCreate = false;
		initConfig();
		getParament();
		this._msg = (TextView) _view.findViewById(R.id.product_trading_msg_id);
		this._openCashButton = (TextView) _view
				.findViewById(R.id.product_trading_opencash_id);
		this._printerButton = (TextView) _view
				.findViewById(R.id.product_trading_print_id);
		this._overButton = (TextView) _view
				.findViewById(R.id.product_trading_over_id);

		this._openCashButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				ClickAnim(v);
				_msg.setText("正在操作钱箱……");
				if (_cashboxHelper._isOpen) {
					_cashboxHelper.operateCashbox(false);
				} else {
					_cashboxHelper.operateCashbox(true);
				}

			}
		});
		this._printerButton.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				ClickAnim(v);
				CreateOrder();

			}
		});
		this._overButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				ClickAnim(v);
				ProductFragment productFragment = new ProductFragment(_super,
						_imageLoader);
				getFragmentManager().beginTransaction()
						.replace(R.id.fragment_content, productFragment)
						.commit();
			}
		});
		return _view;
	}

	/*
	 * 获取传递的参数；
	 */
	private void getParament() {
		// TODO Auto-generated method stub
		_httpHelp = new HttpHelp();
		ArrayList list = getArguments().getParcelableArrayList(
				"SelectedsGroupItem");
		double TitalPrice = getArguments().getDouble(
				"TitalPrice");
		_selectedsGroupItem = (ArrayList<ListItem>) list.get(0);
		_titalPrice=TitalPrice;
	}

	/*
	 * 初始化参数
	 */
	private void initConfig() {
		Time time = new Time("GMT+8");
		time.setToNow();
		if (ReadConfig("Time") != time.monthDay) {
			SaveConfig("Time", time.monthDay);
			SaveConfig("OrderCode", 0);
		}
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
	 * 读取配置文件
	 */
	private int ReadConfig(String configName) {
		SharedPreferences sharedata = _super.getSharedPreferences("Setting", 0);
		return sharedata.getInt(configName, -1);
	}

	/*
	 * 存储配置文
	 */
	public void SaveConfig(String parmeter, int value) {
		SharedPreferences.Editor sharedata = _super.getSharedPreferences(
				"Setting", 0).edit();
		sharedata.putInt(parmeter, value);
		sharedata.commit();
	}

	/*
	 * 下单
	 */
	@SuppressLint("ResourceAsColor")
	private void CreateOrder() {
		if (!isCreate) {
			if (_selectedsGroupItem.size() >= 1) {
				_msg.setText("正在创建订单……");
				_httpHelp.startPostStringAsnyTask(getOrderJson(),
						IpConfig.hostIpString + IpConfig.CreateOrder,
						new GetHttpListener() {
							@Override
							public void getHttpResult(String res) {
								// TODO Auto-generated method stub
								reString = res;
								isCreate = true;
								if (reString != null) {
									_msg.setText("创建订单成功，请勿反复操作！");
									_printerButton
											.setBackgroundColor(R.color.mediumgray_0);
									if (isCreate) {// 如果创建订单成功，则：
										_msg.setText("已加入打印队列，请稍后");
										try{
											addToPrintQueues(reString);
										}catch(Exception e){
											
										}
									}
								} else {
									_msg.setText("创建订单失败，清重试！");
								}
							}
						});
			} else {
				_msg.setText("您并未选择任何商品，请选择后再提交订单！");
			}

		} else {
			_msg.setText("您已经创建此订单，请勿重复！");
		}
	}

	/*
	 * 获取order表json；
	 */
	private String getOrderJson() {
		// TODO Auto-generated method stub
		String jsonresult = "";// 定义返回字符串

		Order order = new Order();
		order.setCounponNum("无");
		order.setDeliveryAddress("无");

		order.setDiscount(0);
		order.setLocationX(0);
		order.setLocationY(0);
		order.setPayType(1);
		order.setPhoneNumber("无");
		order.setType(1);
		
		List<OrderDetail> odlist = new ArrayList<OrderDetail>();
		for (int i = 0; i < _selectedsGroupItem.size(); i++) {

			OrderDetail oDetail = new OrderDetail();
			oDetail.setCount((Integer) _selectedsGroupItem.get(i).mMap
					.get(R.id.selected_number_id));
			oDetail.setProductId((Integer) _selectedsGroupItem.get(i).mMap
					.get(PRODUCT_ID));
			oDetail.setRemark((String) _selectedsGroupItem.get(i).mMap
					.get(R.id.selected_property_id));
			odlist.add(oDetail);
		}
		order.setTitalPrice(_titalPrice);
		order.setDetails(odlist);
		try {
			ObjectMapper mapper = new ObjectMapper();
			jsonresult = mapper.writeValueAsString(order);
			Log.v(TAG, jsonresult);
		} catch (IOException e) {
			e.printStackTrace();
		}
		return jsonresult;
	}

	/*
	 * 获取order表json；
	 */
	private Order getOrderModel() {

		Order order = new Order();
		order.setCounponNum("无");
		order.setDeliveryAddress("无");

		order.setDiscount(0);
		order.setLocationX(0);
		order.setLocationY(0);
		order.setPayType(1);
		order.setPhoneNumber("无");
		order.setType(1);
		List<OrderDetail> odlist = new ArrayList<OrderDetail>();
		for (int i = 0; i < _selectedsGroupItem.size(); i++) {

			OrderDetail oDetail = new OrderDetail();
			oDetail.setCount((Integer) _selectedsGroupItem.get(i).mMap
					.get(R.id.selected_number_id));
			oDetail.setProductId((Integer) _selectedsGroupItem.get(i).mMap
					.get(PRODUCT_ID));
			oDetail.setRemark((String) _selectedsGroupItem.get(i).mMap
					.get(R.id.selected_property_id));
			odlist.add(oDetail);
		}
		order.setDetails(odlist);
		return order;
	}

	/*
	 * 获取当前用户位置
	 */
	private void getLocation() {

	}

	/*
	 * 加入打印队列
	 */
	private void addToPrintQueues(String res) throws JSONException {
		// TODO Auto-generated method stub
		JSONObject jsonObject = new JSONObject(res);
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
		Queues.Task task = new Queues.Task(order);
		Queues.add(task);
	}
}
