package com.android.web;

public class IpConfig {
	public static String hostIpString="http://114.215.156.115";
	//public static String hostIpString="http://localhost:22572";
	
	public static String getOrder="/api/order/get";
	public static String getOrderByCondition="/api/order/GetByCondition";
	public static String getProduct="/api/product/get";
	public static String getCategory="/api/category/get";
	public static String getProductByCondition="/api/product/GetByCondition";
	public static String postOrder="/api/order/post";
	public static String GetOrderDetailByOrder="/api/orderdetail/GetOrderDetailByOrder";
	public static String CreateOrder="/api/order/CreateOrder";
	public static String GetUpdataOrderIsPrintStatusByOrderId="/api/order/GetUpdataOrderIsPrintStatusByOrderId";
	public static String GetTodayOrderNumber="/api/order/GetTodayOrderNumber";
	public static String GetTodayNoPrintNumber="/api/order/GetTodayNoPrintNumber";
}
