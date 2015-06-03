package com.android.listview;

import java.util.Date;

import com.android.coffice.R;

import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.LinearInterpolator;
import android.view.animation.RotateAnimation;
import android.widget.AbsListView;
import android.widget.AbsListView.OnScrollListener;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;

/**
 * 订单定制listView；
 * 
 * @author 贾豆
 * 
 */
public class SuperListView extends ListView implements OnScrollListener {

	private final static int HEADER_RELEASE_To_REFRESH = 0;// 下拉过程的状态值
	private final static int HEADER_PULL_To_REFRESH = 1; // 从下拉返回到不刷新的状态值
	private final static int HEADER_REFRESHING = 2;// 正在刷新的状态值
	private final static int HEADER_DONE = 3;// 更新完成；
	private final static int HEADER_LOADING = 4;// 正在刷新；
	
	private final static int FOOTER_RELEASE_To_REFRESH = 0;// 下拉过程的状态值
	private final static int FOOTER_PULL_To_REFRESH = 1; // 从下拉返回到不刷新的状态值
	private final static int FOOTER_REFRESHING = 2;// 正在刷新的状态值
	private final static int FOOTER_DONE = 3;// 更新完成；
	private final static int FOOTER_LOADING = 4;// 正在刷新；

	// 实际的padding的距离与界面上偏移距离的比例
	private final static int RATIO = 3;
	private LayoutInflater inflater;

	// ListView低部上拉载入更多的布局
	private LinearLayout footerView;// 刷新头部布局视图；
	private TextView lvFooterTipsTv;// 头部视图提示；
	private ProgressBar lvFooterProgressBar;// 头部视图更新进度条；
	private int footerContentHeight;// 定义头部下拉刷新的布局的高度
	
	// ListView头部下拉刷新的布局
	private LinearLayout headerView;// 刷新头部布局视图；
	private TextView lvHeaderTipsTv;// 头部视图提示；
	private TextView lvHeaderLastUpdatedTv;// 头部视图更新信息提示；
	private ImageView lvHeaderArrowIv;// 头部视图图片；
	private ProgressBar lvHeaderProgressBar;// 头部视图更新进度条；
	private int headerContentHeight;// 定义头部下拉刷新的布局的高度
	private RotateAnimation animation;// 旋转动画；
	private RotateAnimation reverseAnimation;

	private int startY;// 起始滑动Y坐标；
	private int headerState;// 滑动状态；
	private int footerState;// 滑动状态；
	private boolean isBack;
	private boolean isRecored;// 用于保证startY的值在一个完整的touch事件中只被记录一次

	private OnRefreshListener refreshListener;// 回调；
	private OnLoadingListener loadingListener;// 回调；
	private boolean isRefreshable;// 是否在刷新；
	private boolean isLoadingable;// 是否在载入更过；
	private int icon_id;
	private int _totalItemCount=0;

	/*
	 * 构造函数
	 */
	public SuperListView(Context context) {
		super(context);
		init(context);
	}

	/*
	 * 构造函数
	 */
	public SuperListView(Context context, AttributeSet attrs) {
		super(context, attrs);
		init(context);
	}

	/*
	 * 设置图片
	 */
	public void setIcon(int resId) {
		this.icon_id = resId;
	}

	/*
	 * 初始化listview
	 */
	private void init(Context context) {
		setCacheColorHint(context.getResources().getColor(R.color.transparent));
		inflater = LayoutInflater.from(context);
		headerView = (LinearLayout) inflater.inflate(R.layout.listview_header,
				null);
		footerView = (LinearLayout) inflater.inflate(R.layout.listview_footer,
				null);
		
		lvHeaderTipsTv = (TextView) headerView
				.findViewById(R.id.lvHeaderTipsTv);
		lvHeaderLastUpdatedTv = (TextView) headerView
				.findViewById(R.id.lvHeaderLastUpdatedTv);

		lvHeaderArrowIv = (ImageView) headerView
				.findViewById(R.id.lvHeaderArrowIv);
		// 设置下拉刷新图标的最小高度和宽度
		lvHeaderArrowIv.setMinimumWidth(70);
		lvHeaderArrowIv.setMinimumHeight(50);

		lvHeaderProgressBar = (ProgressBar) headerView
				.findViewById(R.id.lvHeaderProgressBar);
		lvFooterProgressBar = (ProgressBar) footerView
				.findViewById(R.id.lvFootererProgressBar);
		lvFooterTipsTv = (TextView) footerView
				.findViewById(R.id.lvFooterTipsTv);
		// 设置内边距，正好距离顶部为一个负的整个布局的高度，正好隐藏
		measureView(headerView);
		measureView(footerView);
		headerContentHeight = headerView.getMeasuredHeight();
		headerView.setPadding(0, -1 * headerContentHeight, 0, 0);
		footerContentHeight = footerView.getMeasuredHeight();
		footerView.setPadding(0, 0, 0, -1 * footerContentHeight);
		
		headerView.invalidate();// 重绘一下
		footerView.invalidate();
		
		addHeaderView(headerView, null, false);// 将下拉刷新的布局加入ListView的顶部
		addFooterView(footerView, null, false);//将载入更多加入布局；
		
		setOnScrollListener(this);// 设置滚动监听事件

		// 设置旋转动画事件
		animation = new RotateAnimation(0, -180,
				RotateAnimation.RELATIVE_TO_SELF, 0.5f,
				RotateAnimation.RELATIVE_TO_SELF, 0.5f);
		animation.setInterpolator(new LinearInterpolator());
		animation.setDuration(250);
		animation.setFillAfter(true);

		reverseAnimation = new RotateAnimation(-180, 0,
				RotateAnimation.RELATIVE_TO_SELF, 0.5f,
				RotateAnimation.RELATIVE_TO_SELF, 0.5f);
		reverseAnimation.setInterpolator(new LinearInterpolator());
		reverseAnimation.setDuration(200);
		reverseAnimation.setFillAfter(true);

		// 一开始的状态就是下拉刷新完的状态，所以为DONE
		headerState = HEADER_DONE;
		// 是否正在刷新
		isRefreshable = false;
	}

	/*
	 * 滑动状态改变；
	 */
	@Override
	public void onScrollStateChanged(AbsListView view, int scrollState) {

	}

	/*
	 * 正在滑动
	 */
	@Override
	public void onScroll(AbsListView view, int firstVisibleItem,
			int visibleItemCount, int totalItemCount) {
		if (firstVisibleItem == 0) {
			isRefreshable = true;
		} else {
			isRefreshable = false;
		}
		
		 if(visibleItemCount+firstVisibleItem==totalItemCount){
             Log.e("log", "滑到底部");
             isLoadingable=true;
         }else{
        	 isLoadingable=false;
         }
		 this._totalItemCount=totalItemCount;
	}

	/*
	 * 触摸事件
	 */
	@Override
	public boolean onTouchEvent(MotionEvent ev) {
		onDownmove(ev);
		if(!isRefreshable){//如果不下拉监听；
			onUpmove(ev);
		}
		
		return super.onTouchEvent(ev);
	}
	
	/*
	 * 上拉拉手指监听
	 */
	public boolean onUpmove(MotionEvent ev){
		if(isLoadingable){
			switch (ev.getAction()) {
			case MotionEvent.ACTION_DOWN:
				if (!isRecored) {
					isRecored = true;
					startY = (int) ev.getY();// 手指按下时记录当前位置
				}
				break;
			case MotionEvent.ACTION_UP:
				if (footerState != FOOTER_REFRESHING && footerState != FOOTER_LOADING) {
					if (footerState == FOOTER_PULL_To_REFRESH) {
						footerState = FOOTER_DONE;
						changeFooterViewByState();
					}
					if (footerState == FOOTER_RELEASE_To_REFRESH) {
						footerState = FOOTER_REFRESHING;
						changeFooterViewByState();
						onLvLoading();
					}
				}
				isRecored = false;
				isBack = false;

				break;

			case MotionEvent.ACTION_MOVE:
				int tempY = (int) ev.getY();
				if (!isRecored) {
					isRecored = true;
					startY = tempY;
				}
				if (footerState != FOOTER_REFRESHING && isRecored && footerState != FOOTER_LOADING) {
					if (footerState == FOOTER_RELEASE_To_REFRESH) {// 保证在设置padding的过程中，当前的位置一直是在head，否则如果当列表超出屏幕的话，当在上推的时候，列表会同时进行滚动
						setSelection(_totalItemCount);
						// 往上推了，推到了屏幕足够掩盖head的程度，但是还没有推到全部掩盖的地步
						if (((startY - tempY) / RATIO < footerContentHeight)// 由松开刷新状态转变到下拉刷新状态
								&& (startY - tempY) > 0) {
							footerState = FOOTER_PULL_To_REFRESH;
							changeFooterViewByState();
						}
						// 一下子推到顶了
						else if (startY - tempY <= 0) {// 由松开刷新状态转变到done状态
							footerState = FOOTER_DONE;
							changeFooterViewByState();
						}
					}
					if (footerState == FOOTER_PULL_To_REFRESH) {// 还没有到达显示松开刷新的时候,DONE或者是PULL_To_REFRESH状态
						setSelection(_totalItemCount);
						// 下拉到可以进入RELEASE_TO_REFRESH的状态
						if ((startY - tempY) / RATIO >= footerContentHeight) {// 由done或者下拉刷新状态转变到松开刷新
							footerState = FOOTER_RELEASE_To_REFRESH;
							isBack = true;
							changeFooterViewByState();
						}
						// 上推到顶了
						else if (startY - tempY <= 0) {// 由DOne或者下拉刷新状态转变到done状态
							footerState = FOOTER_DONE;
							changeFooterViewByState();
						}
					}
					if (footerState == FOOTER_DONE) {// done状态下
						if (startY - tempY > 0) {
							footerState = FOOTER_PULL_To_REFRESH;
							changeFooterViewByState();
						}
					}
					if (footerState == FOOTER_PULL_To_REFRESH) {// 更新headView的size
						footerView.setPadding(0, -1 * footerContentHeight
								+ (startY - tempY) / RATIO, 0, 0);
					}
					if (footerState == FOOTER_RELEASE_To_REFRESH) {// 更新headView的paddingTop
						footerView.setPadding(0, (startY - tempY) / RATIO
								- footerContentHeight, 0, 0);
					}

				}
				break;

			default:
				break;
			}
		}
		return true;
	}
	
	/*
	 * 下拉手指监听
	 */
	public boolean onDownmove(MotionEvent ev){
		if (isRefreshable) {
			switch (ev.getAction()) {
			case MotionEvent.ACTION_DOWN:
				if (!isRecored) {
					isRecored = true;
					startY = (int) ev.getY();// 手指按下时记录当前位置
				}
				break;
			case MotionEvent.ACTION_UP:
				if (headerState != HEADER_REFRESHING && headerState != HEADER_LOADING) {
					if (headerState == HEADER_PULL_To_REFRESH) {
						headerState = HEADER_DONE;
						changeHeaderViewByState();
					}
					if (headerState == HEADER_RELEASE_To_REFRESH) {
						headerState = HEADER_REFRESHING;
						changeHeaderViewByState();
						onLvRefresh();
					}
				}
				isRecored = false;
				isBack = false;

				break;

			case MotionEvent.ACTION_MOVE:
				int tempY = (int) ev.getY();
				if (!isRecored) {
					isRecored = true;
					startY = tempY;
				}
				if (headerState != HEADER_REFRESHING && isRecored && headerState != HEADER_LOADING) {
					if (headerState == HEADER_RELEASE_To_REFRESH) {// 保证在设置padding的过程中，当前的位置一直是在head，否则如果当列表超出屏幕的话，当在上推的时候，列表会同时进行滚动
						setSelection(0);
						// 往上推了，推到了屏幕足够掩盖head的程度，但是还没有推到全部掩盖的地步
						if (((tempY - startY) / RATIO < headerContentHeight)// 由松开刷新状态转变到下拉刷新状态
								&& (tempY - startY) > 0) {
							headerState = HEADER_PULL_To_REFRESH;
							changeHeaderViewByState();
						}
						// 一下子推到顶了
						else if (tempY - startY <= 0) {// 由松开刷新状态转变到done状态
							headerState = HEADER_DONE;
							changeHeaderViewByState();
						}
					}
					if (headerState == HEADER_PULL_To_REFRESH) {// 还没有到达显示松开刷新的时候,DONE或者是PULL_To_REFRESH状态
						setSelection(0);
						// 下拉到可以进入RELEASE_TO_REFRESH的状态
						if ((tempY - startY) / RATIO >= headerContentHeight) {// 由done或者下拉刷新状态转变到松开刷新
							headerState = HEADER_RELEASE_To_REFRESH;
							isBack = true;
							changeHeaderViewByState();
						}
						// 上推到顶了
						else if (tempY - startY <= 0) {// 由DOne或者下拉刷新状态转变到done状态
							headerState = HEADER_DONE;
							changeHeaderViewByState();
						}
					}
					if (headerState == HEADER_DONE) {// done状态下
						if (tempY - startY > 0) {
							headerState = HEADER_PULL_To_REFRESH;
							changeHeaderViewByState();
						}
					}
					if (headerState == HEADER_PULL_To_REFRESH) {// 更新headView的size
						headerView.setPadding(0, -1 * headerContentHeight
								+ (tempY - startY) / RATIO, 0, 0);
					}
					if (headerState == HEADER_RELEASE_To_REFRESH) {// 更新headView的paddingTop
						headerView.setPadding(0, (tempY - startY) / RATIO
								- headerContentHeight, 0, 0);
					}

				}
				break;

			default:
				break;
			}
		}
		return true;
	}
	


	/*
	 * 当状态改变时候，调用该方法，以更新界面
	 */
	private void changeHeaderViewByState() {
		switch (headerState) {
		case HEADER_RELEASE_To_REFRESH:
			lvHeaderArrowIv.setVisibility(View.VISIBLE);
			lvHeaderProgressBar.setVisibility(View.GONE);
			lvHeaderTipsTv.setVisibility(View.VISIBLE);
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			lvHeaderArrowIv.clearAnimation();// 清除动画
			lvHeaderArrowIv.startAnimation(animation);// 开始动画效果
			lvHeaderTipsTv.setText("松开刷新");
			break;
		case HEADER_PULL_To_REFRESH:
			lvHeaderProgressBar.setVisibility(View.GONE);
			lvHeaderTipsTv.setVisibility(View.VISIBLE);
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			lvHeaderArrowIv.clearAnimation();
			lvHeaderArrowIv.setVisibility(View.VISIBLE);
			// 是由RELEASE_To_REFRESH状态转变来的
			if (isBack) {
				isBack = false;
				lvHeaderArrowIv.clearAnimation();
				lvHeaderArrowIv.startAnimation(reverseAnimation);

				lvHeaderTipsTv.setText("下拉刷新");
			} else {
				lvHeaderTipsTv.setText("下拉刷新");
			}
			break;

		case HEADER_REFRESHING:

			headerView.setPadding(0, 0, 0, 0);

			lvHeaderProgressBar.setVisibility(View.VISIBLE);
			lvHeaderArrowIv.clearAnimation();
			lvHeaderArrowIv.setVisibility(View.GONE);
			lvHeaderTipsTv.setText("正在刷新...");
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			break;
		case HEADER_DONE:
			headerView.setPadding(0, -1 * headerContentHeight, 0, 0);

			lvHeaderProgressBar.setVisibility(View.GONE);
			lvHeaderArrowIv.clearAnimation();
			lvHeaderArrowIv.setImageResource(icon_id);
			lvHeaderTipsTv.setText("下拉刷新");
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			break;
		}
	}

	/*
	 * 当状态改变时候，调用该方法，以更新界面
	 */
	private void changeFooterViewByState() {
		switch (footerState) {
		case FOOTER_RELEASE_To_REFRESH:
			lvFooterProgressBar.setVisibility(View.GONE);
			lvFooterTipsTv.setVisibility(View.VISIBLE);
			lvFooterTipsTv.setText("松开加载");
			break;
		case FOOTER_PULL_To_REFRESH:
			lvFooterProgressBar.setVisibility(View.GONE);
			lvFooterTipsTv.setVisibility(View.VISIBLE);
			// 是由RELEASE_To_REFRESH状态转变来的
			if (isBack) {
				isBack = false;
				lvFooterTipsTv.setText("上拉加载更多");
			} else {
				lvFooterTipsTv.setText("上拉加载更多");
			}
			break;

		case FOOTER_REFRESHING:
			footerView.setPadding(0, 0, 0, 0);
			lvFooterProgressBar.setVisibility(View.VISIBLE);
			lvFooterTipsTv.setText("正在加载...");
			break;
		case FOOTER_DONE:
			footerView.setPadding(0, 0, 0, -1 * footerContentHeight);
			lvFooterProgressBar.setVisibility(View.GONE);
			lvFooterTipsTv.setText("上拉加载更多");
			break;
		}
	}
	
	/*
	 * 估计headView的width以及height
	 */
	private void measureView(View child) {
		ViewGroup.LayoutParams params = child.getLayoutParams();
		if (params == null) {
			params = new ViewGroup.LayoutParams(
					ViewGroup.LayoutParams.FILL_PARENT,
					ViewGroup.LayoutParams.WRAP_CONTENT);
		}
		int childWidthSpec = ViewGroup.getChildMeasureSpec(0, 0 + 0,
				params.width);
		int lpHeight = params.height;
		int childHeightSpec;
		if (lpHeight > 0) {
			childHeightSpec = MeasureSpec.makeMeasureSpec(lpHeight,
					MeasureSpec.EXACTLY);
		} else {
			childHeightSpec = MeasureSpec.makeMeasureSpec(0,
					MeasureSpec.UNSPECIFIED);
		}
		child.measure(childWidthSpec, childHeightSpec);
	}

	/*
	 * 设置刷新回调函数
	 */
	public void setonRefreshListener(OnRefreshListener refreshListener) {
		this.refreshListener = refreshListener;
		isRefreshable = true;
	}
	
	/*
	 * 设置加载回调函数
	 */
	public void setonLoadingListener(OnLoadingListener loadingListener) {
		this.loadingListener = loadingListener;
		isLoadingable = true;
	}

	/*
	 * 刷新回调接口
	 */
	public interface OnRefreshListener {
		public void onRefresh();
	}

	/*
	 * 加载回调接口
	 */
	public interface OnLoadingListener{
		public void onLoading();
	}
	
	/*
	 * 刷新完成
	 */
	public void onRefreshComplete() {
		headerState = HEADER_DONE;
		lvHeaderLastUpdatedTv.setText("最近更新:" + new Date().toLocaleString());
		changeHeaderViewByState();
	}
	
	/*
	 * 刷新完成
	 */
	public void onLoadingComplete() {
		footerState = FOOTER_DONE;
		lvHeaderLastUpdatedTv.setText("最近更新:" + new Date().toLocaleString());
		changeFooterViewByState();
	}

	/*
	 * 正在刷新
	 */
	private void onLvRefresh() {
		if (refreshListener != null) {
			refreshListener.onRefresh();
		}
	}

	/*
	 * 正在加载
	 */
	private void onLvLoading() {
		if (loadingListener != null) {
			loadingListener.onLoading();
		}
	}
	
	/*
	 * 设置适配器；
	 */
	public void setAdapter(ImageListAdapter adapter) {
		lvHeaderLastUpdatedTv.setText("最近更新:" + new Date().toLocaleString());
		super.setAdapter(adapter);
	}

}
