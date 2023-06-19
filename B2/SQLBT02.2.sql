--1)	Viết hàm kiểm tra mã khách hàng có tồn tại hay không, trả về giá trị True nếu có, ngược lại trả về False.


Create function spCau1 (@MaKH nchar(5))
      returns bit
as
begin
    declare @kq bit
	if exists (select CompanyName
				from Customers
				where CustomerID =  @MaKH)
		set  @kq = 1
	else
		set @kq = 0
	return @kq
end
--2
select *
from Customers
where CustomerID in(select top 10 CustomerID
					from [Order Details] d,Orders o
					where d.OrderID = o.OrderID
					group by CustomerID
					order by SUM(d.Quantity) desc)


--4)	Viết thủ tục thêm đơn đặt hàng vào bảng đơn hàng, kiểm tra khách hàng trước khi thêm (sử dụng câu 1).

Create proc spCau4 (@MaKH nchar(5))
as
begin
	if [dbo].[spCau1](@MaKH) = 1
		insert into Orders (CustomerID,OrderDate)
		values(@MaKH,GETDATE())
	else
		print N'Loi'
end

select *
from Customers
exec spCau4 XNTON

	
select *
from Employees
insert into Employees(LastName,FirstName)
values('Le','Lai')


alter trigger trTest
on employees
instead of insert
as
begin
	Select COUNT(*)
	from Employees
end
--6)	Ngày đặt hàng không lớn hơn ngày hiện tại.
alter table Orders
add constraint chkNgayDatHang
check( Orderdate < getdate())
set dateformat dmy
insert into Orders(OrderDate)
values ('26/11/2022')

select * from Orders
order by OrderDate desc

--7)	Một khách hàng không đặt quá 32 đơn hàng .
--orders   Insert      Update         delete
--            x        (customerID)      -
Select CustomerID,COUNT(OrderID)
from Orders
group by CustomerID
order by 2 desc

insert into Orders(CustomerID,OrderDate)
values('SAVEA',getdate())
update Orders
set CustomerID =  'ERNSH'--ERNSH
where CustomerID = 'CENTC'--CENTC


if OBJECT_ID('trDatHang') is not null
	DROP trigger trDatHang
go
create trigger trDatHang
on Orders
for insert,update
as
begin
	declare @sl int,@slDeleted int
	if UPDATE(CustomerID)
	begin
		select @sl = count(OrderID)
		from Orders
		where CustomerID = (select CustomerID from inserted)
		
		select @slDeleted = count(OrderID)
		from Orders
		where CustomerID = (select CustomerID from deleted)
		--set @sl = @sl + @slDeleted
		if (@sl + @slDeleted) > 32
		begin 
			raiserror ('Them that bai',11,1)
			rollback tran
		end
	end

	if exists(select CustomerID from inserted)
	begin
		
		select @sl = count(OrderID)
		from Orders
		where CustomerID = (select CustomerID from inserted)
		if @sl > 32
		begin 
			raiserror ('Them that bai',11,1)
			rollback tran
		end
	end
end