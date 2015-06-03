package com.android.anim;

import android.text.Layout;
import android.util.Log;
import android.view.View;
import android.view.animation.AccelerateInterpolator;
import android.view.animation.Animation;
import android.view.animation.DecelerateInterpolator;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
/**
 * 3d����������
 * 
 * @author �ֶ�
 * 
 */
public class Rotate3dHelper
{
    Layout homeLayout;
    private Rotate3dX _rotate3dByX = null;
    private Rotate3dY _rotate3dByY = null;
    private View view;
    private int start, end;
    private float depthZ;
    private int R3dTime;
    private OnAnimOverListener _onAnimOverListener;
    public Rotate3dHelper(LinearLayout Layou)
    {
        this.view = Layou;
    }

    public Rotate3dHelper(FrameLayout Layou)
    {
        this.view = Layou;
    }

    public Rotate3dHelper(RelativeLayout Layou)
    {
        this.view = Layou;
    }

    public Rotate3dHelper(View view)
    {
        this.view = view;
    }

    /* ��X����ת3dЧ����ʱ�䣬��ʼ�Ƕȣ������Ƕȣ���ȣ��Ƿ񷵻أ� */
    public boolean Rotate3dByX(int time, int start, int end, float depthZ,boolean isOneTime,OnAnimOverListener onAnimOverListener)
    {
        this.start = start;
        this.end = end;
        this.depthZ=depthZ;
        this._onAnimOverListener=onAnimOverListener;
        final float centerX = view.getWidth() / 2.0f;
        final float centerY = view.getHeight() / 2.0f;
        _rotate3dByX = new Rotate3dX(start, end, centerX, centerY, depthZ, true);
        _rotate3dByX.reset();
        _rotate3dByX.setRepeatCount(0);// �����ظ�����
        _rotate3dByX.setDuration(time);// ��ʱ��Ӧ�Ͷ�Ӧ����ʱ��һ�£�
        _rotate3dByX.setFillAfter(true);
        _rotate3dByX.setInterpolator(new AccelerateInterpolator());
        R3dTime = time;
        /** ��ʼ���� */
        // ���ü���
        if (isOneTime)
        {
            _rotate3dByX.setAnimationListener(new AnimXListener());
        }
        view.startAnimation(_rotate3dByX);
        return false;
    }

    /* ��Y����ת3dЧ����ʱ�䣬��ʼ�Ƕȣ������Ƕȣ���ȣ��Ƿ񷵻أ�*/
    public boolean Rotate3dByY(int time, int start, int end, float depthZ,boolean isOneTime,OnAnimOverListener onAnimOverListener)
    {
        this.start = start;
        this.end = end;
        this.depthZ=depthZ;
        this._onAnimOverListener=onAnimOverListener;
        final float centerX = view.getWidth() / 2.0f;
        final float centerY = view.getHeight() / 2.0f;
        _rotate3dByY = new Rotate3dY(start, end, centerX, centerY, depthZ, true);
        _rotate3dByY.reset();
        _rotate3dByY.setRepeatCount(0);// �����ظ�����
        _rotate3dByY.setDuration(time);// ��ʱ��Ӧ�Ͷ�Ӧ����ʱ��һ�£�
        _rotate3dByY.setFillAfter(true);
        _rotate3dByY.setInterpolator(new AccelerateInterpolator());
        R3dTime = time;
        /** ��ʼ���� */
        // ���ü���
        if (isOneTime)
        {
            _rotate3dByY.setAnimationListener(new AnimYListener());
        }
        view.startAnimation(_rotate3dByY);
        return false;
    }

    /* ����X�ᶯ������ */
    private final class AnimXListener implements Animation.AnimationListener
    {
        // ������ʼ��
        public void onAnimationStart(Animation animation)
        {
        }

        // ����������
        public void onAnimationEnd(Animation animation)
        {
            _onAnimOverListener.AnimOver();
            view.post(new SwapViewsX());
        }

        // �����ظ���
        public void onAnimationRepeat(Animation animation)
        {
        }

        /* �л���ͼ */
        private final class SwapViewsX implements Runnable
        {
            public void run()
            {
                final float centerX = view.getWidth() / 2.0f;
                final float centerY = view.getHeight() / 2.0f;
                Rotate3dX Rotate3dX = null;
                view.requestFocus();
                Rotate3dX = new Rotate3dX(end, start, centerX, centerY,
                        depthZ, false);
                Rotate3dX.setDuration(R3dTime);
                Rotate3dX.setFillAfter(true);
                Rotate3dX.setInterpolator(new DecelerateInterpolator());
                // ��ʼ����
                view.startAnimation(Rotate3dX);
            }
        }
    }
    
    /* ����Y�ᶯ������ */
    private final class AnimYListener implements Animation.AnimationListener
    {
        // ������ʼ��
        public void onAnimationStart(Animation animation)
        {
        }

        // ����������
        public void onAnimationEnd(Animation animation)
        {
            _onAnimOverListener.AnimOver();
            view.post(new SwapViewsY());
        }

        // �����ظ���
        public void onAnimationRepeat(Animation animation)
        {
        }

        /* �л���ͼ */
        private final class SwapViewsY implements Runnable
        {
            public void run()
            {
                final float centerX = view.getWidth() / 2.0f;
                final float centerY = view.getHeight() / 2.0f;
                Rotate3dY Rotate3dY = null;
                view.requestFocus();
                Rotate3dY = new Rotate3dY(end, start, centerX, centerY,
                        depthZ, false);
                Rotate3dY.setDuration(R3dTime);
                Rotate3dY.setFillAfter(true);
                Rotate3dY.setInterpolator(new DecelerateInterpolator());
                // ��ʼ����
                view.startAnimation(Rotate3dY);
            }
        }
    }

    /* 3d�����ص��ӿ�*/
    public interface OnAnimOverListener {
        void AnimOver();
    }
}
