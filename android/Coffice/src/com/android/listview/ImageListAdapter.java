package com.android.listview;

import java.net.InterfaceAddress;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.ImageLoaderConfiguration;
import com.nostra13.universalimageloader.core.assist.ImageLoadingListener;

import android.R.integer;
import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

/**
 * listview适配类（此类可支持子控件监听、网络图片加载等等）
 * 
 * @author 贾豆
 * 
 */
public class ImageListAdapter extends BaseAdapter {
	private List<ListItem> _mData;
	private LayoutInflater _mInflater;
	private ArrayList<Integer> TypeList = new ArrayList<Integer>();
	private Context _context;
	protected static ImageLoader _imageLoader;
	protected static DisplayImageOptions _options; // DisplayImageOptions是用于设置图片显示的类
	private static ImageLoadingListener _animateFirstListener = (ImageLoadingListener) new AnimateFirstDisplayListener();
	private static String TAG = "ImageListAdapter";
	private static IItemClickListener _iItemClickListener;
	private GetViewListener _getViewListener;
	public void AddType(int mResource) {
		TypeList.add(mResource);
	}

	public ImageListAdapter(Context context, List<ListItem> data,
			DisplayImageOptions options, ImageLoader imageLoader,
			IItemClickListener iItemClickListener) {
		this._context = context;
		_mData = data;
		_options = options;
		_mInflater = LayoutInflater.from(context);
		_imageLoader = imageLoader;
		_imageLoader.init(ImageLoaderConfiguration.createDefault(context));
		_iItemClickListener = iItemClickListener;
	}

	@Override
	public int getItemViewType(int position) {
		return _mData.get(position).mType;
	}

	@Override
	public int getViewTypeCount() {
		if (TypeList.size() == 0)
			return 1;
		else
			return TypeList.size();
	}

	public int getCount() {
		// TODO Auto-generated method stub
		return _mData.size();
	}

	public ListItem getItem(int position) {
		// TODO Auto-generated method stub
		return _mData.get(position);
		// return null;
	}

	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	/*
	 * (non-Javadoc)
	 * @see android.widget.Adapter#getView(int, android.view.View, android.view.ViewGroup)
	 */
	public View getView(int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		int type = getItemViewType(position);
		if (convertView == null) {
			holder = new ViewHolder();
			ListItem item = getItem(position);
			convertView = _mInflater.inflate(TypeList.get(type), null);
			for (Iterator<Object> it = item.mMap.keySet().iterator(); it
					.hasNext();) // 遍历
			{
				try{
					int id = (Integer) it.next();
					Object obj = convertView.findViewById(id);
					if (obj != null) {
						holder.List_Object.add(obj);
						holder.List_id.add(id);
					}
					
				}catch(Exception error){
					Log.d(TAG, "非资源值");
				}
			}
			convertView.setTag(holder);

		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		holder.SetValue(_mData.get(position), position,convertView);
		if(_getViewListener!=null){
			_getViewListener.getView(position, convertView,parent);
		}
		return convertView;
	}

	/*
	 * Holder静态变量，提高效率
	 */
	public static class ViewHolder {
		ArrayList<Object> List_Object = new ArrayList<Object>();
		ArrayList<Integer> List_id = new ArrayList<Integer>();
		public boolean SetValue(ListItem item, int position,View parentView) {
			int i = 0;
			Object objectView;
			for (Object obj : List_Object) {

				// try
				{
					int id = List_id.get(i);
					objectView = item.mMap.get(id);

					if (obj.getClass().equals(TextView.class)) {
						((TextView) obj).setText(objectView.toString());
					}

					if (obj.getClass().equals(ImageView.class)) {
						if(_iItemClickListener!=null){//如果实例化了监听回调函数在，则监听
							((ImageView) obj)
							.setOnClickListener(new ItemOnClickListener(
									position,parentView));
						}

						if (objectView.getClass().equals(Integer.class)) {
							((ImageView) obj)
									.setImageResource((Integer) objectView);
				
						} else if (objectView.getClass().equals(String.class)) {
							/**
							 * 显示图片 参数1：图片url 参数2：显示图片的控件 参数3：显示图片的设置 参数4：监听器
							 */
							_imageLoader.displayImage((String) objectView,
									(ImageView) obj, _options,
									_animateFirstListener);
						}
					}

					if (obj.getClass().equals(ImageButton.class)) {
						if (objectView.getClass().equals(Integer.class)) {
							((ImageButton) obj)
									.setImageResource((Integer) objectView);
						}

					}
					// }catch (Exception e) {
					// // TODO: handle exception
					// e.printStackTrace();
				}

				i++;
			}
	
			return false;
		}

		/*
		 *  单击事件实现
		 */
		public class ItemOnClickListener implements OnClickListener {
			public int position;
			public View parentView;
			public ItemOnClickListener(int p,View pv) {
				position = p;
				parentView=pv;
			}
			@Override
			public void onClick(View v) {
				Log.d(TAG, "点击"+position);
				if (_iItemClickListener != null) {
					_iItemClickListener.ItemOnClick(v, parentView,position);
				}
			}
		}
	}
	
	/*
	 * 点击子控件回调接口；
	 */
	public interface IItemClickListener {
		public void ItemOnClick(View v,View parentView, int postion);
	}
	
	/*
	 * getview回调接口
	 **/
	public interface GetViewListener{
		public void getView(int position, View convertView, ViewGroup parent);
	}
	
	public void setGetViewCallback(GetViewListener getViewListener){
		this._getViewListener=getViewListener;
	}

}
