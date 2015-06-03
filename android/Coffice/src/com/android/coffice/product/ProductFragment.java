package com.android.coffice.product;

/**
 * 商品页面；
 * 
 * @author 贾豆
 * 
 */
import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.HashMap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.app.DialogFragment;
import android.app.Fragment;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.view.animation.AnimationSet;
import android.view.animation.ScaleAnimation;
import android.view.animation.TranslateAnimation;
import android.widget.AdapterView;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

import com.android.custom.CustomPropertyPicker;
import com.android.coffice.R;
import com.android.coffice.index.IndexFragment;
import com.android.coffice.order.OrderFragment.GetDataFinshListener;
import com.android.custom.CustomDialog;
import com.android.custom.CustomEditText;
import com.android.custom.CustomNumberPicker;
import com.android.custom.CustomNumberPicker.ValueChangeListener;
import com.android.custom.CustomPropertyPicker.OnSelectedClickListener;
import com.android.hal.cashbox.CashboxHelper;
import com.android.hal.printer.PrinterHelper;
import com.android.hal.printer.PrinterHelper.PrinterFinshListener;
import com.android.listview.ImageListAdapter;
import com.android.listview.ListItem;
import com.android.listview.ImageListAdapter.GetViewListener;
import com.android.listview.ImageListAdapter.IItemClickListener;
import com.android.listview.SuperListView.OnLoadingListener;
import com.android.listview.SuperListView.OnRefreshListener;
import com.android.listview.SuperListView;
import com.android.web.HttpHelp;
import com.android.web.HttpHelp.GetHttpListener;
import com.android.web.IpConfig;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.display.RoundedBitmapDisplayer;

@SuppressLint("ValidFragment")
public class ProductFragment extends Fragment {
	public View _view;
	public String TAG = "ProductFragment";
	public Context _super;
	private DisplayImageOptions _imageLoadingOptions;
	private ArrayList<ListItem> ProductsGroupItem = new ArrayList<ListItem>();
	private ImageListAdapter _productAdapter;
	private ArrayList<ListItem> ClassifiesGroupItem = new ArrayList<ListItem>();
	private ImageListAdapter _classifiesAdapter;
	private ArrayList<ListItem> SelectedsGroupItem = new ArrayList<ListItem>();
	private ListView _selectedsListView;
	private SuperListView _productListView;
	private ListView _classifyListView;
	private ImageListAdapter _selectedsAdapter;
	private ImageLoader _imageLoader;
	private int _layoutType = 0;
	private static int _currSelectedNumber = 1;
	private static String _currSelectedProperty = "";
	private String PRODUCT_ID = "productId";
	private String CLASSIFY_ID = "classId";
	private PrinterHelper _printerHelper;
	private CashboxHelper _cashboxHelper;
	private static HttpHelp _httpHelp;
	private static double _titalPrice=0;
	private static int _classifyId=0;
	private static int _page=1;
	public ProductFragment() {
	}

	public ProductFragment(Context superContext, ImageLoader imageLoader) {
		this._super = superContext;
		this._imageLoader = imageLoader;
		_httpHelp = new HttpHelp();
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		_view = inflater.inflate(R.layout.fragment_product, container, false);
		_httpHelp = new HttpHelp();
		// 使用DisplayImageOptions.Builder()创建DisplayImageOptions
		_imageLoadingOptions = new DisplayImageOptions.Builder()
				.showStubImage(R.drawable.ic_stub) // 设置图片下载期间显示的图片
				.showImageForEmptyUri(R.drawable.ic_empty) // 设置图片Uri为空或是错误的时候显示的图片
				.showImageOnFail(R.drawable.ic_error) // 设置图片加载或解码过程中发生错误显示的图片
				.cacheInMemory(true) // 设置下载的图片是否缓存在内存中
				.cacheOnDisc(true) // 设置下载的图片是否缓存在SD卡中
				.displayer(new RoundedBitmapDisplayer(0)) // 设置成圆角图片
				.build();
		initClassifyList();
		initProductList();
		initSelected();
		initSearch();
		initAddToOrder();
		return _view;
	}

	/*
	 * 初始化菜单；
	 */
	private void initClassifyList() {
		Log.d(TAG, "初始化菜单列表");
		_classifyListView = (ListView) _view
				.findViewById(R.id.classify_listView);
		_classifiesAdapter = new ImageListAdapter(_super, ClassifiesGroupItem,
				_imageLoadingOptions, _imageLoader, ItemChildClick);
		_classifiesAdapter.AddType(R.layout.product_classify_item);
		_classifyListView.setAdapter(_classifiesAdapter);
		GetClassifyAsynTask();
		_classifiesAdapter.notifyDataSetChanged();
		_classifyListView.setOnItemClickListener(onClassifyClickListener);
	}

	/*
	 * 初始化商品
	 */
	private void initProductList() {
		_productListView = (SuperListView) _view.findViewById(R.id.product_listView);
		_productAdapter = new ImageListAdapter(_super, ProductsGroupItem,
				_imageLoadingOptions, _imageLoader, ItemChildClick);
		_productAdapter.AddType(R.layout.product_product_item);
		_productListView.setAdapter(_productAdapter);
		_productAdapter.notifyDataSetChanged();
		_productListView.setOnItemClickListener(onProductClickListener);
		
		_productListView.setonRefreshListener(new OnRefreshListener() {
			@Override
			public void onRefresh() {
//				ProductsGroupItem.clear();
//				_productAdapter.notifyDataSetChanged();
//				GetProductByClassifyAsynTask(1, 10, new GetDataFinshListener() {
//					@Override
//					public void onFinsh(String data) {
//						// TODO Auto-generated method stub
//						ProductsGroupItem.clear();
//						_productListView.onRefreshComplete();
//					}
//				});
				_productListView.onRefreshComplete();
			}
		});
		_productListView.setonLoadingListener(new OnLoadingListener() {
			@Override
			public void onLoading() {
				// TODO Auto-generated method stub
				_page++;
				GetProductByClassifyAsynTask(_page, 10, new GetDataFinshListener() {

					@Override
					public void onFinsh(String data) {
						// TODO Auto-generated method stub
						_productListView.onLoadingComplete();
					}
				});
			}
		});
	}

	/*
	 * 根据选择获取商品列表;
	 */
	private void initSelected() {
		Log.d(TAG, "初始化选择商品列表");
		_selectedsListView = (ListView) _view
				.findViewById(R.id.selected_listView);
		_selectedsAdapter = new ImageListAdapter(_super, SelectedsGroupItem,
				_imageLoadingOptions, _imageLoader, ItemChildClick);
		_selectedsAdapter.AddType(R.layout.product_selected_item);
		_selectedsListView.setAdapter(_selectedsAdapter);
		refreshSelectedList();
		_selectedsListView.setOnItemClickListener(onSelectedClickListener);
	}

	/*
	 * 初始化搜索
	 */
	private void initSearch() {
		ImageView searchImageButton = (ImageView) _view
				.findViewById(R.id.product_seach_id);
		final CustomEditText customEditText = (CustomEditText) _view
				.findViewById(R.id.product_search_string_id);
		searchImageButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				String editText = customEditText.getText().toString().trim();
				ClickAnim(v);
//				Toast.makeText(_super, "关键字：" + editText, Toast.LENGTH_LONG)
//						.show();
				ProductsGroupItem.clear();
				addRefreshingView(_productListView);
				GetSeachProductAsynTask(editText);
				_productAdapter.notifyDataSetChanged();
			}
		});
	}

	/*
	 * 初始化结算
	 */
	private void initAddToOrder() {
		ImageView buyButtonImageView = (ImageView) _view
				.findViewById(R.id.product_buy_id);
		buyButtonImageView.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				ClickAnim(v);
				showBuyWindow();// 结算窗口；
			}
		});
	}

	/*
	 * 获取分类哈希列表；
	 */
	private HashMap<Object, Object> getClassifyHashMap(String classifyName,
			int id) {
		Log.d(TAG, "创建：" + classifyName);
		HashMap<Object, Object> map1 = new HashMap<Object, Object>();
		map1.put(R.id.classify_name, classifyName);
		map1.put(CLASSIFY_ID, id);
		return map1;
	}

	/*
	 * 获取商品的哈希列表；
	 */
	private HashMap<Object, Object> getProductHashmap(String image,
			String name, String detail, Double price, int id, int imageId) {
		Log.d(TAG, "获取商品列表:" + name);
		HashMap<Object, Object> map = new HashMap<Object, Object>();
		map.put(R.id.product_image_id, image);
		map.put(R.id.product_name_id, name);
		map.put(R.id.product_detail_id, detail);
		map.put(R.id.product_price_id, price);
		map.put(R.id.product_add_id, imageId);
		map.put(PRODUCT_ID, id);
		return map;
	}

	/*
	 * 获取选择商品列表；
	 */
	private ListItem getSelectedListItem(ListItem selectedMap) {
		HashMap<Object, Object> map = new HashMap<Object, Object>();
		map.put(R.id.selected_name_id,
				selectedMap.mMap.get(R.id.product_name_id));
		map.put(R.id.selected_number_id, _currSelectedNumber);
		map.put(R.id.selected_property_id, _currSelectedProperty);
		map.put(R.id.selected_price_id,((Double)selectedMap.mMap.get(R.id.product_price_id)*_currSelectedNumber));
		map.put(R.id.selected_delete_id, R.drawable.product_selected_del_button);
		map.put(PRODUCT_ID, selectedMap.mMap.get(PRODUCT_ID));
		return new ListItem(_layoutType, map);
	}

	/*
	 * 异步任务获取商品列表数据源
	 */
	private void GetProductByClassifyAsynTask(int page,int pageCount,final GetDataFinshListener getDataFinshListener) {
		
		_httpHelp.startGetAsnyTask(IpConfig.hostIpString+IpConfig.getProductByCondition+"?categoryId="+_classifyId+"&page="+page+"&pagecount="+pageCount,
				new GetHttpListener() {
					@SuppressWarnings("unchecked")
					@Override
					public void getHttpResult(String res) {
						// TODO Auto-generated method stub
						ArrayList<ListItem> groupItemBuffer = new ArrayList<ListItem>();
						try {
							JSONArray jsonArray = new JSONArray(res);
							for (int i = 0; i < jsonArray.length(); i++) {
								JSONObject jsonObject = jsonArray
										.getJSONObject(i);
								String Image = jsonObject.getString("Image");
								String Name = jsonObject.getString("Name");
								double Price = jsonObject.getDouble("Price");
								int Id=jsonObject.getInt("Id");
								ProductsGroupItem
								.add(new ListItem(
										_layoutType,
										getProductHashmap(
												Image,
												Name,
												"￥",
												Price,
												Id,
												R.drawable.product_product_add_button)));
						removeRefreshingView(_productListView);
							}
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						_productAdapter.notifyDataSetChanged();
						if(getDataFinshListener!=null){
							getDataFinshListener.onFinsh(res);
						}
						
					}

				});

	}

	/*
	 * 异步任务获取商品列表数据源
	 */
	private void GetSeachProductAsynTask(final String keyWord) {
		ProductsGroupItem.clear();
		_httpHelp.startGetAsnyTask(IpConfig.hostIpString+IpConfig.getProductByCondition+"?name="+keyWord+"&page="+1+"&pagecount="+100,
				new GetHttpListener() {
					@SuppressWarnings("unchecked")
					@Override
					public void getHttpResult(String res) {
						// TODO Auto-generated method stub
						ArrayList<ListItem> groupItemBuffer = new ArrayList<ListItem>();
						ProductsGroupItem.clear();
						try {
							JSONArray jsonArray = new JSONArray(res);
							for (int i = 0; i < jsonArray.length(); i++) {
								JSONObject jsonObject = jsonArray
										.getJSONObject(i);
								String Image = jsonObject.getString("Image");
								String Name = jsonObject.getString("Name");
								double Price = jsonObject.getDouble("Price");
								int Id=jsonObject.getInt("Id");
								ProductsGroupItem
								.add(new ListItem(
										_layoutType,
										getProductHashmap(
												Image,
												Name,
												"￥",
												Price,
												Id,
												R.drawable.product_product_add_button)));
						removeRefreshingView(_productListView);
							}
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
						_productAdapter.notifyDataSetChanged();
					}

				});

	}
	
	/*
	 * 异步任务获取商品列表数据源
	 */
	private void GetClassifyAsynTask() {
		ClassifiesGroupItem.clear();
		_httpHelp.startGetAsnyTask(
				IpConfig.hostIpString + IpConfig.getCategory,
				new GetHttpListener() {
					@SuppressWarnings("unchecked")
					@Override
					public void getHttpResult(String res) {
						_classifiesAdapter.notifyDataSetChanged();
						try {
							JSONArray jsonArray = new JSONArray(res);
							for (int i = 0; i < jsonArray.length(); i++) {
								JSONObject jsonObject = jsonArray
										.getJSONObject(i);
								int id = jsonObject.getInt("Id");
								String name = jsonObject
										.getString("CategoryName");
								ClassifiesGroupItem.add(new ListItem(
										_layoutType, getClassifyHashMap(name,
												id)));
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
	 * 点击分类事件；
	 */
	private OnItemClickListener onClassifyClickListener = new OnItemClickListener() {
		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position,
				long id) {

			ClickAnim(view);
			selectedClassify(parent, view, position);
			try {
				ProductsGroupItem.clear();
				addRefreshingView(_productListView);
				ListItem LI = ClassifiesGroupItem.get(position);
				_productAdapter.notifyDataSetChanged();
				_classifyId=(Integer) LI.mMap.get(CLASSIFY_ID);
				GetProductByClassifyAsynTask(1,10,null);
			} catch (Exception e) {

			}
		}
	};

	/*
	 * 点击商品事件；
	 */
	private OnItemClickListener onProductClickListener = new OnItemClickListener() {
		@Override
		public void onItemClick(AdapterView<?> parent, View view,
				final int position, long id) {
			Toast.makeText(_super, "点击了：" + position, Toast.LENGTH_LONG).show();
			ClickAnim(view);
		}
	};

	/*
	 * 点击标记分类
	 */
	private void selectedClassify(AdapterView<?> parent, View view,
			final int positionNow) {
		ResetViewStyle(parent, R.color.coffice_product_gray);
		view.setBackgroundColor(getResources().getColor(
				R.color.coffice_product_pink));
		// 此处消除listview其他view点击印记，及保持点击处印记；
		_classifiesAdapter.setGetViewCallback(new GetViewListener() {
			@Override
			public void getView(int position, View convertView, ViewGroup parent) {
				// TODO Auto-generated method stub
				if (position != positionNow) {
					convertView.setBackgroundColor(getResources().getColor(
							R.color.coffice_product_gray));
				} else {
					convertView.setBackgroundColor(getResources().getColor(
							R.color.coffice_product_pink));
				}
			}
		});
	}

	/*
	 * 点击已选择商品事件；
	 */
	private OnItemClickListener onSelectedClickListener = new OnItemClickListener() {
		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position,
				long id) {
			ClickAnim(view);
		}
	};

	/*
	 * 点击回调函数
	 */
	private IItemClickListener ItemChildClick = new IItemClickListener() {
		@Override
		public void ItemOnClick(View view, View parentView, int position) {
			// TODO Auto-generated method stub
			ClickAnim(view);
			switch (view.getId()) {
			case R.id.product_add_id:// 点击商品添加 ；
				showDatePropertySelecter(parentView, position);
				break;
			case R.id.product_image_id:// 点击商品图片；
				Toast.makeText(_super, "图片点击:" + position, Toast.LENGTH_SHORT)
						.show();
				break;
			case R.id.selected_delete_id:// 点击删除已选商品；
				Toast.makeText(_super, "删除点击:" + position, Toast.LENGTH_SHORT)
						.show();
				MoveDelAnim(parentView, position);
				break;
			default:
				Toast.makeText(_super, "未检测到：" + position, Toast.LENGTH_SHORT)
						.show();
				break;
			}

		}
	};

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
	 * 产品选择添加
	 */
	private boolean ProductAdd(final View view, final int position) {
		/** 设置动画 */
		ListItem selectedMap = ProductsGroupItem.get(position);
		SelectedsGroupItem.add(getSelectedListItem(selectedMap));
		refreshSelectedList();// 刷新选择列表；
		_selectedsListView
				.smoothScrollToPosition(SelectedsGroupItem.size() - 1);
		Toast.makeText(
				_super,
				"添加:" + position + " 数量：" + _currSelectedNumber + " 属性："
						+ _currSelectedProperty + "ID: "
						+ ProductsGroupItem.get(position).mMap.get(PRODUCT_ID),
				Toast.LENGTH_SHORT).show();
		return false;
	}

	/*
	 * 横移添加动画
	 */
	private boolean MoveAddAnim(View view)// Icon移动；
	{
		/** 设置缩放动画 */

		TranslateAnimation animation = new TranslateAnimation(-view.getWidth(),
				0, 0, 0);
		animation.setDuration(500);// 设置动画持续时间
		animation.startNow();
		AnimationSet animationSet = new AnimationSet(true);
		animationSet.addAnimation(animation);
		view.startAnimation(animationSet);
		animationSet.start();
		return false;
	}

	/*
	 * 横移删除动画
	 */
	private boolean MoveDelAnim(View view, final int position) {
		/** 设置缩放动画 */

		TranslateAnimation animation = new TranslateAnimation(0,
				view.getWidth(), 0, 0);
		animation.setDuration(200);// 设置动画持续时间
		animation.startNow();
		AnimationSet animationSet = new AnimationSet(true);
		animationSet.addAnimation(animation);
		view.startAnimation(animationSet);
		animationSet.setAnimationListener(new AnimationListener() {

			@Override
			public void onAnimationStart(Animation animation) {
				// TODO Auto-generated method stub

			}

			@Override
			public void onAnimationRepeat(Animation animation) {
				// TODO Auto-generated method stub

			}

			@Override
			public void onAnimationEnd(Animation animation) {
				// TODO Auto-generated method stub
				SelectedsGroupItem.remove(position);
				refreshSelectedList();
			}
		});
		animationSet.start();
		return false;
	}

	/*
	 * 弹出结算窗口
	 */
	private void showBuyWindow() {
		_currSelectedNumber = 1;
		CustomNumberPicker customNumberPick = new CustomNumberPicker(_super);
		customNumberPick.setInitValue(1, 1, 100);// 设置初始值、最小值、最大值；
		customNumberPick.SetOnchangeCallback(new ValueChangeListener() {// 回调函数处理
					@Override
					public void onChange(int value) {
						// TODO Auto-generated method stub
						_currSelectedNumber = value;
					}
				});
		LayoutInflater inflater = (LayoutInflater) _super
				.getSystemService(_super.LAYOUT_INFLATER_SERVICE);
		View buyView = inflater.inflate(R.layout.product_buy_window, null);
		CustomDialog.Builder customBuilder = new CustomDialog.Builder(_super);
		customBuilder.setTitle("选择支付方式").setMessage(null)
				.setContentView(buyView);
		final Dialog dialog = customBuilder.create();
		dialog.show();
		TextView cashTextView = (TextView) buyView
				.findViewById(R.id.product_buy_cash_id);
		TextView otherTextView = (TextView) buyView
				.findViewById(R.id.product_buy_other_id);
		cashTextView.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				ClickAnim(v);
				dialog.dismiss();
				TradingFragment tradingFragment = new TradingFragment(_super,
						_imageLoader);
				Bundle bundle = new Bundle();  
				ArrayList list = new ArrayList(); //这个list用于在budnle中传递 需要传递的ArrayList<Object>
				list.add(SelectedsGroupItem);
				bundle.putParcelableArrayList("SelectedsGroupItem", list);  
				bundle.putDouble("TitalPrice", _titalPrice);
				tradingFragment.setArguments(bundle);  
				getFragmentManager().beginTransaction()
						.replace(R.id.fragment_content, tradingFragment)
						.commit();
			}
		});
		otherTextView.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				ClickAnim(v);
				dialog.dismiss();
			}
		});
	}

	/**
	 * 弹出数字选择器
	 */
	private void showDateNumberPicker(final View parentView, final int position) {
		_currSelectedNumber = 1;
		CustomNumberPicker customNumberPick = new CustomNumberPicker(_super);
		customNumberPick.setInitValue(1, 1, 100);// 设置初始值、最小值、最大值；
		customNumberPick.SetOnchangeCallback(new ValueChangeListener() {// 回调函数处理
					@Override
					public void onChange(int value) {
						// TODO Auto-generated method stub
						_currSelectedNumber = value;
					}
				});

		CustomDialog.Builder customBuilder = new CustomDialog.Builder(_super);
		customBuilder.setTitle("选择数量").setMessage(null)
				.setContentView(customNumberPick)
				.setNegativeButton("取消", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						dialog.dismiss();
					}
				})
				.setPositiveButton("确定", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						dialog.dismiss();
						ProductAdd(parentView, position);

					}
				});
		customBuilder.create().show();
	}

	/*
	 * 弹出属性选择器
	 */
	private void showDatePropertySelecter(final View parentView,
			final int position) {
		_currSelectedNumber = 1;
		CustomPropertyPicker customPropertyPicker = new CustomPropertyPicker(
				_super);
		CustomDialog.Builder customBuilder = new CustomDialog.Builder(_super);
		customBuilder.setTitle("选择属性").setMessage(null)
				.setContentView(customPropertyPicker);
		final Dialog dialog = customBuilder.create();
		dialog.show();
		customPropertyPicker
				.setSelectedClickCallback(new OnSelectedClickListener() {
					@Override
					public void onSelectedClick(String name, int id, View view) {
						// TODO Auto-generated method stub
						_currSelectedProperty = name;
						ClickAnim(view);
						dialog.dismiss();
						showDateNumberPicker(parentView, position);
					}
				});
	}

	/*
	 * 刷新商品选择列表；
	 */
	private void refreshSelectedList() {
		_selectedsAdapter.notifyDataSetChanged();
		// if (SelectedsGroupItem.size() > 0) {
		// MoveAddAnim(getViewByPosition(SelectedsGroupItem.size() - 1,
		// _selectedsListView));// 横移添加动画
		// }
		TextView amountView = (TextView) _view
				.findViewById(R.id.product_amount_id);
		TextView countView = (TextView) _view
				.findViewById(R.id.product_count_id);
		Double amountDouble = 0.0;
		int countAmount = 0;
		for (ListItem listItem : SelectedsGroupItem) {
			Double price = (Double) listItem.mMap.get(R.id.selected_price_id);
			int count = (Integer) listItem.mMap.get(R.id.selected_number_id);
			amountDouble = amountDouble + price ;
			countAmount = countAmount + count;
		}
		DecimalFormat df = new DecimalFormat("0.0");
		_titalPrice=amountDouble;
		amountView.setText(df.format(amountDouble));
		countView.setText(String.valueOf(countAmount));
	}

	/*
	 * 获取listview中item；
	 */
	private View getViewByPosition(int pos, ListView listView) {
		final int firstListItemPosition = listView.getFirstVisiblePosition();
		final int lastListItemPosition = firstListItemPosition
				+ listView.getChildCount() - 1;

		if (pos < firstListItemPosition || pos > lastListItemPosition) {
			return listView.getAdapter().getView(pos, null, listView);
		} else {
			final int childIndex = pos - firstListItemPosition;
			return listView.getChildAt(childIndex);
		}
	}

	/*
	 * 重置样式；
	 */
	private void ResetViewStyle(AdapterView<?> parent, int color) {
		// TODO Auto-generated method stub
		for (int i = 0; i < parent.getChildCount(); i++) {
			View v = parent.getChildAt(i);
			v.setBackgroundColor(getResources().getColor(color));
		}
	}

	/*
	 * 添加正在刷新视图
	 */
	private void addRefreshingView(View oldView) {
		LayoutInflater inflater = (LayoutInflater) _super
				.getSystemService(_super.LAYOUT_INFLATER_SERVICE);
		FrameLayout lpLayout = ((FrameLayout) oldView.getParent());
		try {
			lpLayout.removeViewAt(lpLayout.indexOfChild(oldView) + 1);// 去除覆盖页面；
			lpLayout.addView(inflater.inflate(R.layout.refreshing, null),
					lpLayout.indexOfChild(oldView) + 1);// 添加覆盖页面；

		} catch (Exception error1) {
			try {
				lpLayout.addView(inflater.inflate(R.layout.refreshing, null),
						lpLayout.indexOfChild(oldView) + 1);// 添加覆盖页面；
			} catch (Exception error2) {
			}
		}
	}

	/*
	 * 删除正在刷新视图
	 */
	private void removeRefreshingView(View oldView) {
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