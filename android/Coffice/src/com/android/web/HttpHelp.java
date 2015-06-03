package com.android.web;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.protocol.HTTP;
import org.apache.http.util.EntityUtils;
import org.json.JSONObject;

import android.os.AsyncTask;

public class HttpHelp {
	public String postResult = null;
	public String getResult = null;
	/*
	 * Http监听接口；
	 */
	public interface GetHttpListener {
		public void getHttpResult(String res);
	}

	/*
	 * 启动Post异步任务
	 */
	public void startPostAsnyTask(final List<DataPair> dPairs, String hostIpString,
			GetHttpListener httpListener) {
		// TODO Auto-generated method stub
		String httpUrl = hostIpString;
		final HttpPost httpRequest = new HttpPost(httpUrl);
		final GetHttpListener postHttpListener = httpListener;
		AsyncTask<Void, Void, Boolean> aTask = new AsyncTask<Void, Void, Boolean>() {
			@Override
			protected Boolean doInBackground(Void... params) {
				// TODO Auto-generated method stub
				List<NameValuePair> params1 = new ArrayList<NameValuePair>();
				if (dPairs != null) {
					for (DataPair dpDataPair : dPairs) {
						params1.add(new BasicNameValuePair(// 用户名；
								dpDataPair.getName(), dpDataPair.getValue()));
					}
				}
				HttpEntity httpentity = null;
				try {
					httpentity = new UrlEncodedFormEntity(params1, "utf8");
				} catch (UnsupportedEncodingException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				httpRequest.setEntity(httpentity);
				HttpClient httpclient = new DefaultHttpClient();
				HttpResponse httpResponse = null;
				try {
					httpResponse = httpclient.execute(httpRequest);
				} catch (ClientProtocolException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				if (httpResponse.getStatusLine().getStatusCode() == 200) {
					try {
						postResult = EntityUtils.toString(httpResponse
								.getEntity());
					} catch (Exception e) {
						e.printStackTrace();
					}

					return true;
				} else {
					return false;
				}
			}

			@Override
			protected void onPostExecute(Boolean result) {
				super.onPostExecute(result);
				postHttpListener.getHttpResult(postResult);
			}
		};
		aTask.execute();
	}

	
	/*
	 * 启动Post异步任务
	 */
	public void startPostStringAsnyTask(final String param ,final String hostIpString,
			final GetHttpListener httpListener) {
		new AsyncTask<Void, Void, Boolean>() {
			@Override
			protected Boolean doInBackground(Void... params) {
				// TODO Auto-generated method stub
				HttpPost httpPost = new HttpPost(hostIpString);
				httpPost.setHeader("Accept", "application/json");
		        httpPost.setHeader("Content-type", "application/json");
				HttpResponse httpResponse=null;
				try {
					httpPost.setEntity(new StringEntity(param, HTTP.UTF_8));
					HttpClient httpclient = new DefaultHttpClient();
					httpResponse = httpclient.execute(httpPost);
				} catch (Exception e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
				int resCode=httpResponse.getStatusLine().getStatusCode();
				if ( resCode== 200) {
					try {
						postResult = EntityUtils.toString(httpResponse
								.getEntity());
					} catch (Exception e) {
						e.printStackTrace();
					}

					return true;
				} else {
					return true;
				}
			}

			@Override
			protected void onPostExecute(Boolean result) {
				super.onPostExecute(result);
				httpListener.getHttpResult(postResult);
			}
		}.execute();
	}
	
	/*
	 * 启动Get异步任务
	 */
	public void startGetAsnyTask( String url,GetHttpListener httpListener) {
		final String getUrl=url;
		final GetHttpListener getHttpListener = httpListener;
		AsyncTask<Void, Void, Boolean> aTask = new AsyncTask<Void, Void, Boolean>() {
			@Override
			protected Boolean doInBackground(Void... params) {
				// TODO Auto-generated method stub
				/* URL可以随意改 */
				String uriAPI = getUrl;
				HttpGet httpRequest = new HttpGet(uriAPI);/* 建立HTTP Get对象 */
				try {
					HttpResponse httpResponse = new DefaultHttpClient()
							.execute(httpRequest); /* 发送请求并等待响应 */
					if (httpResponse.getStatusLine().getStatusCode() == 200) // 若状态码为200
																			
					{
						getResult = EntityUtils.toString(httpResponse
								.getEntity());/* 读 */
					} else {
					}
				} catch (ClientProtocolException e) {
					e.printStackTrace();
				} catch (IOException e) {
					e.printStackTrace();
				} catch (Exception e) {
					e.printStackTrace();
				}
				return true;

			}
			@Override
			protected void onPostExecute(Boolean result) {
				super.onPostExecute(result);
				getHttpListener.getHttpResult(getResult);
			}
		};
		aTask.execute();
	}
}
