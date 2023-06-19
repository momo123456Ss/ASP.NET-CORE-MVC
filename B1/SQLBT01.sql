USE Northwind
Declare @ngaySinh datetime
Declare @tuoi int
Set dateformat dmy
Set @ngaySinh = '07/05/2002'
Print N'Tuoi sinh vien la ' + Convert(varchar(10),YEAR(GETDATE()) - YEAR(@ngaySinh),102)



/*Câu 2*/ 
SELECT EmployeeID,COUNT(OrderID)AS SoLuongDonHang,
			case
				when COUNT(OrderID) < 30 then N'So luong don hang qua it'
				else 'abc'
			end TrangThaiDH
FROM Orders
Where YEAR(OrderDate) = 1997
Group by EmployeeID
 /*3.	Viết khối lệnh với tham số vào là @MaHD, đưa ra danh mục các sản phẩm có trong hóa đơn trên.*/
 Declare @maDH int 
 set @maDH = 10248
 Select o.ProductID,p.ProductName
 FROM [Order Details] o ,Products p 
 WHERE OrderID = @maDH and o.ProductID = p.ProductID

 /*4.	Viết khối lệnh kiểm tra tên nhân viên đã nhập từ bàn phím có tồn tại chưa, nếu chưa thì thực hiện them mới 1 nhân viên vào bảng NV*/
 Declare @tenNV nvarchar(10)
 set @tenNV= 'Hiep Thien'

 IF not exists(Select *
			FROM Employees
			WHERE FirstName = @tenNV)
			Insert into Employees(FirstName,LastName) values(' ',@tenNV)
/*5.	Viết khối lệnh tìm kiếm những khách hàng chỉ mới đặt 1 đơn hàng nhưng số lượng sản phẩm đã đặt lớn hơn 5*/
Select c.CustomerID,  COUNT(o.OrderID) as SLDH,SUM(d.Quantity) as SLSP
FROM Customers c , [Order Details] d, Orders o
WHERE c.CustomerID = o.CustomerID and d.OrderID = o.OrderID
GROUP by c.CustomerID
having  COUNT(o.OrderID) <= 5 and SUM(d.Quantity) >= 5

--7.	Viết khối lệnh cho phép năm nhập vào từ ngươi dùng, hiển thị thông tin Số đơn hang đã lập trong năm của các nhân viên với các cột sau:
--Mã NV	Tên NV	Số đơn hang đã lập	Chênh lệch
--(Chênh lệnh tính bằng trung bình các đơn hang đã lập trong năm – số đơn hàng của NV đã lập)
declare @ngayDH datetime,@slnv int,@tongDH int, @TBDH float
set dateformat dmy
set @ngayDH = '5/7/1996'

select @slnv = COUNT(*)
from Employees
select @tongDH = COUNT(*)
from Orders
set @TBDH = ROUND(@tongDH/@slnv,0)

select e.EmployeeID,e.FirstName+' '+ e.LastName as HoTen,COUNT(OrderId) as SLDHDL,'Chech lech' = @TBDH -COUNT(OrderID)
FROM Employees e, Orders o
where e.EmployeeID = o.EmployeeID and YEAR(o.OrderDate) = YEAR(@ngayDH)
Group by e.EmployeeID,e.FirstName+' '+ e.LastName


/*6.	Viết khối lệnh cho phép năm nhập vào từ ngươi dùng, hiển thị thông tin những nhân viên có doanh số cao hơn doanh số trung bình trong năm đó.*/

declare @ThongKeDS table (MaNV int,HoVaTen varchar(50),DoanhSo decimal(18,2))
declare @ngayDH6 datetime,@slnv6 int,@tongDS6 money, @TBDH6 decimal(18,2)


set dateformat dmy
set @ngayDH6 = '5/7/1996'

select @slnv6 = count(*)
from Employees 


select @tongDS6 = SUM(d.Quantity * d.UnitPrice *(1-d.Discount))
from [Order Details] d, Orders o
where d.OrderID = o.OrderID and YEAR(o.OrderDate) = YEAR(@ngayDH6) 


set @TBDH6 = ROUND(@tongDS6/@slnv6,2)

Insert into @ThongKeDS
select e.EmployeeID , e.FirstName+ ' ' + e.LastName as HoVaTen , SUM(d.Quantity*d.UnitPrice*(1-d.Discount)) as DoanhSo
from Employees e, [Order Details] d , Orders o 
where e.EmployeeID = o.EmployeeID  and YEAR(o.OrderDate)= YEAR(@ngayDH6)

group by e.EmployeeID, e.FirstName+ ' ' + e.LastName
select * 
from @ThongKeDS
where DoanhSo>@TBDH6

--declare @ThongKeDS table (MaNV int,HoTenNV varchar(50), DS decimal(18,2))
--declare @NgayDH datetime, @slnv int, @tongDS money, @TBDH decimal(18,2)
--set dateformat dmy
--Set @NgayDH = '5/7/1996'
----số lượng nv
--select @slnv=COUNT(*)
--from Employees
----Tổng doanh số tất cả nv
--select @tongDS=sum(Quantity*UnitPrice*(1-Discount))
--from Orders o, [Order Details] d
--where o.OrderID = d.OrderID and YEAR(OrderDate)=YEAR(@NgayDH)
----doanh số trung bình
--set @TBDH=ROUND(@tongDS/@slnv,2)
----Doanh số từng nv
--insert into @ThongkeDS
--select e.EmployeeID, e.LastName + ' ' + e.FirstName as HoTen, sum(Quantity*UnitPrice*(1-Discount))
--from Employees e, Orders o, [Order Details] d
--where e.EmployeeID = o.EmployeeID and YEAR(OrderDate)=YEAR(@NgayDH)
--group by e.EmployeeID, e.LastName + ' ' + e.FirstName
----xuất kết quả
--select * from @ThongKeDS
--where DS>@TBDH