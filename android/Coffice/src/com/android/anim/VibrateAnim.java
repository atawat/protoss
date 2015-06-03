package com.android.anim;

import android.view.animation.Animation;
import android.view.animation.Transformation;
/**
 * ���Զ����ࣻ
 * 
 * @author �ֶ�
 * 
 */
public class VibrateAnim extends Animation{
	private float time=0;	
	private float start_X=0,start_Y=0,end_X=0,end_Y=0;
	private float v_length=0;
	public VibrateAnim(){
				
	}
	/* ������λ��*/		
	public VibrateAnim(float start_x,float start_y,float end_x,float end_y){
		this.start_X = start_x;
		this.start_Y = start_y;
		this.end_X = end_x;
		this.end_Y = end_y;
		this.v_length=(float) Math.sqrt((end_x-start_x)*(end_x-start_x)+(end_y-start_y)*(end_y-start_y));
	}
	
	@Override  
    public void initialize(int width, int height, int parentWidth, int parentHeight) {  
        super.initialize(width, height, parentWidth, parentHeight);  
    }
	//����interpolatedTime Ϊ��ǰ����֡��Ӧ�����ʱ��,ֵ����0-1֮��
	@Override
	protected void applyTransformation(float interpolatedTime,Transformation t) {
		time=time+interpolatedTime;
		//�񶯷��̣�
		float Wn=(float) (2*Math.PI/4); //�˶����ڣ�
		float Q=(float) Math.PI/2;//��ʼλ�ã�
		float S=(float) 0.1;
		float A=v_length;//�����
		float Wd=Wn;
		float leagth_xy=-(float) ((float) A*Math.pow(Math.E, -S*Wn*time)*Math.cos(Wd*time+Q));
		float x=(leagth_xy)*((start_X-end_X)/v_length);
		float y=(leagth_xy)*((start_Y-end_Y)/v_length);
		t.getMatrix().setTranslate(-x,-y);
	}
}
