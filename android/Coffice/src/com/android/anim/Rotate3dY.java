
package com.android.anim; 
import android.graphics.Camera; 
import android.graphics.Matrix; 
import android.view.animation.Animation; 
import android.view.animation.Transformation; 
/**
 * 3d动画绕Y轴旋转类；
 * 
 * @author 贾豆
 * 
 */
public class Rotate3dY extends Animation 
{ 
	// 开始角度 
	private final float mFromDegrees; 
	// 结束角度 
	private final float mToDegrees; 
	// 中心点 
	private final float mCenterX; 
	private final float mCenterY; 
	private final float mDepthZ; 
	// 是否需要扭曲 
	private final boolean mReverse; 
	// 摄像头 
	private Camera mCamera; 
	public Rotate3dY(float fromDegrees, float toDegrees, float centerX,float centerY, float depthZ, boolean reverse) { 
		mFromDegrees = fromDegrees; 
		mToDegrees = toDegrees; 
		mCenterX = centerX; 
		mCenterY = centerY; 
		mDepthZ = depthZ; 
		mReverse = reverse; 
	} 
	@Override 
	public void initialize(int width, int height, int parentWidth, int parentHeight) { 
		super.initialize(width, height, parentWidth, parentHeight); 
		mCamera = new Camera(); 
	} 
	// 生成Transformation 
	@Override 
	protected void applyTransformation(float interpolatedTime, Transformation t) 
	{ 
		final float fromDegrees = mFromDegrees; 
		// 生成中间角度 
		float degrees = fromDegrees + ((mToDegrees - fromDegrees) * interpolatedTime); 
		final float centerX = mCenterX; 
		final float centerY = mCenterY; 
		final Camera camera = mCamera; 
		final Matrix matrix = t.getMatrix(); 
		camera.save(); 
		if (mReverse) { 
			camera.translate(0.0f, 0.0f, mDepthZ * interpolatedTime); 
		} else { 
			camera.translate(0.0f, 0.0f, mDepthZ * (1.0f - interpolatedTime)); 
		} 
		camera.rotateY(degrees);//此处为改变饶轴； 
		// 取得变换后的矩阵 
		camera.getMatrix(matrix); 
		camera.restore(); 
		matrix.preTranslate(-centerX, -centerY); 
		matrix.postTranslate(centerX, centerY); 
	} 
} 