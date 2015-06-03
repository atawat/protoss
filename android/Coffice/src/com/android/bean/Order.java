package com.android.bean;

import java.util.List;

import android.R.interpolator;

/**
 * ¶©µ¥Ä£ÐÍ£»
 * 
 * @author ¼Ö¶¹
 * 
 */
public class Order {
	public int Id;
	public String DeliveryAddress;
	
	public String OrderNum;

	public String PhoneNumber;

	public String CounponNum;

	public int Type;

	public int PayType;

	public double LocationX;

	public double LocationY;

	public double Discount;

	public double TitalPrice;
	
	public List<OrderDetail> Details;

	public void setId(int id) {
		this.Id = id;
	}
	
	public void setDeliveryAddress(String DeliveryAddress) {
		this.DeliveryAddress = DeliveryAddress;
	}

	public void setPhoneNumber(String PhoneNumber) {
		this.PhoneNumber = PhoneNumber;
	}

	public void setCounponNum(String CounponNum) {
		this.CounponNum = CounponNum;
	}

	public void setType(int Type) {
		this.Type = Type;
	}

	public void setPayType(int PayType) {
		this.PayType = PayType;
	}

	public void setLocationX(double LocationX) {
		this.LocationX = LocationX;
	};

	public void setLocationY(double LocationY) {
		this.LocationY = LocationY;
	};

	public void setDiscount(double Discount) {
		this.Discount = Discount;
	};

	public void setDetails(List<OrderDetail> DetailList) {
		this.Details=DetailList;
	};
	
	public void setTitalPrice(double TitalPrice) {
		this.TitalPrice=TitalPrice;
	};
	
	public void setOrderNum(String OrderNum){
		this.OrderNum=OrderNum;
	}
}
