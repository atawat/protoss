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
 * ��������listView��
 * 
 * @author �ֶ�
 * 
 */
public class SuperListView extends ListView implements OnScrollListener {

	private final static int HEADER_RELEASE_To_REFRESH = 0;// �������̵�״ֵ̬
	private final static int HEADER_PULL_To_REFRESH = 1; // ���������ص���ˢ�µ�״ֵ̬
	private final static int HEADER_REFRESHING = 2;// ����ˢ�µ�״ֵ̬
	private final static int HEADER_DONE = 3;// ������ɣ�
	private final static int HEADER_LOADING = 4;// ����ˢ�£�
	
	private final static int FOOTER_RELEASE_To_REFRESH = 0;// �������̵�״ֵ̬
	private final static int FOOTER_PULL_To_REFRESH = 1; // ���������ص���ˢ�µ�״ֵ̬
	private final static int FOOTER_REFRESHING = 2;// ����ˢ�µ�״ֵ̬
	private final static int FOOTER_DONE = 3;// ������ɣ�
	private final static int FOOTER_LOADING = 4;// ����ˢ�£�

	// ʵ�ʵ�padding�ľ����������ƫ�ƾ���ı���
	private final static int RATIO = 3;
	private LayoutInflater inflater;

	// ListView�Ͳ������������Ĳ���
	private LinearLayout footerView;// ˢ��ͷ��������ͼ��
	private TextView lvFooterTipsTv;// ͷ����ͼ��ʾ��
	private ProgressBar lvFooterProgressBar;// ͷ����ͼ���½�������
	private int footerContentHeight;// ����ͷ������ˢ�µĲ��ֵĸ߶�
	
	// ListViewͷ������ˢ�µĲ���
	private LinearLayout headerView;// ˢ��ͷ��������ͼ��
	private TextView lvHeaderTipsTv;// ͷ����ͼ��ʾ��
	private TextView lvHeaderLastUpdatedTv;// ͷ����ͼ������Ϣ��ʾ��
	private ImageView lvHeaderArrowIv;// ͷ����ͼͼƬ��
	private ProgressBar lvHeaderProgressBar;// ͷ����ͼ���½�������
	private int headerContentHeight;// ����ͷ������ˢ�µĲ��ֵĸ߶�
	private RotateAnimation animation;// ��ת������
	private RotateAnimation reverseAnimation;

	private int startY;// ��ʼ����Y���ꣻ
	private int headerState;// ����״̬��
	private int footerState;// ����״̬��
	private boolean isBack;
	private boolean isRecored;// ���ڱ�֤startY��ֵ��һ��������touch�¼���ֻ����¼һ��

	private OnRefreshListener refreshListener;// �ص���
	private OnLoadingListener loadingListener;// �ص���
	private boolean isRefreshable;// �Ƿ���ˢ�£�
	private boolean isLoadingable;// �Ƿ������������
	private int icon_id;
	private int _totalItemCount=0;

	/*
	 * ���캯��
	 */
	public SuperListView(Context context) {
		super(context);
		init(context);
	}

	/*
	 * ���캯��
	 */
	public SuperListView(Context context, AttributeSet attrs) {
		super(context, attrs);
		init(context);
	}

	/*
	 * ����ͼƬ
	 */
	public void setIcon(int resId) {
		this.icon_id = resId;
	}

	/*
	 * ��ʼ��listview
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
		// ��������ˢ��ͼ�����С�߶ȺͿ��
		lvHeaderArrowIv.setMinimumWidth(70);
		lvHeaderArrowIv.setMinimumHeight(50);

		lvHeaderProgressBar = (ProgressBar) headerView
				.findViewById(R.id.lvHeaderProgressBar);
		lvFooterProgressBar = (ProgressBar) footerView
				.findViewById(R.id.lvFootererProgressBar);
		lvFooterTipsTv = (TextView) footerView
				.findViewById(R.id.lvFooterTipsTv);
		// �����ڱ߾࣬���þ��붥��Ϊһ�������������ֵĸ߶ȣ���������
		measureView(headerView);
		measureView(footerView);
		headerContentHeight = headerView.getMeasuredHeight();
		headerView.setPadding(0, -1 * headerContentHeight, 0, 0);
		footerContentHeight = footerView.getMeasuredHeight();
		footerView.setPadding(0, 0, 0, -1 * footerContentHeight);
		
		headerView.invalidate();// �ػ�һ��
		footerView.invalidate();
		
		addHeaderView(headerView, null, false);// ������ˢ�µĲ��ּ���ListView�Ķ���
		addFooterView(footerView, null, false);//�����������벼�֣�
		
		setOnScrollListener(this);// ���ù��������¼�

		// ������ת�����¼�
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

		// һ��ʼ��״̬��������ˢ�����״̬������ΪDONE
		headerState = HEADER_DONE;
		// �Ƿ�����ˢ��
		isRefreshable = false;
	}

	/*
	 * ����״̬�ı䣻
	 */
	@Override
	public void onScrollStateChanged(AbsListView view, int scrollState) {

	}

	/*
	 * ���ڻ���
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
             Log.e("log", "�����ײ�");
             isLoadingable=true;
         }else{
        	 isLoadingable=false;
         }
		 this._totalItemCount=totalItemCount;
	}

	/*
	 * �����¼�
	 */
	@Override
	public boolean onTouchEvent(MotionEvent ev) {
		onDownmove(ev);
		if(!isRefreshable){//���������������
			onUpmove(ev);
		}
		
		return super.onTouchEvent(ev);
	}
	
	/*
	 * ��������ָ����
	 */
	public boolean onUpmove(MotionEvent ev){
		if(isLoadingable){
			switch (ev.getAction()) {
			case MotionEvent.ACTION_DOWN:
				if (!isRecored) {
					isRecored = true;
					startY = (int) ev.getY();// ��ָ����ʱ��¼��ǰλ��
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
					if (footerState == FOOTER_RELEASE_To_REFRESH) {// ��֤������padding�Ĺ����У���ǰ��λ��һֱ����head������������б�����Ļ�Ļ����������Ƶ�ʱ���б��ͬʱ���й���
						setSelection(_totalItemCount);
						// �������ˣ��Ƶ�����Ļ�㹻�ڸ�head�ĳ̶ȣ����ǻ�û���Ƶ�ȫ���ڸǵĵز�
						if (((startY - tempY) / RATIO < footerContentHeight)// ���ɿ�ˢ��״̬ת�䵽����ˢ��״̬
								&& (startY - tempY) > 0) {
							footerState = FOOTER_PULL_To_REFRESH;
							changeFooterViewByState();
						}
						// һ�����Ƶ�����
						else if (startY - tempY <= 0) {// ���ɿ�ˢ��״̬ת�䵽done״̬
							footerState = FOOTER_DONE;
							changeFooterViewByState();
						}
					}
					if (footerState == FOOTER_PULL_To_REFRESH) {// ��û�е�����ʾ�ɿ�ˢ�µ�ʱ��,DONE������PULL_To_REFRESH״̬
						setSelection(_totalItemCount);
						// ���������Խ���RELEASE_TO_REFRESH��״̬
						if ((startY - tempY) / RATIO >= footerContentHeight) {// ��done��������ˢ��״̬ת�䵽�ɿ�ˢ��
							footerState = FOOTER_RELEASE_To_REFRESH;
							isBack = true;
							changeFooterViewByState();
						}
						// ���Ƶ�����
						else if (startY - tempY <= 0) {// ��DOne��������ˢ��״̬ת�䵽done״̬
							footerState = FOOTER_DONE;
							changeFooterViewByState();
						}
					}
					if (footerState == FOOTER_DONE) {// done״̬��
						if (startY - tempY > 0) {
							footerState = FOOTER_PULL_To_REFRESH;
							changeFooterViewByState();
						}
					}
					if (footerState == FOOTER_PULL_To_REFRESH) {// ����headView��size
						footerView.setPadding(0, -1 * footerContentHeight
								+ (startY - tempY) / RATIO, 0, 0);
					}
					if (footerState == FOOTER_RELEASE_To_REFRESH) {// ����headView��paddingTop
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
	 * ������ָ����
	 */
	public boolean onDownmove(MotionEvent ev){
		if (isRefreshable) {
			switch (ev.getAction()) {
			case MotionEvent.ACTION_DOWN:
				if (!isRecored) {
					isRecored = true;
					startY = (int) ev.getY();// ��ָ����ʱ��¼��ǰλ��
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
					if (headerState == HEADER_RELEASE_To_REFRESH) {// ��֤������padding�Ĺ����У���ǰ��λ��һֱ����head������������б�����Ļ�Ļ����������Ƶ�ʱ���б��ͬʱ���й���
						setSelection(0);
						// �������ˣ��Ƶ�����Ļ�㹻�ڸ�head�ĳ̶ȣ����ǻ�û���Ƶ�ȫ���ڸǵĵز�
						if (((tempY - startY) / RATIO < headerContentHeight)// ���ɿ�ˢ��״̬ת�䵽����ˢ��״̬
								&& (tempY - startY) > 0) {
							headerState = HEADER_PULL_To_REFRESH;
							changeHeaderViewByState();
						}
						// һ�����Ƶ�����
						else if (tempY - startY <= 0) {// ���ɿ�ˢ��״̬ת�䵽done״̬
							headerState = HEADER_DONE;
							changeHeaderViewByState();
						}
					}
					if (headerState == HEADER_PULL_To_REFRESH) {// ��û�е�����ʾ�ɿ�ˢ�µ�ʱ��,DONE������PULL_To_REFRESH״̬
						setSelection(0);
						// ���������Խ���RELEASE_TO_REFRESH��״̬
						if ((tempY - startY) / RATIO >= headerContentHeight) {// ��done��������ˢ��״̬ת�䵽�ɿ�ˢ��
							headerState = HEADER_RELEASE_To_REFRESH;
							isBack = true;
							changeHeaderViewByState();
						}
						// ���Ƶ�����
						else if (tempY - startY <= 0) {// ��DOne��������ˢ��״̬ת�䵽done״̬
							headerState = HEADER_DONE;
							changeHeaderViewByState();
						}
					}
					if (headerState == HEADER_DONE) {// done״̬��
						if (tempY - startY > 0) {
							headerState = HEADER_PULL_To_REFRESH;
							changeHeaderViewByState();
						}
					}
					if (headerState == HEADER_PULL_To_REFRESH) {// ����headView��size
						headerView.setPadding(0, -1 * headerContentHeight
								+ (tempY - startY) / RATIO, 0, 0);
					}
					if (headerState == HEADER_RELEASE_To_REFRESH) {// ����headView��paddingTop
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
	 * ��״̬�ı�ʱ�򣬵��ø÷������Ը��½���
	 */
	private void changeHeaderViewByState() {
		switch (headerState) {
		case HEADER_RELEASE_To_REFRESH:
			lvHeaderArrowIv.setVisibility(View.VISIBLE);
			lvHeaderProgressBar.setVisibility(View.GONE);
			lvHeaderTipsTv.setVisibility(View.VISIBLE);
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			lvHeaderArrowIv.clearAnimation();// �������
			lvHeaderArrowIv.startAnimation(animation);// ��ʼ����Ч��
			lvHeaderTipsTv.setText("�ɿ�ˢ��");
			break;
		case HEADER_PULL_To_REFRESH:
			lvHeaderProgressBar.setVisibility(View.GONE);
			lvHeaderTipsTv.setVisibility(View.VISIBLE);
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			lvHeaderArrowIv.clearAnimation();
			lvHeaderArrowIv.setVisibility(View.VISIBLE);
			// ����RELEASE_To_REFRESH״̬ת������
			if (isBack) {
				isBack = false;
				lvHeaderArrowIv.clearAnimation();
				lvHeaderArrowIv.startAnimation(reverseAnimation);

				lvHeaderTipsTv.setText("����ˢ��");
			} else {
				lvHeaderTipsTv.setText("����ˢ��");
			}
			break;

		case HEADER_REFRESHING:

			headerView.setPadding(0, 0, 0, 0);

			lvHeaderProgressBar.setVisibility(View.VISIBLE);
			lvHeaderArrowIv.clearAnimation();
			lvHeaderArrowIv.setVisibility(View.GONE);
			lvHeaderTipsTv.setText("����ˢ��...");
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			break;
		case HEADER_DONE:
			headerView.setPadding(0, -1 * headerContentHeight, 0, 0);

			lvHeaderProgressBar.setVisibility(View.GONE);
			lvHeaderArrowIv.clearAnimation();
			lvHeaderArrowIv.setImageResource(icon_id);
			lvHeaderTipsTv.setText("����ˢ��");
			lvHeaderLastUpdatedTv.setVisibility(View.VISIBLE);
			break;
		}
	}

	/*
	 * ��״̬�ı�ʱ�򣬵��ø÷������Ը��½���
	 */
	private void changeFooterViewByState() {
		switch (footerState) {
		case FOOTER_RELEASE_To_REFRESH:
			lvFooterProgressBar.setVisibility(View.GONE);
			lvFooterTipsTv.setVisibility(View.VISIBLE);
			lvFooterTipsTv.setText("�ɿ�����");
			break;
		case FOOTER_PULL_To_REFRESH:
			lvFooterProgressBar.setVisibility(View.GONE);
			lvFooterTipsTv.setVisibility(View.VISIBLE);
			// ����RELEASE_To_REFRESH״̬ת������
			if (isBack) {
				isBack = false;
				lvFooterTipsTv.setText("�������ظ���");
			} else {
				lvFooterTipsTv.setText("�������ظ���");
			}
			break;

		case FOOTER_REFRESHING:
			footerView.setPadding(0, 0, 0, 0);
			lvFooterProgressBar.setVisibility(View.VISIBLE);
			lvFooterTipsTv.setText("���ڼ���...");
			break;
		case FOOTER_DONE:
			footerView.setPadding(0, 0, 0, -1 * footerContentHeight);
			lvFooterProgressBar.setVisibility(View.GONE);
			lvFooterTipsTv.setText("�������ظ���");
			break;
		}
	}
	
	/*
	 * ����headView��width�Լ�height
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
	 * ����ˢ�»ص�����
	 */
	public void setonRefreshListener(OnRefreshListener refreshListener) {
		this.refreshListener = refreshListener;
		isRefreshable = true;
	}
	
	/*
	 * ���ü��ػص�����
	 */
	public void setonLoadingListener(OnLoadingListener loadingListener) {
		this.loadingListener = loadingListener;
		isLoadingable = true;
	}

	/*
	 * ˢ�»ص��ӿ�
	 */
	public interface OnRefreshListener {
		public void onRefresh();
	}

	/*
	 * ���ػص��ӿ�
	 */
	public interface OnLoadingListener{
		public void onLoading();
	}
	
	/*
	 * ˢ�����
	 */
	public void onRefreshComplete() {
		headerState = HEADER_DONE;
		lvHeaderLastUpdatedTv.setText("�������:" + new Date().toLocaleString());
		changeHeaderViewByState();
	}
	
	/*
	 * ˢ�����
	 */
	public void onLoadingComplete() {
		footerState = FOOTER_DONE;
		lvHeaderLastUpdatedTv.setText("�������:" + new Date().toLocaleString());
		changeFooterViewByState();
	}

	/*
	 * ����ˢ��
	 */
	private void onLvRefresh() {
		if (refreshListener != null) {
			refreshListener.onRefresh();
		}
	}

	/*
	 * ���ڼ���
	 */
	private void onLvLoading() {
		if (loadingListener != null) {
			loadingListener.onLoading();
		}
	}
	
	/*
	 * ������������
	 */
	public void setAdapter(ImageListAdapter adapter) {
		lvHeaderLastUpdatedTv.setText("�������:" + new Date().toLocaleString());
		super.setAdapter(adapter);
	}

}
