package com.android.custom.page;

import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.VelocityTracker;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Scroller;

/**
 * 滑动页面类；
 * 
 * @author 贾豆
 * 
 */
public class ScrollLayout extends ViewGroup//
{

	private static final String TAG = "ScrollLayout";
	private VelocityTracker mVelocityTracker; // 用于判断甩动手势
	private static final int SNAP_VELOCITY = 100; // 滑动距离
	private Scroller mScroller; // 滑动控制器
	private int mCurScreen; // 设置当前页面；
	private int mDefaultScreen = 0; // 设置当前默认页面；
	private float mLastMotionX;
	private int _snapSpeed=6;//滑动速度，越大越快；
	private IOnViewChange mOnViewChangeListener;

	public ScrollLayout(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
		init(context);
	}

	public ScrollLayout(Context context, AttributeSet attrs) {
		super(context, attrs);
		// TODO Auto-generated constructor stub
		init(context);
	}

	public ScrollLayout(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		// TODO Auto-generated constructor stub
		init(context);
	}

	private void init(Context context) {
		mCurScreen = mDefaultScreen; // 设置当前默认页面；
		// mTouchSlop =
		// ViewConfiguration.get(getContext()).getScaledTouchSlop();
		mScroller = new Scroller(context);

	}

	@Override
	protected void onLayout(boolean changed, int l, int t, int r, int b) {
		// TODO Auto-generated method stub
		if (changed) {
			int childLeft = 0;
			final int childCount = getChildCount();
			for (int i = 0; i < childCount; i++) {
				final View childView = getChildAt(i);
				if (childView.getVisibility() != View.GONE) {
					final int childWidth = childView.getMeasuredWidth();
					childView.layout(childLeft, 0, childLeft + childWidth,
							childView.getMeasuredHeight());
					childLeft += childWidth;
				}
			}
		}
	}

	@Override
	protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
		// TODO Auto-generated method stub
		super.onMeasure(widthMeasureSpec, heightMeasureSpec);
		final int width = MeasureSpec.getSize(widthMeasureSpec);
		final int widthMode = MeasureSpec.getMode(widthMeasureSpec);

		final int count = getChildCount();
		for (int i = 0; i < count; i++) {
			getChildAt(i).measure(widthMeasureSpec, heightMeasureSpec);
		}
		scrollTo(mCurScreen * width, 0);
	}

	public void snapToDestination() {
		final int screenWidth = getWidth();
		final int destScreen = (getScrollX() + screenWidth / 2) / screenWidth;
		snapToScreen(destScreen);
	}

	public void snapToScreen(int whichScreen) {
		// get the valid layout page
		whichScreen = Math.max(0, Math.min(whichScreen, getChildCount() - 1));
		if (getScrollX() != (whichScreen * getWidth())) {
			final int delta = whichScreen * getWidth() - getScrollX();
			mScroller.startScroll(getScrollX(), 0, delta, 0,
					Math.abs(delta)/_snapSpeed);

			mCurScreen = whichScreen;
			invalidate(); // Redraw the layout
			if (mOnViewChangeListener != null) {
				mOnViewChangeListener.OnViewChange(mCurScreen);
			}
		}
	}

	@Override
	public void computeScroll() {
		// TODO Auto-generated method stub
		if (mScroller.computeScrollOffset()) {
			scrollTo(mScroller.getCurrX(), mScroller.getCurrY());
			postInvalidate();
		}
	}

	@Override
	public boolean onTouchEvent(MotionEvent event) // 触摸事件响应：包括；
	{
		// TODO Auto-generated method stub
		final int action = event.getAction();
		final float x = event.getX();
		final float y = event.getY();

		switch (action) {
		case MotionEvent.ACTION_DOWN:
			Log.i("", "onTouchEvent  ACTION_DOWN");
			if (mVelocityTracker == null) // 用于判断甩动手势；
			{
				mVelocityTracker = VelocityTracker.obtain();
				mVelocityTracker.addMovement(event);
			}
			if (!mScroller.isFinished()) {
				mScroller.abortAnimation();
			}
			mLastMotionX = x;
			break;

		case MotionEvent.ACTION_MOVE:
			int deltaX = (int) (mLastMotionX - x);// deltaX为滑动距离，mLastMotionX为最后一次触摸的X坐标；
			if (IsCanMove(deltaX))// 如果判定滑动距离足够移动则执行下面；
			{
				if (mVelocityTracker != null) {
					mVelocityTracker.addMovement(event);
				}
				mLastMotionX = x;
				scrollBy(deltaX, 0);// 根据当前触摸坐标滑动页面，但不滑动至目的页面；
			}

			break;
		case MotionEvent.ACTION_UP:
			int velocityX = 0;
			if (mVelocityTracker != null) {
				mVelocityTracker.addMovement(event);
				mVelocityTracker.computeCurrentVelocity(1000);
				velocityX = (int) mVelocityTracker.getXVelocity();
			}
			if (velocityX > SNAP_VELOCITY && mCurScreen > 0)// 向左滑动
			{
				// Fling enough to move left
				Log.v(TAG, "snap left");
				snapToScreen(mCurScreen - 1);// 向左滑动（-1）；
			} else if (velocityX < -SNAP_VELOCITY
					&& mCurScreen < getChildCount() - 1) // 向右滑动；
			{
				// Fling enough to move right
				Log.v(TAG, "snap right");
				snapToScreen(mCurScreen + 1); // 向左滑动（+1）；
			} else {
				snapToDestination(); // 滑动至目标页面；
			}

			if (mVelocityTracker != null) {
				mVelocityTracker.recycle();
				mVelocityTracker = null;
			}
			// mTouchState = TOUCH_STATE_REST;
			break;
		}
		return true;
	}

	private boolean IsCanMove(int deltaX)// 判断页面是否该滑动；
	{
		if (getScrollX() <= 0 && deltaX < 0) {
			return false;
		}
		if (getScrollX() >= (getChildCount() - 1) * getWidth() && deltaX > 0) {
			return false;
		}
		return true;
	}

	public void SetOnViewChangeListener(IOnViewChange listener) {
		mOnViewChangeListener = listener;
	}
}