--1. Viết hàm fKTMDH, có tham số vào là mã khách hàng. Trả về thông tin khách hàng có mua hàng hay chưa. (0 và 1)
if OBJECT_ID('fkTMDH') is not null
	DROP function fkTMDH
GO

Create function fkTMDH (@CustumerID nvarchar(5))
		returns bit
as
begin
		declare @kq bit;
		if exists(select * from Orders where CustomerID = @CustumerID)
			set @kq = 1
		else
			set @kq = 0
		return @kq
end
print dbo.fkTMDH('4723FDFLKJGTK')--0
print dbo.fkTMDH('LILAS')--1
--2. Viết Stored Procedure tên KTHoTenSV. Nhận tham số vào: Mã khách hàng. Xuất ra tổng số lượng đơn hàng mà khách hàng đã đặt, với mỗi đơn hàng cho biết trị giá đơn hàng. (Có kiểm tra khách hàng đã mua hàng hay chưa ở câu 1)
--Giao diện như sau: (Số liệu chỉ là ví dụ)
-------
--Mã khách Hàng: SAVEA, số lượng đơn hàng: 2
--Chi tiết đơn hàng
--Mã đơn hàng	Tổng thành tiền
--10248		30000
--10249		15000
---------
if OBJECT_ID('KTHoTenSV') is not null
    DROP proc KTHoTenSV
GO
Create proc KTHoTenSV @CustumerID nvarchar(5)
as
begin
	if (dbo.fkTMDH(@CustumerID) = 1)
		Select d.OrderID , SUM(d.UnitPrice) as UnitPrice
		FROM [Order Details] d, Orders o
		where o.CustomerID = @CustumerID and o.OrderID = d.OrderID
		group by d.OrderID 
	else
		print 'Chua mua hang'

end
exec KTHoTenSV 'LILAS' -- Da mua hang
exec KTHoTenSV '3213sADE'--- Chua mua hang

--3. Viết trigger quản lý khi thêm 1 chi tiết đơn hàng thì kiểm tra 1 đơn hàng không có quá 10 sản phẩm khác nhau.
--Lưu file với tên: BTK1_N5_HoTen_MSSV.sql
if OBJECT_ID('trQuanLyDonHang') is not null
    DROP trigger trQuanLyDonHang
GO
create trigger trQuanLyDonHang
on [Order Details]
for insert
as
begin
	if exists (select OrderID from inserted)
	declare @sl int
	select @sl = count(ProductID) from [Order Details] where OrderID in (select OrderID from inserted) 
	group by OrderID
	if @sl > 10 
	begin
		raiserror ('Don hang khong qua 10  san pham khach nhau',11,1)
		rollback tran
	end
end

select  OrderID , count(ProductID) from [Order Details] group by OrderID order by 2 desc
select * from [Order Details] where OrderID='10260'

insert into [Order Details] (OrderID,ProductID,UnitPrice,Quantity,Discount)
values(10260,54,10,5,0.1)




