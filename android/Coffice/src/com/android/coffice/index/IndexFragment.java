package com.android.coffice.index;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.w3c.dom.Text;

import com.android.coffice.R;
import com.android.coffice.index.page.ScrollLayout;
import com.android.custom.CustomDialog;
import com.android.custom.CustomPropertyPicker;
import com.android.custom.CustomPropertyPicker.OnSelectedClickListener;
import com.android.custom.CustomSetting;
import com.android.custom.CustomSetting.valueChangeListener;
import com.android.listview.ListItem;
import com.android.web.HttpHelp;
import com.android.web.IpConfig;
import com.android.web.HttpHelp.GetHttpListener;
import com.nostra13.universalimageloader.core.ImageLoader;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.app.Fragment;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.animation.Animation;
import android.view.animation.AnimationSet;
import android.view.animation.ScaleAnimation;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

/**
 * ��ҳ���棻
 * 
 * @author �ֶ�
 * 
 */
@SuppressLint({ "ValidFragment", "NewApi" })
public class IndexFragment extends Fragment {
	private View _view;
	private ScrollLayout _mScrollLayout;
	private Context _super;
	private ImageView _settingButton;
	private static String _domainString;
	private TextView _orderNumberTextView;
	private TextView _orderUnprintTextView;
	private HttpHelp _httpHelp=new HttpHelp();
	Handler handler = new Handler();

	public IndexFragment() {
	}

	public IndexFragment(Context superContext, ImageLoader imageLoader) {
		this._super = superContext;
		
	}

	private void initParmament() {
		_orderNumberTextView=(TextView)_view.findViewById(R.id.index_order_number);
		_orderUnprintTextView=(TextView)_view.findViewById(R.id.index_unprint_number);
		// TODO Auto-generated method stub
		_httpHelp.startGetAsnyTask(IpConfig.hostIpString+IpConfig.GetTodayOrderNumber,
				new GetHttpListener() {
					public void getHttpResult(String res) {
						_orderNumberTextView.setText("���ն���  "+res+" ��");
					}

				});
		_httpHelp.startGetAsnyTask(IpConfig.hostIpString+IpConfig.GetTodayNoPrintNumber,
				new GetHttpListener() {
					public void getHttpResult(String res) {
						_orderUnprintTextView.setText("δ������  "+res+" ��");
					}
				});
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		_view = inflater.inflate(R.layout.fragment_index, container, false);
		_mScrollLayout = (com.android.coffice.index.page.ScrollLayout) _view
				.findViewById(R.id.scroll_bg_layout);// ����ҳ�沼�����ID��
		// handler.postDelayed(runnable, _durTime);// ÿ����ִ��һ��runnable.
		this._settingButton = (ImageView) _view
				.findViewById(R.id.index_setting_id);
		this._settingButton.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				ClickAnim(v);
				showSettingView();
			}
		});
		initParmament();
		return _view;
	}

	/*
	 * �������
	 */
	private boolean ClickAnim(View view) {
		/** �������Ŷ��� */
		ScaleAnimation animation = new ScaleAnimation(0.0f, 1.0f, 0.0f, 1.0f,
				Animation.RELATIVE_TO_SELF, 0.5f, Animation.RELATIVE_TO_SELF,
				0.5f);
		animation.setDuration(200);// ���ö�������ʱ��
		animation.startNow();
		AnimationSet animationSet = new AnimationSet(true);
		animationSet.addAnimation(animation);
		view.startAnimation(animationSet);
		animationSet.start();
		return false;
	}

	/*
	 * ��������ѡ����
	 */
	private void showSettingView() {
		CustomSetting customSetting = new CustomSetting(
				_super);
		customSetting.setValueChangeListener(new valueChangeListener() {
			@Override
			public void onChange(String msg) {
				// TODO Auto-generated method stub
				_domainString=msg;
				Toast.makeText(_super, _domainString, Toast.LENGTH_SHORT).show();
			}
		});
		CustomDialog.Builder customBuilder = new CustomDialog.Builder(_super);
		customBuilder.setTitle("����").setMessage(null).setContentView(customSetting)
				.setNegativeButton("ȡ��", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						dialog.dismiss();
					}
				})
				.setPositiveButton("����", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
						dialog.dismiss();
						try{
							SaveDomain("domain",_domainString);
							Toast.makeText(_super, "����ɹ�", Toast.LENGTH_SHORT).show();
						}catch(Exception error){
							Toast.makeText(_super, "�������", Toast.LENGTH_SHORT).show();
						}
						
					}
				});
		customBuilder.create().show();
	}
	
    /* �洢������ */
    public void SaveDomain(String parmeter,String value)
    {
        SharedPreferences.Editor sharedata = _super.getSharedPreferences(
                "Setting", 0).edit();
        //sharedata.clear();
        sharedata.putString(parmeter, value);
        sharedata.commit();
    }

}