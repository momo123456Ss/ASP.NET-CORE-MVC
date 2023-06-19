--1)	Viết stored- procedure xuất danh sách các danh mục chưa có sản phẩm nào.
Use Northwind
if OBJECT_ID('spCau1') is not null
	DROP PROC spCau1
GO
Create proc spCau1
as
begin
	select *
	from Categories
	where CategoryID not in (select distinct CategoryID)
end

insert into Categories(CategoryName)
values('Nuoc giai khat')

exec spCau1

--2)	Viết stored- procedure xuất danh sách khách hàng có đơn đặt hàng chưa giao với số lượng sản phẩm mua > 1.
--o	Thủ tục có tham số vào

if OBJECT_ID('spCau2') is not null
	DROP PROC spCau2
GO
Create proc spCau2
as
begin
	select c.CompanyName, o.OrderID
	from Orders o ,[Order Details] d , Customers c
	where o.OrderID = d.OrderID and o.CustomerID = c.CustomerID and d.Quantity > 1 and o.ShippedDate is  null 
	group by c.CompanyName, o.OrderID
end
GO
exec spCau2
--3)	Viết stored-procedure truyền vào mã sản phẩm, xuất ra thông tin sản phẩm.
if OBJECT_ID('spCau3') is not null
	DROP PROC spCau3
GO
Create proc spCau3 @MaSP int
as
begin
	select *
	from Products
	where ProductID = @MaSP
end
GO
declare @MaSP int
Set @MaSP = 1
exec spCau3 @MaSP

--4)	Viết stored-procedure truyền vào ngày bắt đầu, ngày kết thúc, 
--xuất danh sách sản phẩm được đặt hàng trong khoảng thời gian trên. 
--(Nếu không nhập ngày bắt đầu thì lấy ngày đầu tiên của tháng hiện hành, 
--nếu ngày kết thúc không nhập thì lấy ngày hiện hành).
--o	Thủ tục có tham số vào và ra
if OBJECT_ID('spCau4') is not null
	DROP PROC spCau4
GO
Create proc spCau4 @NgayBD datetime,@NgayKT datetime

as
begin

	if @NgayBD is null
		set @NgayBD = DATEADD(dd,-(DAY(getdate())-1),getdate())
	select *
	from Products
	where ProductID in (select ProductID 
						from [Order Details]
						where OrderID in (select OrderID
										from Orders
										where OrderDate between @NgayBD and @NgayKT))

end

set dateformat dmy
declare @ngayBDTMP datetime,@ngayKTTMP datetime
set @ngayBDTMP = '25/6/1996'
set @ngayKTTMP = '25/7/1996'
exec spCau4 @ngayBDTMP,@ngayKTTMP
--6)	Viết stored-procedure thêm một sản phẩm mới
--Input: 	thông tin sản phẩm
--Output: 	1: Thêm sản phẩm thành công 
--2: Mã sản phẩm đã tồn tại 
---1: Lỗi hệ thống 
if OBJECT_ID('spCau6') is not null
	DROP PROC spCau6
GO
create proc spCau6 @tenSP nvarchar(40),@DG money,@SL int ,@TT bit,@ketQua int output
as
begin
	 if exists (select ProductName
				from Products
				where ProductName = @tenSP)
		set @ketQua = 2
     else
		insert into Products(ProductName,UnitPrice,QuantityPerUnit,Discontinued)
		values(@tenSP,@DG,@SL,@TT)
	begin
		if @@ROWCOUNT > 0 
			set @ketQua = 1
		else
			set @ketQua = -1
	end
end

declare @KQ int
exec spCau6 'Cola',8000,2,1,@KQ output
print @KQ