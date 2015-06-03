package com.android.bean;


public class OrderDetail {
        public int ProductId ;

        public int Count ;

        public String Remark ;
        
        public String Name ;
        
        public double Price ;

        public void setProductId (int ProductId){
        	this.ProductId=ProductId;
        };

        public void setCount  (int Count){
        	this.Count=Count;
        };

        public void setRemark (String Remark ){
        	this.Remark=Remark;
        } ;
        public void setName (String Name ){
        	this.Name=Name;
        } ;
        public void setPrice (double Price ){
        	this.Price=Price;
        } ;
}
